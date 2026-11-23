using System.Text.Json;
using System.Text.Json.Nodes;
using DeckPilot.Core;

internal static class BoardProtocolChecks
{
    public static int Run()
    {
        int count = 0;
        void Expect(bool value) { if (!value) throw new Exception("Board protocol assertion failed"); }
        void Check(string name, Action action) { action(); count++; Console.WriteLine("PASS " + name); }
        const string token = "1234ABCD";
        const string info = "DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.0|12|3";
        Check("USB handshake validates session, board, key count, version and capabilities", () => {
            Expect(BoardProtocol.Info(info, token)?.Firmware.Version == "1.1.0");
            Expect(BoardProtocol.Info(info, "DEADBEEF") == null);
            Expect(BoardProtocol.Info(info.Replace("promicro-32u4-16mhz", "other-board"), token) == null);
            Expect(BoardProtocol.Info(info.Replace("|12|3", "|11|3"), token) == null);
            Expect(BoardProtocol.Info(info.Replace("|12|3", "|12|0"), token) == null);
            Expect(BoardProtocol.Info(info.Replace("1.1.0", "1.1"), token) == null);
            Expect(BoardProtocol.Info(info + "|extra", token) == null);
            Expect(BoardProtocol.Info(new string('X', 200), token) == null);
            Expect(BoardProtocol.ValidSession(BoardProtocol.NewSession()));
        });
        Check("Heartbeat and key frames reject stale sessions, wrong requests and invalid masks", () => {
            Expect(BoardProtocol.Pong("DP2|PONG|1234ABCD|7|42|1|4095", token, 7, 3)?.Mask == 4095);
            Expect(BoardProtocol.Pong("DP2|PONG|1234ABCD|6|42|1|1", token, 7, 3) == null);
            Expect(BoardProtocol.Pong("DP2|PONG|DEADBEEF|7|42|1|1", token, 7, 3) == null);
            Expect(BoardProtocol.Keys("DP2|KEYS|1234ABCD|1|4096", token) == null);
            Expect(BoardProtocol.Keys("DP2|KEYS|1234ABCD|-1|1", token) == null);
            Expect(BoardProtocol.Keys("DP2|KEYS|1234ABCD|4294967296|1", token) == null);
            Expect(BoardProtocol.Keys("DP2|KEYS|DEADBEEF|1|1", token) == null);
            Expect(BoardProtocol.Newer(0, uint.MaxValue) && !BoardProtocol.Newer(4, 4) && !BoardProtocol.Newer(3, 4));
        });
        Check("A complete USB session ignores duplicate keys and an out-of-order heartbeat snapshot", () => {
            using var cancellation = new CancellationTokenSource();
            using var transport = new ScriptedTransport();
            transport.OnWrite = line => {
                var parts = line.Split('|');
                if (line == "DECKPILOT?") { transport.Replies.Enqueue("old noise"); transport.Replies.Enqueue("DECKPILOT|1|promicro-32u4-16mhz|1.1.0"); }
                else if (parts[1] == "HELLO") transport.Replies.Enqueue(info.Replace(token, parts[2]));
                else if (parts[1] == "PING") {
                    string session = parts[2], request = parts[3];
                    transport.Replies.Enqueue($"DP2|PONG|DEADBEEF|{request}|41|8|0");
                    transport.Replies.Enqueue($"DP2|KEYS|{session}|10|1");
                    transport.Replies.Enqueue($"DP2|KEYS|{session}|10|2");
                    transport.Replies.Enqueue($"DP2|KEYS|{session}|9|4");
                    transport.Replies.Enqueue($"DP2|PONG|{session}|{request}|42|9|4");
                }
            };
            var client = new BoardProtocolClient(transport);
            Expect(client.Connect(cancellation.Token).Protocol == 2);
            var reports = new List<BoardTelemetry>();
            try { client.Run(value => { reports.Add(value); if (reports.Count == 2) cancellation.Cancel(); }, cancellation.Token); }
            catch (OperationCanceledException) { }
            Expect(reports.Count == 2 && reports.All(r => r.Mask == 1 && r.Sequence == 10) && reports[1].Uptime == 42);
        });
        Check("Firmware 1.0.0 remains compatible and cancellation releases a waiting session", () => {
            using var transport = new ScriptedTransport();
            transport.OnWrite = line => transport.Replies.Enqueue("DECKPILOT|1|promicro-32u4-16mhz|1.0.0");
            var client = new BoardProtocolClient(transport);
            Expect(client.Connect(default).Protocol == 1 && transport.Writes.Count == 1);
            using var cancellation = new CancellationTokenSource(); cancellation.Cancel();
            bool stopped = false;
            try { client.Run(_ => throw new Exception("Legacy telemetry was invented"), cancellation.Token); }
            catch (OperationCanceledException) { stopped = true; }
            Expect(stopped);
        });
        Check("Disconnect and cancellation cannot produce a successful USB identity", () => {
            using var transport = new ScriptedTransport();
            var client = new BoardProtocolClient(transport);
            bool failed = false;
            try { client.Connect(default); } catch (IOException) { failed = true; }
            Expect(failed);
            transport.Timeout = true;
            using var cancellation = new CancellationTokenSource(TimeSpan.FromMilliseconds(30));
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool stopped = false;
            try { client.Connect(cancellation.Token); } catch (OperationCanceledException) { stopped = true; }
            Expect(stopped && watch.Elapsed < TimeSpan.FromSeconds(1));
        });
        Check("Two serial decks keep ordered presses separate and ignore heartbeat masks for actions", () => {
            using var a = HybridTransport();
            using var b = HybridTransport();
            var first = new BoardProtocolClient(a);
            var second = new BoardProtocolClient(b);
            Expect(first.Connect(default).SupportsSerialInput && second.Connect(default).SupportsSerialInput);
            Expect(!first.EnableSerialInput(default) && first.EnableSerialInput(default));
            Expect(!second.EnableSerialInput(default) && second.EnableSerialInput(default));
            Expect(a.Writes[1] != b.Writes[1]);

            static List<int> Read(BoardProtocolClient client)
            {
                var frames = new List<int>();
                var displayed = new List<int>();
                try { client.Run(state => displayed.Add(state.Mask), default, frames.Add); }
                catch (IOException ex) when (ex.Message.Contains("Séquence", StringComparison.Ordinal))
                {
                    if (!displayed.SequenceEqual(new[] { 0, 1, 0 })) throw new Exception("The display must follow the ordered input frames.");
                    return frames;
                }
                throw new Exception("A gap in the input sequence was accepted.");
            }
            var left = Task.Run(() => Read(first));
            var right = Task.Run(() => Read(second));
            Task.WaitAll(left, right);
            Expect(left.Result.SequenceEqual(new[] { 1, 0 }) && right.Result.SequenceEqual(left.Result));
        });
        Check("Device bindings retain independent profiles, key maps and modes across a settings reload", () => {
            string directory = Path.Combine(Path.GetTempPath(), "deckpilot-hybrid-" + Guid.NewGuid());
            try {
                var store = new SettingsStore(Path.Combine(directory, "settings.json"));
                var settings = store.Load();
                Expect(settings.BoardBindings.Count == 0);
                var second = new Profile { Name = "Soundboard" };
                settings.Profiles.Add(second);
                settings.BoardBindings["DECK-A"] = new(settings.ActiveProfileId, []);
                settings.BoardBindings["DECK-B"] = new(second.Id, Enumerable.Range(Calibration.FirstKey, 12).Reverse().ToList(), false);
                store.Save(settings);
                var loaded = store.Load();
                var first = loaded.BoardBindings["deck-a"];
                var other = loaded.BoardBindings["deck-b"];
                Expect(first.Enabled && !other.Enabled && first.ProfileId != other.ProfileId);
                Expect(first.ButtonIndex(0) == 0 && other.ButtonIndex(0) == 11);
                Expect(ProfileNavigation.Resolve(loaded, "next", second.Id).Id == first.ProfileId);
            } finally { if (Directory.Exists(directory)) Directory.Delete(directory, true); }
        });
        Check("First board keeps the keyboard profile, second gets its own, and shared profiles split without losing actions", () => {
            var settings = new Settings { KeyMap = Enumerable.Range(Calibration.FirstKey, 12).Reverse().ToList() };
            settings.ActiveProfile.Buttons[0] = new() { Kind = ActionKind.Media, Key = 0xB3, Label = "Lecture" };
            var first = BoardSetup.Prepare(settings, "board-a", "COM3");
            Expect(!first.AddProfile && first.Profile.Id == settings.ActiveProfile.Id
                && first.Binding.KeyMap.SequenceEqual(settings.KeyMap));
            settings.BoardBindings["board-a"] = first.Binding;
            var second = BoardSetup.Prepare(settings, "board-b", "COM4");
            Expect(second.AddProfile && second.Profile.Id != first.Profile.Id && second.Binding.KeyMap.Count == 0
                && second.Profile.Buttons.All(button => button.Kind == ActionKind.Passthrough));
            settings.BoardBindings["board-b"] = first.Binding with { Enabled = false };
            var split = BoardSetup.Prepare(settings, "BOARD-B", "COM4");
            Expect(split.AddProfile && split.Binding.Enabled && split.Profile.Id != first.Profile.Id
                && split.Profile.Buttons[0].Label == "Lecture" && split.Binding.KeyMap.SequenceEqual(first.Binding.KeyMap));
            split.Profile.Buttons[0].Label = "Autre action";
            Expect(first.Profile.Buttons[0].Label == "Lecture");
            settings.Profiles.Add(split.Profile);
            settings.BoardBindings["board-b"] = split.Binding;
            var reopen = BoardSetup.Prepare(settings, "board-b", "COM4");
            Expect(!reopen.AddProfile && reopen.Profile.Id == split.Profile.Id);
        });
        Check("Firmware prompt preferences default safely and survive a settings round-trip", () => {
            string path = Path.Combine(Path.GetTempPath(), "deckpilot-board-" + Guid.NewGuid() + ".json");
            try {
                var node = JsonNode.Parse(JsonSerializer.Serialize(new Settings()))!;
                node.AsObject().Remove("AutoCheckFirmware"); node.AsObject().Remove("PreferredBoardDeviceId"); node.AsObject().Remove("FirmwareSnoozedUntil");
                File.WriteAllText(path, node.ToJsonString());
                var store = new SettingsStore(path); var settings = store.Load();
                Expect(settings.AutoCheckFirmware && settings.PreferredBoardDeviceId == "");
                settings.AutoCheckFirmware = false; settings.PreferredBoardDeviceId = @"USB\VID_2341&PID_8036\REFERENCE";
                settings.FirmwareSnoozedUntil = DateTimeOffset.UtcNow.AddHours(24); store.Save(settings);
                var loaded = store.Load();
                Expect(!loaded.AutoCheckFirmware && loaded.PreferredBoardDeviceId == settings.PreferredBoardDeviceId && loaded.FirmwareSnoozedUntil == settings.FirmwareSnoozedUntil);
            } finally { File.Delete(path); }
        });
        return count;
    }

    private static ScriptedTransport HybridTransport()
    {
        var transport = new ScriptedTransport();
        bool held = true;
        transport.OnWrite = line =>
        {
            var parts = line.Split('|');
            if (line == "DECKPILOT?") transport.Replies.Enqueue("DECKPILOT|1|promicro-32u4-16mhz|1.2.0");
            else if (parts[1] == "HELLO") transport.Replies.Enqueue($"DP2|INFO|{parts[2]}|promicro-32u4-16mhz|1.2.0|12|7");
            else if (parts[1] == "INPUT")
            {
                transport.Replies.Enqueue(held ? $"DP2|BUSY|{parts[2]}" : line);
                held = false;
            }
            else if (parts[1] == "PING")
            {
                transport.Replies.Enqueue($"DP2|PONG|{parts[2]}|{parts[3]}|25|4|8");
                transport.Replies.Enqueue($"DP2|KEYS|{parts[2]}|1|1");
                transport.Replies.Enqueue($"DP2|KEYS|{parts[2]}|2|0");
                transport.Replies.Enqueue($"DP2|KEYS|{parts[2]}|4|8");
            }
        };
        return transport;
    }
    private sealed class ScriptedTransport : IBoardTransport
    {
        public Queue<string> Replies { get; } = new();
        public List<string> Writes { get; } = new();
        public Action<string>? OnWrite;
        public bool Timeout;
        public void WriteLine(string line) { Writes.Add(line); OnWrite?.Invoke(line); }
        public string ReadLine()
        {
            if (Replies.Count > 0) return Replies.Dequeue();
            if (Timeout) { Thread.Sleep(5); throw new TimeoutException(); }
            throw new IOException("Simulated disconnect");
        }
        public void Dispose() { }
    }
}
