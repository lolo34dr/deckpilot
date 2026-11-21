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
    internal static void ValidateProfiles(List<Profile> profiles) => Validate(new ProfilePackage { Profiles = profiles });

    public static List<Profile> Copy(IEnumerable<Profile> profiles)
    {
        var package = new ProfilePackage { Profiles = profiles.ToList() };
        Validate(package);
        return package.Profiles.Select(profile => profile.Copy()).ToList();
    }
    public static List<Profile> CopyAsNew(IEnumerable<Profile> profiles)
    {
        var copies = Copy(profiles);
        var ids = copies.ToDictionary(profile => profile.Id, _ => Guid.NewGuid());
        foreach (var profile in copies)
        {
            profile.Id = ids[profile.Id];
            foreach (var action in profile.Buttons.SelectMany(a => a.LongPressAction == null ? new[] { a } : new[] { a, a.LongPressAction }))
                if (action.Kind == ActionKind.Profile && Guid.TryParse(action.Target, out var destination) && ids.TryGetValue(destination, out var newId))
                    action.Target = newId.ToString();
        }
        return copies;
    }
    internal static bool HaveSameContent(IReadOnlyList<Profile> left, IReadOnlyList<Profile> right)
    {
        if (left.Count != right.Count) return false;
        for (int i = 0; i < left.Count; i++)
        {
            var a = left[i]; var b = right[i];
            if (a == null || b == null || a.Id != b.Id || a.Name != b.Name || a.Buttons == null || b.Buttons == null || !a.Buttons.SequenceEqual(b.Buttons)) return false;
        }
        return true;
    }
    public static List<Profile> AddAsNew(Settings settings, ProfilePackage package)
    {
        Validate(package);
        if (settings.Profiles.Count + package.Profiles.Count > 100) throw new InvalidDataException("Limite de 100 profils atteinte.");
        var imported = CopyAsNew(package.Profiles);
        var names = settings.Profiles.Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var p in imported)
        {
            string original = p.Name;
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
        SaveJson(directory, ProfileExchange.Export(profiles));
    }
    public static void Save(string directory, Settings settings) => SaveJson(directory, PortableProfiles.Export(settings, settings.Profiles, requireAudio: false));
    private static void SaveJson(string directory, string json)
    {
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
    private sealed record Snapshot(List<Profile> Profiles, Guid Active, List<SoundboardClip> Clips);
    private Snapshot current;
    private readonly List<Snapshot> undo = [], redo = [];
    public ProfileHistory(Settings settings) => current = Capture(settings);
    public bool CanUndo => undo.Count > 0;
    public bool CanRedo => redo.Count > 0;
    private static List<Profile> CopyProfiles(List<Profile> profiles) => profiles.Select(profile => profile.Copy()).ToList();
    public IEnumerable<string> RetainedSoundPaths => undo.Concat(redo).Append(current)
        .SelectMany(snapshot => snapshot.Clips).Select(clip => clip.FilePath);
    private static List<SoundboardClip> CaptureClips(Settings settings)
    {
        var ids = PortableProfiles.ReferencedSounds(settings.Profiles).ToHashSet();
        return settings.Soundboard.Clips.Where(clip => ids.Contains(clip.Id)).Select(clip => clip with { }).ToList();
    }
    private static Snapshot Capture(Settings settings)
    {
        ProfileExchange.ValidateProfiles(settings.Profiles);
        return new(CopyProfiles(settings.Profiles), settings.ActiveProfile.Id, CaptureClips(settings));
    }
    public void Record(Settings settings)
    {
        if (ProfileExchange.HaveSameContent(settings.Profiles, current.Profiles))
        {
            current = current with { Active = settings.ActiveProfile.Id, Clips = CaptureClips(settings) };
            return;
        }
        var next = Capture(settings);
        undo.Add(current); if (undo.Count > 50) undo.RemoveAt(0);
        redo.Clear();
        current = next;
    }
    public bool Undo(Settings settings, Action<Settings>? save = null) => Restore(settings, undo, redo, save);
    public bool Redo(Settings settings, Action<Settings>? save = null) => Restore(settings, redo, undo, save);
    private bool Restore(Settings settings, List<Snapshot> source, List<Snapshot> destination, Action<Settings>? save)
    {
        if (source.Count == 0) return false;
        var next = source[^1];
        var clips = settings.Soundboard.Clips.Concat(next.Clips
            .Where(saved => settings.Soundboard.Clips.All(clip => clip.Id != saved.Id))
            .Select(clip => clip with { })).ToList();
        if (clips.Count > SoundboardSettings.MaximumClips)
            throw new InvalidDataException("Libère de la place dans le soundboard pour rétablir les sons de ce profil.");

        settings.CommitLibrary(CopyProfiles(next.Profiles), next.Active, clips, save);
        destination.Add(current);
        source.RemoveAt(source.Count - 1);
        current = Capture(settings);
        return true;
    }
}
