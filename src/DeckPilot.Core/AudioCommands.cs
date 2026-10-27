namespace DeckPilot.Core;

public sealed record AudioDevice(string Id, string Name) { public override string ToString() => Name; }
public interface IAudioBackend
{
    IReadOnlyList<AudioDevice> Outputs();
    IReadOnlyList<AudioDevice> Microphones();
    string DefaultOutput(int role);
    void SetDefaultOutput(string id, int role);
    bool IsMuted(string id);
    void SetMuted(string id, bool muted);
}
public static class AudioCommands
{
    public static bool ToggleMicrophones(IAudioBackend backend)
    {
        var devices = backend.Microphones();
        if (devices.Count == 0) throw new InvalidOperationException("Aucun microphone Windows actif.");
        var previous = devices.Select(d => (d.Id, Muted: backend.IsMuted(d.Id))).ToList();
        bool mute = !previous.All(d => d.Muted);
        try { foreach (var d in previous) backend.SetMuted(d.Id, mute); }
        catch
        {
            foreach (var d in previous) try { backend.SetMuted(d.Id, d.Muted); } catch { }
            throw;
        }
        return mute;
    }
    public static string ToggleOutput(IAudioBackend backend, string primary, string secondary)
    {
        var devices = backend.Outputs();
        if (string.IsNullOrWhiteSpace(primary) || primary == secondary || !devices.Any(d => d.Id == primary) || !devices.Any(d => d.Id == secondary))
            throw new InvalidOperationException("Choisissez deux sorties audio distinctes et connectées dans les réglages de cette action.");
        string[] previous = Enumerable.Range(0, 3).Select(backend.DefaultOutput).ToArray();
        string next = previous[0] == primary ? secondary : primary;
        try { for (int role = 0; role < 3; role++) backend.SetDefaultOutput(next, role); }
        catch
        {
            for (int role = 0; role < 3; role++) if (!string.IsNullOrEmpty(previous[role])) try { backend.SetDefaultOutput(previous[role], role); } catch { }
            throw;
        }
        return next;
    }
}
