using DeckPilot.Windows;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

internal static class NoiseSuppressionChecks
{
    private sealed class ConstantSamples(float sample) : ISampleProvider
    {
        public WaveFormat WaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(48000, 1);
        public int Read(float[] buffer, int offset, int count)
        {
            Array.Fill(buffer, sample, offset, count);
            return count;
        }
    }

    private sealed class GainDenoiser(float gain) : INoiseFrameDenoiser
    {
        public void ProcessFrame(float[] input, float[] output)
        {
            for (int i = 0; i < input.Length; i++) output[i] = input[i] * gain;
        }
        public void Dispose() { }
    }

    public static int Run()
    {
        int passed = 0;
        void Check(string name, Action test)
        {
            test();
            passed++;
            Console.WriteLine("PASS " + name);
        }
        void Expect(bool value)
        {
            if (!value) throw new Exception("Noise suppression regression");
        }

        Check("Voice denoising blends to the requested intensity without changing the sample clock", () =>
        {
            using var denoiser = new GainDenoiser(.5f);
            var provider = new NoiseSuppressionSampleProvider(new ConstantSamples(.5f), denoiser, 100);
            var output = new float[120];
            for (int frame = 0; frame < 4; frame++)
            {
                Expect(provider.Read(output, 0, output.Length) == output.Length);
                Expect(output.All(value => Math.Abs(value - .25f) < .0001f));
            }
            provider.StrengthPercent = 50;
            Expect(provider.Read(output, 0, output.Length) == output.Length);
            Expect(output.All(value => Math.Abs(value - .375f) < .0001f));
            provider.StrengthPercent = 0;
            Expect(provider.Read(output, 0, output.Length) == output.Length);
            Expect(output.All(value => Math.Abs(value - .5f) < .0001f));
        });

        Check("The microphone path can switch between untouched and denoised audio without changing its format", () =>
        {
            var format = WaveFormat.CreateIeeeFloatWaveFormat(48000, 1);
            var raw = new ConstantSamples(.2f);
            var filtered = new ConstantSamples(.8f);
            var route = new SelectableSampleProvider(raw, format);
            var output = new float[64];
            Expect(route.Read(output, 0, output.Length) == output.Length && output.All(value => value == .2f));
            route.Select(filtered);
            Expect(route.Read(output, 0, output.Length) == output.Length && output.All(value => value == .8f));
        });

        bool requireNative = Environment.GetEnvironmentVariable("DECKPILOT_REQUIRE_RNNOISE") == "1";
        try
        {
            Check("The bundled RNNoise engine runs 48 kHz frames and suppresses stationary noise", () =>
            {
                using var denoiser = new RnNoiseDenoiser();
                var random = new Random(173);
                var input = new float[RnNoiseDenoiser.FrameSize];
                var output = new float[RnNoiseDenoiser.FrameSize];
                double inputEnergy = 0, outputEnergy = 0;
                int measured = 0;
                for (int frame = 0; frame < 100; frame++)
                {
                    for (int i = 0; i < input.Length; i++) input[i] = (float)(random.NextDouble() * .2 - .1);
                    denoiser.ProcessFrame(input, output);
                    Expect(output.All(float.IsFinite));
                    if (frame < 20) continue;
                    for (int i = 0; i < input.Length; i++)
                    {
                        inputEnergy += input[i] * input[i];
                        outputEnergy += output[i] * output[i];
                        measured++;
                    }
                }
                Expect(measured > 0 && outputEnergy < inputEnergy * .97d);
            });
        }
        catch (Exception ex) when (!requireNative && (ex is DllNotFoundException or EntryPointNotFoundException || ex.InnerException is DllNotFoundException or EntryPointNotFoundException))
        {
            Console.WriteLine("SKIP RNNoise native check (native library not built for this platform)");
        }

        return passed;
    }
}
