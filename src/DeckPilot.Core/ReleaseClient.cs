using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace DeckPilot.Core;

public sealed class ReleaseClient : IDisposable
{
    private readonly HttpClient http;
    public ReleaseClient(HttpMessageHandler? handler = null)
    {
        http = new HttpClient(handler ?? new HttpClientHandler { AllowAutoRedirect = false }) { Timeout = TimeSpan.FromSeconds(30) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd("DeckPilot/1.0 (+https://github.com/lolo34dr/deckpilot)");
    }
    public async Task<UpdateRelease?> CheckAsync(Version current, CancellationToken cancellation = default)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        timeout.CancelAfter(TimeSpan.FromSeconds(30));
        cancellation = timeout.Token;
        using var response = await RequestAsync(new Uri($"https://api.github.com/repos/{UpdateRelease.Repository}/releases/latest"), cancellation);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            // The transition release works before the repository is renamed as well as afterward.
            using var legacy = await RequestAsync(new Uri($"https://api.github.com/repos/{UpdateRelease.LegacyRepository}/releases/latest"), cancellation);
            if (legacy.StatusCode == HttpStatusCode.NotFound) return null;
            legacy.EnsureSuccessStatusCode();
            return UpdateRelease.Parse(await ReadTextAsync(legacy, 1024 * 1024, cancellation), current);
        }
        response.EnsureSuccessStatusCode();
        return UpdateRelease.Parse(await ReadTextAsync(response, 1024 * 1024, cancellation), current);
    }
    public async Task<string> DownloadAsync(UpdateRelease release, string directory, IProgress<double>? progress, CancellationToken cancellation, string assetName = "DeckPilot.exe")
    {
        if (assetName is not ("DeckPilot.exe" or "DeckPilot-Setup.exe")) throw new InvalidDataException("Fichier de mise à jour non autorisé.");
        Directory.CreateDirectory(directory);
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        timeout.CancelAfter(TimeSpan.FromMinutes(10));
        var token = timeout.Token;
        using var manifestResponse = await RequestAsync(release.Checksums, token);
        manifestResponse.EnsureSuccessStatusCode();
        string expected = UpdateRelease.ExecutableHash(await ReadTextAsync(manifestResponse, 64 * 1024, token), assetName);
        string path = Path.Combine(directory, assetName);
        bool created = false;
        try
        {
            using var response = await RequestAsync(release.Executable, token);
            response.EnsureSuccessStatusCode();
            if (response.Content.Headers.ContentLength is long length && length != release.Size)
                throw new InvalidDataException("La taille annoncée du téléchargement ne correspond pas à la release.");
            await using (var source = await response.Content.ReadAsStreamAsync(token))
            await using (var target = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, useAsync: true))
            {
                created = true;
                byte[] buffer = new byte[81920]; long received = 0; int count;
                while ((count = await source.ReadAsync(buffer, token)) != 0)
                {
                    received += count;
                    if (received > release.Size) throw new InvalidDataException("Téléchargement trop volumineux.");
                    await target.WriteAsync(buffer.AsMemory(0, count), token);
                    progress?.Report((double)received / release.Size);
                }
                if (received != release.Size) throw new InvalidDataException("Le téléchargement est incomplet.");
            }
            await UpdateRelease.VerifyFileAsync(path, expected, token);
            await using var file = File.OpenRead(path);
            if (file.ReadByte() != 'M' || file.ReadByte() != 'Z') throw new InvalidDataException("Le fichier reçu n’est pas un exécutable Windows.");
            return expected;
        }
        catch { if (created && File.Exists(path)) File.Delete(path); throw; }
    }
    private async Task<HttpResponseMessage> RequestAsync(Uri uri, CancellationToken cancellation)
    {
        for (int redirects = 0; redirects <= 5; redirects++)
        {
            if (uri.Scheme != "https" || !uri.IsDefaultPort || uri.UserInfo.Length != 0 ||
                uri.Host is not ("api.github.com" or "github.com" or "release-assets.githubusercontent.com" or "objects.githubusercontent.com" or "github-releases.githubusercontent.com"))
                throw new InvalidDataException("Destination de mise à jour non autorisée.");
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(uri.Host == "api.github.com" ? "application/vnd.github+json" : "application/octet-stream"));
            var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellation);
            if (response.StatusCode is not (HttpStatusCode.MovedPermanently or HttpStatusCode.Redirect or HttpStatusCode.SeeOther or HttpStatusCode.TemporaryRedirect or HttpStatusCode.PermanentRedirect)) return response;
            Uri? location = response.Headers.Location;
            response.Dispose();
            if (location == null) throw new InvalidDataException("Redirection GitHub incomplète.");
            uri = new Uri(uri, location);
        }
        throw new InvalidDataException("Trop de redirections pendant la mise à jour.");
    }
    private static async Task<string> ReadTextAsync(HttpResponseMessage response, int maximum, CancellationToken cancellation)
    {
        await using var stream = await response.Content.ReadAsStreamAsync(cancellation);
        using var output = new MemoryStream();
        byte[] buffer = new byte[8192]; int count;
        while ((count = await stream.ReadAsync(buffer, cancellation)) != 0)
        {
            if (output.Length + count > maximum) throw new InvalidDataException("Réponse de mise à jour trop volumineuse.");
            output.Write(buffer, 0, count);
        }
        return Encoding.UTF8.GetString(output.ToArray());
    }
    public void Dispose() => http.Dispose();
}
