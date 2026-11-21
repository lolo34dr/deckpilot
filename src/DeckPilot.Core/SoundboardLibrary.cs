namespace DeckPilot.Core;

public static class SoundboardLibrary
{
    private static bool IsAudioExtension(string extension) =>
        extension.Equals(".mp3", StringComparison.OrdinalIgnoreCase) ||
        extension.Equals(".wav", StringComparison.OrdinalIgnoreCase);

    public static async Task<SoundboardClip> CopyAsync(string soundsRoot, string sourcePath, CancellationToken cancellation = default)
    {
        string extension = Path.GetExtension(sourcePath);
        if (!IsAudioExtension(extension)) throw new InvalidDataException("Choisis un fichier MP3 ou WAV.");

        string name = Path.GetFileNameWithoutExtension(sourcePath).Trim();
        var clip = new SoundboardClip { Name = name.Length == 0 ? "Son" : name[..Math.Min(64, name.Length)] };
        string folder = Path.Combine(soundsRoot, clip.Id.ToString("N"));
        clip.FilePath = Path.Combine(folder, clip.Id.ToString("N") + extension.ToLowerInvariant());
        bool created = false;
        try
        {
            const int bufferSize = 81920;
            await using var source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize,
                FileOptions.Asynchronous | FileOptions.SequentialScan);
            if (source.Length == 0) throw new InvalidDataException("Le fichier audio est vide.");
            cancellation.ThrowIfCancellationRequested();
            Directory.CreateDirectory(folder);
            await using var target = new FileStream(clip.FilePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize,
                FileOptions.Asynchronous);
            created = true;
            await source.CopyToAsync(target, cancellation).ConfigureAwait(false);
            return clip;
        }
        catch
        {
            if (created) TryDeleteManagedFile(soundsRoot, clip.FilePath);
            throw;
        }
    }

    public static void SaveClips(Settings settings, List<SoundboardClip> clips, Action<Settings> save) =>
        settings.CommitLibrary(settings.Profiles, settings.ActiveProfile.Id, clips, save);

    public static void TryDeleteManagedFile(string soundsRoot, string path)
    {
        try
        {
            string fullPath = Path.GetFullPath(path);
            string? folder = Path.GetDirectoryName(fullPath);
            if (folder == null || !string.Equals(Path.GetDirectoryName(folder), Path.TrimEndingDirectorySeparator(Path.GetFullPath(soundsRoot)),
                    OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) ||
                !Guid.TryParseExact(Path.GetFileName(folder), "N", out _) ||
                !Guid.TryParseExact(Path.GetFileNameWithoutExtension(fullPath), "N", out _) ||
                !IsAudioExtension(Path.GetExtension(fullPath)) ||
                (File.GetAttributes(folder) & FileAttributes.ReparsePoint) != 0 ||
                (File.GetAttributes(fullPath) & FileAttributes.ReparsePoint) != 0) return;

            File.Delete(fullPath);
            if (!Directory.EnumerateFileSystemEntries(folder).Any()) Directory.Delete(folder);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException) { }
    }
}
