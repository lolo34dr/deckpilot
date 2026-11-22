using System;
using System.IO;

namespace DeckPilot.Windows;

internal static class BetaEnvironment
{
#if DECKPILOT_BETA
    public static bool IsEnabled => true;
#else
    public static bool IsEnabled => false;
#endif

    public static string DisplayName => IsEnabled ? "DeckPilot bêta" : "DeckPilot";

    public static string LocalData => Path.Combine(
        LocalRoot,
        IsEnabled ? "DeckPilot-Beta" : "DeckPilot");

    private static string LocalRoot
    {
        get
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return string.IsNullOrWhiteSpace(root) ? Path.GetTempPath() : root;
        }
    }

    public static string SettingsPath => Path.Combine(
        IsEnabled ? LocalData : Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeckPilot"),
        "settings.json");
}
