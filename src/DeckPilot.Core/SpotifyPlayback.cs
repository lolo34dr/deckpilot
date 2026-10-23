namespace DeckPilot.Core;

public interface ISpotifyMediaSession
{
    string AppId { get; }
    bool IsPlaying { get; }
    Task<bool> SendAsync(int key, CancellationToken cancellation);
}

public static class SpotifyPlayback
{
    public static bool IsSpotify(string appId) => appId.Equals("Spotify.exe", StringComparison.OrdinalIgnoreCase)
        || appId.Equals("Spotify", StringComparison.OrdinalIgnoreCase)
        || appId.StartsWith("SpotifyAB.SpotifyMusic_zpdnekdrzrea0!", StringComparison.OrdinalIgnoreCase);

    public static async Task SendAsync(IEnumerable<ISpotifyMediaSession> sessions, int key, CancellationToken cancellation)
    {
        if (key is not (0xB3 or 0xB0 or 0xB1)) throw new ArgumentException("Commande Spotify invalide.");
        cancellation.ThrowIfCancellationRequested();
        var spotify = sessions.Where(s => IsSpotify(s.AppId)).OrderByDescending(s => s.IsPlaying).FirstOrDefault()
            ?? throw new InvalidOperationException("Ouvrez l’application Spotify sur ce PC et lancez une lecture. Spotify dans le navigateur n’est pas pris en charge.");
        if (!await spotify.SendAsync(key, cancellation))
            throw new InvalidOperationException("Spotify a refusé la commande. Vérifiez qu’une lecture est disponible dans l’application.");
    }
}
