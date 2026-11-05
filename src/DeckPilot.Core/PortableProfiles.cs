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
    public static string Export(Settings settings, IEnumerable<Profile> profiles, bool requireAudio = true)
    {
        var selected = profiles.ToList();
        var ids = Actions(selected).Where(a => a.Kind == ActionKind.Soundboard && Guid.TryParse(a.Target, out _))
            .Select(a => Guid.Parse(a.Target)).Distinct().ToArray();
        string plain = ProfileExchange.Export(selected);
        if (ids.Length == 0) return plain;
        var package = new PortableProfilePackage { Profiles = plain };
        long size = 0;
        foreach (var id in ids)
        {
            var clip = settings.Soundboard.Clips.FirstOrDefault(c => c.Id == id);
            if (clip == null) { if (requireAudio) throw new InvalidDataException("Un son affecté à un bouton est introuvable."); continue; }
            byte[]? audio = null;
            if (File.Exists(clip.FilePath))
            {
                size += new FileInfo(clip.FilePath).Length;
                if (size > MaximumAudioBytes) throw new InvalidDataException("Les sons du profil dépassent 32 Mo.");
                audio = File.ReadAllBytes(clip.FilePath);
                if (audio.Length == 0) { if (requireAudio) throw new InvalidDataException("Un fichier audio est vide."); audio = null; }
            }
            else if (requireAudio) throw new FileNotFoundException("Un son du profil est introuvable.", clip.FilePath);
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
        if (doc.RootElement.GetProperty("Format").GetString() == "DeckPilot.Profiles")
        { ProfileExchange.Import(json); return new() { Profiles = json }; }
        var package = JsonSerializer.Deserialize<PortableProfilePackage>(json) ?? throw new InvalidDataException("Paquet vide.");
        Validate(package); return package;
    }
    private static void Validate(PortableProfilePackage package)
    {
        if (package.Format != "DeckPilot.SoundProfiles" || package.Version != 1 || package.Profiles == null || package.Sounds == null || package.Sounds.Count > 48)
            throw new InvalidDataException("Paquet de sons incompatible.");
        ProfileExchange.Import(package.Profiles);
        long size = 0; var ids = new HashSet<Guid>();
        foreach (var sound in package.Sounds)
        {
            if (sound == null || sound.Id == Guid.Empty || !ids.Add(sound.Id) || sound.Name is not { Length: > 0 and <= 64 }
                || sound.Audio is { Length: 0 } || sound.Extension is not (".mp3" or ".wav")) throw new InvalidDataException("Définition de son invalide.");
            size += sound.Audio?.Length ?? 0;
            if (size > MaximumAudioBytes) throw new InvalidDataException("Les sons du profil dépassent 32 Mo.");
        }
    }
    public static List<Profile> Apply(Settings settings, PortableProfilePackage package, bool replace, string soundsRoot)
    {
        Validate(package);
        var profiles = ProfileExchange.Import(package.Profiles);
        if (!replace && settings.Profiles.Count + profiles.Profiles.Count > 100) throw new InvalidDataException("Limite de 100 profils atteinte.");
        var imported = replace ? ProfileExchange.Copy(profiles.Profiles) : ProfileExchange.CopyAsNew(profiles.Profiles);
        var referenced = Actions(imported).Where(a => a.Kind == ActionKind.Soundboard && Guid.TryParse(a.Target, out _)).Select(a => Guid.Parse(a.Target)).ToHashSet();
        var sounds = package.Sounds.Where(c => referenced.Contains(c.Id)).ToArray();
        bool Reusable(PortableSound sound)
        {
            var clip = settings.Soundboard.Clips.FirstOrDefault(c => c.Id == sound.Id && c.Name == sound.Name);
            if (clip == null) return false;
            if (sound.Audio == null) return true;
            return File.Exists(clip.FilePath) && new FileInfo(clip.FilePath).Length == sound.Audio.Length
                && File.ReadAllBytes(clip.FilePath).AsSpan().SequenceEqual(sound.Audio);
        }
        var reusable = sounds.Where(Reusable).Select(c => c.Id).ToHashSet();
        if (settings.Soundboard.Clips.Count + sounds.Count(c => !reusable.Contains(c.Id)) > 48) throw new InvalidDataException("Limite de 48 sons atteinte. Retire les sons inutilisés avant d’importer.");
        string folder = Path.Combine(soundsRoot, Guid.NewGuid().ToString("N"));
        var clips = new List<SoundboardClip>(); var map = new Dictionary<Guid, Guid>();
        try
        {
            foreach (var sound in sounds)
            {
                if (reusable.Contains(sound.Id)) { map.Add(sound.Id, sound.Id); continue; }
                Guid id = Guid.NewGuid(); map.Add(sound.Id, id);
                string path = Path.Combine(folder, id.ToString("N") + sound.Extension);
                if (sound.Audio != null) { Directory.CreateDirectory(folder); File.WriteAllBytes(path, sound.Audio); }
                clips.Add(new() { Id = id, Name = sound.Name, FilePath = path });
            }
            foreach (var action in Actions(imported))
                if (action.Kind == ActionKind.Soundboard && Guid.TryParse(action.Target, out var id) && map.TryGetValue(id, out var next)) action.Target = next.ToString();
        }
        catch { if (Directory.Exists(folder)) Directory.Delete(folder, true); throw; }
        if (replace) settings.Profiles = imported;
        else
        {
            var names = settings.Profiles.Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var profile in imported)
            {
                string original = profile.Name;
                for (int i = 1; !names.Add(profile.Name); i++)
                { string suffix = $" (import {i})"; profile.Name = original[..Math.Min(original.Length, 60 - suffix.Length)] + suffix; }
            }
            settings.Profiles.AddRange(imported);
        }
        settings.Soundboard.Clips.AddRange(clips); settings.ActiveProfileId = imported[0].Id;
        return imported;
    }
}
