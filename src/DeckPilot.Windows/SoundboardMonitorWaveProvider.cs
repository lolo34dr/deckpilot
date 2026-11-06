using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DeckPilot.Windows;

// WASAPI shared mode expects the device's exact mix format. Sample providers
// use ordinary float WAVEFORMATEX values, which can fail for extensible
// endpoint formats (common with USB headsets). Keep the endpoint format and
// convert the monitor mix directly into its PCM/float samples.
internal sealed class SoundboardMonitorWaveProvider : IWaveProvider
{
    private readonly ISampleProvider source;
    private readonly WaveFormat format;
    private readonly bool floatingPoint;
    private readonly int bytesPerSample;
    private readonly int bitsPerSample;
    private float[] samples = Array.Empty<float>();

    public SoundboardMonitorWaveProvider(ISampleProvider source, WaveFormat endpointFormat)
    {
        this.source = source ?? throw new ArgumentNullException(nameof(source));
        format = endpointFormat ?? throw new ArgumentNullException(nameof(endpointFormat));
        WaveFormat standard = endpointFormat is WaveFormatExtensible extensible
            ? extensible.ToStandardWaveFormat()
            : endpointFormat;

        if (source.WaveFormat.SampleRate != standard.SampleRate || source.WaveFormat.Channels != standard.Channels)
            throw new ArgumentException("Le mixeur du retour doit utiliser la fréquence et le nombre de canaux de la sortie.", nameof(source));

        floatingPoint = standard.Encoding == WaveFormatEncoding.IeeeFloat && standard.BitsPerSample == 32;
        if (!floatingPoint && standard.Encoding != WaveFormatEncoding.Pcm)
            throw new NotSupportedException("Le format de sortie audio n’est ni PCM ni flottant.");
        if (standard.BitsPerSample is not (8 or 16 or 24 or 32))
            throw new NotSupportedException($"Profondeur PCM du retour audio non prise en charge : {standard.BitsPerSample} bits.");

        bitsPerSample = standard.BitsPerSample;
        bytesPerSample = standard.BitsPerSample / 8;
    }

    public WaveFormat WaveFormat => format;

    public int Read(byte[] buffer, int offset, int count)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0 || count < 0 || offset > buffer.Length - count || count % format.BlockAlign != 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        int requiredSamples = count / bytesPerSample;
        if (samples.Length < requiredSamples) Array.Resize(ref samples, requiredSamples);
        int readSamples = source.Read(samples, 0, requiredSamples);
        if (floatingPoint)
        {
            Buffer.BlockCopy(samples, 0, buffer, offset, readSamples * bytesPerSample);
            return readSamples * bytesPerSample;
        }

        int write = offset;
        for (int i = 0; i < readSamples; i++)
        {
            float input = samples[i];
            double value = float.IsFinite(input) ? Math.Clamp((double)input, -1d, 1d) : 0d;
            if (bytesPerSample == 1)
            {
                int pcm = (int)Math.Round(value * 127.5d + 127.5d, MidpointRounding.AwayFromZero);
                buffer[write++] = (byte)Math.Clamp(pcm, 0, 255);
                continue;
            }

            long minimum = -(1L << (bitsPerSample - 1));
            long maximum = (1L << (bitsPerSample - 1)) - 1;
            long pcmValue = value <= -1d ? minimum
                : value >= 1d ? maximum
                : (long)Math.Round(value * (value < 0 ? -minimum : maximum), MidpointRounding.AwayFromZero);
            ulong packed = unchecked((ulong)pcmValue);
            for (int b = 0; b < bytesPerSample; b++)
            {
                buffer[write++] = (byte)packed;
                packed >>= 8;
            }
        }
        return readSamples * bytesPerSample;
    }

}
