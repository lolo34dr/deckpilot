using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
using DeckPilot.Core;

namespace DeckPilot.Factory;

public sealed record FactoryUpdate(Version AppVersion, Version FirmwareVersion, Uri Installer, Uri Checksums, long Size)
{
    public string Description(Version currentApp, Version currentFirmware)
    {
        var changes = new List<string>();
        if (AppVersion > currentApp) changes.Add($"DeckPilot Atelier {currentApp} → {AppVersion}");
        if (FirmwareVersion > currentFirmware) changes.Add($"firmware {currentFirmware} → {FirmwareVersion}");
        return string.Join("\n", changes);
    }
}

public sealed record StagedFactoryUpdate(string Folder, string Hash);

public sealed class FactoryUpdateService : IDisposable
{
    private const string Repository = "lolo34dr/deckpilot";
    private const string InstallerName = "DeckPilot-Atelier-Setup.exe";
    private const string ChecksumsName = "SHA256SUMS.txt";
    private const string MetadataName = "DeckPilot-Atelier-version.json";
    public static readonly Version CurrentVersion = new(Assembly.GetExecutingAssembly().GetName().Version!.ToString(3));
    private readonly HttpClient http = new(new HttpClientHandler { AllowAutoRedirect = false }) { Timeout = TimeSpan.FromSeconds(30) };

    public FactoryUpdateService() => http.DefaultRequestHeaders.UserAgent.ParseAdd("DeckPilot-Atelier/1.0 (+https://github.com/lolo34dr/deckpilot)");

    public async Task<FactoryUpdate?> CheckAsync(Version currentFirmware, CancellationToken cancellation)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation); timeout.CancelAfter(TimeSpan.FromSeconds(30));
        using var response = await GetAsync(new Uri($"https://api.github.com/repos/{Repository}/releases?per_page=100"), timeout.Token);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        string body = await ReadTextAsync(response, 4 * 1024 * 1024, timeout.Token);
        string? latestJson = ReleaseChannels.LatestStableReleaseJson(body, "atelier-v");
        if (latestJson == null) return null;
        using var document = JsonDocument.Parse(latestJson); var root = document.RootElement;
        string tag = root.GetProperty("tag_name").GetString() ?? "";
        if (!Version.TryParse(tag[9..], out var taggedVersion)) throw new InvalidDataException("Version Atelier tag invalide.");
        var assets = root.GetProperty("assets").EnumerateArray().ToArray();
        Uri Asset(string name)
        {
            var matches = assets.Where(a => a.GetProperty("name").GetString() == name).ToArray();
            if (matches.Length != 1) throw new InvalidDataException($"La version GitHub ne contient pas un seul fichier {name}.");
            var uri = new Uri(matches[0].GetProperty("browser_download_url").GetString()!);
            if (!ExpectedAsset(uri, tag, name)) throw new InvalidDataException("Le fichier de mise à jour ne provient pas du dépôt GitHub attendu.");
            return uri;
        }
        Uri installer = Asset(InstallerName), sums = Asset(ChecksumsName), metadata = Asset(MetadataName);
        long size = assets.Single(a => a.GetProperty("name").GetString() == InstallerName).GetProperty("size").GetInt64();
        if (size < 1024 || size > 512L * 1024 * 1024) throw new InvalidDataException("Taille de l’installeur Atelier invalide.");
        using var metadataResponse = await GetAsync(metadata, timeout.Token); metadataResponse.EnsureSuccessStatusCode();
        using var metadataDoc = JsonDocument.Parse((await ReadTextAsync(metadataResponse, 4096, timeout.Token)).TrimStart('\uFEFF'));
        if (!Version.TryParse(metadataDoc.RootElement.GetProperty("factoryVersion").GetString(), out var appVersion) ||
            !Version.TryParse(metadataDoc.RootElement.GetProperty("firmwareVersion").GetString(), out var firmwareVersion) ||
            appVersion.Build < 0 || firmwareVersion.Build < 0 || appVersion != taggedVersion) throw new InvalidDataException("Les versions Atelier ou firmware sont invalides ou ne correspondent pas au tag.");
        if (appVersion <= CurrentVersion && firmwareVersion <= currentFirmware) return null;
        if (appVersion < CurrentVersion || firmwareVersion < currentFirmware) throw new InvalidDataException("La version GitHub est plus ancienne que celle installée. Aucune mise à jour ne sera lancée.");
        return new(appVersion, firmwareVersion, installer, sums, size);
    }

    public async Task<StagedFactoryUpdate> DownloadAsync(FactoryUpdate update, IProgress<double>? progress, CancellationToken cancellation)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation); timeout.CancelAfter(TimeSpan.FromMinutes(10));
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilotFactory", "Updates", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder); string path = Path.Combine(folder, InstallerName); bool created = false;
        try
        {
            using var sumsResponse = await GetAsync(update.Checksums, timeout.Token); sumsResponse.EnsureSuccessStatusCode();
            string sums = await ReadTextAsync(sumsResponse, 64 * 1024, timeout.Token);
            var matches = sums.Split('\n').Select(l => Regex.Match(l.Trim(), @"^([a-fA-F0-9]{64})[ \t]+\*?" + Regex.Escape(InstallerName) + "$")).Where(m => m.Success).ToArray();
            if (matches.Length != 1) throw new InvalidDataException("L’empreinte de l’installeur Atelier est absente ou ambiguë.");
            string hash = matches[0].Groups[1].Value.ToLowerInvariant();
            using var response = await GetAsync(update.Installer, timeout.Token); response.EnsureSuccessStatusCode();
            if (response.Content.Headers.ContentLength is long length && length != update.Size) throw new InvalidDataException("La taille annoncée de l’installeur est incorrecte.");
            await using (var source = await response.Content.ReadAsStreamAsync(timeout.Token))
            await using (var target = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true))
            {
                created = true; byte[] buffer = new byte[81920]; long received = 0; int count;
                while ((count = await source.ReadAsync(buffer, timeout.Token)) != 0)
                {
                    received += count; if (received > update.Size) throw new InvalidDataException("Installeur téléchargé trop volumineux.");
                    await target.WriteAsync(buffer.AsMemory(0, count), timeout.Token); progress?.Report((double)received / update.Size);
                }
                if (received != update.Size) throw new InvalidDataException("Le téléchargement de l’installeur est incomplet.");
            }
            await VerifyAsync(path, hash, timeout.Token);
            await using var file = File.OpenRead(path); if (file.ReadByte() != 'M' || file.ReadByte() != 'Z') throw new InvalidDataException("Le téléchargement n’est pas un installeur Windows valide.");
            return new(folder, hash);
        }
        catch { if (created && File.Exists(path)) File.Delete(path); try { Directory.Delete(folder, true); } catch { } throw; }
    }

    public static Process LaunchInstaller(StagedFactoryUpdate staged)
    {
        string path = Path.GetFullPath(Path.Combine(staged.Folder, InstallerName));
        var start = new ProcessStartInfo(path) { UseShellExecute = true, WorkingDirectory = staged.Folder };
        start.ArgumentList.Add("/UPDATE=1"); start.ArgumentList.Add("/SILENT"); start.ArgumentList.Add("/NORESTART");
        start.ArgumentList.Add("/DIR=" + Path.GetDirectoryName(FactoryUpdateServiceExecutablePath())!);
        return Process.Start(start) ?? throw new IOException("Windows n’a pas pu lancer l’installeur Atelier.");
    }

    private static string FactoryUpdateServiceExecutablePath()
    {
        string path = Environment.ProcessPath ?? "";
        if (!Path.GetExtension(path).Equals(".exe", StringComparison.OrdinalIgnoreCase) || Path.GetFileNameWithoutExtension(path).Equals("dotnet", StringComparison.OrdinalIgnoreCase))
            throw new IOException("La mise à jour automatique nécessite la version installée de DeckPilot Atelier.");
        return Path.GetFullPath(path);
    }

    private static bool ExpectedAsset(Uri uri, string tag, string name) => uri.Scheme == "https" && uri.Host == "github.com" && uri.IsDefaultPort && uri.UserInfo.Length == 0 && uri.Query.Length == 0 && uri.AbsolutePath == $"/{Repository}/releases/download/{tag}/{name}";

    private async Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken cancellation)
    {
        for (int redirects = 0; redirects <= 5; redirects++)
        {
            if (uri.Scheme != "https" || !uri.IsDefaultPort || uri.UserInfo.Length != 0 || uri.Host is not ("api.github.com" or "github.com" or "release-assets.githubusercontent.com" or "objects.githubusercontent.com" or "github-releases.githubusercontent.com"))
                throw new InvalidDataException("Destination réseau non autorisée pour la mise à jour.");
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(uri.Host == "api.github.com" ? "application/vnd.github+json" : "application/octet-stream"));
            var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellation);
            if (response.StatusCode is not (HttpStatusCode.MovedPermanently or HttpStatusCode.Redirect or HttpStatusCode.SeeOther or HttpStatusCode.TemporaryRedirect or HttpStatusCode.PermanentRedirect)) return response;
            Uri? location = response.Headers.Location; response.Dispose();
            if (location == null) throw new InvalidDataException("Redirection GitHub incomplète.");
            uri = new Uri(uri, location);
        }
        throw new InvalidDataException("Trop de redirections pendant la mise à jour.");
    }

    private static async Task<string> ReadTextAsync(HttpResponseMessage response, int maximum, CancellationToken cancellation)
    {
        await using var stream = await response.Content.ReadAsStreamAsync(cancellation); using var output = new MemoryStream(); byte[] buffer = new byte[8192]; int count;
        while ((count = await stream.ReadAsync(buffer, cancellation)) != 0) { if (output.Length + count > maximum) throw new InvalidDataException("Réponse GitHub trop volumineuse."); output.Write(buffer, 0, count); }
        return System.Text.Encoding.UTF8.GetString(output.ToArray());
    }

    private static async Task VerifyAsync(string path, string expected, CancellationToken cancellation)
    {
        byte[] wanted; try { wanted = Convert.FromHexString(expected); } catch (FormatException) { throw new InvalidDataException("Empreinte SHA-256 invalide."); }
        if (wanted.Length != 32) throw new InvalidDataException("Empreinte SHA-256 invalide.");
        await using var stream = File.OpenRead(path); byte[] actual = await SHA256.HashDataAsync(stream, cancellation);
        if (!CryptographicOperations.FixedTimeEquals(wanted, actual)) throw new InvalidDataException("L’empreinte de l’installeur est incorrecte. Aucun fichier installé n’a été modifié.");
    }

    public void Dispose() => http.Dispose();
}
