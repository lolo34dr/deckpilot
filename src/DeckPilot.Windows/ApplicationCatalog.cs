using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public sealed record ApplicationCatalogResult(IReadOnlyList<InstalledApplication> Applications, string? Warning);
public static class ApplicationCatalog
{
    public static async Task<ApplicationCatalogResult> FindAsync(CancellationToken cancellation)
    {
        var applications = await Task.Run(() =>
        {
            var result = new List<InstalledApplication>();
            foreach (var location in new[] { Environment.SpecialFolder.StartMenu, Environment.SpecialFolder.CommonStartMenu })
                Scan(Environment.GetFolderPath(location), result, cancellation);
            return result;
        }, cancellation);
        string? warning = null;
        try
        {
            string powershell = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"WindowsPowerShell\v1.0\powershell.exe");
            var start = new ProcessStartInfo(powershell) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true, StandardOutputEncoding = Encoding.UTF8 };
            foreach (string argument in new[] { "-NoLogo", "-NoProfile", "-NonInteractive", "-Command", "[Console]::OutputEncoding = [System.Text.UTF8Encoding]::new($false); @(Get-StartApps -ErrorAction Stop | Select-Object Name,AppID) | ConvertTo-Json -Compress" }) start.ArgumentList.Add(argument);
            using var process = Process.Start(start) ?? throw new IOException("Impossible de lire les applications Windows.");
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
            timeout.CancelAfter(TimeSpan.FromSeconds(20));
            try
            {
                Task<string> output = process.StandardOutput.ReadToEndAsync(timeout.Token);
                Task<string> errors = process.StandardError.ReadToEndAsync(timeout.Token);
                await process.WaitForExitAsync(timeout.Token);
                string json = await output;
                await errors;
                if (process.ExitCode != 0) throw new IOException("Le catalogue Windows n’est pas disponible.");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    using var document = JsonDocument.Parse(json);
                    var items = document.RootElement.ValueKind == JsonValueKind.Array
                        ? document.RootElement.EnumerateArray() : new[] { document.RootElement }.AsEnumerable();
                    foreach (var item in items)
                    {
                        cancellation.ThrowIfCancellationRequested();
                        string? name = item.GetProperty("Name").GetString();
                        string? id = item.GetProperty("AppID").GetString();
                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id)) continue;
                        try { applications.Add(new(name, ApplicationTarget.ForAppId(id), "Applications Windows")); }
                        catch (InvalidDataException) { /* Unsupported shell entries can still be chosen by file. */ }
                    }
                }
            }
            finally { if (!process.HasExited) process.Kill(entireProcessTree: true); }
        }
        catch (OperationCanceledException) when (!cancellation.IsCancellationRequested) { warning = "Le catalogue Windows met trop de temps à répondre. Les raccourcis restent disponibles."; }
        catch (Exception ex) when (ex is IOException or System.ComponentModel.Win32Exception or JsonException)
        { warning = "Certaines applications Windows n’ont pas pu être listées. Le choix manuel reste disponible."; }
        cancellation.ThrowIfCancellationRequested();
        return new(ApplicationTarget.Merge(applications), warning);
    }
    private static void Scan(string directory, List<InstalledApplication> result, CancellationToken cancellation)
    {
        if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory)) return;
        try
        {
            foreach (string file in Directory.EnumerateFiles(directory))
            {
                cancellation.ThrowIfCancellationRequested();
                if (Path.GetExtension(file).ToLowerInvariant() is ".lnk" or ".appref-ms")
                    result.Add(new(Path.GetFileNameWithoutExtension(file), file, "Menu Démarrer"));
            }
            foreach (string child in Directory.EnumerateDirectories(directory))
                if ((File.GetAttributes(child) & FileAttributes.ReparsePoint) == 0) Scan(child, result, cancellation);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException) { /* Another readable directory may contain the application. */ }
    }
}
