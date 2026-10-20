using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeckPilot.Core;

public enum ActionKind { Passthrough, Application, Folder, Website, Shortcut, Media }
public sealed class ButtonAction
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
    public ButtonAction Copy() => (ButtonAction)MemberwiseClone();
}
public sealed class Profile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Profil par défaut";
    public List<ButtonAction> Buttons { get; set; } = Enumerable.Range(0, 12).Select(_ => new ButtonAction()).ToList();
    public override string ToString() => Name;
}
public sealed class Settings
{
    public int Version { get; set; } = 1;
    public bool AutoCheckUpdates { get; set; } = true;
    public DateTimeOffset UpdateSnoozedUntil { get; set; }
    public List<int> KeyMap { get; set; } = [];
    public List<Profile> Profiles { get; set; } = [new()];
    public Guid ActiveProfileId { get; set; }
    [JsonIgnore] public Profile ActiveProfile => Profiles.FirstOrDefault(p => p.Id == ActiveProfileId) ?? Profiles[0];
    [JsonIgnore] public bool IsCalibrated => Calibration.IsValid(KeyMap);
    public void Validate()
    {
        if (Version != 1 || Profiles is null || Profiles.Count == 0 ||
            Profiles.Any(p => p is null || string.IsNullOrWhiteSpace(p.Name) || p.Id == Guid.Empty ||
                p.Buttons is null || p.Buttons.Count != 12 || p.Buttons.Any(b => b is null ||
                    !Enum.IsDefined(b.Kind) || b.Label is null || b.Target is null || b.Arguments is null || b.ApplicationName is null)) ||
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
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true, Converters = { new JsonStringEnumConverter() } };
    public Settings Load()
    {
        if (!File.Exists(path)) return new();
        var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(path), Options)
            ?? throw new InvalidDataException("Configuration vide.");
        settings.Validate();
        return settings;
    }
    public void Save(Settings settings)
    {
        settings.Validate();
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var temporary = path + ".tmp";
        File.WriteAllText(temporary, JsonSerializer.Serialize(settings, Options));
        File.Move(temporary, path, overwrite: true);
    }
}
