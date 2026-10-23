using System;
using Microsoft.Win32;

namespace DeckPilot.Windows;
public static class StartupService
{
    private const string Run = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string Preferences = @"Software\DeckPilot";
    private static readonly string[] Approvals = [@"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run", @"Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run32"];
    private static string Command => "\"" + UpdateService.ExecutablePath() + "\" --background";
    public static bool BlockedByWindows
    {
        get
        {
            foreach (string path in Approvals)
            {
                using var key = Registry.CurrentUser.OpenSubKey(path);
                if (key?.GetValue("DeckPilot") is byte[] bytes && bytes.Length >= 4 && bytes[0] is 3 or 7) return true;
            }
            return false;
        }
    }
    public static bool Enabled
    {
        get { using var key = Registry.CurrentUser.OpenSubKey(Run); return string.Equals(key?.GetValue("DeckPilot") as string, Command, StringComparison.OrdinalIgnoreCase) && !BlockedByWindows; }
    }
    public static string Status => BlockedByWindows ? "Windows bloque ce démarrage. Réactivez l’option ici ou dans Paramètres Windows > Applications > Démarrage."
        : Enabled ? "Activé pour votre session Windows · Démarre près de l’horloge."
        : "Démarrage automatique désactivé pour votre session Windows.";
    public static void Initialize()
    {
        using var preferences = Registry.CurrentUser.CreateSubKey(Preferences);
        if (preferences.GetValue("StartupEnabled") == null)
        {
            using var run = Registry.CurrentUser.OpenSubKey(Run);
            bool enabled = preferences.GetValue("StartupConfigured") == null || run?.GetValue("DeckPilot") != null;
            preferences.SetValue("StartupEnabled", enabled ? 1 : 0);
            preferences.SetValue("StartupConfigured", 1);
        }
        // Repair a stale/missing entry from the saved preference, but respect a Windows block.
        if (Convert.ToInt32(preferences.GetValue("StartupEnabled", 0)) == 1)
        {
            using var run = Registry.CurrentUser.CreateSubKey(Run);
            run.SetValue("DeckPilot", Command);
        }
    }
    public static void Set(bool enabled)
    {
        using var key = Registry.CurrentUser.CreateSubKey(Run);
        if (enabled)
        {
            key.SetValue("DeckPilot", Command);
            // Only explicit user activation clears a known per-user Windows disable flag.
            foreach (string path in Approvals)
            {
                using var approval = Registry.CurrentUser.OpenSubKey(path, writable: true);
                if (approval?.GetValue("DeckPilot") is byte[] bytes && bytes.Length >= 4 && bytes[0] is 3 or 7)
                    approval.DeleteValue("DeckPilot", false);
            }
        }
        else key.DeleteValue("DeckPilot", false);
        using var preferences = Registry.CurrentUser.CreateSubKey(Preferences);
        preferences.SetValue("StartupConfigured", 1);
        preferences.SetValue("StartupEnabled", enabled ? 1 : 0);
    }
    public static void RemoveInstalledEntry()
    {
        using var key = Registry.CurrentUser.OpenSubKey(Run, writable: true);
        if (string.Equals(key?.GetValue("DeckPilot") as string, Command, StringComparison.OrdinalIgnoreCase)) key!.DeleteValue("DeckPilot", false);
    }
}
