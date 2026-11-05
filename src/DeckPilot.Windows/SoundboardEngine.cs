using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using DeckPilot.Core;
using System.IO;
using System.Linq;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DeckPilot.Windows;

internal sealed record AudioEndpoint(string Id, string Name, bool IsVirtualCable = false)
{
    public override string ToString() => Name;
}

// The selected playback endpoint must be the input side of a virtual audio cable.
// Its capture side can then be selected as a microphone in Discord or a game.
internal sealed class SoundboardEngine : IDisposable
{
    private sealed record Playing(AudioFileReader Reader, ISampleProvider Samples);
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
    private volatile bool stopping;
    public event Action? StateChanged;
    public bool IsRunning => playback != null;
    private IReadOnlyList<AudioEndpoint> Endpoints(DataFlow flow)
    {
        var result = new List<AudioEndpoint>();
        var collection = devices.EnumerateAudioEndPoints(flow, DeviceState.Active);
        foreach (var device in collection)
        {
            using (device) result.Add(new(device.ID, device.FriendlyName, VirtualCableEndpoints.IsCableDevice(device.DeviceFriendlyName)
                || (flow == DataFlow.Render ? VirtualCableEndpoints.IsPlaybackEndpoint(device.FriendlyName) : VirtualCableEndpoints.IsRecordingEndpoint(device.FriendlyName))));
        }
        return result;
    }
    public IReadOnlyList<AudioEndpoint> Microphones() => Endpoints(DataFlow.Capture);
    public IReadOnlyList<AudioEndpoint> Outputs() => Endpoints(DataFlow.Render);
    public bool IsPlaying(Guid id) => playing.ContainsKey(id);
    // Called on the UI thread: never dispose a reader inside the mixer's audio callback.
    public string? ProcessEvents()
    {
        while (finished.TryDequeue(out var samples))
        {
            var item = playing.FirstOrDefault(p => ReferenceEquals(p.Value.Samples, samples));
            if (item.Value != null) StopClip(item.Key);
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

    public void Start(string microphoneId, string outputId)
    {
        Stop();
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
        }
        catch { Stop(); throw; }
    }

    public bool Toggle(Guid id, string path)
    {
        if (mixer == null || mixFormat == null) throw new InvalidOperationException("Active d’abord le micro virtuel.");
        if (playing.Remove(id, out var previous))
        {
            bool wasPlaying = previous.Reader.Position < previous.Reader.Length;
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
            playing.Add(id, new(reader, samples));
            StateChanged?.Invoke();
            return true;
        }
        catch { reader?.Dispose(); throw; }
    }

    public void StopClip(Guid id)
    {
        if (!playing.Remove(id, out var item)) return;
        mixer?.RemoveMixerInput(item.Samples); item.Reader.Dispose();
    }

    public void Stop()
    {
        stopping = true;
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
