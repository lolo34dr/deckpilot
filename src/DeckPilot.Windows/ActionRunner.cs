using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public static class ActionRunner
{
    public static void Run(ButtonAction action)
    {
        switch (action.Kind)
        {
            case ActionKind.Profile: throw new InvalidOperationException("Les profils utilisent la navigation DeckPilot.");
            case ActionKind.System:
                RunSystemCommand(action.Target);
                break;
            case ActionKind.Microphone:
            case ActionKind.AudioOutput: throw new InvalidOperationException("Les commandes audio utilisent le service Windows Audio.");
            case ActionKind.Spotify: throw new InvalidOperationException("Les commandes Spotify utilisent la session dédiée.");
            case ActionKind.Passthrough: return;
            case ActionKind.Application:
                if (ApplicationTarget.IsPackaged(action.Target))
                {
                    var launcher = new ProcessStartInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe")) { UseShellExecute = false };
                    launcher.ArgumentList.Add("shell:AppsFolder\\" + ApplicationTarget.GetAppId(action.Target));
                    Process.Start(launcher)?.Dispose();
                    break;
                }
                if (!File.Exists(action.Target)) throw new IOException("Le programme sélectionné est introuvable.");
                Process.Start(new ProcessStartInfo(action.Target) { UseShellExecute = true, Arguments = action.Arguments, WorkingDirectory = Path.GetDirectoryName(action.Target)! })?.Dispose();
                break;
            case ActionKind.Folder:
                if (!Directory.Exists(action.Target)) throw new IOException("Le dossier sélectionné est introuvable.");
                Process.Start(new ProcessStartInfo(action.Target) { UseShellExecute = true })?.Dispose();
                break;
            case ActionKind.Website:
                if (!Uri.TryCreate(action.Target, UriKind.Absolute, out var uri) || (uri.Scheme != "https" && uri.Scheme != "http"))
                    throw new ArgumentException("Saisissez une adresse commençant par https:// ou http://.");
                Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true })?.Dispose();
                break;
            case ActionKind.Shortcut:
                if (!IsShortcutKey(action.Key)) throw new ArgumentException("Choisissez une touche pour le raccourci.");
                SendChord(action);
                break;
            case ActionKind.Media:
                if (action.Key is not (0xAD or 0xAE or 0xAF or 0xB0 or 0xB1 or 0xB3)) throw new ArgumentException("Choisissez une commande multimédia.");
                SendKeys([action.Key]);
                break;
            default: throw new InvalidOperationException("Ce type d’action n’est pas pris en charge.");
        }
    }
    private static void RunSystemCommand(string id)
    {
        _ = SystemActions.Get(id);
        if (id == "lock")
        {
            if (!LockWorkStation()) throw new Win32Exception(Marshal.GetLastWin32Error());
            return;
        }
        string target = id switch
        {
            "settings" => "ms-settings:",
            "sound-settings" => "ms-settings:sound",
            "display-settings" => "ms-settings:display",
            "bluetooth-settings" => "ms-settings:bluetooth",
            "network-settings" => "ms-settings:network-status",
            "task-manager" => Path.Combine(Environment.SystemDirectory, "Taskmgr.exe"),
            _ => throw new InvalidOperationException("Choisissez une commande Windows.")
        };
        Process.Start(new ProcessStartInfo(target) { UseShellExecute = true })?.Dispose();
    }
    public static bool IsShortcutKey(int key) => key is >= 0x30 and <= 0x39 or >= 0x41 and <= 0x5A or >= 0x70 and <= 0x87 or 0x0D or 0x1B or 0x20 or 0x09 or 0x08 or >= 0x21 and <= 0x28 or 0x2D or 0x2E;
    private static void SendChord(ButtonAction action)
    {
        // Do not release or reuse modifiers currently held by the user.
        int[] modifiers = [0x11, 0x12, 0x10, 0x5B, 0x5C];
        if (modifiers.Any(k => (GetAsyncKeyState(k) & 0x8000) != 0))
            throw new InvalidOperationException("Relâchez Ctrl, Alt, Maj et Windows avant d’utiliser ce raccourci.");
        var keys = new List<int>();
        if (action.Ctrl) keys.Add(0x11);
        if (action.Alt) keys.Add(0x12);
        if (action.Shift) keys.Add(0x10);
        if (action.Win) keys.Add(0x5B);
        keys.Add(action.Key);
        SendKeys(keys);
    }
    private static void SendKeys(IReadOnlyList<int> keys)
    {
        var inputs = keys.Select(k => InputFor(k, false)).Concat(keys.Reverse().Select(k => InputFor(k, true))).ToArray();
        if (SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<Input>()) != inputs.Length)
        {
            // Best effort release if Windows accepted only part of the chord.
            var releases = keys.Reverse().Select(k => InputFor(k, true)).ToArray();
            SendInput((uint)releases.Length, releases, Marshal.SizeOf<Input>());
            throw new Win32Exception("Windows a refusé le raccourci. L’application cible peut être exécutée comme administrateur.");
        }
    }
    private static Input InputFor(int key, bool up) => new() { Type = 1, Data = new InputUnion { Keyboard = new KeyboardInput { Key = (ushort)key, Flags = (up ? 2u : 0u) | (key is >= 0x21 and <= 0x2E or 0x5B or 0x5C or >= 0xAD and <= 0xB3 ? 1u : 0u) } } };
    [StructLayout(LayoutKind.Sequential)] private struct Input { public uint Type; public InputUnion Data; }
    [StructLayout(LayoutKind.Explicit)] private struct InputUnion
    {
        [FieldOffset(0)] public KeyboardInput Keyboard;
        [FieldOffset(0)] public MouseInput Mouse;
    }
    [StructLayout(LayoutKind.Sequential)] private struct KeyboardInput { public ushort Key, Scan; public uint Flags, Time; public UIntPtr Extra; }
    [StructLayout(LayoutKind.Sequential)] private struct MouseInput { public int X, Y; public uint MouseData, Flags, Time; public UIntPtr Extra; }
    [DllImport("user32.dll", SetLastError = true)] private static extern uint SendInput(uint count, Input[] inputs, int size);
    [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int key);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool LockWorkStation();
}
