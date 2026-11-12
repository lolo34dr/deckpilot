namespace DeckPilot.Core;

public sealed record ActionPreset(string Name, string Details, ButtonAction Action)
{
    public bool Matches(string query) => new InstalledApplication(Name, "", Details).Matches(query);
}

public static class ActionSearch
{
    public static readonly string[] KindNames = ["Touche libre", "Ouvrir une application", "Ouvrir un dossier", "Ouvrir un site web", "Raccourci clavier", "Multimédia", "Spotify", "Microphone Windows", "Changer de sortie audio", "Changer de profil", "Commandes Windows", "Soundboard"];
    // Streaming remains a reserved value so 2.3 profiles stay readable.
    public static bool IsAvailable(ActionKind kind) => (uint)kind < KindNames.Length;
    public static string KindName(ActionKind kind) => IsAvailable(kind) ? KindNames[(int)kind] : "Action retirée";
    public static IReadOnlyList<ActionPreset> Find(string query, IEnumerable<InstalledApplication> applications)
    {
        if (string.IsNullOrWhiteSpace(query)) return [];
        var results = Enum.GetValues<ActionKind>().Where(IsAvailable).Select(k => new ActionPreset(KindNames[(int)k], "Configurer une action", new() { Kind = k, Key = k is ActionKind.Media or ActionKind.Spotify ? 0xB3 : 0x41 }));
        var commands = new[] { ("Lecture / pause", 0xB3, "play pause musique"), ("Piste suivante", 0xB0, "suivant skip next musique"), ("Piste précédente", 0xB1, "précédent previous musique"), ("Volume +", 0xAF, "augmenter son volume"), ("Volume −", 0xAE, "baisser son volume"), ("Muet", 0xAD, "mute couper son volume") };
        var spotifyCommands = commands.Take(3).Concat(new[]
        {
            ("Volume +", 0xAF, "augmenter volume Spotify uniquement dans le mixeur Windows"),
            ("Volume −", 0xAE, "baisser volume Spotify uniquement dans le mixeur Windows")
        });
        results = results.Concat(commands.Select(c => new ActionPreset("Multimédia — " + c.Item1, c.Item3, new() { Label = c.Item1, Kind = ActionKind.Media, Key = c.Item2 })))
            .Concat(spotifyCommands.Select(c => new ActionPreset("Spotify — " + c.Item1, c.Item3, new() { Label = "Spotify · " + c.Item1, Kind = ActionKind.Spotify, Key = c.Item2 })))
            .Concat(SystemActions.All.Select(action => new ActionPreset(action.Name, action.Description + " Windows système", new() { Label = action.Name, Kind = ActionKind.System, Target = action.Id })))
            .Concat(new[]
            {
                new ActionPreset("Profil suivant", "Passer au prochain profil du macropad", new() { Label = "Profil suivant", Kind = ActionKind.Profile, Target = "next" }),
                new ActionPreset("Profil précédent", "Revenir au profil précédent du macropad", new() { Label = "Profil précédent", Kind = ActionKind.Profile, Target = "previous" })
            })
            .Where(p => p.Matches(query));
        var apps = new[] { ApplicationTarget.DeckPilotApplication }.Concat(applications).DistinctBy(a => a.Name, StringComparer.OrdinalIgnoreCase)
            .Where(a => a.Matches(query)).Select(a => new ActionPreset(a.Name, "Application · " + a.Source,
            new() { Kind = ActionKind.Application, Label = a.Name[..Math.Min(50, a.Name.Length)], Target = a.Target, ApplicationName = a.Name }));
        return results.Concat(apps).Take(40).ToList();
    }
}
