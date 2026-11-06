namespace DeckPilot.Core;

public interface ISpotifyVolumeSession
{
    string ProcessName { get; }
    bool IsActive { get; }
    float Volume { get; }
    void SetVolume(float volume);
}

public static class SpotifyVolume
{
    public const float Step = 0.05f;

    public static bool IsSpotifyProcess(string processName) =>
        processName.Equals("Spotify", StringComparison.OrdinalIgnoreCase)
        || processName.Equals("Spotify.exe", StringComparison.OrdinalIgnoreCase)
        || processName.Equals("SpotifyAB.SpotifyMusic", StringComparison.OrdinalIgnoreCase)
        || processName.StartsWith("SpotifyAB.SpotifyMusic_", StringComparison.OrdinalIgnoreCase);

    public static int Adjust(IEnumerable<ISpotifyVolumeSession> sessions, int key)
    {
        if (key is not (0xAF or 0xAE)) throw new ArgumentException("Commande de volume Spotify invalide.");

        var spotify = sessions.Where(s => IsSpotifyProcess(s.ProcessName)).ToArray();
        if (spotify.Length == 0)
            throw new InvalidOperationException("Aucune session audio Spotify n’est disponible. Lancez Spotify et démarrez une lecture, puis réessayez. Le volume général ne sera pas modifié.");

        var active = spotify.Where(s => s.IsActive).ToArray();
        var targets = active.Length > 0 ? active : spotify;
        float delta = key == 0xAF ? Step : -Step;
        foreach (var session in targets)
            session.SetVolume(Math.Clamp(session.Volume + delta, 0f, 1f));
        return targets.Length;
    }
}
