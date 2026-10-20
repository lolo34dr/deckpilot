using System.Globalization;

namespace DeckPilot.Core;

public sealed record InstalledApplication(string Name, string Target, string Source)
{
    public bool Matches(string query) => CultureInfo.CurrentCulture.CompareInfo.IndexOf(
        Name + " " + Source, query.Trim(), CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
}
public static class ApplicationTarget
{
    private const string Prefix = "appsfolder:";
    public static bool IsPackaged(string target) => target.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase);
    public static string ForAppId(string appId)
    {
        if (string.IsNullOrWhiteSpace(appId) || appId.Length > 1024 || appId.Any(c => char.IsControl(c) || c is '"' or '\\' or '/'))
            throw new InvalidDataException("Identifiant d’application Windows invalide.");
        return Prefix + appId;
    }
    public static string GetAppId(string target)
    {
        if (!IsPackaged(target)) throw new InvalidDataException("Ce n’est pas une application Windows.");
        string appId = target[Prefix.Length..];
        ForAppId(appId);
        return appId;
    }
    public static IReadOnlyList<InstalledApplication> Merge(IEnumerable<InstalledApplication> applications) => applications
        .Where(a => !string.IsNullOrWhiteSpace(a.Name) && !string.IsNullOrWhiteSpace(a.Target))
        // Start menu entries are supplied first and take priority over AppsFolder duplicates.
        .DistinctBy(a => a.Name.Trim(), StringComparer.OrdinalIgnoreCase)
        .OrderBy(a => a.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
}
