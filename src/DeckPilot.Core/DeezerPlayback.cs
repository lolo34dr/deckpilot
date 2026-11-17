namespace DeckPilot.Core;

public interface IDeezerMediaSession
{
    string AppId { get; }
    bool IsPlaying { get; }
    Task<bool> SendAsync(int key, CancellationToken cancellation);
}

public static class DeezerPlayback
{
    public static bool IsDeezer(string appId) => HasDeezerIdentity(appId);

    internal static bool HasDeezerIdentity(string? identity) => !string.IsNullOrWhiteSpace(identity)
        && identity.Split(['.', '_', '!', '-', '/', '\\', ' '], StringSplitOptions.RemoveEmptyEntries)
            .Any(part => part.Equals("Deezer", StringComparison.OrdinalIgnoreCase));

    public static async Task SendAsync(IEnumerable<IDeezerMediaSession> sessions, int key, CancellationToken cancellation)
    {
        if (key is not (0xB3 or 0xB0 or 0xB1)) throw new ArgumentException("Commande Deezer invalide.");
        cancellation.ThrowIfCancellationRequested();
        var deezer = sessions.Where(s => IsDeezer(s.AppId)).OrderByDescending(s => s.IsPlaying).FirstOrDefault()
            ?? throw new InvalidOperationException("Ouvrez l’application Deezer sur ce PC et lancez une lecture. Deezer dans le navigateur n’est pas pris en charge.");
        if (!await deezer.SendAsync(key, cancellation))
            throw new InvalidOperationException("Deezer a refusé la commande. Vérifiez qu’une lecture est disponible dans l’application.");
    }
}
