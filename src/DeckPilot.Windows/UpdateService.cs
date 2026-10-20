using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public sealed record StagedUpdate(string Directory, string Hash, UpdateRelease Release);
public sealed class UpdateService : IDisposable
{
    private readonly ReleaseClient client = new();
    public static readonly string UpdatesRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilot", "Updates");
    public static Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version!;
    public static string DisplayVersion => CurrentVersion.ToString(3);
    public Task<UpdateRelease?> CheckAsync(CancellationToken cancellation) => client.CheckAsync(CurrentVersion, cancellation);
    public async Task<StagedUpdate> StageAsync(UpdateRelease release, IProgress<double> progress, CancellationToken cancellation)
    {
        string executable = ExecutablePath();
        ProbeWritable(Path.GetDirectoryName(executable)!);
        string directory = Path.Combine(UpdatesRoot, Guid.NewGuid().ToString("N"));
        string hash = await client.DownloadAsync(release, directory, progress, cancellation);
        return new(directory, hash, release);
    }
    public async Task StartInstallerAsync(StagedUpdate staged, bool background)
    {
        using var parent = Process.GetCurrentProcess();
        string target = ExecutablePath();
        string planPath = Path.Combine(staged.Directory, "update.json");
        var plan = new UpdatePlan { ParentProcessId = parent.Id, ParentStartTicks = parent.StartTime.ToUniversalTime().Ticks, TargetPath = target,
            OriginalHash = await UpdateRelease.HashFileAsync(target), DownloadHash = staged.Hash, NewVersion = staged.Release.Version.ToString(), Background = background };
        await File.WriteAllTextAsync(planPath, JsonSerializer.Serialize(plan));
        var start = new ProcessStartInfo(Path.Combine(staged.Directory, "DeckPilot.exe")) { UseShellExecute = false, WorkingDirectory = staged.Directory };
        start.ArgumentList.Add("--apply-update"); start.ArgumentList.Add(planPath);
        using var worker = Process.Start(start) ?? throw new IOException("Impossible de démarrer la mise à jour.");
        string ready = Path.Combine(staged.Directory, "ready");
        for (int attempt = 0; attempt < 300; attempt++)
        {
            if (File.Exists(ready)) return;
            if (worker.HasExited)
            {
                string error = Path.Combine(staged.Directory, "error.txt");
                throw new IOException(File.Exists(error) ? await File.ReadAllTextAsync(error) : "La préparation de la mise à jour a échoué.");
            }
            await Task.Delay(100);
        }
        // Only terminate the helper this instance just started; the app stays open.
        if (!worker.HasExited) worker.Kill(entireProcessTree: true);
        throw new IOException("La préparation de la mise à jour a pris trop de temps. DeckPilot reste ouvert.");
    }
    public static string ExecutablePath()
    {
        string path = Environment.ProcessPath ?? "";
        if (!Path.GetExtension(path).Equals(".exe", StringComparison.OrdinalIgnoreCase) || Path.GetFileNameWithoutExtension(path).Equals("dotnet", StringComparison.OrdinalIgnoreCase))
            throw new IOException("La mise à jour automatique est disponible depuis la version publiée DeckPilot.exe.");
        return Path.GetFullPath(path);
    }
    public static void ProbeWritable(string directory)
    {
        string probe = Path.Combine(directory, ".deckpilot-write-" + Guid.NewGuid().ToString("N"));
        try { using (File.Create(probe)) { } }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        { throw new IOException("Ce dossier ne peut pas être modifié. Placez DeckPilot dans un dossier personnel pour activer les mises à jour.", ex); }
        finally { if (File.Exists(probe)) File.Delete(probe); }
    }
    public static bool IsStageDirectory(string directory)
    {
        string full = Path.GetFullPath(directory);
        return string.Equals(Path.GetDirectoryName(full), Path.GetFullPath(UpdatesRoot), StringComparison.OrdinalIgnoreCase)
            && Guid.TryParseExact(Path.GetFileName(full), "N", out _);
    }
    public static void WriteHealth(string path)
    {
        if (!IsStageDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!) || Path.GetFileName(path) != "healthy.json") return;
        File.WriteAllText(path, JsonSerializer.Serialize(new { ProcessId = Environment.ProcessId, Version = CurrentVersion.ToString(3) }));
    }
    public static void CleanOldDownloads()
    {
        try
        {
            if (!Directory.Exists(UpdatesRoot)) return;
            foreach (string directory in Directory.EnumerateDirectories(UpdatesRoot))
                if (IsStageDirectory(directory) && Directory.GetLastWriteTimeUtc(directory) < DateTime.UtcNow.AddDays(-7) && (File.GetAttributes(directory) & FileAttributes.ReparsePoint) == 0)
                    try { Directory.Delete(directory, recursive: true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
    }
    public void Dispose() => client.Dispose();
}
