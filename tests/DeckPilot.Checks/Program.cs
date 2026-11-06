using DeckPilot.Core;
using System.Text.Json;

int passed = 0;
void Check(string name, Action test)
{
    test(); passed++; Console.WriteLine($"PASS {name}");
}
void Expect(bool condition) { if (!condition) throw new Exception("Assertion failed"); }
void Reject(Action action)
{
    try { action(); } catch (InvalidDataException) { return; }
    throw new Exception("Invalid data was accepted");
}
var map = Enumerable.Range(Calibration.FirstKey, 12).Reverse().ToList();
Check("Random physical arrangement maps every F13–F24 exactly once", () => {
    Expect(Calibration.IsValid(map));
    Expect(Calibration.KeyName(map[0]) == "F24");
    Expect(Calibration.KeyName(map[11]) == "F13");
});
Check("Duplicate, missing and foreign calibration keys are rejected", () => {
    Expect(!Calibration.IsValid(map.Take(11).ToList()));
    Expect(!Calibration.IsValid(Enumerable.Repeat(0x7C, 12).ToList()));
    var foreign = map.ToList(); foreign[0] = 0x41;
    Expect(!Calibration.IsValid(foreign));
});
Check("Version 2 theme and soundboard preferences validate without breaking old settings", () => {
    var prefs = new Settings(); prefs.Validate();
    prefs.Theme.Accent = "#AABBCC";
    prefs.Soundboard.Clips.Add(new() { Name = "Intro", FilePath = @"C:\sounds\intro.mp3" });
    prefs.Validate();
    prefs.Theme.Accent = "green"; Reject(prefs.Validate);
    prefs.Theme.Accent = "#AABBCC";
    prefs.Soundboard.Clips[0].FilePath = "sound.exe"; Reject(prefs.Validate);
});
Check("Soundboard monitor preferences persist separately from the virtual microphone", () => {
    var prefs = new Settings();
    Expect(!prefs.Soundboard.MonitorEnabled && prefs.Soundboard.MonitorOutputId == "" && prefs.Soundboard.MonitorVolumePercent == 50);
    Expect(prefs.Soundboard.NoiseSuppressionEnabled && prefs.Soundboard.NoiseSuppressionStrengthPercent == 75);
    var legacySoundboard = JsonSerializer.Deserialize<SoundboardSettings>("{\"AutoStart\":true,\"MicrophoneId\":\"old-mic\",\"VirtualOutputId\":\"old-cable\"}")!;
    Expect(legacySoundboard.NoiseSuppressionEnabled && legacySoundboard.NoiseSuppressionStrengthPercent == 75
        && legacySoundboard.MicrophoneId == "old-mic");
    prefs.Soundboard.MonitorEnabled = true;
    prefs.Soundboard.MonitorOutputId = "physical-speakers";
    prefs.Soundboard.MonitorVolumePercent = 35;
    prefs.Soundboard.NoiseSuppressionEnabled = false;
    prefs.Soundboard.NoiseSuppressionStrengthPercent = 62;
    string directory = Path.Combine(Path.GetTempPath(), "deckpilot-monitor-" + Guid.NewGuid().ToString("N"));
    try
    {
        var store = new SettingsStore(Path.Combine(directory, "settings.json"));
        store.Save(prefs);
        var restored = store.Load();
        Expect(restored.Soundboard.MonitorEnabled && restored.Soundboard.MonitorOutputId == "physical-speakers"
            && restored.Soundboard.MonitorVolumePercent == 35 && restored.Soundboard.AutoStart
            && !restored.Soundboard.NoiseSuppressionEnabled && restored.Soundboard.NoiseSuppressionStrengthPercent == 62);
        restored.Soundboard.MonitorVolumePercent = 101; Reject(restored.Validate);
        restored.Soundboard.MonitorVolumePercent = -1; Reject(restored.Validate);
        restored.Soundboard.MonitorVolumePercent = 0;
        restored.Soundboard.NoiseSuppressionStrengthPercent = 101; Reject(restored.Validate);
        restored.Soundboard.NoiseSuppressionStrengthPercent = -1; Reject(restored.Validate);
        restored.Soundboard.NoiseSuppressionStrengthPercent = 75;
        restored.Soundboard.MonitorOutputId = null!; Reject(restored.Validate);
    }
    finally { if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true); }
});
Check("VB-CABLE routes only to its playback endpoint and hides its virtual microphone from physical input choices", () => {
    Expect(VirtualCableEndpoints.IsPlaybackEndpoint("CABLE Input (VB-Audio Virtual Cable)"));
    Expect(VirtualCableEndpoints.IsRecordingEndpoint("CABLE Output (VB-Audio Virtual Cable)"));
    Expect(!VirtualCableEndpoints.IsPlaybackEndpoint("Speakers (Realtek Audio)"));
    Expect(!VirtualCableEndpoints.IsPlaybackEndpoint("VoiceMeeter Aux Input"));
    Expect(!VirtualCableEndpoints.IsRecordingEndpoint("CABLE Input (VB-Audio Virtual Cable)"));
});
Check("A new profile leaves all twelve keys free", () => {
    var settings = new Settings(); settings.Validate();
    Expect(!settings.IsCalibrated);
    Expect(settings.ActiveProfile.Buttons.Count == 12);
    Expect(settings.ActiveProfile.Buttons.All(b => b.Kind == ActionKind.Passthrough));
});
Check("An assigned key is swallowed until release and runs once", () => {
    var state = new DeckKeyState(); int actions = 0;
    Expect(state.Process(0x7C, true, _ => { actions++; return true; }));
    for (int i = 0; i < 20; i++) Expect(state.Process(0x7C, true, _ => throw new Exception("Repeated action")));
    Expect(state.Process(0x7C, false, _ => throw new Exception("Release action")));
    Expect(actions == 1);
    Expect(state.Process(0x7C, true, _ => { actions++; return true; }));
    Expect(actions == 2);
});
Check("Free keys retain normal down, repeat and release events", () => {
    var state = new DeckKeyState();
    Expect(!state.Process(0x80, true, _ => false));
    Expect(!state.Process(0x80, true, _ => throw new Exception("Repeated callback")));
    Expect(!state.Process(0x80, false, _ => throw new Exception("Release callback")));
});
Check("Profile switching while held preserves the release decision", () => {
    var state = new DeckKeyState();
    Expect(state.Process(0x7C, true, _ => true));
    Expect(state.Process(0x7C, true, _ => false));
    Expect(state.Process(0x7C, false, _ => false));
    Expect(!state.Process(0x7C, true, _ => false));
    Expect(!state.Process(0x7C, false, _ => true));
});
Check("Simultaneous buttons maintain independent state", () => {
    var state = new DeckKeyState();
    Expect(state.Process(0x7C, true, _ => true));
    Expect(!state.Process(0x7D, true, _ => false));
    Expect(state.Process(0x7C, false, _ => false));
    Expect(!state.Process(0x7D, false, _ => true));
});
Check("Other keyboard keys never trigger actions", () => {
    var state = new DeckKeyState();
    Expect(!state.Process(0x41, true, _ => throw new Exception("Foreign key callback")));
});
Check("Duplicating actions does not modify the original profile", () => {
    var original = new ButtonAction { Label = "Jeu", Kind = ActionKind.Shortcut, Ctrl = true, Key = 0x4D };
    var copy = original.Copy(); copy.Label = "Musique"; copy.Ctrl = false;
    Expect(original.Label == "Jeu" && original.Ctrl && copy.Key == original.Key);
});
Check("Calibration remains common when the active profile changes", () => {
    var settings = new Settings { KeyMap = map };
    var second = new Profile { Name = "Jeu" };
    settings.Profiles.Add(second); settings.ActiveProfileId = second.Id;
    settings.Validate(); Expect(settings.ActiveProfile == second && settings.KeyMap.SequenceEqual(map));
});
Check("Malformed profile and unsupported schema are rejected", () => {
    Reject(() => new Settings { Version = 2 }.Validate());
    Reject(() => new Settings { Profiles = [] }.Validate());
    var settings = new Settings(); settings.ActiveProfile.Buttons.RemoveAt(0); Reject(settings.Validate);
    var duplicated = new Settings(); duplicated.Profiles.Add(duplicated.Profiles[0]); Reject(duplicated.Validate);
});
string temp = Path.Combine(Path.GetTempPath(), "DeckPilot-check-" + Guid.NewGuid());
try
{
    var store = new SettingsStore(Path.Combine(temp, "settings.json"));
    Check("First launch needs no existing settings file", () => Expect(!store.Load().IsCalibrated));
    var settings = new Settings { KeyMap = map };
    var profile = settings.ActiveProfile;
    profile.Name = "Création & jeux"; settings.ActiveProfileId = profile.Id;
    profile.Buttons[0] = new() { Label = "Éditeur", Kind = ActionKind.Application, Target = @"C:\Program Files\Editor\editor.exe", Arguments = "--new-window" };
    profile.Buttons[1] = new() { Label = "Micro", Kind = ActionKind.Shortcut, Ctrl = true, Shift = true, Key = 0x4D };
    profile.Buttons[2] = new() { Kind = ActionKind.Media, Key = 0xB3 };
    settings.Profiles.Add(new() { Name = "Autre profil" });
    Check("Settings round-trip preserves positions, profiles and actions", () => {
        store.Save(settings); var loaded = store.Load();
        Expect(loaded.KeyMap.SequenceEqual(map) && loaded.Profiles.Count == 2);
        Expect(loaded.ActiveProfile.Name == profile.Name && loaded.ActiveProfileId == profile.Id);
        Expect(loaded.ActiveProfile.Buttons[0].Arguments == "--new-window");
        Expect(loaded.ActiveProfile.Buttons[0].Target == profile.Buttons[0].Target);
        Expect(loaded.ActiveProfile.Buttons[1].Ctrl && loaded.ActiveProfile.Buttons[1].Shift);
        Expect(loaded.ActiveProfile.Buttons[2].Key == 0xB3);
    });
    Check("Saving twice replaces the configuration without leftover temporary files", () => {
        settings.ActiveProfile.Name = "Modifié"; store.Save(settings);
        Expect(store.Load().ActiveProfile.Name == "Modifié");
        Expect(!File.Exists(Path.Combine(temp, "settings.json.tmp")));
    });
    Check("Invalid settings cannot overwrite a valid saved configuration", () => {
        settings.KeyMap = [0x7C]; Reject(() => store.Save(settings));
        Expect(store.Load().IsCalibrated);
    });
    Check("A damaged configuration is reported without overwriting it", () => {
        File.WriteAllText(Path.Combine(temp, "settings.json"), "{broken");
        bool rejected = false;
        try { store.Load(); } catch (System.Text.Json.JsonException) { rejected = true; }
        Expect(rejected && File.ReadAllText(Path.Combine(temp, "settings.json")) == "{broken");
    });
}
finally { if (Directory.Exists(temp)) Directory.Delete(temp, true); }
passed += await AdditionalChecks.RunAsync();
passed += FirmwareChecks.Run();
passed += BoardProtocolChecks.Run();
passed += await ActionToolsChecks.RunAsync();
passed += await ApplicationCatalogChecks.RunAsync();
passed += ProfileGestureAudioChecks.Run();
passed += FeatureChecks.Run();
passed += AuditFixChecks.Run();
passed += RealtimeAudioChecks.Run();
passed += SoundboardMonitorChecks.Run();
passed += NoiseSuppressionChecks.Run();
passed += await AudioEndpointCacheChecks.RunAsync();
Console.WriteLine($"{passed} checks passed.");
