using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeckPilot.Core;

public enum ActionKind { Passthrough, Application, Folder, Website, Shortcut, Media, Spotify, Microphone, AudioOutput, Profile, System, Soundboard, Streaming, Deezer }
public static class ActionKeys
{
    public static bool IsValid(ActionKind kind, int key) => kind switch
    {
        ActionKind.Shortcut => key is >= 0x30 and <= 0x39 or >= 0x41 and <= 0x5A or >= 0x70 and <= 0x87
            or 0x0D or 0x1B or 0x20 or 0x09 or 0x08 or >= 0x21 and <= 0x28 or 0x2D or 0x2E,
        ActionKind.Media => key == 0xAD || IsPlayerKey(key),
        ActionKind.Spotify or ActionKind.Deezer => IsPlayerKey(key),
        _ => true
    };

    private static bool IsPlayerKey(int key) => key is 0xB3 or 0xB0 or 0xB1 or 0xAF or 0xAE;

    internal static void RepairLegacyKey(ButtonAction action)
    {
        if (!IsValid(action.Kind, action.Key))
            action.Key = action.Kind == ActionKind.Shortcut ? 0x41 : 0xB3;
    }
}

public sealed record ButtonAction
{
    public string Label { get; set; } = "";
    public ActionKind Kind { get; set; }
    public string Target { get; set; } = "";
    public string Arguments { get; set; } = "";
    public string ApplicationName { get; set; } = "";
    public string Accent { get; set; } = "";
    public int Key { get; set; } = 0x41;
    public bool Ctrl { get; set; }
    public bool Alt { get; set; }
    public bool Shift { get; set; }
    public bool Win { get; set; }
    public bool LongPressEnabled { get; set; }
    public int LongPressMilliseconds { get; set; } = 600;
    public ButtonAction? LongPressAction { get; set; }
    public ButtonAction Copy()
    {
        var copy = (ButtonAction)MemberwiseClone();
        copy.LongPressAction = LongPressAction?.CopyBasic();
        return copy;
    }
    public ButtonAction CopyBasic()
    {
        var copy = (ButtonAction)MemberwiseClone();
        copy.LongPressEnabled = false; copy.LongPressAction = null;
        return copy;
    }
    public bool IsValid(bool nested = false) => Enum.IsDefined(Kind) && Label != null && Target != null && Arguments != null && ApplicationName != null
        && ActionAppearance.IsValidAccent(Accent)
        && ActionKeys.IsValid(Kind, Key)
        && LongPressMilliseconds is >= 200 and <= 3000
        && (Kind != ActionKind.Streaming || IsValidStreamingAction())
        && (!nested || (!LongPressEnabled && LongPressAction == null))
        && (LongPressAction == null || LongPressAction.IsValid(true))
        && (!LongPressEnabled || (!nested && Kind != ActionKind.Passthrough && LongPressAction is { Kind: not ActionKind.Passthrough }));
    private bool IsValidStreamingAction()
    {
        return StreamingActionCatalog.Find(ApplicationName, Target) != null && Arguments.Length <= 512;
    }
}
public sealed class Profile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Profil par défaut";
    public List<ButtonAction> Buttons { get; set; } = Enumerable.Range(0, 12).Select(_ => new ButtonAction()).ToList();
    public override string ToString() => Name;
    internal Profile Copy()
    {
        var copy = (Profile)MemberwiseClone();
        copy.Buttons = Buttons.Select(button => button.Copy()).ToList();
        return copy;
    }
}
public sealed class ThemeSettings
{
    public string Background { get; set; } = "#08090B";
    public string Panel { get; set; } = "#14161A";
    public string Accent { get; set; } = "#9B8CFF";
    public bool IsValid() => new[] { Background, Panel, Accent }.All(c => c != null && System.Text.RegularExpressions.Regex.IsMatch(c, "^#[0-9A-Fa-f]{6}$"));
}
public sealed record SoundboardClip
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string FilePath { get; set; } = "";
}
public sealed class SoundboardSettings
{
    public const int MaximumClips = 48;
    public bool AutoStart { get; set; } = true;
    public string MicrophoneId { get; set; } = "";
    public bool NoiseSuppressionEnabled { get; set; } = true;
    public int NoiseSuppressionStrengthPercent { get; set; } = 75;
    public bool MonitorEnabled { get; set; }
    public string MonitorOutputId { get; set; } = "";
    public int MonitorVolumePercent { get; set; } = 50;
    public List<SoundboardClip> Clips { get; set; } = [];
    internal SoundboardSettings WithClips(List<SoundboardClip> clips)
    {
        var copy = (SoundboardSettings)MemberwiseClone();
        copy.Clips = clips;
        return copy;
    }
    public bool IsValid() => MicrophoneId != null && MonitorOutputId != null
        && MicrophoneId.Length < 1024 && MonitorOutputId.Length < 1024
        && NoiseSuppressionStrengthPercent is >= 0 and <= 100 && MonitorVolumePercent is >= 0 and <= 100
        && Clips != null && Clips.Count <= MaximumClips && Clips.All(c => c != null && c.Id != Guid.Empty && c.Name is { Length: > 0 and <= 64 } && c.FilePath is { Length: > 0 and <= 4096 } && (c.FilePath.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || c.FilePath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))) && Clips.Select(c => c.Id).Distinct().Count() == Clips.Count;
}
public sealed class StreamingConnections
{
    public string ObsHost { get; set; } = "127.0.0.1";
    public int ObsPort { get; set; } = 4455;
    public string ObsPasswordProtected { get; set; } = "";
    public string StreamlabsHost { get; set; } = "127.0.0.1";
    public int StreamlabsPort { get; set; } = 59650;
    public string StreamlabsTokenProtected { get; set; } = "";
    public bool IsValid() => IsHost(ObsHost) && ObsPort is > 0 and <= 65535 && ObsPasswordProtected != null && ObsPasswordProtected.Length <= 8192
        && IsHost(StreamlabsHost) && StreamlabsPort is > 0 and <= 65535 && StreamlabsTokenProtected != null && StreamlabsTokenProtected.Length <= 8192;
    private static bool IsHost(string value) => value != null && value.Length is > 0 and <= 253 && !value.Any(char.IsWhiteSpace)
        && Uri.CheckHostName(value) is (UriHostNameType.Dns or UriHostNameType.IPv4 or UriHostNameType.IPv6);
}
public sealed class WindowPlacementSettings
{
    public double Width { get; set; } = 1180;
    public double Height { get; set; } = 900;
    public double? Left { get; set; }
    public double? Top { get; set; }
    public bool IsValid() => double.IsFinite(Width) && Width is >= 400 and <= 20000
        && double.IsFinite(Height) && Height is >= 400 and <= 20000
        && (Left == null) == (Top == null)
        && (Left == null || (double.IsFinite(Left.Value) && Left.Value is >= -100000 and <= 100000
            && double.IsFinite(Top!.Value) && Top.Value is >= -100000 and <= 100000));
}
public sealed record BoardBinding(Guid ProfileId, List<int> KeyMap, bool Enabled = true)
{
    public int ButtonIndex(int bit) => KeyMap.Count == 0 ? bit : KeyMap.IndexOf(Calibration.FirstKey + bit);
}

public sealed class Settings
{
    public Settings() => ActiveProfileId = Profiles[0].Id;
    public int Version { get; set; } = 1;
    public int LayoutColumns { get; set; } = 4;
    public bool AutoCheckFirmware { get; set; } = true;
    public DateTimeOffset FirmwareSnoozedUntil { get; set; }
    public string PreferredBoardDeviceId { get; set; } = "";
    public Dictionary<string, BoardBinding> BoardBindings { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public bool AutoCheckUpdates { get; set; } = true;
    public bool ReduceMotion { get; set; }
    public ProfileOverlaySettings ProfileOverlay { get; set; } = new();
    public ThemeSettings Theme { get; set; } = new();
    public SoundboardSettings Soundboard { get; set; } = new();
    public StreamingConnections Streaming { get; set; } = new();
    public WindowPlacementSettings WindowPlacement { get; set; } = new();
    public DateTimeOffset UpdateSnoozedUntil { get; set; }
    public List<int> KeyMap { get; set; } = [];
    public List<Profile> Profiles { get; set; } = [new()];
    public Guid ActiveProfileId { get; set; }
    [JsonIgnore] public Profile ActiveProfile => Profiles.FirstOrDefault(p => p.Id == ActiveProfileId) ?? Profiles[0];
    [JsonIgnore] public bool IsCalibrated => Calibration.IsValid(KeyMap);

    // Save the proposed state before changing what the app is using.
    internal void CommitLibrary(List<Profile> profiles, Guid activeProfileId, List<SoundboardClip> clips, Action<Settings>? save)
    {
        var candidate = (Settings)MemberwiseClone();
        candidate.Profiles = profiles;
        candidate.ActiveProfileId = activeProfileId;
        candidate.Soundboard = Soundboard.WithClips(clips);
        save?.Invoke(candidate);
        Profiles = candidate.Profiles;
        ActiveProfileId = candidate.ActiveProfileId;
        Soundboard = candidate.Soundboard;
    }
    public bool MigrateLayout()
    {
        if (LayoutColumns == 4) return false;
        LayoutColumns = 4; KeyMap.Clear();
        return true;
    }
    public void Validate()
    {
        if (Theme is null || !Theme.IsValid() || Soundboard is null || !Soundboard.IsValid() || Streaming is null || !Streaming.IsValid() || ProfileOverlay is null || !ProfileOverlay.IsValid() || WindowPlacement is null || !WindowPlacement.IsValid() || PreferredBoardDeviceId is null || PreferredBoardDeviceId.Length > 512 || Version != 1 || LayoutColumns is not (3 or 4) || Profiles is null || Profiles.Count == 0 ||
            Profiles.Any(p => p is null || string.IsNullOrWhiteSpace(p.Name) || p.Id == Guid.Empty ||
                p.Buttons is null || p.Buttons.Count != 12 || p.Buttons.Any(b => b is null ||
                    !b.IsValid())) ||
            Profiles.Select(p => p.Id).Distinct().Count() != Profiles.Count || KeyMap is null ||
            (KeyMap.Count != 0 && !IsCalibrated) || BoardBindings is null ||
            BoardBindings.Any(pair => string.IsNullOrWhiteSpace(pair.Key) || pair.Key.Length > 512 ||
                pair.Value is null || pair.Value.ProfileId == Guid.Empty || pair.Value.KeyMap is null ||
                (pair.Value.KeyMap.Count != 0 && !Calibration.IsValid(pair.Value.KeyMap))) ||
            BoardBindings.Keys.Distinct(StringComparer.OrdinalIgnoreCase).Count() != BoardBindings.Count)
            throw new InvalidDataException("Le fichier de configuration est incomplet ou incompatible.");
    }
}
public static class Calibration
{
    public const int FirstKey = 0x7C;
    public const int LastKey = 0x87;
    public static bool IsDeckKey(int key) => key >= FirstKey && key <= LastKey;
    public static bool IsValid(IReadOnlyList<int> keys) => keys.Count == 12 && keys.All(IsDeckKey) && keys.Distinct().Count() == 12;
    public static string KeyName(int key) => IsDeckKey(key) ? $"F{key - FirstKey + 13}" : "?";
}
public sealed class SettingsStore(string path)
{
    public string BackupsPath => Path.Combine(Path.GetDirectoryName(path)!, "Backups");
    public string SoundsPath => Path.Combine(Path.GetDirectoryName(path)!, "Sounds");
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true, Converters = { new JsonStringEnumConverter() } };
    public Settings Load()
    {
        if (!File.Exists(path)) return new();
        return Deserialize(File.ReadAllText(path));
    }
    private static Settings Deserialize(string json)
    {
        var settings = JsonSerializer.Deserialize<Settings>(json, Options)
            ?? throw new InvalidDataException("Configuration vide.");
        using var document = JsonDocument.Parse(json);
        if (!document.RootElement.TryGetProperty("LayoutColumns", out _)) settings.LayoutColumns = 3;
        // Older configurations could store command codes that the editor could not display.
        if (settings.Profiles != null)
            foreach (var profile in settings.Profiles)
                if (profile?.Buttons != null)
                    foreach (var action in profile.Buttons)
                        if (action != null)
                        {
                            ActionKeys.RepairLegacyKey(action);
                            if (action.LongPressAction is { } longAction) ActionKeys.RepairLegacyKey(longAction);
                        }
        settings.Validate();
        settings.BoardBindings = new(settings.BoardBindings, StringComparer.OrdinalIgnoreCase);
        settings.ActiveProfileId = settings.ActiveProfile.Id;
        return settings;
    }
    public void Save(Settings settings)
    {
        settings.Validate();
        settings.ActiveProfileId = settings.ActiveProfile.Id;
        string json = JsonSerializer.Serialize(settings, Options);
        Settings? previous = null;
        if (File.Exists(path))
        {
            // Compare the actual file, not a cached timestamp: another program
            // may have changed or removed it since this instance last saved.
            string previousJson = File.ReadAllText(path);
            if (previousJson == json) return;
            try { previous = Deserialize(previousJson); }
            catch (Exception ex) when (ex is JsonException or InvalidDataException)
            {
                Directory.CreateDirectory(BackupsPath);
                File.Copy(path, Path.Combine(BackupsPath, "recovered-settings-" + Guid.NewGuid().ToString("N") + ".json"));
            }
        }
        bool profilesChanged = previous == null || !ProfileExchange.HaveSameContent(previous.Profiles, settings.Profiles);
        if (profilesChanged) _ = ProfileExchange.Export(settings.Profiles);
        if (previous != null && (profilesChanged || !previous.Soundboard.Clips.SequenceEqual(settings.Soundboard.Clips)))
            ProfileBackups.Save(BackupsPath, previous);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var temporary = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporary, json);
            // ReplaceFile on Windows keeps existing delete-sharing readers on
            // their old snapshot; MoveFileEx cannot reliably overwrite it.
            if (OperatingSystem.IsWindows() && File.Exists(path)) File.Replace(temporary, path, null);
            else File.Move(temporary, path, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}
