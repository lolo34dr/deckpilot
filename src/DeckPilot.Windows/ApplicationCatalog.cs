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
public static class ApplicationCatalog
{
    private static readonly ApplicationCatalogCache Cache = new(DiscoverAsync);
    public static async Task<ApplicationCatalogResult> FindAsync(CancellationToken cancellation, bool forceRefresh = false)
    {
        using var wait = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        wait.CancelAfter(TimeSpan.FromSeconds(30));
        try { return await Cache.GetAsync(wait.Token, forceRefresh).ConfigureAwait(false); }
        catch (OperationCanceledException) when (!cancellation.IsCancellationRequested)
        { throw new TimeoutException("La recherche des applications a dépassé le délai prévu."); }
    }
    private static async Task<ApplicationCatalogResult> DiscoverAsync()
    {
        using var discovery = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        return await Task.Run(() => DiscoverAsync(discovery.Token), discovery.Token).ConfigureAwait(false);
    }
    private static async Task<ApplicationCatalogResult> DiscoverAsync(CancellationToken cancellation)
    {
        var applications = new List<InstalledApplication>();
        foreach (var location in new[] { Environment.SpecialFolder.StartMenu, Environment.SpecialFolder.CommonStartMenu })
            Scan(Environment.GetFolderPath(location), applications, cancellation);
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
                await process.WaitForExitAsync(timeout.Token).ConfigureAwait(false);
                string json = await output.ConfigureAwait(false);
                await errors.ConfigureAwait(false);
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
            finally
            {
                // The child can finish between HasExited and Kill.
                try { if (!process.HasExited) process.Kill(entireProcessTree: true); }
                catch (InvalidOperationException) { }
            }
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
