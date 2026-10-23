using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public enum MicrophoneState { None, Active, Muted, Mixed, Unavailable }
public sealed record AudioSnapshot(IReadOnlyList<AudioDevice> Outputs, string DefaultOutput, MicrophoneState Microphones, string Error = "");
public static class WindowsAudio
{
    private static readonly object Gate = new();
    public static AudioSnapshot Read()
    {
        lock (Gate)
        {
            try
            {
                using var backend = new Backend();
                var outputs = backend.Outputs(); string current = backend.DefaultOutput(0);
                try
                {
                    var mics = backend.Microphones();
                    int muted = mics.Count(m => backend.IsMuted(m.Id));
                    return new(outputs, current, mics.Count == 0 ? MicrophoneState.None : muted == mics.Count ? MicrophoneState.Muted : muted == 0 ? MicrophoneState.Active : MicrophoneState.Mixed);
                }
                catch (Exception ex) { return new(outputs, current, MicrophoneState.Unavailable, ex.Message); }
            }
            catch (Exception ex) { return new([], "", MicrophoneState.Unavailable, ex.Message); }
        }
    }
    public static void ToggleMicrophones()
    {
        lock (Gate) { using var backend = new Backend(); AudioCommands.ToggleMicrophones(backend); }
    }
    public static void ToggleOutput(string primary, string secondary)
    {
        lock (Gate) { using var backend = new Backend(); AudioCommands.ToggleOutput(backend, primary, secondary); }
    }
    private static void Check(int result) => Marshal.ThrowExceptionForHR(result);
    private static void Release(object? instance) { if (instance != null && Marshal.IsComObject(instance)) Marshal.ReleaseComObject(instance); }
    private sealed class Backend : IAudioBackend, IDisposable
    {
        private readonly IMMDeviceEnumerator enumerator = (IMMDeviceEnumerator)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("BCDE0395-E52F-467C-8E3D-C4579291692E"))!)!;
        public IReadOnlyList<AudioDevice> Outputs() => Devices(0);
        public IReadOnlyList<AudioDevice> Microphones() => Devices(1);
        private IReadOnlyList<AudioDevice> Devices(int flow)
        {
            Check(enumerator.EnumAudioEndpoints(flow, 1, out var collection));
            try
            {
                Check(collection.GetCount(out uint count)); var result = new List<AudioDevice>();
                for (uint i = 0; i < count; i++)
                {
                    Check(collection.Item(i, out var device));
                    try { Check(device.GetId(out string id)); result.Add(new(id, Name(device, id))); }
                    finally { Release(device); }
                }
                return result;
            }
            finally { Release(collection); }
        }
        private static string Name(IMMDevice device, string fallback)
        {
            IPropertyStore? store = null;
            try
            {
                Check(device.OpenPropertyStore(0, out store));
                var key = new PropertyKey { Format = new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"), Id = 14 };
                Check(store.GetValue(ref key, out var value));
                try { return value.Type == 31 ? Marshal.PtrToStringUni(value.Pointer) ?? fallback : fallback; }
                finally { PropVariantClear(ref value); }
            }
            catch { return fallback; }
            finally { Release(store); }
        }
        public string DefaultOutput(int role)
        {
            int hr = enumerator.GetDefaultAudioEndpoint(0, role, out var device);
            if (hr == unchecked((int)0x80070490)) return "";
            Check(hr);
            try { Check(device.GetId(out string id)); return id; }
            finally { Release(device); }
        }
        public void SetDefaultOutput(string id, int role)
        {
            var policy = (IPolicyConfig)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("870AF99C-171D-4F9E-AF0D-E63DF40C2BC9"))!)!;
            try { Check(policy.SetDefaultEndpoint(id, role)); }
            finally { Release(policy); }
        }
        private T Volume<T>(string id, Func<IAudioEndpointVolume, T> command)
        {
            Check(enumerator.GetDevice(id, out var device)); object? volume = null;
            try
            {
                var iid = typeof(IAudioEndpointVolume).GUID;
                Check(device.Activate(ref iid, 23, IntPtr.Zero, out volume));
                return command((IAudioEndpointVolume)volume);
            }
            finally { Release(volume); Release(device); }
        }
        public bool IsMuted(string id) => Volume(id, v => { Check(v.GetMute(out bool muted)); return muted; });
        public void SetMuted(string id, bool muted) => Volume(id, v => { Guid context = Guid.Empty; Check(v.SetMute(muted, ref context)); return true; });
        public void Dispose() => Release(enumerator);
    }
    [StructLayout(LayoutKind.Sequential)] private struct PropertyKey { public Guid Format; public uint Id; }
    [StructLayout(LayoutKind.Explicit, Size = 24)] private struct PropVariant { [FieldOffset(0)] public ushort Type; [FieldOffset(8)] public IntPtr Pointer; }
    [DllImport("ole32.dll")] private static extern int PropVariantClear(ref PropVariant value);
    [ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceEnumerator
    {
        [PreserveSig] int EnumAudioEndpoints(int flow, uint mask, out IMMDeviceCollection collection);
        [PreserveSig] int GetDefaultAudioEndpoint(int flow, int role, out IMMDevice device);
        [PreserveSig] int GetDevice([MarshalAs(UnmanagedType.LPWStr)] string id, out IMMDevice device);
    }
    [ComImport, Guid("0BD7A1BE-7A1A-44DB-8397-C0A8A7D2532C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceCollection
    {
        [PreserveSig] int GetCount(out uint count);
        [PreserveSig] int Item(uint index, out IMMDevice device);
    }
    [ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDevice
    {
        [PreserveSig] int Activate(ref Guid iid, uint context, IntPtr parameters, [MarshalAs(UnmanagedType.IUnknown)] out object instance);
        [PreserveSig] int OpenPropertyStore(uint mode, out IPropertyStore store);
        [PreserveSig] int GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        [PreserveSig] int GetState(out uint state);
    }
    [ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IPropertyStore
    {
        [PreserveSig] int GetCount(out uint count);
        [PreserveSig] int GetAt(uint index, out PropertyKey key);
        [PreserveSig] int GetValue(ref PropertyKey key, out PropVariant value);
    }
    [ComImport, Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioEndpointVolume
    {
        [PreserveSig] int RegisterControlChangeNotify(IntPtr notify);
        [PreserveSig] int UnregisterControlChangeNotify(IntPtr notify);
        [PreserveSig] int GetChannelCount(out uint count);
        [PreserveSig] int SetMasterVolumeLevel(float value, ref Guid context);
        [PreserveSig] int SetMasterVolumeLevelScalar(float value, ref Guid context);
        [PreserveSig] int GetMasterVolumeLevel(out float value);
        [PreserveSig] int GetMasterVolumeLevelScalar(out float value);
        [PreserveSig] int SetChannelVolumeLevel(uint channel, float value, ref Guid context);
        [PreserveSig] int SetChannelVolumeLevelScalar(uint channel, float value, ref Guid context);
        [PreserveSig] int GetChannelVolumeLevel(uint channel, out float value);
        [PreserveSig] int GetChannelVolumeLevelScalar(uint channel, out float value);
        [PreserveSig] int SetMute([MarshalAs(UnmanagedType.Bool)] bool muted, ref Guid context);
        [PreserveSig] int GetMute([MarshalAs(UnmanagedType.Bool)] out bool muted);
    }
    // Windows' desktop PolicyConfig interface sets default endpoints for all three roles.
    [ComImport, Guid("F8679F50-850A-41CF-9C72-430F290290C8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IPolicyConfig
    {
        [PreserveSig] int GetMixFormat([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr format);
        [PreserveSig] int GetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string id, int defaults, IntPtr format);
        [PreserveSig] int ResetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string id);
        [PreserveSig] int SetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr endpoint, IntPtr mix);
        [PreserveSig] int GetProcessingPeriod([MarshalAs(UnmanagedType.LPWStr)] string id, int defaults, IntPtr period, IntPtr minimum);
        [PreserveSig] int SetProcessingPeriod([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr period);
        [PreserveSig] int GetShareMode([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr mode);
        [PreserveSig] int SetShareMode([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr mode);
        [PreserveSig] int GetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string id, ref PropertyKey key, out PropVariant value);
        [PreserveSig] int SetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string id, ref PropertyKey key, ref PropVariant value);
        [PreserveSig] int SetDefaultEndpoint([MarshalAs(UnmanagedType.LPWStr)] string id, int role);
        [PreserveSig] int SetEndpointVisibility([MarshalAs(UnmanagedType.LPWStr)] string id, int visible);
    }
}
