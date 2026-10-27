namespace DeckPilot.Core;

public sealed record ActionAccent(string Value, string Name) { public override string ToString() => Name; }

public static class ActionAppearance
{
    public static IReadOnlyList<ActionAccent> Accents { get; } = Array.AsReadOnly(new[]
    {
        new ActionAccent("", "Automatique"),
        new ActionAccent("#78B7A0", "Sauge"),
        new ActionAccent("#70A7E8", "Bleu"),
        new ActionAccent("#AD91E8", "Violet"),
        new ActionAccent("#E3B46F", "Ambre"),
        new ActionAccent("#DF8898", "Rose")
    });

    public static bool IsValidAccent(string? value) => value != null && Accents.Any(a => string.Equals(a.Value, value, StringComparison.OrdinalIgnoreCase));
}

public static class ProfileNavigation
{
    // Resolving a destination never changes the active profile or runs its actions.
    public static Profile Resolve(Settings settings, string target)
    {
        ArgumentNullException.ThrowIfNull(settings);
        if (settings.Profiles.Count == 0) throw new InvalidOperationException("Aucun profil disponible.");
        if (target is "next" or "previous")
        {
            int current = settings.Profiles.IndexOf(settings.ActiveProfile);
            int step = target == "next" ? 1 : -1;
            return settings.Profiles[(current + step + settings.Profiles.Count) % settings.Profiles.Count];
        }
        if (!Guid.TryParse(target, out var id) || id == Guid.Empty)
            throw new InvalidOperationException("Choisissez un profil à activer dans les réglages de cette action.");
        return settings.Profiles.FirstOrDefault(p => p.Id == id)
            ?? throw new InvalidOperationException("Le profil associé à cette touche n’existe plus. Choisissez un autre profil.");
    }
}

public sealed record SystemActionPreset(string Id, string Name, string Description) { public override string ToString() => Name; }

public static class SystemActions
{
    public static IReadOnlyList<SystemActionPreset> All { get; } = Array.AsReadOnly(new[]
    {
        new SystemActionPreset("settings", "Paramètres Windows", "Ouvrir les paramètres du PC"),
        new SystemActionPreset("sound-settings", "Paramètres du son", "Configurer le volume, les micros et les sorties audio"),
        new SystemActionPreset("display-settings", "Paramètres d’affichage", "Configurer les écrans et leur résolution"),
        new SystemActionPreset("bluetooth-settings", "Bluetooth et appareils", "Gérer les périphériques Bluetooth"),
        new SystemActionPreset("network-settings", "Réseau et Internet", "Ouvrir les paramètres Wi-Fi et Ethernet"),
        new SystemActionPreset("task-manager", "Gestionnaire des tâches", "Voir les applications et les performances du PC"),
        new SystemActionPreset("lock", "Verrouiller le PC", "Verrouiller la session Windows ; une reconnexion sera nécessaire")
    });

    public static SystemActionPreset Get(string id) => All.FirstOrDefault(action => action.Id == id)
        ?? throw new InvalidOperationException("Choisissez une commande Windows dans les réglages de cette action.");
}

public sealed record ProfileTemplate(string Id, string Name, string Description) { public override string ToString() => Name; }

public static class ProfileTemplates
{
    public static IReadOnlyList<ProfileTemplate> All { get; } = Array.AsReadOnly(new[]
    {
        new ProfileTemplate("multimedia", "Musique et audio", "Spotify, volume Windows, microphone et réglages du son."),
        new ProfileTemplate("productivity", "Bureau et productivité", "Copier, coller, annuler, capture d’écran et outils Windows."),
        new ProfileTemplate("creator", "Création", "Enregistrer, rechercher, capturer l’écran et gérer ses projets.")
    });

    public static Profile Create(string id)
    {
        var template = All.FirstOrDefault(t => t.Id == id)
            ?? throw new InvalidOperationException("Ce modèle de profil n’est pas disponible.");
        var profile = new Profile { Name = template.Name };
        ButtonAction Shortcut(string label, int key, bool shift = false, bool win = false) =>
            new() { Kind = ActionKind.Shortcut, Label = label, Key = key, Ctrl = !win, Shift = shift, Win = win, Accent = "#70A7E8" };
        ButtonAction Media(string label, int key) => new() { Kind = ActionKind.Media, Label = label, Key = key, Accent = "#E3B46F" };
        ButtonAction Spotify(string label, int key) => new() { Kind = ActionKind.Spotify, Label = label, Key = key, Accent = "#78B7A0" };
        ButtonAction System(string target) => new() { Kind = ActionKind.System, Label = SystemActions.Get(target).Name, Target = target, Accent = "#AD91E8" };
        ButtonAction Profile(string target, string label) => new() { Kind = ActionKind.Profile, Target = target, Label = label, Accent = "#DF8898" };
        profile.Buttons = id switch
        {
            "multimedia" =>
            [
                Spotify("Spotify · Précédent", 0xB1), Spotify("Spotify · Pause", 0xB3), Spotify("Spotify · Suivant", 0xB0), new(),
                Media("Volume −", 0xAE), Media("Volume +", 0xAF), Media("Son muet", 0xAD), new() { Kind = ActionKind.Microphone, Label = "Micro", Accent = "#DF8898" },
                System("sound-settings"), System("bluetooth-settings"), Profile("previous", "Profil précédent"), Profile("next", "Profil suivant")
            ],
            "productivity" =>
            [
                Shortcut("Copier", 0x43), Shortcut("Coller", 0x56), Shortcut("Couper", 0x58), Shortcut("Tout sélectionner", 0x41),
                Shortcut("Annuler", 0x5A), Shortcut("Rétablir", 0x59), Shortcut("Capture d’écran", 0x53, shift: true, win: true), Shortcut("Explorateur", 0x45, win: true),
                System("settings"), System("task-manager"), Profile("previous", "Profil précédent"), Profile("next", "Profil suivant")
            ],
            _ =>
            [
                Shortcut("Enregistrer", 0x53), Shortcut("Enregistrer sous", 0x53, shift: true), Shortcut("Ouvrir", 0x4F), Shortcut("Rechercher", 0x46),
                Shortcut("Annuler", 0x5A), Shortcut("Rétablir", 0x59), Shortcut("Capture d’écran", 0x53, shift: true, win: true), Shortcut("Zoom par défaut", 0x30),
                Shortcut("Nouveau", 0x4E), new() { Kind = ActionKind.Microphone, Label = "Micro", Accent = "#DF8898" }, Profile("previous", "Profil précédent"), Profile("next", "Profil suivant")
            ]
        };
        return profile;
    }
}
