using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public enum BoardConnectionKind { Searching, Disconnected, ChooseDevice, Connecting, Connected, Unidentified, Paused, Error }
public sealed record BoardConnectionState(BoardConnectionKind Kind, string Message, IReadOnlyList<FirmwareDevice> Devices,
    FirmwareDevice? Device = null, BoardInfo? Info = null, BoardTelemetry? Telemetry = null, bool SerialInput = false, int ConnectedCount = 0);

internal sealed record BoardInputFrame(string DeviceId, Guid Session, int? Mask, long At);

internal sealed class SerialBoardTransport : IBoardTransport
{
    private readonly SerialPort port;
    private readonly StringBuilder line = new();
    public SerialBoardTransport(string name)
    {
        port = new SerialPort(name, 115200) { DtrEnable = true, ReadTimeout = 100, WriteTimeout = 500, NewLine = "\n" };
        try { port.Open(); port.DiscardInBuffer(); }
        catch { port.Dispose(); throw; }
    }
    public void WriteLine(string value) => port.WriteLine(value);
    public string ReadLine()
    {
        long deadline = Environment.TickCount64 + 100;
        while (Environment.TickCount64 < deadline)
        {
            int value = port.ReadByte(); // The partial frame survives the bounded serial timeout.
            if (value == '\n') { string result = line.ToString(); line.Clear(); return result; }
            if (value == '\r') continue;
            if (value < 32 || value > 126 || line.Length >= 127) throw new IOException("Trame USB invalide. Reconnexion en cours…");
            line.Append((char)value);
        }
        throw new TimeoutException();
    }
    public void Dispose() => port.Dispose();
}

internal sealed class BoardConnectionService : IDisposable
{
    private sealed class Link(FirmwareDevice device, bool requestInput, CancellationToken cancellation)
    {
        public FirmwareDevice Device { get; } = device;
        public bool RequestInput { get; } = requestInput;
        public Guid Session { get; } = Guid.NewGuid();
        public CancellationTokenSource Stop { get; } = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        public Task Worker { get; set; } = Task.CompletedTask;
        public BoardConnectionKind Kind { get; set; } = BoardConnectionKind.Connecting;
        public string Message { get; set; } = "Connexion au macropad…";
        public BoardInfo? Info { get; set; }
        public BoardTelemetry? Telemetry { get; set; }
        public bool SerialInput { get; set; }
    }

    private readonly CancellationTokenSource lifetime = new();
    private readonly SemaphoreSlim gate = new(1, 1);
    private readonly SemaphoreSlim devicesChanged = new(0, 1);
    private readonly object sync = new();
    private readonly Dictionary<string, Link> links = new(StringComparer.OrdinalIgnoreCase);
    private readonly IProgress<BoardConnectionState> progress;
    private readonly IProgress<BoardInputFrame> input;
    private readonly Task worker;
    private IReadOnlyList<FirmwareDevice> devices = [];
    private HashSet<string> serialDevices;
    private string preferred;
    private volatile bool paused;

    public BoardConnectionService(string preferredDeviceId, IEnumerable<string> serialDeviceIds,
        IProgress<BoardConnectionState> progress, IProgress<BoardInputFrame> input)
    {
        preferred = preferredDeviceId;
        serialDevices = new(serialDeviceIds, StringComparer.OrdinalIgnoreCase);
        this.progress = progress;
        this.input = input;
        worker = Task.Run(WatchAsync);
    }

    public void Select(string id)
    {
        lock (sync)
        {
            preferred = id;
            ReportState();
        }
    }

    public void SetSerialDevices(IEnumerable<string> ids)
    {
        lock (sync) serialDevices = new(ids, StringComparer.OrdinalIgnoreCase);
        NotifyDevicesChanged();
    }

    public bool IsInputCurrent(string id, Guid session)
    {
        lock (sync) return !paused && !lifetime.IsCancellationRequested && serialDevices.Contains(id)
            && links.TryGetValue(id, out var link) && link.Session == session && link.SerialInput;
    }

    // Call under sync. The selected device is only the subject of the diagnostics.
    private void ReportState()
    {
        var device = devices.FirstOrDefault(d => string.Equals(d.DeviceId, preferred, StringComparison.OrdinalIgnoreCase))
            ?? (devices.Count == 1 ? devices[0] : null);
        int connectedCount = links.Values.Count(link => link.Kind == BoardConnectionKind.Connected);
        if (paused) progress.Report(new(BoardConnectionKind.Paused, "Connexions libérées pour le firmware.", devices, device));
        else if (device != null && links.TryGetValue(device.DeviceId, out var link))
            progress.Report(new(link.Kind, link.Message, devices, device, link.Info, link.Telemetry, link.SerialInput, connectedCount));
        else if (devices.Count > 1)
            progress.Report(new(BoardConnectionKind.ChooseDevice, "Choisissez le macropad à régler.", devices, ConnectedCount: connectedCount));
        else if (device != null)
            progress.Report(new(BoardConnectionKind.Connecting, "Connexion au macropad…", devices, device));
        else
            progress.Report(new(BoardConnectionKind.Disconnected, "Macropad déconnecté", devices));
    }

    public async Task PauseAsync()
    {
        paused = true;
        await gate.WaitAsync(lifetime.Token);
        try
        {
            await StopLinksAsync(_ => true);
            lock (sync) ReportState();
        }
        finally { gate.Release(); }
    }

    public void Resume()
    {
        paused = false;
        NotifyDevicesChanged();
    }

    public void NotifyDevicesChanged()
    {
        try { devicesChanged.Release(); }
        catch (Exception ex) when (ex is SemaphoreFullException or ObjectDisposedException) { }
    }

    private async Task WatchAsync()
    {
        var cancellation = lifetime.Token;
        try
        {
            while (!cancellation.IsCancellationRequested)
            {
                await gate.WaitAsync(cancellation);
                try
                {
                    if (!paused) await RefreshLinksAsync(cancellation);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    lock (sync) progress.Report(new(BoardConnectionKind.Error, "Détection USB indisponible : " + ex.Message, devices));
                }
                finally { gate.Release(); }
                await devicesChanged.WaitAsync(TimeSpan.FromSeconds(3), cancellation);
            }
        }
        catch (OperationCanceledException) when (cancellation.IsCancellationRequested) { }
        finally
        {
            await gate.WaitAsync();
            try { await StopLinksAsync(_ => true); }
            finally { gate.Release(); }
        }
    }

    private async Task RefreshLinksAsync(CancellationToken cancellation)
    {
        var found = FirmwareService.Discover().Where(d => !d.Bootloader)
            .GroupBy(d => d.DeviceId, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() == 1).Select(group => group.Single()).ToArray();
        HashSet<string> requested;
        lock (sync)
        {
            devices = found;
            requested = serialDevices;
        }
        await StopLinksAsync(link => link.Worker.IsCompleted || !found.Contains(link.Device)
            || requested.Contains(link.Device.DeviceId) != link.RequestInput);
        if (paused || cancellation.IsCancellationRequested) return;
        lock (sync)
        {
            foreach (var device in found)
            {
                if (links.ContainsKey(device.DeviceId)) continue;
                var link = new Link(device, requested.Contains(device.DeviceId), cancellation);
                links.Add(device.DeviceId, link);
                link.Worker = Task.Run(() => RunLink(link));
            }
            ReportState();
        }
    }

    private async Task StopLinksAsync(Func<Link, bool> shouldStop)
    {
        Link[] stopping;
        lock (sync)
        {
            stopping = links.Values.Where(shouldStop).ToArray();
            foreach (var link in stopping) links.Remove(link.Device.DeviceId);
        }
        foreach (var link in stopping) link.Stop.Cancel();
        await Task.WhenAll(stopping.Select(link => link.Worker));
        foreach (var link in stopping) link.Stop.Dispose();
    }

    private void RunLink(Link link)
    {
        var cancellation = link.Stop.Token;
        string id = link.Device.DeviceId;
        try
        {
            cancellation.ThrowIfCancellationRequested();
            using var transport = new SerialBoardTransport(link.Device.Port);
            var client = new BoardProtocolClient(transport);
            var info = client.Connect(cancellation);
            bool active = link.RequestInput && client.EnableSerialInput(cancellation);
            UpdateLink(link, () =>
            {
                link.Info = info;
                link.SerialInput = active;
                link.Kind = BoardConnectionKind.Connected;
                link.Message = active ? "Mode DeckPilot actif" : !link.RequestInput ? "Mode clavier · F13 à F24"
                    : info.SupportsSerialInput ? "Relâchez les touches pour activer le mode DeckPilot."
                    : "Mode clavier · Firmware 1.2.0 requis pour le mode DeckPilot.";
            });
            if (link.RequestInput && info.SupportsSerialInput && !active)
            {
                cancellation.WaitHandle.WaitOne(TimeSpan.FromMilliseconds(500));
                return; // Retry the handshake on the next discovery pass.
            }
            if (active) input.Report(new(id, link.Session, 0, Environment.TickCount64));
            client.Run(telemetry => UpdateLink(link, () => link.Telemetry = telemetry), cancellation,
                mask => input.Report(new(id, link.Session, mask, Environment.TickCount64)));
        }
        catch (OperationCanceledException) when (cancellation.IsCancellationRequested) { }
        catch (Exception ex)
        {
            UpdateLink(link, () =>
            {
                link.Kind = link.Info == null && ex is BoardResponseTimeoutException ? BoardConnectionKind.Unidentified : BoardConnectionKind.Error;
                link.Message = "Liaison indisponible : " + ex.Message;
                link.SerialInput = false;
            });
        }
        finally
        {
            if (link.RequestInput)
            {
                UpdateLink(link, () => link.SerialInput = false);
                input.Report(new(id, link.Session, null, Environment.TickCount64));
            }
        }
    }

    private void UpdateLink(Link link, Action update)
    {
        lock (sync)
        {
            if (!links.TryGetValue(link.Device.DeviceId, out var current) || current != link) return;
            update();
            ReportState();
        }
    }

    public void Dispose()
    {
        lifetime.Cancel();
        _ = worker.ContinueWith(_ => { lifetime.Dispose(); gate.Dispose(); devicesChanged.Dispose(); }, TaskScheduler.Default);
    }
}
