using System.Text.Json;
using System.Text.RegularExpressions;

namespace DeckPilot.Core;

public static class ReleaseChannels
{
    public static string? LatestStableReleaseJson(string json, string tagPrefix)
    {
        if (string.IsNullOrWhiteSpace(tagPrefix) || !Regex.IsMatch(tagPrefix, @"^[a-zA-Z0-9-]+$", RegexOptions.CultureInvariant))
            throw new ArgumentException("Invalid GitHub release tag prefix.", nameof(tagPrefix));
        using var document = JsonDocument.Parse(json);
        if (document.RootElement.ValueKind != JsonValueKind.Array) throw new InvalidDataException("La liste des releases GitHub est invalide.");
        var candidates = new List<(JsonElement Release, Version Version)>();
        string pattern = "^" + Regex.Escape(tagPrefix) + @"([0-9]+\.[0-9]+\.[0-9]+)$";
        foreach (var release in document.RootElement.EnumerateArray())
        {
            if (release.GetProperty("draft").GetBoolean() || release.GetProperty("prerelease").GetBoolean()) continue;
            string tag = release.GetProperty("tag_name").GetString() ?? "";
            var match = Regex.Match(tag, pattern, RegexOptions.CultureInvariant);
            if (match.Success && Version.TryParse(match.Groups[1].Value, out var version)) candidates.Add((release, version));
        }
        var latest = candidates.OrderByDescending(candidate => candidate.Version).FirstOrDefault();
        return latest.Release.ValueKind == JsonValueKind.Undefined ? null : latest.Release.GetRawText();
    }
}
