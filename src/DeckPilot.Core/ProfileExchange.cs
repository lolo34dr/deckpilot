using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeckPilot.Core;

public sealed class ProfilePackage
{
    public string Format { get; set; } = "DeckPilot.Profiles";
    public int Version { get; set; } = 1;
    public List<Profile> Profiles { get; set; } = [];
}
public static class ProfileExchange
{
    public const int MaximumBytes = 1024 * 1024;
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true, MaxDepth = 16, UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        Converters = { new JsonStringEnumConverter() }
    };
    public static string Export(IEnumerable<Profile> profiles)
    {
        var package = new ProfilePackage { Profiles = profiles.ToList() };
        Validate(package);
        string json = JsonSerializer.Serialize(package, Options);
        if (Encoding.UTF8.GetByteCount(json) > MaximumBytes) throw new InvalidDataException("Le fichier de profils dépasse 1 Mo.");
        return json;
    }
    public static ProfilePackage Import(string json)
    {
        if (Encoding.UTF8.GetByteCount(json) > MaximumBytes) throw new InvalidDataException("Le fichier de profils dépasse 1 Mo.");
        try
        {
            using var document = JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 16 });
            if (document.RootElement.ValueKind != JsonValueKind.Object || !document.RootElement.TryGetProperty("Format", out _) || !document.RootElement.TryGetProperty("Version", out _))
                throw new InvalidDataException("Le format ou la version du fichier de profils manque.");
            var package = JsonSerializer.Deserialize<ProfilePackage>(json, Options) ?? throw new InvalidDataException("Fichier vide.");
            Validate(package); return package;
        }
        catch (JsonException ex) { throw new InvalidDataException("Le fichier de profils est invalide ou incompatible.", ex); }
    }
    public static ProfilePackage Read(string path)
    {
        if (new FileInfo(path).Length > MaximumBytes) throw new InvalidDataException("Le fichier de profils dépasse 1 Mo.");
        return Import(File.ReadAllText(path));
    }
    private static void Validate(ProfilePackage package)
    {
        if (package.Format != "DeckPilot.Profiles" || package.Version != 1 || package.Profiles == null || package.Profiles.Count is < 1 or > 100)
            throw new InvalidDataException("Format de profils incompatible.");
        new Settings { Profiles = package.Profiles }.Validate();
        foreach (var p in package.Profiles)
        {
            if (p.Name.Length > 60) throw new InvalidDataException("Nom de profil trop long.");
            foreach (var a in p.Buttons.SelectMany(a => a.LongPressAction == null ? new[] { a } : new[] { a, a.LongPressAction }))
                if (a.Label.Length > 50 || a.ApplicationName.Length > 1024 || a.Target.Length > 32768 || a.Arguments.Length > 32768)
                    throw new InvalidDataException("Action de profil trop longue.");
        }
    }
    public static List<Profile> Copy(IEnumerable<Profile> profiles) => Import(Export(profiles)).Profiles;
    public static List<Profile> AddAsNew(Settings settings, ProfilePackage package)
    {
        var imported = Copy(package.Profiles);
        var names = settings.Profiles.Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var p in imported)
        {
            p.Id = Guid.NewGuid(); string original = p.Name;
            for (int i = 1; !names.Add(p.Name); i++)
            {
                string suffix = $" (import {i})";
                p.Name = original[..Math.Min(original.Length, 60 - suffix.Length)] + suffix;
            }
        }
        settings.Profiles.AddRange(imported); return imported;
    }
}
public static class ProfileBackups
{
    public static void Save(string directory, IEnumerable<Profile> profiles)
    {
        string json = ProfileExchange.Export(profiles);
        string hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(json)))[..12];
        Directory.CreateDirectory(directory);
        if (Directory.GetFiles(directory, "profiles-*-" + hash + ".deckpilot.json").Length == 0)
        {
            string file = Path.Combine(directory, $"profiles-{DateTime.UtcNow:yyyyMMdd-HHmmssfffffff}-{hash}.deckpilot.json");
            string temporary = file + ".tmp";
            try { File.WriteAllText(temporary, json); File.Move(temporary, file, false); }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }
        foreach (string old in Directory.GetFiles(directory, "profiles-*.deckpilot.json").OrderByDescending(p => p, StringComparer.Ordinal).Skip(20))
            try { File.Delete(old); } catch (IOException) { } catch (UnauthorizedAccessException) { }
    }
}
public sealed class ProfileHistory
{
    private sealed record Snapshot(string Profiles, Guid Active);
    private Snapshot current;
    private readonly List<Snapshot> undo = [], redo = [];
    public ProfileHistory(Settings settings) => current = Capture(settings);
    public bool CanUndo => undo.Count > 0;
    public bool CanRedo => redo.Count > 0;
    private static Snapshot Capture(Settings settings) => new(ProfileExchange.Export(settings.Profiles), settings.ActiveProfile.Id);
    public void Record(Settings settings)
    {
        var next = Capture(settings);
        if (next.Profiles != current.Profiles)
        {
            undo.Add(current); if (undo.Count > 50) undo.RemoveAt(0);
            redo.Clear();
        }
        current = next;
    }
    public bool Undo(Settings settings) => Restore(settings, undo, redo);
    public bool Redo(Settings settings) => Restore(settings, redo, undo);
    private bool Restore(Settings settings, List<Snapshot> source, List<Snapshot> destination)
    {
        if (source.Count == 0) return false;
        destination.Add(current); current = source[^1]; source.RemoveAt(source.Count - 1);
        settings.Profiles = ProfileExchange.Import(current.Profiles).Profiles;
        settings.ActiveProfileId = current.Active;
        return true;
    }
}
