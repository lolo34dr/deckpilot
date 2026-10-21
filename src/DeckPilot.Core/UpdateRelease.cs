using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DeckPilot.Core;

public sealed record UpdateRelease(string Tag, Version Version, Uri Executable, Uri Checksums, long Size)
{
    public const string Repository = "lolo34dr/deckpilot";
    public const string LegacyRepository = "lolo34dr/streamdeck-diy";
    public static UpdateRelease? Parse(string json, Version current)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        if (root.GetProperty("draft").GetBoolean() || root.GetProperty("prerelease").GetBoolean()) return null;
        string tag = root.GetProperty("tag_name").GetString() ?? "";
        if (!Regex.IsMatch(tag, @"^v[0-9]+\.[0-9]+\.[0-9]+$", RegexOptions.CultureInvariant) ||
            !Version.TryParse(tag[1..], out var version) || version <= new Version(current.Major, current.Minor, Math.Max(current.Build, 0))) return null;
        var assets = root.GetProperty("assets").EnumerateArray().ToArray();
        Uri Asset(string name)
        {
            var matches = assets.Where(a => a.GetProperty("name").GetString() == name).ToArray();
            if (matches.Length != 1) throw new InvalidDataException($"Fichier de mise à jour absent ou ambigu : {name}");
            var url = new Uri(matches[0].GetProperty("browser_download_url").GetString()!);
            string path = $"/{Repository}/releases/download/{tag}/{name}";
            string legacyPath = $"/{LegacyRepository}/releases/download/{tag}/{name}";
            if (url.Scheme != "https" || url.Host != "github.com" || (url.AbsolutePath != path && url.AbsolutePath != legacyPath) || url.Query.Length != 0 || url.UserInfo.Length != 0 || !url.IsDefaultPort)
                throw new InvalidDataException("La mise à jour ne provient pas du dépôt GitHub attendu.");
            return url;
        }
        var executable = Asset("DeckPilot.exe");
        var checksums = Asset("SHA256SUMS.txt");
        long size = assets.Single(a => a.GetProperty("name").GetString() == "DeckPilot.exe").GetProperty("size").GetInt64();
        if (size < 1024 || size > 512L * 1024 * 1024) throw new InvalidDataException("Taille de mise à jour invalide.");
        return new(tag, version, executable, checksums, size);
    }
    public static string ExecutableHash(string manifest)
    {
        var matches = manifest.Split('\n').Select(line => Regex.Match(line.Trim(), @"^([a-fA-F0-9]{64})[ \t]+\*?DeckPilot\.exe$"))
            .Where(m => m.Success).ToArray();
        if (matches.Length != 1) throw new InvalidDataException("L’empreinte de DeckPilot.exe est absente ou ambiguë.");
        return matches[0].Groups[1].Value.ToLowerInvariant();
    }
    public static async Task VerifyFileAsync(string path, string expected, CancellationToken cancellation = default)
    {
        byte[] hash;
        try { hash = Convert.FromHexString(expected); }
        catch (FormatException) { throw new InvalidDataException("Empreinte SHA-256 invalide."); }
        if (hash.Length != 32) throw new InvalidDataException("Empreinte SHA-256 invalide.");
        await using var input = File.OpenRead(path);
        var actual = await SHA256.HashDataAsync(input, cancellation);
        if (!CryptographicOperations.FixedTimeEquals(actual, hash))
            throw new InvalidDataException("La vérification du téléchargement a échoué. Aucun fichier n’a été remplacé.");
    }
    public static async Task<string> HashFileAsync(string path)
    {
        await using var input = File.OpenRead(path);
        return Convert.ToHexString(await SHA256.HashDataAsync(input)).ToLowerInvariant();
    }
}

public sealed class UpdatePlan
{
    public int ParentProcessId { get; set; }
    public long ParentStartTicks { get; set; }
    public string TargetPath { get; set; } = "";
    public string OriginalHash { get; set; } = "";
    public string DownloadHash { get; set; } = "";
    public string NewVersion { get; set; } = "";
    public bool Background { get; set; }
}

// The backup is kept until the new process reports successful initialization.
public sealed class UpdateTransaction(string target, string staged, string backup)
{
    public bool Replaced { get; private set; }
    public void Replace()
    {
        if (Replaced || File.Exists(backup)) throw new IOException("Une sauvegarde existe déjà pour cette mise à jour.");
        string incoming = backup + ".incoming-" + Guid.NewGuid().ToString("N");
        try
        {
            File.Copy(staged, incoming, overwrite: false);
            File.Replace(incoming, target, backup);
            Replaced = true;
        }
        finally { if (File.Exists(incoming)) File.Delete(incoming); }
    }
    public void Rollback()
    {
        if (!Replaced) return;
        File.Replace(backup, target, null);
        Replaced = false;
    }
    public void Commit()
    {
        if (!Replaced) throw new InvalidOperationException("Aucun remplacement à confirmer.");
        if (File.Exists(backup)) File.Delete(backup);
    }
}
