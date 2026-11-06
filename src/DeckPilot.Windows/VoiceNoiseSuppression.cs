using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DeckPilot.Windows;

internal interface INoiseFrameDenoiser : IDisposable
{
    void ProcessFrame(float[] input, float[] output);
}

// RNNoise uses 480 mono samples at 48 kHz per call and expects float samples
// in signed-16-bit amplitude units. Keep that scaling inside this boundary.
internal sealed unsafe class RnNoiseDenoiser : INoiseFrameDenoiser
{
    public const int FrameSize = 480;
    private const string LibraryName = "rnnoise";
    private const string EmbeddedLibraryName = "DeckPilot.Native.rnnoise.dll";
    private static readonly float SignalScale = short.MaxValue;
    private static readonly float SignalScaleInverse = 1f / short.MaxValue;
    private readonly float[] nativeInput = new float[FrameSize];
    private readonly IntPtr state;
    private bool disposed;

    static RnNoiseDenoiser()
    {
        NativeLibrary.SetDllImportResolver(typeof(RnNoiseDenoiser).Assembly, ResolveNativeLibrary);
    }

    public RnNoiseDenoiser()
    {
        state = Create(IntPtr.Zero);
        if (state == IntPtr.Zero) throw new InvalidOperationException("RNNoise n’a pas pu créer son état de traitement.");
    }

    public void ProcessFrame(float[] input, float[] output)
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        if (input == null || input.Length != FrameSize) throw new ArgumentException("RNNoise requiert une trame de 480 échantillons.", nameof(input));
        if (output == null || output.Length < FrameSize) throw new ArgumentException("La sortie RNNoise doit contenir 480 échantillons.", nameof(output));

        for (int i = 0; i < FrameSize; i++)
        {
            float sample = float.IsFinite(input[i]) ? Math.Clamp(input[i], -1f, 1f) : 0f;
            nativeInput[i] = sample * SignalScale;
        }

        fixed (float* source = nativeInput)
        fixed (float* destination = output)
            _ = Process(state, destination, source);

        for (int i = 0; i < FrameSize; i++)
            output[i] = float.IsFinite(output[i]) ? output[i] * SignalScaleInverse : 0f;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        Destroy(state);
    }

    private static IntPtr ResolveNativeLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (!string.Equals(libraryName, LibraryName, StringComparison.Ordinal)) return IntPtr.Zero;
        using Stream? resource = assembly.GetManifestResourceStream(EmbeddedLibraryName);
        if (resource == null) return NativeLibrary.Load(libraryName, assembly, searchPath);

        using var memory = new MemoryStream();
        resource.CopyTo(memory);
        byte[] image = memory.ToArray();
        string hash = Convert.ToHexString(SHA256.HashData(image)).ToLowerInvariant();
        string root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (string.IsNullOrWhiteSpace(root)) root = Path.GetTempPath();
        string directory = Path.Combine(root, "DeckPilot", "Native", hash[..16]);
        string path = Path.Combine(directory, "rnnoise.dll");
        Directory.CreateDirectory(directory);

        bool currentMatches = File.Exists(path) && CryptographicOperations.FixedTimeEquals(
            SHA256.HashData(File.ReadAllBytes(path)), Convert.FromHexString(hash));
        if (!currentMatches)
        {
            string temporary = Path.Combine(directory, "rnnoise." + Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                File.WriteAllBytes(temporary, image);
                File.Move(temporary, path, overwrite: true);
            }
            finally
            {
                try { if (File.Exists(temporary)) File.Delete(temporary); }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
        }
        return NativeLibrary.Load(path);
    }

    [DllImport(LibraryName, EntryPoint = "rnnoise_create", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Create(IntPtr model);

    [DllImport(LibraryName, EntryPoint = "rnnoise_destroy", CallingConvention = CallingConvention.Cdecl)]
    private static extern void Destroy(IntPtr denoiseState);

    [DllImport(LibraryName, EntryPoint = "rnnoise_process_frame", CallingConvention = CallingConvention.Cdecl)]
    private static extern float Process(IntPtr denoiseState, float* output, float* input);
}

// This provider is placed only on the physical-microphone branch. Clips are
// added separately to the virtual-microphone mixer and never pass through it.
internal sealed class NoiseSuppressionSampleProvider : ISampleProvider
{
    private readonly ISampleProvider source;
    private readonly INoiseFrameDenoiser denoiser;
    private readonly float[] input = new float[RnNoiseDenoiser.FrameSize];
    private readonly float[] processed = new float[RnNoiseDenoiser.FrameSize];
    private int strengthPercent;
    private int position = RnNoiseDenoiser.FrameSize;
    private int available;

    public WaveFormat WaveFormat { get; }

    public NoiseSuppressionSampleProvider(ISampleProvider source, INoiseFrameDenoiser denoiser, int strengthPercent = 75)
    {
        this.source = source ?? throw new ArgumentNullException(nameof(source));
        this.denoiser = denoiser ?? throw new ArgumentNullException(nameof(denoiser));
        if (source.WaveFormat.SampleRate != 48000 || source.WaveFormat.Channels != 1)
            throw new ArgumentException("La réduction de bruit requiert un signal mono à 48 kHz.", nameof(source));
        StrengthPercent = strengthPercent;
        WaveFormat = source.WaveFormat;
    }

    public int StrengthPercent
    {
        get => Volatile.Read(ref strengthPercent);
        set
        {
            if (value is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(value));
            Volatile.Write(ref strengthPercent, value);
        }
    }

    public int Read(float[] buffer, int offset, int count)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0 || count < 0 || offset > buffer.Length - count) throw new ArgumentOutOfRangeException(nameof(count));

        if (StrengthPercent == 0)
        {
            position = RnNoiseDenoiser.FrameSize;
            available = 0;
            return source.Read(buffer, offset, count);
        }

        int written = 0;
        while (written < count)
        {
            if (position >= available)
            {
                int read = source.Read(input, 0, input.Length);
                if (read <= 0) break;
                if (read > input.Length) throw new InvalidDataException("Le microphone a renvoyé une trame audio trop longue.");
                if (read < input.Length) Array.Clear(input, read, input.Length - read);
                denoiser.ProcessFrame(input, processed);
                float strength = StrengthPercent / 100f;
                float dry = 1f - strength;
                for (int i = 0; i < read; i++) processed[i] = input[i] * dry + processed[i] * strength;
                available = read;
                position = 0;
            }

            int copied = Math.Min(count - written, available - position);
            Array.Copy(processed, position, buffer, offset + written, copied);
            position += copied;
            written += copied;
        }
        return written;
    }
}

internal sealed class SelectableSampleProvider : ISampleProvider
{
    private ISampleProvider active;
    public WaveFormat WaveFormat { get; }

    public SelectableSampleProvider(ISampleProvider initial, WaveFormat format)
    {
        active = initial ?? throw new ArgumentNullException(nameof(initial));
        WaveFormat = format ?? throw new ArgumentNullException(nameof(format));
        Select(initial);
    }

    public void Select(ISampleProvider provider)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));
        if (provider.WaveFormat.SampleRate != WaveFormat.SampleRate || provider.WaveFormat.Channels != WaveFormat.Channels)
            throw new ArgumentException("Les voies du microphone doivent utiliser le même format.", nameof(provider));
        Volatile.Write(ref active, provider);
    }

    public int Read(float[] buffer, int offset, int count) => Volatile.Read(ref active).Read(buffer, offset, count);
}
