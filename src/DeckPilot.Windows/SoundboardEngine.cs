using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using DeckPilot.Core;
using System.IO;
using System.Linq;
using System.Diagnostics;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DeckPilot.Windows;

internal sealed record AudioEndpoint(string Id, string Name, bool IsVirtualCable = false)
{
    public override string ToString() => Name;
}

internal sealed record SoundboardEndpointSnapshot(
    IReadOnlyList<AudioEndpoint> Microphones,
    IReadOnlyList<AudioEndpoint> Outputs,
    string Error = "");

// The selected playback endpoint must be the input side of a virtual audio cable.
// Its capture side can then be selected as a microphone in Discord or a game.
internal sealed class SoundboardEngine : IDisposable
{
    private sealed class Playing(AudioFileReader reader, ISampleProvider samples, string path)
    {
        public AudioFileReader Reader { get; } = reader;
        public ISampleProvider Samples { get; } = samples;
        public string Path { get; } = path;
        public long StartedAt { get; } = Stopwatch.GetTimestamp();
        public AudioFileReader? MonitorReader { get; set; }
        public ISampleProvider? MonitorSamples { get; set; }
        public VolumeSampleProvider? MonitorVolume { get; set; }
    }
    private readonly MMDeviceEnumerator devices = new();
    private readonly Dictionary<Guid, Playing> playing = new();
    private MMDevice? microphoneDevice;
    private MMDevice? outputDevice;
    private WasapiCapture? capture;
    private RealtimeAudioBuffer? voiceBuffer;
    private WasapiOut? playback;
    private MixingSampleProvider? mixer;
    private WaveFormat? mixFormat;
    private readonly ConcurrentQueue<ISampleProvider> finished = new();
    private readonly ConcurrentQueue<string> faults = new();
    private MMDevice? monitorDevice;
    private WasapiOut? monitorPlayback;
    private MixingSampleProvider? monitorMixer;
    private WaveFormat? monitorFormat;
    private readonly ConcurrentQueue<ISampleProvider> monitorFinished = new();
    private readonly ConcurrentQueue<string> monitorFaults = new();
    private bool monitorEnabled;
    private string monitorOutputId = "";
    private int monitorVolumePercent = 50;
    private volatile bool monitorStopping;
    private volatile bool stopping;
    public event Action? StateChanged;
    public bool IsRunning => playback != null;
    public string? MonitorError { get; private set; }
    public static SoundboardEndpointSnapshot DiscoverEndpoints()
    {
        try
        {
            using var enumerator = new MMDeviceEnumerator();
            return new(Endpoints(enumerator, DataFlow.Capture), Endpoints(enumerator, DataFlow.Render));
        }
        catch (Exception ex)
        {
            return new(Array.Empty<AudioEndpoint>(), Array.Empty<AudioEndpoint>(), ex.Message);
        }
    }

    private static IReadOnlyList<AudioEndpoint> Endpoints(MMDeviceEnumerator enumerator, DataFlow flow)
    {
        var result = new List<AudioEndpoint>();
        var collection = enumerator.EnumerateAudioEndPoints(flow, DeviceState.Active);
        foreach (var device in collection)
        {
            using (device) result.Add(new(device.ID, device.FriendlyName, VirtualCableEndpoints.IsCableDevice(device.DeviceFriendlyName)
                || (flow == DataFlow.Render ? VirtualCableEndpoints.IsPlaybackEndpoint(device.FriendlyName) : VirtualCableEndpoints.IsRecordingEndpoint(device.FriendlyName))));
        }
        return result.AsReadOnly();
    }
    public bool IsPlaying(Guid id) => playing.ContainsKey(id);
    // Called on the UI thread: never dispose a reader inside the mixer's audio callback.
    public string? ProcessEvents()
    {
        while (finished.TryDequeue(out var samples))
        {
            var item = playing.FirstOrDefault(p => ReferenceEquals(p.Value.Samples, samples));
            if (item.Value != null) StopClip(item.Key);
        }
        while (monitorFinished.TryDequeue(out var samples))
        {
            var item = playing.Values.FirstOrDefault(p => ReferenceEquals(p.MonitorSamples, samples));
            if (item != null) StopMonitorClip(item);
        }
        if (monitorFaults.TryDequeue(out var monitorFault))
        {
            StopMonitor();
            MonitorError = "Retour soundboard interrompu : " + monitorFault;
        }
        if (!faults.TryDequeue(out var fault)) return null;
        Stop(); return fault;
    }
    private void AudioStopped(object? sender, StoppedEventArgs e)
    {
        if (stopping || (!ReferenceEquals(sender, capture) && !ReferenceEquals(sender, playback))) return;
        faults.Enqueue(e.Exception?.Message ?? "Le périphérique audio s’est arrêté. Rebranche-le puis réactive le micro virtuel.");
        StateChanged?.Invoke();
    }
    private void MonitorStopped(object? sender, StoppedEventArgs e)
    {
        if (monitorStopping || !ReferenceEquals(sender, monitorPlayback)) return;
        monitorFaults.Enqueue(e.Exception?.Message ?? "La sortie audio sélectionnée s’est arrêtée. Actualise les périphériques ou choisis une autre sortie.");
        StateChanged?.Invoke();
    }

    // The monitor has its own mixer and decoder instances. The microphone is
    // deliberately never added to it: only soundboard clips reach speakers.
    public void ConfigureMonitor(bool enabled, string outputId, int volumePercent)
    {
        if (outputId == null) throw new ArgumentNullException(nameof(outputId));
        if (volumePercent is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(volumePercent));
        bool sameRoute = monitorEnabled == enabled && string.Equals(monitorOutputId, outputId, StringComparison.OrdinalIgnoreCase);
        monitorEnabled = enabled;
        monitorOutputId = outputId;
        monitorVolumePercent = volumePercent;
        MonitorError = null;
        if (!IsRunning) return;
        if (sameRoute && monitorPlayback != null)
        {
            foreach (var item in playing.Values)
                if (item.MonitorVolume != null) item.MonitorVolume.Volume = volumePercent / 100f;
            return;
        }
        StopMonitor();
        if (!enabled || string.IsNullOrWhiteSpace(outputId)) return;
        try { StartMonitor(); }
        catch
        {
            StopMonitor();
            throw;
        }
    }

    private void StartMonitor()
    {
        monitorDevice = devices.GetDevice(monitorOutputId);
        var output = monitorDevice;
        if (output.State != DeviceState.Active || output.DataFlow != DataFlow.Render)
            throw new IOException("La sortie d’écoute est débranchée ou n’est pas un haut-parleur.");
        if (VirtualCableEndpoints.IsCableDevice(output.DeviceFriendlyName) || VirtualCableEndpoints.IsPlaybackEndpoint(output.FriendlyName))
            throw new InvalidOperationException("Choisis un casque ou des haut-parleurs, pas VB-CABLE, pour le retour audio.");
        var format = output.AudioClient.MixFormat;
        monitorFormat = WaveFormat.CreateIeeeFloatWaveFormat(format.SampleRate, format.Channels);
        monitorMixer = new MixingSampleProvider(monitorFormat) { ReadFully = true };
        monitorMixer.MixerInputEnded += (_, e) => { monitorFinished.Enqueue(e.SampleProvider); StateChanged?.Invoke(); };
        monitorStopping = false;
        monitorPlayback = new WasapiOut(output, AudioClientShareMode.Shared, true, RealtimeAudio.PlaybackBufferMilliseconds);
        monitorPlayback.PlaybackStopped += MonitorStopped;
        monitorPlayback.Init(monitorMixer);
        foreach (var item in playing.Values) AddMonitorClip(item, resume: true);
        monitorPlayback.Play();
    }

    private void AddMonitorClip(Playing item, bool resume = false)
    {
        if (monitorMixer == null || monitorFormat == null) return;
        AudioFileReader? reader = null;
        try
        {
            reader = new AudioFileReader(item.Path);
            if (resume)
            {
                double seconds = (Stopwatch.GetTimestamp() - item.StartedAt) / (double)Stopwatch.Frequency;
                reader.CurrentTime = TimeSpan.FromSeconds(Math.Min(seconds, reader.TotalTime.TotalSeconds));
            }
            var volume = new VolumeSampleProvider(RealtimeAudio.Convert(reader, monitorFormat)) { Volume = monitorVolumePercent / 100f };
            monitorMixer.AddMixerInput(volume);
            item.MonitorReader = reader;
            item.MonitorSamples = volume;
            item.MonitorVolume = volume;
        }
        catch { reader?.Dispose(); throw; }
    }

    private void StopMonitorClip(Playing item)
    {
        // Speaker cleanup must never interrupt the virtual microphone path.
        try { if (item.MonitorSamples != null) monitorMixer?.RemoveMixerInput(item.MonitorSamples); }
        catch (Exception) { }
        try { item.MonitorReader?.Dispose(); }
        catch (Exception) { }
        item.MonitorReader = null;
        item.MonitorSamples = null;
        item.MonitorVolume = null;
    }

    private void StopMonitor()
    {
        monitorStopping = true;
        var oldPlayback = monitorPlayback;
        monitorPlayback = null;
        if (oldPlayback != null)
        {
            oldPlayback.PlaybackStopped -= MonitorStopped;
            try { oldPlayback.Stop(); }
            catch (Exception) { }
            try { oldPlayback.Dispose(); }
            catch (Exception) { }
        }
        foreach (var item in playing.Values) StopMonitorClip(item);
        monitorMixer = null; monitorFormat = null;
        try { monitorDevice?.Dispose(); }
        catch (Exception) { }
        monitorDevice = null;
        monitorFinished.Clear(); monitorFaults.Clear();
    }

    public void Start(string microphoneId, string outputId)
    {
        Stop();
        MonitorError = null;
        if (string.IsNullOrWhiteSpace(microphoneId) || string.IsNullOrWhiteSpace(outputId)) throw new InvalidOperationException("Choisis un micro physique et la sortie d’un câble audio virtuel.");
        try
        {
            microphoneDevice = devices.GetDevice(microphoneId);
            outputDevice = devices.GetDevice(outputId);
            var microphone = microphoneDevice; var output = outputDevice;
            if (microphone.State != DeviceState.Active || output.State != DeviceState.Active) throw new IOException("Un périphérique audio sélectionné est débranché.");
            stopping = false;
            capture = new WasapiCapture(microphone, true, RealtimeAudio.CaptureBufferMilliseconds);
            capture.RecordingStopped += AudioStopped;
            var format = output.AudioClient.MixFormat;
            mixFormat = WaveFormat.CreateIeeeFloatWaveFormat(format.SampleRate, format.Channels);
            voiceBuffer = new RealtimeAudioBuffer(capture.WaveFormat);
            mixer = new MixingSampleProvider(mixFormat) { ReadFully = true };
            mixer.MixerInputEnded += (_, e) => { finished.Enqueue(e.SampleProvider); StateChanged?.Invoke(); };
            mixer.AddMixerInput(RealtimeAudio.Convert(voiceBuffer, mixFormat));
            capture.DataAvailable += (_, data) => voiceBuffer?.AddSamples(data.Buffer, 0, data.BytesRecorded);
            playback = new WasapiOut(output, AudioClientShareMode.Shared, true, RealtimeAudio.PlaybackBufferMilliseconds);
            playback.PlaybackStopped += AudioStopped;
            playback.Init(mixer);
            capture.StartRecording();
            playback.Play();
            if (monitorEnabled && !string.IsNullOrWhiteSpace(monitorOutputId))
            {
                try { StartMonitor(); }
                catch (Exception ex)
                {
                    StopMonitor();
                    MonitorError = "Retour soundboard indisponible : " + ex.Message;
                    StateChanged?.Invoke();
                }
            }
        }
        catch { Stop(); throw; }
    }

    public bool Toggle(Guid id, string path)
    {
        if (mixer == null || mixFormat == null) throw new InvalidOperationException("Active d’abord le micro virtuel.");
        if (playing.Remove(id, out var previous))
        {
            bool wasPlaying = previous.Reader.Position < previous.Reader.Length;
            StopMonitorClip(previous);
            mixer.RemoveMixerInput(previous.Samples);
            previous.Reader.Dispose();
            if (wasPlaying) { StateChanged?.Invoke(); return false; }
        }
        if (!File.Exists(path) || !(path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))) throw new IOException("Fichier MP3 ou WAV introuvable.");
        AudioFileReader? reader = null;
        try
        {
            reader = new AudioFileReader(path);
            var samples = RealtimeAudio.Convert(reader, mixFormat);
            mixer.AddMixerInput(samples);
            var item = new Playing(reader, samples, path);
            playing.Add(id, item);
            if (monitorMixer != null)
            {
                try { AddMonitorClip(item); }
                catch (Exception ex)
                {
                    StopMonitor();
                    MonitorError = "Retour soundboard indisponible : " + ex.Message;
                }
            }
            StateChanged?.Invoke();
            return true;
        }
        catch { reader?.Dispose(); throw; }
    }

    public void StopClip(Guid id)
    {
        if (!playing.Remove(id, out var item)) return;
        StopMonitorClip(item);
        mixer?.RemoveMixerInput(item.Samples); item.Reader.Dispose();
    }

    public void Stop()
    {
        stopping = true;
        StopMonitor();
        capture?.StopRecording(); capture?.Dispose(); capture = null;
        playback?.Stop(); playback?.Dispose(); playback = null;
        foreach (var item in playing.Values) { item.Reader.Dispose(); }
        playing.Clear();
        finished.Clear(); faults.Clear();
        voiceBuffer = null; mixer = null; mixFormat = null;
        microphoneDevice?.Dispose(); microphoneDevice = null; outputDevice?.Dispose(); outputDevice = null;
    }
    public void Dispose() { Stop(); devices.Dispose(); }
}
