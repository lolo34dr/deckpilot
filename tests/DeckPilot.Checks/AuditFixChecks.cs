using DeckPilot.Core;

internal static class AuditFixChecks
{
    public static int Run()
    {
        int count = 0;
        void Check(string name, Action test) { test(); count++; Console.WriteLine("PASS " + name); }
        void Expect(bool value) { if (!value) throw new Exception("Audit regression failed"); }
        void Reject(Action action) { try { action(); } catch (InvalidDataException) { return; } throw new Exception("Invalid sound package accepted"); }
        string root = Path.Combine(Path.GetTempPath(), "DeckPilotAudit-" + Guid.NewGuid().ToString("N")); Directory.CreateDirectory(root);
        try
        {
            string path = Path.Combine(root, "source.wav"); byte[] bytes = [82, 73, 70, 70, 1, 2, 3, 4]; File.WriteAllBytes(path, bytes);
            var source = new Settings(); var clip = new SoundboardClip { Name = "Test", FilePath = path }; source.Soundboard.Clips.Add(clip);
            source.Profiles[0].Buttons[0] = new() { Kind = ActionKind.Soundboard, Target = clip.Id.ToString(), LongPressEnabled = true,
                LongPressAction = new() { Kind = ActionKind.Soundboard, Target = clip.Id.ToString() } };
            Check("Automatic virtual microphone startup defaults on for existing settings and preserves an explicit stop", () =>
            {
                var prefsPath=Path.Combine(root,"autostart","prefs.json"); Directory.CreateDirectory(Path.GetDirectoryName(prefsPath)!);
                File.WriteAllText(prefsPath,"{\"Version\":1,\"LayoutColumns\":4}");
                var prefsStore=new SettingsStore(prefsPath); var prefs=prefsStore.Load(); Expect(prefs.Soundboard.AutoStart);
                prefs.Soundboard.AutoStart=false; prefs.Soundboard.MicrophoneId="saved-physical-microphone"; prefsStore.Save(prefs);
                var restored=prefsStore.Load(); Expect(!restored.Soundboard.AutoStart && restored.Soundboard.MicrophoneId=="saved-physical-microphone");
                restored.Soundboard.AutoStart=true; prefsStore.Save(restored); Expect(prefsStore.Load().Soundboard.AutoStart);
            });
            Check("Portable sound profiles preserve audio and remap short and long actions without leaking source paths", () =>
            {
                source.Profiles[0].Buttons[0].Target = clip.Id.ToString().ToUpperInvariant();
                string json = PortableProfiles.Export(source, source.Profiles); Expect(!json.Contains(path));
                var package = PortableProfiles.Import(json); var destination = new Settings();
                var imported = PortableProfiles.Apply(destination, package, false, Path.Combine(root, "managed"));
                var sound = destination.Soundboard.Clips.Single();
                Expect(sound.Id != clip.Id && File.ReadAllBytes(sound.FilePath).SequenceEqual(bytes));
                Expect(imported[0].Buttons[0].Target == sound.Id.ToString() && imported[0].Buttons[0].LongPressAction!.Target == sound.Id.ToString());
                Expect(destination.Profiles.Count == 2 && destination.Soundboard.MicrophoneId == "");
            });
            Check("Legacy profile imports remain supported and repeated restore reuses matching audio", () =>
            {
                var plain = PortableProfiles.Import(ProfileExchange.Export(new Settings().Profiles)); Expect(plain.Sounds.Count == 0);
                var package = PortableProfiles.Import(PortableProfiles.Export(source, source.Profiles));
                PortableProfiles.Apply(source, package, true, Path.Combine(root, "restore"));
                Expect(source.Soundboard.Clips.Count == 1 && source.Profiles[0].Buttons[0].Target == clip.Id.ToString());
            });
            Check("Sound packages reject traversal extensions and duplicate identifiers before touching settings", () =>
            {
                var package = PortableProfiles.Import(PortableProfiles.Export(source, source.Profiles)); package.Sounds[0].Extension = "../../bad.exe";
                var destination = new Settings(); Reject(() => PortableProfiles.Apply(destination, package, false, root)); Expect(destination.Profiles.Count == 1);
                package.Sounds[0].Extension = ".wav"; package.Sounds.Add(package.Sounds[0]); Reject(() => PortableProfiles.Apply(destination, package, false, root));
            });
            Check("Automatic profile backups retain the prior sound bytes", () =>
            {
                var store = new SettingsStore(Path.Combine(root, "prefs.json")); store.Save(source); source.Profiles[0].Buttons[0].Label = "New label"; store.Save(source);
                var backup = PortableProfiles.Read(Directory.GetFiles(store.BackupsPath).Single());
                Expect(backup.Sounds.Single().Audio!.SequenceEqual(bytes)); Expect(ProfileExchange.Import(backup.Profiles).Profiles[0].Buttons[0].Label == "");
            });
            Check("Missing audio is explicit on export and retained as unavailable in a backup", () =>
            {
                File.Delete(path); bool missing = false; try { PortableProfiles.Export(source, source.Profiles); } catch (FileNotFoundException) { missing = true; }
                Expect(missing); var package = PortableProfiles.Import(PortableProfiles.Export(source, source.Profiles, false)); Expect(package.Sounds.Single().Audio == null);
            });
            Check("Cable adapter identity survives endpoint renaming without accepting other audio drivers", () =>
            {
                Expect(VirtualCableEndpoints.IsCableDevice("VB-Audio Virtual Cable")); Expect(!VirtualCableEndpoints.IsCableDevice("Realtek Audio"));
                Expect(!VirtualCableEndpoints.IsCableDevice("VB-Audio VoiceMeeter VAIO"));
            });
            Check("Optional firmware identification allows a silent sketch but preserves serial errors", () =>
            {
                Expect(new BoardProtocolClient(new SilentTransport()).IdentifyOptional(CancellationToken.None) == null);
                bool serialError = false; try { new BoardProtocolClient(new BrokenTransport()).IdentifyOptional(CancellationToken.None); } catch(IOException) { serialError = true; }
                Expect(serialError);
                using var cancelled = new CancellationTokenSource(); cancelled.Cancel(); bool cancellation = false;
                try { new BoardProtocolClient(new SilentTransport()).IdentifyOptional(cancelled.Token); } catch(OperationCanceledException) { cancellation = true; } Expect(cancellation);
            });
        }
        finally { Directory.Delete(root, true); }
        return count;
    }
    private sealed class SilentTransport : IBoardTransport { public void Dispose() {} public void WriteLine(string line) {} public string ReadLine() { Thread.Sleep(10); throw new TimeoutException(); } }
    private sealed class BrokenTransport : IBoardTransport { public void Dispose() {} public void WriteLine(string line) => throw new IOException("Port disconnected"); public string ReadLine() => throw new IOException(); }
}
