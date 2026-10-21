using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;

namespace DeckPilot.Core;

public sealed record BoardInfo(FirmwareIdentity Firmware, int Protocol, uint Capabilities);
public sealed record BoardTelemetry(uint Uptime, uint Sequence, int Mask, int LatencyMilliseconds);

public static class BoardProtocol
{
    public static string NewSession() => Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
    public static bool ValidSession(string value) => value.Length == 8 && value.All(c => c is >= '0' and <= '9' or >= 'A' and <= 'F');
    public static bool Newer(uint next, uint previous) => unchecked((int)(next - previous)) > 0;
    private static string[] Fields(string line) => line.Length <= 127 ? line.TrimEnd('\r', '\n').Split('|') : [];
    private static bool Number(string text, out uint value) => uint.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out value);
    public static BoardInfo? Info(string line, string session)
    {
        var p = Fields(line);
        if (!ValidSession(session) || p.Length != 7 || p[0] != "DP2" || p[1] != "INFO" || p[2] != session || p[3] != FirmwareIdentity.Board || p[5] != "12" || !Number(p[6], out uint capabilities) || (capabilities & 1) == 0) return null;
        var firmware = FirmwareIdentity.Parse($"DECKPILOT|1|{p[3]}|{p[4]}");
        return firmware == null ? null : new(firmware, 2, capabilities);
    }
    public static BoardTelemetry? Pong(string line, string session, uint request, int latency)
    {
        var p = Fields(line);
        return p.Length == 7 && p[0] == "DP2" && p[1] == "PONG" && p[2] == session && Number(p[3], out uint reply) && reply == request && Number(p[4], out uint uptime) && Number(p[5], out uint sequence) && Number(p[6], out uint mask) && mask <= 0xfff
            ? new(uptime, sequence, (int)mask, latency) : null;
    }
    public static (uint Sequence, int Mask)? Keys(string line, string session)
    {
        var p = Fields(line);
        return p.Length == 5 && p[0] == "DP2" && p[1] == "KEYS" && p[2] == session && Number(p[3], out uint sequence) && Number(p[4], out uint mask) && mask <= 0xfff ? (sequence, (int)mask) : null;
    }
}

public interface IBoardTransport : IDisposable
{
    void WriteLine(string line);
    // Implementations must use a short, bounded timeout and retain partial lines.
    string ReadLine();
}

public sealed class BoardProtocolClient(IBoardTransport transport)
{
    private readonly string session = BoardProtocol.NewSession();
    public BoardInfo? Info { get; private set; }
    public FirmwareIdentity Identify(CancellationToken cancellation)
    {
        transport.WriteLine("DECKPILOT?");
        return ReadUntil(FirmwareIdentity.Parse, cancellation, TimeSpan.FromMilliseconds(1500));
    }
    public BoardInfo Connect(CancellationToken cancellation)
    {
        var identity = Identify(cancellation);
        if (Version.Parse(identity.Version) < new Version(1, 1, 0)) return Info = new(identity, 1, 0);
        transport.WriteLine("DP2|HELLO|" + session);
        var info = ReadUntil(line => BoardProtocol.Info(line, session), cancellation, TimeSpan.FromMilliseconds(1500));
        if (info.Firmware != identity) throw new IOException("La carte a changé d’identité pendant la connexion.");
        return Info = info;
    }
    private T ReadUntil<T>(Func<string, T?> parse, CancellationToken cancellation, TimeSpan timeout) where T : class
    {
        var watch = Stopwatch.StartNew();
        while (watch.Elapsed < timeout)
        {
            cancellation.ThrowIfCancellationRequested();
            try { var result = parse(transport.ReadLine()); if (result != null) return result; }
            catch (TimeoutException) { }
        }
        throw new IOException("La carte ne répond pas avec le protocole attendu. Fermez les autres outils série puis réessayez.");
    }
    public void Run(Action<BoardTelemetry> report, CancellationToken cancellation)
    {
        var info = Info ?? throw new InvalidOperationException("Connect first.");
        if (info.Protocol == 1)
        {
            while (!cancellation.WaitHandle.WaitOne(TimeSpan.FromSeconds(5)))
                if (Identify(cancellation) != info.Firmware) throw new IOException("Le firmware de la carte a changé. Nouvelle détection nécessaire.");
            cancellation.ThrowIfCancellationRequested(); return;
        }
        uint request = 0; uint? lastSequence = null;
        var state = new BoardTelemetry(0, 0, 0, -1);
        long sent = 0, next = 0;
        bool waiting = false;
        while (true)
        {
            cancellation.ThrowIfCancellationRequested();
            long now = Environment.TickCount64;
            if (waiting && now - sent > 1500) throw new IOException("La liaison USB ne répond plus. Reconnexion en cours…");
            if (!waiting && now >= next)
            {
                request = unchecked(request + 1); sent = now; waiting = true;
                transport.WriteLine($"DP2|PING|{session}|{request.ToString(CultureInfo.InvariantCulture)}");
            }
            string line;
            try { line = transport.ReadLine(); } catch (TimeoutException) { continue; }
            var pong = waiting ? BoardProtocol.Pong(line, session, request, (int)Math.Clamp(Environment.TickCount64 - sent, 0, 1500)) : null;
            if (pong != null)
            {
                waiting = false; next = Environment.TickCount64 + 2000;
                bool current = lastSequence == null || pong.Sequence == lastSequence || BoardProtocol.Newer(pong.Sequence, lastSequence.Value);
                state = pong with { Mask = current ? pong.Mask : state.Mask, Sequence = current ? pong.Sequence : state.Sequence };
                if (current) lastSequence = pong.Sequence;
                report(state); continue;
            }
            var keys = (info.Capabilities & 2) != 0 ? BoardProtocol.Keys(line, session) : null;
            if (keys != null && (lastSequence == null || BoardProtocol.Newer(keys.Value.Sequence, lastSequence.Value)))
            {
                lastSequence = keys.Value.Sequence;
                state = state with { Sequence = keys.Value.Sequence, Mask = keys.Value.Mask };
                report(state);
            }
        }
    }
}
