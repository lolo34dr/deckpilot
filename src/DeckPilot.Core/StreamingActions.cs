namespace DeckPilot.Core;

public sealed record StreamingCommand(string Id, string Name, string Details, bool NeedsTarget = false);
public sealed record StreamingCommandPreset(string ProviderId, string ProviderName, StreamingCommand Command);

public static class StreamingActionCatalog
{
    public static IReadOnlyList<StreamingCommand> For(string provider) => provider switch
    {
        "obs" => Obs,
        "streamlabs" => Streamlabs,
        _ => Array.Empty<StreamingCommand>()
    };

    public static StreamingCommand? Find(string provider, string id) => For(provider).FirstOrDefault(command => command.Id == id);

    private static readonly StreamingCommand[] Obs =
    [
        new("scene", "Changer de scène", "Met une scène choisie à l’antenne." , true),
        new("stream-start", "Commencer le live", "Démarre le live OBS si nécessaire."),
        new("stream-stop", "Arrêter le live", "Arrête le live OBS si nécessaire."),
        new("record-start", "Commencer l’enregistrement", "Démarre l’enregistrement local."),
        new("record-stop", "Arrêter l’enregistrement", "Arrête l’enregistrement local."),
        new("record-pause", "Mettre en pause l’enregistrement", "Met en pause l’enregistrement local."),
        new("record-resume", "Reprendre l’enregistrement", "Reprend l’enregistrement local."),
        new("mic-toggle", "Basculer le micro", "Inverse le mode muet de l’entrée audio indiquée.", true),
        new("desktop-audio-toggle", "Basculer le son du bureau", "Inverse le mode muet de l’entrée audio indiquée.", true),
        new("source-toggle", "Afficher / masquer une source", "Inverse la visibilité d’une source dans une scène.", true),
        new("replay-start", "Démarrer le replay buffer", "Démarre le tampon de replay OBS."),
        new("replay-stop", "Arrêter le replay buffer", "Arrête le tampon de replay OBS."),
        new("replay-save", "Sauvegarder le replay", "Enregistre le replay courant."),
        new("studio-toggle", "Activer / désactiver le mode studio", "Bascule le mode Studio d’OBS."),
        new("preview-transition", "Envoyer l’aperçu à l’antenne", "Applique la transition du mode Studio."),
        new("virtualcam-start", "Démarrer la caméra virtuelle", "Démarre la caméra virtuelle OBS."),
        new("virtualcam-stop", "Arrêter la caméra virtuelle", "Arrête la caméra virtuelle OBS.")
    ];

    private static readonly StreamingCommand[] Streamlabs =
    [
        new("scene", "Changer de scène", "Met une scène choisie à l’antenne." , true),
        new("stream-start", "Commencer le live", "Démarre le live Streamlabs si nécessaire."),
        new("stream-stop", "Arrêter le live", "Arrête le live Streamlabs si nécessaire."),
        new("record-start", "Commencer l’enregistrement", "Démarre l’enregistrement local."),
        new("record-stop", "Arrêter l’enregistrement", "Arrête l’enregistrement local."),
        new("mic-toggle", "Basculer le micro", "Inverse le mode muet d’une source audio indiquée.", true),
        new("desktop-audio-toggle", "Basculer le son du bureau", "Inverse le mode muet d’une source audio indiquée.", true),
        new("source-toggle", "Afficher / masquer une source", "Inverse la visibilité d’une source dans la scène active.", true),
        new("replay-start", "Démarrer le replay buffer", "Démarre le tampon de replay."),
        new("replay-stop", "Arrêter le replay buffer", "Arrête le tampon de replay."),
        new("replay-save", "Sauvegarder le replay", "Enregistre le replay courant.")
    ];

    public static IReadOnlyList<StreamingCommandPreset> All { get; } = Array.AsReadOnly(
        Obs.Select(command => new StreamingCommandPreset("obs", "OBS", command))
            .Concat(Streamlabs.Select(command => new StreamingCommandPreset("streamlabs", "Streamlabs", command))).ToArray());
}
