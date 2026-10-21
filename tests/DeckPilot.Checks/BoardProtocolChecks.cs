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
