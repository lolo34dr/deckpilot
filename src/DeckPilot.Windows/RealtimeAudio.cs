using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DeckPilot.Windows;

// Bound the age of microphone audio, not just memory use. If the consumer stalls,
// retain the newest complete frames rather than queueing stale speech.
internal sealed class RealtimeAudioBuffer : IWaveProvider
{
    private readonly object gate = new();
    private readonly byte[] data;
    private int start, length;
    public WaveFormat WaveFormat { get; }
    public RealtimeAudioBuffer(WaveFormat format, int milliseconds = 100)
    {
        if (milliseconds <= 0) throw new ArgumentOutOfRangeException(nameof(milliseconds));
        WaveFormat = format;
        int frames = Math.Max(1, checked(format.SampleRate * milliseconds / 1000));
        data = new byte[checked(frames * format.BlockAlign)];
    }
    public int BufferedBytes { get { lock (gate) return length; } }
    private void Validate(byte[] buffer, int offset, int count)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0 || count < 0 || offset > buffer.Length - count || count % WaveFormat.BlockAlign != 0)
            throw new ArgumentOutOfRangeException(nameof(count));
    }
    public void AddSamples(byte[] buffer, int offset, int count)
    {
        Validate(buffer, offset, count);
        lock (gate)
        {
            if (count >= data.Length) { offset += count - data.Length; count = data.Length; start = length = 0; }
            int drop = Math.Max(0, length + count - data.Length);
            start = (start + drop) % data.Length; length -= drop;
            int end = (start + length) % data.Length;
            int first = Math.Min(count, data.Length - end);
            Buffer.BlockCopy(buffer, offset, data, end, first);
            Buffer.BlockCopy(buffer, offset + first, data, 0, count - first);
            length += count;
        }
    }
    public int Read(byte[] buffer, int offset, int count)
    {
        Validate(buffer, offset, count);
        lock (gate)
        {
            int available = Math.Min(count, length), first = Math.Min(available, data.Length - start);
            Buffer.BlockCopy(data, start, buffer, offset, first);
            Buffer.BlockCopy(data, 0, buffer, offset + first, available - first);
            start = (start + available) % data.Length; length -= available;
            // Keep the live input in the mixer during a short capture underrun.
            Array.Clear(buffer, offset + available, count - available);
        }
        return count;
    }
}

internal static class RealtimeAudio
{
    public const int CaptureBufferMilliseconds = 20;
    public const int PlaybackBufferMilliseconds = 40;
    public static ISampleProvider Convert(IWaveProvider input, WaveFormat output)
    {
        // WASAPI commonly exposes extensible PCM/float descriptors. Normalize
        // the descriptor without changing the captured bytes.
        if (input.WaveFormat is WaveFormatExtensible extensible)
            input = new StandardWaveProvider(input, extensible.ToStandardWaveFormat());
        ISampleProvider samples = input.ToSampleProvider();
        if (samples.WaveFormat.SampleRate != output.SampleRate)
            samples = new WdlResamplingSampleProvider(samples, output.SampleRate);
        if (samples.WaveFormat.Channels != output.Channels)
            samples = new ChannelAdapter(samples, output.Channels);
        return samples;
    }
    private sealed class StandardWaveProvider(IWaveProvider source, WaveFormat format) : IWaveProvider
    {
        public WaveFormat WaveFormat => format;
        public int Read(byte[] buffer, int offset, int count) => source.Read(buffer, offset, count);
    }
    private sealed class ChannelAdapter : ISampleProvider
    {
        private readonly ISampleProvider source;
        private float[] input = Array.Empty<float>();
        public WaveFormat WaveFormat { get; }
        public ChannelAdapter(ISampleProvider source, int channels)
        {
            if (channels < 1) throw new ArgumentOutOfRangeException(nameof(channels));
            this.source = source; WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, channels);
        }
        public int Read(float[] buffer, int offset, int count)
        {
            int frames = count / WaveFormat.Channels;
            int needed = checked(frames * source.WaveFormat.Channels);
            if (input.Length < needed) input = new float[needed];
            int received = source.Read(input, 0, needed) / source.WaveFormat.Channels;
            if (source.WaveFormat.Channels == 1)
            {
                for (int frame = 0; frame < received; frame++)
                {
                    float sample = input[frame];
                    int target = offset + frame * WaveFormat.Channels;
                    for (int channel = 0; channel < WaveFormat.Channels; channel++) buffer[target + channel] = sample;
                }
                return received * WaveFormat.Channels;
            }
            for (int frame = 0; frame < received; frame++)
            {
                float mono = 0;
                for (int channel = 0; channel < source.WaveFormat.Channels; channel++) mono += input[frame * source.WaveFormat.Channels + channel];
                mono /= source.WaveFormat.Channels;
                for (int channel = 0; channel < WaveFormat.Channels; channel++) buffer[offset + frame * WaveFormat.Channels + channel] = mono;
            }
            return received * WaveFormat.Channels;
        }
    }
}
