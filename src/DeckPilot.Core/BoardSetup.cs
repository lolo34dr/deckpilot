namespace DeckPilot.Core;

public sealed record BoardSetup(Profile Profile, BoardBinding Binding, bool AddProfile)
{
    public static bool IsShared(Settings settings, string deviceId, Guid profileId) =>
        settings.BoardBindings.Any(pair => !StringComparer.OrdinalIgnoreCase.Equals(pair.Key, deviceId)
            && pair.Value.ProfileId == profileId);

    public static BoardSetup Prepare(Settings settings, string deviceId, string port)
    {
        var binding = settings.BoardBindings.GetValueOrDefault(deviceId);
        var existing = settings.Profiles.FirstOrDefault(profile => profile.Id == binding?.ProfileId);
        if (existing != null && !IsShared(settings, deviceId, existing.Id))
            return new(existing, binding! with { Enabled = true }, false);

        // The first device can take over the current keyboard layout and actions.
        var active = settings.ActiveProfile;
        if (binding == null && settings.BoardBindings.Count == 0)
            return new(active, new(active.Id, [.. settings.KeyMap]), false);

        if (settings.Profiles.Count >= 100) throw new InvalidOperationException("La limite de 100 profils est atteinte. Libérez un profil ou attribuez un profil existant.");
        var independent = existing != null ? ProfileExchange.CopyAsNew([existing])[0] : new Profile();
        string baseName = $"Macropad {port}";
        independent.Name = baseName;
        for (int suffix = 2; settings.Profiles.Any(profile => string.Equals(profile.Name, independent.Name, StringComparison.OrdinalIgnoreCase)); suffix++)
            independent.Name = $"{baseName} ({suffix})";
        return new(independent, new(independent.Id, binding == null ? [] : [.. binding.KeyMap]), true);
    }
}
