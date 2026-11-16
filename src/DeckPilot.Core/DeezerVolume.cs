namespace DeckPilot.Core;

public interface IDeezerVolumeSession
{
    string ProcessName { get; }
    bool IsActive { get; }
    float Volume { get; }
    void SetVolume(float volume);
}

public static class DeezerVolume
{
    public const float Step = 0.05f;

    public static bool IsDeezerProcess(string processName) =>
        processName.Equals("Deezer", StringComparison.OrdinalIgnoreCase)
        || processName.Equals("Deezer.exe", StringComparison.OrdinalIgnoreCase)
        || processName.Equals("Deezer", StringComparison.OrdinalIgnoreCase)
        || processName.StartsWith("Deezer_", StringComparison.OrdinalIgnoreCase);

    public static int Adjust(IEnumerable<IDeezerVolumeSession> sessions, int key)
    {
        if (key is not (0xAF or 0xAE)) throw new ArgumentException("Commande de volume Deezer invalide.");

        var deezer = sessions.Where(s => IsDeezerProcess(s.ProcessName)).ToArray();
        if (deezer.Length == 0)
            throw new InvalidOperationException("Aucune session audio Deezer n’est disponible. Lancez Deezer et démarrez une lecture, puis réessayez. Le volume général ne sera pas modifié.");

        var active = deezer.Where(s => s.IsActive).ToArray();
        var targets = active.Length > 0 ? active : deezer;
        float delta = key == 0xAF ? Step : -Step;
        foreach (var session in targets)
            session.SetVolume(Math.Clamp(session.Volume + delta, 0f, 1f));
        return targets.Length;
    }
}
