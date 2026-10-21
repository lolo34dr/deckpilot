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
    FirmwareDevice? Device = null, BoardInfo? Info = null, BoardTelemetry? Telemetry = null);

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

public sealed class BoardConnectionService : IDisposable
{
    private readonly CancellationTokenSource lifetime = new();
    private readonly SemaphoreSlim portGate = new(1, 1);
    private readonly object sync = new();
    private readonly IProgress<BoardConnectionState>? progress;
    private CancellationTokenSource? sessionCancellation;
    private readonly Task worker;
    private volatile bool paused;
    private string preferred;
    private BoardConnectionState state = new(BoardConnectionKind.Searching, "Recherche du macropad…", []);
    public BoardConnectionState State { get { lock (sync) return state; } }
    public BoardConnectionService(string preferredDeviceId, IProgress<BoardConnectionState>? progress = null)
    {
        preferred = preferredDeviceId; this.progress = progress;
        worker = Task.Run(WatchAsync);
    }
    public void Select(string id)
    {
        lock (sync)
        {
            if (preferred == id) return;
            preferred = id; CancelSession();
            state = new(BoardConnectionKind.Searching, "Changement de macropad…", state.Devices);
            progress?.Report(state);
        }
    }
    private void CancelSession()
    {
        try { sessionCancellation?.Cancel(); } catch (ObjectDisposedException) { }
    }
    private void Publish(Func<BoardConnectionState, BoardConnectionState> update)
    {
        lock (sync) { state = update(state); progress?.Report(state); }
    }
    public async Task PauseAsync()
    {
        paused = true;
        lock (sync) CancelSession();
        await portGate.WaitAsync(lifetime.Token);
        portGate.Release();
        Publish(s => new(BoardConnectionKind.Paused, "Connexion libérée pour le firmware.", s.Devices));
    }
    public void Resume() => paused = false;
    private async Task WatchAsync()
    {
        var cancellation = lifetime.Token;
        try
        {
            while (!cancellation.IsCancellationRequested)
            {
                if (paused) { await Task.Delay(200, cancellation); continue; }
                try
                {
                    var devices = FirmwareService.Discover().Where(d => !d.Bootloader).ToList();
                    string selected; lock (sync) selected = preferred;
                    var device = devices.FirstOrDefault(d => d.DeviceId == selected) ?? (devices.Count == 1 ? devices[0] : null);
                    if (device == null)
                    {
                        Publish(_ => new(devices.Count == 0 ? BoardConnectionKind.Disconnected : BoardConnectionKind.ChooseDevice,
                            devices.Count == 0 ? "Macropad déconnecté" : "Choisissez le macropad à connecter.", devices));
                    }
                    else
                    {
                        await portGate.WaitAsync(cancellation);
                        try
                        {
                            if (paused) continue;
                            using var session = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
                            lock (sync) { sessionCancellation = session; if (preferred != selected) session.Cancel(); }
                            session.Token.ThrowIfCancellationRequested();
                            Publish(s => session.IsCancellationRequested ? s : new(BoardConnectionKind.Connecting, "Connexion au macropad…", devices, device));
                            using var transport = new SerialBoardTransport(device.Port);
                            var client = new BoardProtocolClient(transport);
                            BoardInfo info;
                            try { info = client.Connect(session.Token); }
                            catch (IOException ex)
                            {
                                Publish(_ => new(BoardConnectionKind.Unidentified, "Carte non identifiée : " + ex.Message, devices, device));
                                await Task.Delay(1500, session.Token); continue;
                            }
                            session.Token.ThrowIfCancellationRequested();
                            Publish(s => session.IsCancellationRequested ? s : new(BoardConnectionKind.Connected, "Macropad connecté", devices, device, info));
                            var run = Task.Run(() => client.Run(telemetry => Publish(s => s with { Telemetry = telemetry }), session.Token), session.Token);
                            try
                            {
                                while (!run.IsCompleted && !session.IsCancellationRequested)
                                {
                                    if (await Task.WhenAny(run, Task.Delay(3000, session.Token)) == run) break;
                                    if (session.IsCancellationRequested) break;
                                    var current = FirmwareService.Discover().Where(d => !d.Bootloader).ToList();
                                    Publish(s => s with { Devices = current });
                                    if (!current.Any(d => d.DeviceId == device.DeviceId && d.Port == device.Port)) session.Cancel();
                                }
                            }
                            finally { session.Cancel(); await run; }
                        }
                        finally
                        {
                            lock (sync) sessionCancellation = null;
                            portGate.Release();
                        }
                    }
                }
                catch (OperationCanceledException) when (!cancellation.IsCancellationRequested)
                {
                    if (!paused) Publish(s => new(BoardConnectionKind.Searching, "Reconnexion du macropad…", s.Devices));
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    Publish(s => new(BoardConnectionKind.Error, "Liaison indisponible : " + ex.Message, s.Devices));
                }
                await Task.Delay(1500, cancellation);
            }
        }
        catch (OperationCanceledException) { }
    }
    public void Dispose()
    {
        lifetime.Cancel();
        lock (sync) CancelSession();
        _ = worker.ContinueWith(_ => { lifetime.Dispose(); portGate.Dispose(); }, TaskScheduler.Default);
    }
}
