using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public sealed class DeckKeyEventArgs(int key) : EventArgs
{
    public int Key { get; } = key;
    public bool Handled { get; set; }
}
public sealed class KeyboardHook : IDisposable
{
    private delegate IntPtr HookProc(int code, IntPtr message, IntPtr data);
    private readonly HookProc callback;
    private readonly DeckKeyState state = new();
    private IntPtr handle;
    public event EventHandler<DeckKeyEventArgs>? KeyPressed;
    public event EventHandler<DeckKeyEventArgs>? KeyReleased;
    public KeyboardHook()
    {
        callback = OnKey;
        using var process = Process.GetCurrentProcess();
        using var module = process.MainModule!;
        handle = SetWindowsHookEx(13, callback, GetModuleHandle(module.ModuleName), 0);
        if (handle == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
    }
    private IntPtr OnKey(int code, IntPtr message, IntPtr data)
    {
        if (code < 0) return CallNextHookEx(handle, code, message, data);
        var input = Marshal.PtrToStructure<KeyboardData>(data);
        int key = (int)input.Key;
        if ((input.Flags & 0x10) != 0 || !Calibration.IsDeckKey(key))
            return CallNextHookEx(handle, code, message, data);
        long msg = message.ToInt64();
        if (msg is 0x100 or 0x104 or 0x101 or 0x105)
        {
            bool suppress = state.Process(key, msg is 0x100 or 0x104, pressedKey =>
            {
                var args = new DeckKeyEventArgs(pressedKey);
                KeyPressed?.Invoke(this, args);
                return args.Handled;
            }, releasedKey => KeyReleased?.Invoke(this, new DeckKeyEventArgs(releasedKey)));
            if (suppress) return (IntPtr)1;
        }
        return CallNextHookEx(handle, code, message, data);
    }
    public void Dispose()
    {
        if (handle != IntPtr.Zero) { UnhookWindowsHookEx(handle); handle = IntPtr.Zero; }
        GC.KeepAlive(callback);
    }
    [StructLayout(LayoutKind.Sequential)] private struct KeyboardData { public uint Key, Scan, Flags, Time; public UIntPtr Extra; }
    [DllImport("user32.dll", SetLastError = true)] private static extern IntPtr SetWindowsHookEx(int id, HookProc callback, IntPtr module, uint thread);
    [DllImport("user32.dll")] private static extern bool UnhookWindowsHookEx(IntPtr hook);
    [DllImport("user32.dll")] private static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr message, IntPtr data);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string? name);
}
