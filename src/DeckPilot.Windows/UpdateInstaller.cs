using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public static class UpdateInstaller
{
    public static async Task<int> ApplyAsync(string planPath)
    {
        string? directory = null;
        UpdateTransaction? transaction = null;
        UpdatePlan? plan = null;
        Process? replacement = null;
        bool parentExited = false;
        try
        {
            string source = Path.GetFullPath(Environment.ProcessPath!);
            directory = Path.GetDirectoryName(source)!;
            if (!UpdateService.IsStageDirectory(directory) || !Path.GetFileName(source).Equals("DeckPilot.exe", StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(Path.GetFullPath(planPath), Path.Combine(directory, "update.json"), StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Dossier de mise à jour invalide.");
            plan = JsonSerializer.Deserialize<UpdatePlan>(await File.ReadAllTextAsync(planPath)) ?? throw new InvalidDataException("Plan de mise à jour vide.");
            plan.TargetPath = Path.GetFullPath(plan.TargetPath);
            if (!Path.GetExtension(plan.TargetPath).Equals(".exe", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(source, plan.TargetPath, StringComparison.OrdinalIgnoreCase) ||
                plan.NewVersion != UpdateService.CurrentVersion.ToString(3) || plan.ParentProcessId == Environment.ProcessId)
                throw new InvalidDataException("La mise à jour ne correspond pas à l’application attendue.");
            await UpdateRelease.VerifyFileAsync(source, plan.DownloadHash);
            await UpdateRelease.VerifyFileAsync(plan.TargetPath, plan.OriginalHash);
            UpdateService.ProbeWritable(Path.GetDirectoryName(plan.TargetPath)!);
            using var parent = Process.GetProcessById(plan.ParentProcessId);
            if (parent.StartTime.ToUniversalTime().Ticks != plan.ParentStartTicks)
                throw new InvalidDataException("Le processus d’origine a changé depuis la préparation de la mise à jour.");
            if (!FileIdentity.SameFile(parent.MainModule?.FileName ?? "", plan.TargetPath))
                throw new InvalidDataException("Le processus d’origine n’utilise plus le fichier attendu.");
            await File.WriteAllTextAsync(Path.Combine(directory, "ready"), "ready");
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(90));
            await parent.WaitForExitAsync(timeout.Token);
            parentExited = true;
            await UpdateRelease.VerifyFileAsync(plan.TargetPath, plan.OriginalHash);
            string backup = plan.TargetPath + ".previous-" + Path.GetFileName(directory);
            transaction = new(plan.TargetPath, source, backup);
            await RetryAsync(transaction.Replace);
            replacement = Launch(plan, Path.Combine(directory, "healthy.json"));
            string healthPath = Path.Combine(directory, "healthy.json");
            bool healthy = false;
            for (int attempt = 0; attempt < 300; attempt++)
            {
                if (File.Exists(healthPath))
                {
                    try
                    {
                        using var document = JsonDocument.Parse(await File.ReadAllTextAsync(healthPath));
                        healthy = document.RootElement.GetProperty("ProcessId").GetInt32() == replacement.Id && document.RootElement.GetProperty("Version").GetString() == plan.NewVersion;
                    }
                    catch (Exception ex) when (ex is IOException or JsonException) { }
                    if (healthy) break;
                }
                if (replacement.HasExited) break;
                await Task.Delay(100);
            }
            if (!healthy) throw new IOException("La nouvelle version n’a pas confirmé son démarrage.");
            // Backup cleanup is best effort: a working installation must not roll back for a cleanup failure.
            try { transaction.Commit(); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            try { await File.WriteAllTextAsync(Path.Combine(directory, "complete"), "ok"); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            return 0;
        }
        catch (Exception ex)
        {
            if (directory != null && UpdateService.IsStageDirectory(directory))
                try { await File.WriteAllTextAsync(Path.Combine(directory, "error.txt"), ex.Message); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            if (parentExited && plan != null)
            {
                try
                {
                    if (replacement != null && !replacement.HasExited) { replacement.Kill(entireProcessTree: true); await replacement.WaitForExitAsync(); }
                    if (transaction?.Replaced == true) await RetryAsync(transaction.Rollback);
                    Launch(plan, null, failed: true).Dispose();
                }
                catch (Exception rollbackError)
                {
                    if (directory != null) try { await File.AppendAllTextAsync(Path.Combine(directory, "error.txt"), "\nRestauration : " + rollbackError.Message); } catch { }
                }
            }
            return 1;
        }
        finally { replacement?.Dispose(); }
    }
    private static Process Launch(UpdatePlan plan, string? health, bool failed = false)
    {
        var start = new ProcessStartInfo(plan.TargetPath) { UseShellExecute = false, WorkingDirectory = Path.GetDirectoryName(plan.TargetPath)! };
        if (plan.Background) start.ArgumentList.Add("--background");
        if (failed) start.ArgumentList.Add("--update-failed");
        if (health != null) { start.ArgumentList.Add("--update-health"); start.ArgumentList.Add(health); }
        return Process.Start(start) ?? throw new IOException("Impossible de relancer DeckPilot.");
    }
    private static async Task RetryAsync(Action action)
    {
        for (int attempt = 0; ; attempt++)
        {
            try { action(); return; }
            catch (IOException) when (attempt < 19) { await Task.Delay(250); }
        }
    }
}
