using System.Text;
using System.Text.Json;

namespace DeckPilot.Core;

public sealed class PortableSound
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Extension { get; set; } = "";
    public byte[]? Audio { get; set; }
}
public sealed class PortableProfilePackage
{
    public string Format { get; set; } = "DeckPilot.SoundProfiles";
    public int Version { get; set; } = 1;
    public string Profiles { get; set; } = "";
    public List<PortableSound> Sounds { get; set; } = [];
}

public static class PortableProfiles
{
    public const int MaximumAudioBytes = 32 * 1024 * 1024;
    public const int MaximumPackageBytes = 48 * 1024 * 1024;
    private static IEnumerable<ButtonAction> Actions(IEnumerable<Profile> profiles) => profiles.SelectMany(p => p.Buttons)
        .SelectMany(a => a.LongPressAction == null ? new[] { a } : new[] { a, a.LongPressAction });
    internal static IEnumerable<Guid> ReferencedSounds(IEnumerable<Profile> profiles)
    {
        foreach (var action in Actions(profiles))
            if (action.Kind == ActionKind.Soundboard && Guid.TryParse(action.Target, out var id))
                yield return id;
    }

    public static string Export(Settings settings, IEnumerable<Profile> profiles, bool requireAudio = true)
    {
        var selected = profiles.ToList();
        var ids = ReferencedSounds(selected).Distinct().ToArray();
        string plain = ProfileExchange.Export(selected);
        if (ids.Length == 0) return plain;
        var package = new PortableProfilePackage { Profiles = plain };
        int remaining = MaximumAudioBytes;
        foreach (var id in ids)
        {
            var clip = settings.Soundboard.Clips.FirstOrDefault(c => c.Id == id);
            if (clip == null) { if (requireAudio) throw new InvalidDataException("Un son affecté à un bouton est introuvable."); continue; }
            byte[]? audio = null;
            try
            {
                using var source = File.OpenRead(clip.FilePath);
                long length = source.Length;
                if (length == 0) throw new InvalidDataException("Un fichier audio est vide.");
                if (length > remaining) throw new InvalidDataException("Les sons du profil dépassent 32 Mo.");
                var bytes = new byte[(int)length];
                source.ReadExactly(bytes);
                remaining -= bytes.Length;
                audio = bytes;
            }
            catch (Exception ex) when (!requireAudio && ex is (IOException or InvalidDataException or UnauthorizedAccessException))
            {
                // Keep the clip identity in automatic backups even when its audio cannot be embedded.
            }
            package.Sounds.Add(new() { Id = id, Name = clip.Name, Extension = Path.GetExtension(clip.FilePath).ToLowerInvariant(), Audio = audio });
        }
        string json = JsonSerializer.Serialize(package);
        if (Encoding.UTF8.GetByteCount(json) > MaximumPackageBytes) throw new InvalidDataException("Le paquet de profils est trop volumineux.");
        return json;
    }
    public static PortableProfilePackage Read(string path)
    {
        if (new FileInfo(path).Length > MaximumPackageBytes) throw new InvalidDataException("Le paquet de profils est trop volumineux.");
        return Import(File.ReadAllText(path));
    }
    public static PortableProfilePackage Import(string json)
    {
        if (Encoding.UTF8.GetByteCount(json) > MaximumPackageBytes) throw new InvalidDataException("Le paquet de profils est trop volumineux.");
        using var doc = JsonDocument.Parse(json);
        if (doc.RootElement.ValueKind != JsonValueKind.Object ||
            !doc.RootElement.TryGetProperty("Format", out var format) || format.ValueKind != JsonValueKind.String ||
            !doc.RootElement.TryGetProperty("Version", out _))
            throw new InvalidDataException("Le format ou la version du fichier de profils manque.");
        if (format.GetString() == "DeckPilot.Profiles")
        { ProfileExchange.Import(json); return new() { Profiles = json }; }
        var package = JsonSerializer.Deserialize<PortableProfilePackage>(json) ?? throw new InvalidDataException("Paquet vide.");
        Validate(package); return package;
    }
    private static ProfilePackage Validate(PortableProfilePackage package)
    {
        if (package.Format != "DeckPilot.SoundProfiles" || package.Version != 1 || package.Profiles == null ||
            package.Sounds == null || package.Sounds.Count > SoundboardSettings.MaximumClips)
            throw new InvalidDataException("Paquet de sons incompatible.");
        var profiles = ProfileExchange.Import(package.Profiles);
        long size = 0; var ids = new HashSet<Guid>();
        foreach (var sound in package.Sounds)
        {
            if (sound == null || sound.Id == Guid.Empty || !ids.Add(sound.Id) || sound.Name is not { Length: > 0 and <= 64 }
                || sound.Audio is { Length: 0 } || sound.Extension is not (".mp3" or ".wav")) throw new InvalidDataException("Définition de son invalide.");
            size += sound.Audio?.Length ?? 0;
            if (size > MaximumAudioBytes) throw new InvalidDataException("Les sons du profil dépassent 32 Mo.");
        }
        return profiles;
    }

    private static bool SameAudio(string path, ReadOnlySpan<byte> audio)
    {
        try
        {
            using var source = File.OpenRead(path);
            if (source.Length != audio.Length) return false;
            Span<byte> buffer = stackalloc byte[4096];
            while (!audio.IsEmpty)
            {
                var chunk = buffer[..Math.Min(buffer.Length, audio.Length)];
                source.ReadExactly(chunk);
                if (!chunk.SequenceEqual(audio[..chunk.Length])) return false;
                audio = audio[chunk.Length..];
            }
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { return false; }
    }

    public static List<Profile> Apply(Settings settings, PortableProfilePackage package, bool replace, string soundsRoot,
        Action<Settings>? save = null)
    {
        var profiles = Validate(package);
        if (!replace && settings.Profiles.Count + profiles.Profiles.Count > 100) throw new InvalidDataException("Limite de 100 profils atteinte.");
        var imported = replace ? profiles.Profiles : ProfileExchange.CopyAsNew(profiles.Profiles);
        var referenced = ReferencedSounds(imported).ToHashSet();
        var sounds = package.Sounds.Where(c => referenced.Contains(c.Id)).ToArray();
        SoundboardClip? FindReusable(PortableSound sound)
        {
            if (sound.Audio is not { } audio) return settings.Soundboard.Clips.FirstOrDefault(c => c.Id == sound.Id);
            return settings.Soundboard.Clips
                .Where(c => c.Id == sound.Id || c.Name == sound.Name && Path.GetExtension(c.FilePath).Equals(sound.Extension, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(c => c.Id == sound.Id)
                .FirstOrDefault(c => SameAudio(c.FilePath, audio));
        }
        var reusable = sounds.ToDictionary(sound => sound.Id, FindReusable);
        if (settings.Soundboard.Clips.Count + reusable.Values.Count(clip => clip == null) > SoundboardSettings.MaximumClips)
            throw new InvalidDataException("Limite de 48 sons atteinte. Retire les sons inutilisés avant d’importer.");
        string folder = Path.Combine(soundsRoot, Guid.NewGuid().ToString("N"));
        var clips = new List<SoundboardClip>(); var map = new Dictionary<Guid, Guid>();
        try
        {
            foreach (var sound in sounds)
            {
                if (reusable[sound.Id] is { } existing) { map.Add(sound.Id, existing.Id); continue; }
                Guid id = Guid.NewGuid(); map.Add(sound.Id, id);
                string path = Path.Combine(folder, id.ToString("N") + sound.Extension);
                if (sound.Audio != null) { Directory.CreateDirectory(folder); File.WriteAllBytes(path, sound.Audio); }
                clips.Add(new() { Id = id, Name = sound.Name, FilePath = path });
            }
            foreach (var action in Actions(imported))
                if (action.Kind == ActionKind.Soundboard && Guid.TryParse(action.Target, out var id) && map.TryGetValue(id, out var next)) action.Target = next.ToString();
            if (!replace)
            {
                var names = settings.Profiles.Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
                foreach (var profile in imported)
                {
                    string original = profile.Name;
                    for (int i = 1; !names.Add(profile.Name); i++)
                    {
                        string suffix = $" (import {i})";
                        profile.Name = original[..Math.Min(original.Length, 60 - suffix.Length)] + suffix;
                    }
                }
            }
            settings.CommitLibrary(replace ? imported : [.. settings.Profiles, .. imported],
                imported[0].Id, [.. settings.Soundboard.Clips, .. clips], save);
            return imported;
        }
        catch
        {
            try { Directory.Delete(folder, recursive: true); }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
            throw;
        }
    }
}
