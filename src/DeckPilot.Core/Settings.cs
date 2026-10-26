using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeckPilot.Core;

public enum ActionKind { Passthrough, Application, Folder, Website, Shortcut, Media, Spotify, Microphone, AudioOutput }
public sealed record ButtonAction
{
    public string Label { get; set; } = "";
    public ActionKind Kind { get; set; }
    public string Target { get; set; } = "";
    public string Arguments { get; set; } = "";
    public string ApplicationName { get; set; } = "";
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
        && LongPressMilliseconds is >= 200 and <= 3000
        && (!nested || (!LongPressEnabled && LongPressAction == null))
        && (LongPressAction == null || LongPressAction.IsValid(true))
        && (!LongPressEnabled || (!nested && Kind != ActionKind.Passthrough && LongPressAction is { Kind: not ActionKind.Passthrough }));
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
public sealed class Settings
{
    public int Version { get; set; } = 1;
    public int LayoutColumns { get; set; } = 4;
    public bool AutoCheckFirmware { get; set; } = true;
    public DateTimeOffset FirmwareSnoozedUntil { get; set; }
    public string PreferredBoardDeviceId { get; set; } = "";
    public bool AutoCheckUpdates { get; set; } = true;
    public DateTimeOffset UpdateSnoozedUntil { get; set; }
    public List<int> KeyMap { get; set; } = [];
    public List<Profile> Profiles { get; set; } = [new()];
    public Guid ActiveProfileId { get; set; }
    [JsonIgnore] public Profile ActiveProfile => Profiles.FirstOrDefault(p => p.Id == ActiveProfileId) ?? Profiles[0];
    [JsonIgnore] public bool IsCalibrated => Calibration.IsValid(KeyMap);
    public bool MigrateLayout()
    {
        if (LayoutColumns == 4) return false;
        LayoutColumns = 4; KeyMap.Clear();
        return true;
    }
    public void Validate()
    {
        if (PreferredBoardDeviceId is null || PreferredBoardDeviceId.Length > 512 || Version != 1 || LayoutColumns is not (3 or 4) || Profiles is null || Profiles.Count == 0 ||
            Profiles.Any(p => p is null || string.IsNullOrWhiteSpace(p.Name) || p.Id == Guid.Empty ||
                p.Buttons is null || p.Buttons.Count != 12 || p.Buttons.Any(b => b is null ||
                    !b.IsValid())) ||
            Profiles.Select(p => p.Id).Distinct().Count() != Profiles.Count || KeyMap is null ||
            (KeyMap.Count != 0 && !IsCalibrated))
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
        settings.Validate();
        return settings;
    }
    public void Save(Settings settings)
    {
        settings.Validate();
        string json = JsonSerializer.Serialize(settings, Options);
        if (File.Exists(path))
        {
            // Compare the actual file, not a cached timestamp: another program
            // may have changed or removed it since this instance last saved.
            string previousJson = File.ReadAllText(path);
            if (previousJson == json)
            {
                _ = ProfileExchange.Export(settings.Profiles);
                return;
            }
            Settings? previous = null;
            try { previous = Deserialize(previousJson); }
            catch (Exception ex) when (ex is JsonException or InvalidDataException)
            {
                Directory.CreateDirectory(BackupsPath);
                File.Copy(path, Path.Combine(BackupsPath, "recovered-settings-" + Guid.NewGuid().ToString("N") + ".json"));
            }
            if (previous != null)
            {
                _ = ProfileExchange.Export(settings.Profiles);
                if (!ProfileExchange.HaveSameContent(previous.Profiles, settings.Profiles))
                    ProfileBackups.Save(BackupsPath, previous.Profiles);
            }
        }
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
