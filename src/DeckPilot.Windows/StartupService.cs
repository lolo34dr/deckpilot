using System;
using Microsoft.Win32;

namespace DeckPilot.Windows;
public static class StartupService
{
    private const string Run = @"Software\Microsoft\Windows\CurrentVersion\Run";
    public static bool Enabled
    {
        get { using var key = Registry.CurrentUser.OpenSubKey(Run); return key?.GetValue("DeckPilot") != null; }
    }
    public static void Initialize()
    {
        using var preferences = Registry.CurrentUser.CreateSubKey(@"Software\DeckPilot");
        if (preferences.GetValue("StartupConfigured") == null) Set(true);
        else if (Enabled) Set(true); // Refresh an old portable path after installation or an upgrade.
    }
    public static void Set(bool enabled)
    {
        using var key = Registry.CurrentUser.CreateSubKey(Run);
        if (enabled) key.SetValue("DeckPilot", "\"" + UpdateService.ExecutablePath() + "\" --background");
        else key.DeleteValue("DeckPilot", false);
        using var preferences = Registry.CurrentUser.CreateSubKey(@"Software\DeckPilot");
        preferences.SetValue("StartupConfigured", 1);
    }
    public static void RemoveInstalledEntry()
    {
        using var key = Registry.CurrentUser.OpenSubKey(Run, writable: true);
        string command = key?.GetValue("DeckPilot") as string ?? "";
        if (command.Equals("\"" + UpdateService.ExecutablePath() + "\" --background", StringComparison.OrdinalIgnoreCase)) key!.DeleteValue("DeckPilot", false);
    }
}
