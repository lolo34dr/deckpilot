using DeckPilot.Core;

public static class ProfileGestureAudioChecks
{
    public static int Run()
    {
        int passed = 0;
        void Check(string name, Action test) { test(); passed++; Console.WriteLine("PASS " + name); }
        void Expect(bool value) { if (!value) throw new Exception("Profile/gesture/audio assertion failed"); }
        void Reject(Action action) { bool rejected = false; try { action(); } catch (InvalidDataException) { rejected = true; } Expect(rejected); }
        ButtonAction Dual(int threshold = 600) => new() { Kind = ActionKind.Media, Key = 0xAF, Label = "Court", LongPressEnabled = true, LongPressMilliseconds = threshold, LongPressAction = new() { Kind = ActionKind.Microphone, Label = "Long" } };
        Check("Action value equality and deep copies include both press configurations", () =>
        {
            var action = Dual(); var copy = action.Copy();
            Expect(action == copy && !ReferenceEquals(action, copy) && !ReferenceEquals(action.LongPressAction, copy.LongPressAction));
            copy.LongPressAction!.Target = "Another microphone"; Expect(action != copy && action.LongPressAction!.Target == "");
            copy = action.Copy(); copy.LongPressMilliseconds = 850; Expect(action != copy);
            copy = action.Copy(); copy.Ctrl = true; Expect(action != copy);
            var settings = new Settings(); settings.ActiveProfile.Buttons[0] = action;
            Expect(ProfileExchange.Import(ProfileExchange.Export(settings.Profiles)).Profiles[0].Buttons[0] == action);
        });
        Check("Long-press mode sends short only on release, long only once at the threshold", () =>
        {
            var gestures = new PressGestures();
            Expect(gestures.Begin(124, 0, Dual(), 0) == null && gestures.Tick(599).Count == 0);
            Expect(gestures.Release(124, 599)?.Action.Label == "Court");
            gestures.Begin(124, 0, Dual(), 1000);
            Expect(gestures.Tick(1600).Single().Action.Label == "Long");
            Expect(gestures.Tick(3000).Count == 0 && gestures.Release(124, 3001) == null);
        });
        Check("Boundary releases and simultaneous holds use captured independent configurations", () =>
        {
            var gestures = new PressGestures(); var action = Dual();
            gestures.Begin(124, 0, action, 0); gestures.Begin(125, 1, Dual(1000), 0);
            action.LongPressAction!.Label = "Changed";
            Expect(gestures.Release(124, 600)?.Action.Label == "Long");
            Expect(gestures.Release(125, 999)?.Index == 1);
            gestures.Begin(124, 0, Dual(), 2000); gestures.CancelIndex(0);
            Expect(gestures.Tick(9999).Count == 0 && gestures.Release(124, 9999) == null);
        });
        Check("Keyboard repeats and releases cannot trigger both gesture actions", () =>
        {
            var keyboard = new DeckKeyState(); var gestures = new PressGestures(); var sent = new List<GestureInvocation>();
            long at = 0;
            bool Key(bool down) => keyboard.Process(124, down, k => { var call = gestures.Begin(k, 0, Dual(), at); if (call != null) sent.Add(call); return true; }, k => { var call = gestures.Release(k, at); if (call != null) sent.Add(call); });
            Expect(Key(true)); for (int i = 0; i < 30; i++) Expect(Key(true));
            at = 600; sent.AddRange(gestures.Tick(at)); Expect(Key(false));
            Expect(!Key(false) && sent.Count == 1 && sent[0].Action.Label == "Long");
            Expect(gestures.Begin(125, 1, new(), 1000) == null);
            Expect(gestures.Begin(125, 1, new() { Kind = ActionKind.Media }, 1000) != null);
        });
        Check("Invalid thresholds, free-key dual actions and nested gestures are refused", () =>
        {
            var settings = new Settings(); settings.ActiveProfile.Buttons[0] = Dual(1); Reject(settings.Validate);
            settings.ActiveProfile.Buttons[0] = Dual(); settings.ActiveProfile.Buttons[0].Kind = ActionKind.Passthrough; Reject(settings.Validate);
            settings.ActiveProfile.Buttons[0] = Dual(); settings.ActiveProfile.Buttons[0].LongPressAction!.LongPressEnabled = true; Reject(settings.Validate);
        });
        Check("Undo and redo restore deep profile snapshots without changing calibration or preferences", () =>
        {
            var settings = new Settings { KeyMap = Enumerable.Range(124, 12).ToList() }; var history = new ProfileHistory(settings);
            settings.ActiveProfile.Buttons[0] = Dual(); history.Record(settings);
            settings.ActiveProfile.Buttons[0].LongPressAction!.Label = "Edited"; history.Record(settings);
            settings.AutoCheckUpdates = false;
            Expect(history.Undo(settings) && settings.ActiveProfile.Buttons[0].LongPressAction!.Label == "Long");
            Expect(history.Undo(settings) && settings.ActiveProfile.Buttons[0].Kind == ActionKind.Passthrough);
            Expect(history.Redo(settings) && settings.ActiveProfile.Buttons[0].LongPressEnabled);
            Expect(settings.IsCalibrated && !settings.AutoCheckUpdates);
            settings.ActiveProfile.Name = "New change"; history.Record(settings); Expect(!history.CanRedo);
        });
        Check("History is bounded and profile selection alone does not create edits", () =>
        {
            var settings = new Settings(); settings.Profiles.Add(new() { Name = "Second" }); var history = new ProfileHistory(settings);
            settings.ActiveProfileId = settings.Profiles[1].Id; history.Record(settings); Expect(!history.CanUndo);
            for (int i = 1; i <= 65; i++) { settings.ActiveProfile.Buttons[0].Label = i.ToString(); history.Record(settings); }
            int count = 0; while (history.Undo(settings)) count++;
            Expect(count == 50 && settings.ActiveProfile.Buttons[0].Label == "15");
        });
        Check("Unchanged history records preserve redo and remember profile selection without adding edits", () =>
        {
            var settings = new Settings(); var first = settings.ActiveProfile; var second = new Profile { Name = "Second" };
            settings.Profiles.Add(second); var history = new ProfileHistory(settings);
            first.Buttons[0] = Dual(); history.Record(settings);
            first.Buttons[0].LongPressAction!.Label = "Edited"; history.Record(settings);
            Expect(history.Undo(settings));
            history.Record(settings); settings.ActiveProfileId = second.Id; settings.AutoCheckUpdates = false; history.Record(settings);
            Expect(history.CanRedo && history.Redo(settings) && settings.ActiveProfile.Id == first.Id && settings.ActiveProfile.Buttons[0].LongPressAction!.Label == "Edited");
            Expect(history.Undo(settings) && settings.ActiveProfile.Id == second.Id && settings.Profiles[0].Buttons[0].LongPressAction!.Label == "Long" && !settings.AutoCheckUpdates);
        });
        Check("Exported profiles include both gestures but exclude machine preferences and calibration", () =>
        {
            var settings = new Settings { KeyMap = Enumerable.Range(124, 12).ToList(), PreferredBoardDeviceId = "PRIVATE-BOARD" };
            settings.ActiveProfile.Buttons[0] = Dual();
            string json = ProfileExchange.Export(settings.Profiles);
            Expect(!json.Contains("KeyMap") && !json.Contains("PRIVATE-BOARD") && !json.Contains("AutoCheckUpdates"));
            var package = ProfileExchange.Import(json); var imported = ProfileExchange.AddAsNew(settings, package);
            Expect(imported[0].Id != package.Profiles[0].Id && imported[0].Name != package.Profiles[0].Name && imported[0].Buttons[0].LongPressAction!.Label == "Long");
            Expect(settings.IsCalibrated && settings.Profiles.Count == 2);
            Reject(() => ProfileExchange.Import(json.Replace("DeckPilot.Profiles", "Unknown")));
            Reject(() => ProfileExchange.Import(json.Replace("\"Version\": 1", "\"Version\": 999")));
            Reject(() => ProfileExchange.Import("{}"));
            Reject(() => ProfileExchange.Import(new string('x', ProfileExchange.MaximumBytes + 1)));
        });
        string temp = Path.Combine(Path.GetTempPath(), "deckpilot-profile-tests-" + Guid.NewGuid()); Directory.CreateDirectory(temp);
        try
        {
            Check("Identical saves keep the settings file intact and recreate it after external deletion", () =>
            {
                string folder = Path.Combine(temp, "no-op"); string path = Path.Combine(folder, "settings.json");
                var store = new SettingsStore(path); var settings = new Settings(); store.Save(settings);
                string original = File.ReadAllText(path);
                File.SetLastWriteTimeUtc(path, new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                DateTime stamp = File.GetLastWriteTimeUtc(path);
                // A read-only sharing lease would reject an unnecessary replacement on Windows.
                using (var reader = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    store.Save(settings); store.Save(store.Load());
                    Expect(File.GetLastWriteTimeUtc(path) == stamp && File.ReadAllText(path) == original);
                    if (OperatingSystem.IsWindows())
                    {
                        settings.AutoCheckUpdates = false;
                        bool failed = false;
                        try { store.Save(settings); } catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { failed = true; }
                        Expect(failed && File.ReadAllText(path) == original);
                    }
                }
                if (OperatingSystem.IsWindows())
                {
                    store.Save(settings); Expect(!store.Load().AutoCheckUpdates);
                    original = File.ReadAllText(path);
                }
                Expect(!Directory.Exists(store.BackupsPath) && !Directory.GetFiles(folder, "*.tmp").Any());
                File.Delete(path); store.Save(settings);
                Expect(store.Load().Profiles[0].Id == settings.Profiles[0].Id && File.ReadAllText(path) == original);
            });
            Check("External profile edits are backed up even when file length and timestamp are unchanged", () =>
            {
                string folder = Path.Combine(temp, "external-edit"); string path = Path.Combine(folder, "settings.json");
                var store = new SettingsStore(path); var settings = new Settings(); settings.ActiveProfile.Name = "Original"; store.Save(settings);
                string original = File.ReadAllText(path); DateTime stamp = File.GetLastWriteTimeUtc(path);
                long length = new FileInfo(path).Length;
                File.WriteAllText(path, original.Replace("Original", "External")); File.SetLastWriteTimeUtc(path, stamp);
                Expect(new FileInfo(path).Length == length && File.GetLastWriteTimeUtc(path) == stamp);
                store.Save(settings);
                Expect(store.Load().ActiveProfile.Name == "Original");
                var backups = Directory.GetFiles(store.BackupsPath, "*.deckpilot.json");
                Expect(backups.Length == 1 && ProfileExchange.Read(backups[0]).Profiles[0].Name == "External");
            });
            Check("Automatic backups preserve previous profiles, skip preference-only saves and keep 20 readable versions", () =>
            {
                var store = new SettingsStore(Path.Combine(temp, "settings.json")); var settings = new Settings(); store.Save(settings);
                settings.ActiveProfile.Buttons[0] = Dual(); store.Save(settings);
                var files = Directory.GetFiles(store.BackupsPath, "*.deckpilot.json");
                Expect(files.Length == 1 && ProfileExchange.Read(files[0]).Profiles[0].Buttons[0].Kind == ActionKind.Passthrough);
                settings.AutoCheckUpdates = false; store.Save(settings); Expect(Directory.GetFiles(store.BackupsPath, "*.deckpilot.json").Length == 1);
                for (int i = 0; i < 25; i++) { settings.ActiveProfile.Buttons[0].Label = "Version " + i; store.Save(settings); }
                files = Directory.GetFiles(store.BackupsPath, "*.deckpilot.json"); Expect(files.Length == 20);
                foreach (var file in files) Expect(ProfileExchange.Read(file).Profiles.Count == 1);
                Expect(!Directory.GetFiles(temp, "*.tmp", SearchOption.AllDirectories).Any());
            });
            Check("Atomic settings saves preserve an old snapshot held by a shared reader", () =>
            {
                string folder = Path.Combine(temp, "shared-reader"); Directory.CreateDirectory(folder);
                string path = Path.Combine(folder, "settings.json");
                var store = new SettingsStore(path); var settings = new Settings(); store.Save(settings);
                string original = File.ReadAllText(path);
                using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                for (int i = 0; i < 3; i++) { settings.ActiveProfile.Name = "Replacement " + i; store.Save(settings); Expect(store.Load().ActiveProfile.Name == settings.ActiveProfile.Name); }
                using var reader = new StreamReader(stream);
                Expect(reader.ReadToEnd() == original && !Directory.GetFiles(folder, "*.tmp").Any());
            });
            Check("Backup failure cannot overwrite the current settings file", () =>
            {
                string folder = Path.Combine(temp, "blocked"); Directory.CreateDirectory(folder);
                var store = new SettingsStore(Path.Combine(folder, "settings.json")); var settings = new Settings(); store.Save(settings);
                File.WriteAllText(store.BackupsPath, "not a directory"); settings.ActiveProfile.Name = "Changed";
                bool failed = false; try { store.Save(settings); } catch (IOException) { failed = true; }
                Expect(failed && store.Load().ActiveProfile.Name != "Changed");
                File.Delete(store.BackupsPath); store.Save(settings);
                Expect(store.Load().ActiveProfile.Name == "Changed" && Directory.GetFiles(store.BackupsPath, "*.deckpilot.json").Length == 1);
            });
        }
        finally { Directory.Delete(temp, true); }
        Check("Microphone toggle mutes all active capture endpoints, then unmutes them", () =>
        {
            var backend = new AudioFake(); backend.Mutes["m2"] = true;
            Expect(AudioCommands.ToggleMicrophones(backend) && backend.Mutes.Values.All(v => v));
            Expect(!AudioCommands.ToggleMicrophones(backend) && backend.Mutes.Values.All(v => !v));
            backend.FailMic = true; bool failed = false;
            try { AudioCommands.ToggleMicrophones(backend); } catch (IOException) { failed = true; }
            Expect(failed && backend.Mutes.Values.All(v => !v));
        });
        Check("Audio output toggle switches all Windows roles and refuses disconnected or identical targets", () =>
        {
            var backend = new AudioFake();
            Expect(AudioCommands.ToggleOutput(backend, "a", "b") == "b" && backend.Defaults.All(v => v == "b"));
            Expect(AudioCommands.ToggleOutput(backend, "a", "b") == "a" && backend.Defaults.All(v => v == "a"));
            bool invalid = false; try { AudioCommands.ToggleOutput(backend, "a", "missing"); } catch (InvalidOperationException) { invalid = true; }
            Expect(invalid && backend.Defaults.All(v => v == "a"));
            invalid = false; try { AudioCommands.ToggleOutput(backend, "a", "a"); } catch (InvalidOperationException) { invalid = true; }
            Expect(invalid);
        });
        Check("An output-routing failure restores the previous default for each role", () =>
        {
            var backend = new AudioFake { FailRole = true }; backend.Defaults[2] = "b";
            bool failed = false; try { AudioCommands.ToggleOutput(backend, "a", "b"); } catch (IOException) { failed = true; }
            Expect(failed && backend.Defaults.SequenceEqual(new[] { "a", "a", "b" }));
        });
        return passed;
    }
    private sealed class AudioFake : IAudioBackend
    {
        public string[] Defaults = ["a", "a", "a"];
        public Dictionary<string, bool> Mutes = new() { ["m1"] = false, ["m2"] = false };
        public bool FailMic, FailRole;
        public IReadOnlyList<AudioDevice> Outputs() => [new("a", "Casque"), new("b", "Enceintes")];
        public IReadOnlyList<AudioDevice> Microphones() => Mutes.Keys.Select(k => new AudioDevice(k, k)).ToArray();
        public string DefaultOutput(int role) => Defaults[role];
        public void SetDefaultOutput(string id, int role) { if (FailRole && role == 1) { FailRole = false; throw new IOException("Driver error"); } Defaults[role] = id; }
        public bool IsMuted(string id) => Mutes[id];
        public void SetMuted(string id, bool muted) { if (FailMic && id == "m2") { FailMic = false; throw new IOException("Driver error"); } Mutes[id] = muted; }
    }
}
