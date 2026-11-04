using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DeckPilot.Windows;

internal sealed record AudioEndpoint(string Id, string Name)
{
    public override string ToString() => Name;
}

// The selected playback endpoint must be the input side of a virtual audio cable.
// Its capture side can then be selected as a microphone in Discord or a game.
internal sealed class SoundboardEngine : IDisposable
{
    private sealed record Playing(AudioFileReader Reader, MediaFoundationResampler Converter, ISampleProvider Samples);
    private readonly MMDeviceEnumerator devices = new();
    private readonly Dictionary<Guid, Playing> playing = new();
    private MMDevice? microphoneDevice;
    private MMDevice? outputDevice;
    private WasapiCapture? capture;
    private BufferedWaveProvider? voiceBuffer;
    private MediaFoundationResampler? voiceConverter;
    private WasapiOut? playback;
    private MixingSampleProvider? mixer;
    private WaveFormat? mixFormat;
    public bool IsRunning => playback != null;
    public IReadOnlyList<AudioEndpoint> Microphones() => devices.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).Select(d => new AudioEndpoint(d.ID, d.FriendlyName)).ToArray();
    public IReadOnlyList<AudioEndpoint> Outputs() => devices.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Select(d => new AudioEndpoint(d.ID, d.FriendlyName)).ToArray();

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
            capture = new WasapiCapture(microphone);
            var format = output.AudioClient.MixFormat;
            mixFormat = WaveFormat.CreateIeeeFloatWaveFormat(format.SampleRate, format.Channels);
            voiceBuffer = new BufferedWaveProvider(capture.WaveFormat) { BufferDuration = TimeSpan.FromSeconds(2), DiscardOnBufferOverflow = true, ReadFully = true };
            voiceConverter = new MediaFoundationResampler(voiceBuffer, mixFormat) { ResamplerQuality = 30 };
            mixer = new MixingSampleProvider(mixFormat) { ReadFully = true };
            mixer.AddMixerInput(voiceConverter.ToSampleProvider());
            capture.DataAvailable += (_, data) => voiceBuffer?.AddSamples(data.Buffer, 0, data.BytesRecorded);
            playback = new WasapiOut(output, AudioClientShareMode.Shared, true, 100);
            playback.Init(mixer);
            playback.Play();
            capture.StartRecording();
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
            previous.Converter.Dispose(); previous.Reader.Dispose();
            if (wasPlaying) return false;
        }
        if (!File.Exists(path) || !(path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))) throw new IOException("Fichier MP3 ou WAV introuvable.");
        AudioFileReader? reader = null;
        MediaFoundationResampler? converter = null;
        try
        {
            reader = new AudioFileReader(path);
            converter = new MediaFoundationResampler(reader, mixFormat) { ResamplerQuality = 30 };
            var samples = converter.ToSampleProvider();
            mixer.AddMixerInput(samples);
            playing.Add(id, new(reader, converter, samples));
            return true;
        }
        catch { converter?.Dispose(); reader?.Dispose(); throw; }
    }

    public void StopClip(Guid id)
    {
        if (!playing.Remove(id, out var item)) return;
        mixer?.RemoveMixerInput(item.Samples); item.Converter.Dispose(); item.Reader.Dispose();
    }

    public void Stop()
    {
        capture?.StopRecording(); capture?.Dispose(); capture = null;
        playback?.Stop(); playback?.Dispose(); playback = null;
        foreach (var item in playing.Values) { item.Converter.Dispose(); item.Reader.Dispose(); }
        playing.Clear();
        voiceConverter?.Dispose(); voiceConverter = null; voiceBuffer = null; mixer = null; mixFormat = null;
        microphoneDevice?.Dispose(); microphoneDevice = null; outputDevice?.Dispose(); outputDevice = null;
    }
    public void Dispose() { Stop(); devices.Dispose(); }
}
