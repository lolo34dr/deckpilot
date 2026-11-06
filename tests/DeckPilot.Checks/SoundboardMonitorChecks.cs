using NAudio.Wave;
using NAudio.Wave.SampleProviders;

internal static class SoundboardMonitorChecks
{
    private sealed class Samples(float[] values, int channels) : ISampleProvider
    {
        private int position;
        public WaveFormat WaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(48000, channels);

        public int Read(float[] buffer, int offset, int count)
        {
            int read = Math.Min(count, values.Length - position);
            Array.Copy(values, position, buffer, offset, read);
            position += read;
            return read;
        }
    }

    public static int Run()
    {
        int passed = 0;
        void Check(string name, Action test)
        {
            test();
            passed++;
            Console.WriteLine($"PASS {name}");
        }
        void Expect(bool condition)
        {
            if (!condition) throw new Exception("Soundboard monitor assertion failed");
        }

        Check("Soundboard return preserves the endpoint's extensible PCM format", () =>
        {
            var endpoint = new WaveFormatExtensible(48000, 16, 2);
            var provider = new DeckPilot.Windows.SoundboardMonitorWaveProvider(new Samples([-1f, 1f], 2), endpoint);
            var output = new byte[4];
            Expect(ReferenceEquals(provider.WaveFormat, endpoint));
            Expect(provider.Read(output, 0, output.Length) == 4);
            Expect(output.SequenceEqual(new byte[] { 0, 128, 255, 127 }));
        });

        Check("Soundboard return converts PCM24 with correct signed little-endian samples", () =>
        {
            var endpoint = new WaveFormatExtensible(48000, 24, 2);
            var provider = new DeckPilot.Windows.SoundboardMonitorWaveProvider(new Samples([-1f, 1f], 2), endpoint);
            var output = new byte[6];
            Expect(provider.Read(output, 0, output.Length) == 6);
            Expect(output.SequenceEqual(new byte[] { 0, 0, 128, 255, 255, 127 }));
        });

        Check("Soundboard return keeps extensible IEEE float samples unchanged", () =>
        {
            var endpoint = new WaveFormatExtensible(48000, 32, 2);
            var provider = new DeckPilot.Windows.SoundboardMonitorWaveProvider(new Samples([0.25f, -0.5f], 2), endpoint);
            var output = new byte[8];
            Expect(provider.Read(output, 0, output.Length) == 8);
            Expect(output.SequenceEqual(BitConverter.GetBytes(0.25f).Concat(BitConverter.GetBytes(-0.5f))));
        });

        Check("Soundboard return maps PCM8 silence and clips to the unsigned range", () =>
        {
            var endpoint = new WaveFormat(48000, 8, 1);
            var provider = new DeckPilot.Windows.SoundboardMonitorWaveProvider(new Samples([-1f, 0f, 1f, float.NaN], 1), endpoint);
            var output = new byte[4];
            Expect(provider.Read(output, 0, output.Length) == 4);
            Expect(output.SequenceEqual(new byte[] { 0, 128, 255, 128 }));
        });

        Check("Soundboard return rejects partial frames and mismatched endpoint channels", () =>
        {
            var provider = new DeckPilot.Windows.SoundboardMonitorWaveProvider(
                new Samples([0f, 0f], 2), new WaveFormatExtensible(48000, 16, 2));
            bool partialRejected = false;
            try { provider.Read(new byte[4], 0, 2); }
            catch (ArgumentOutOfRangeException) { partialRejected = true; }
            bool channelsRejected = false;
            try { _ = new DeckPilot.Windows.SoundboardMonitorWaveProvider(new Samples([0f], 1), new WaveFormatExtensible(48000, 16, 2)); }
            catch (ArgumentException) { channelsRejected = true; }
            Expect(partialRejected && channelsRejected);
        });

        return passed;
    }
}
