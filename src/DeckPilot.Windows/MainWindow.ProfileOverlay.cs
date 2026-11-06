using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private sealed record OverlayPositionChoice(string Name, ProfileOverlayPosition Position)
    {
        public override string ToString() => Name;
    }
    private ProfileOverlayWindow? profileOverlay;

    private void InitializeProfileOverlayPreferences()
    {
        ProfileOverlayEnabledCheck.IsChecked = settings.ProfileOverlay.Enabled;
        var positions = new[]
        {
            new OverlayPositionChoice("En haut à droite", ProfileOverlayPosition.TopRight),
            new OverlayPositionChoice("En haut à gauche", ProfileOverlayPosition.TopLeft),
            new OverlayPositionChoice("En bas à droite", ProfileOverlayPosition.BottomRight),
            new OverlayPositionChoice("En bas à gauche", ProfileOverlayPosition.BottomLeft),
            new OverlayPositionChoice("Position personnalisée", ProfileOverlayPosition.Custom)
        };
        ProfileOverlayPositionPicker.ItemsSource = positions;
        ProfileOverlayPositionPicker.SelectedItem = positions.First(p => p.Position == settings.ProfileOverlay.Position);
        ProfileOverlayXSlider.Value = settings.ProfileOverlay.XPercent;
        ProfileOverlayYSlider.Value = settings.ProfileOverlay.YPercent;
        UpdateProfileOverlayCustomPanel();
    }

    private void ProfileOverlayPositionChanged(object sender, SelectionChangedEventArgs e)
        => UpdateProfileOverlayCustomPanel();

    private void UpdateProfileOverlayCustomPanel()
    {
        if (ProfileOverlayCustomPanel == null) return;
        ProfileOverlayCustomPanel.Visibility = ProfileOverlayPositionPicker.SelectedItem is OverlayPositionChoice { Position: ProfileOverlayPosition.Custom }
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void SaveProfileOverlayClick(object sender, RoutedEventArgs e)
    {
        if (ProfileOverlayPositionPicker.SelectedItem is not OverlayPositionChoice choice) return;
        var previous = settings.ProfileOverlay;
        settings.ProfileOverlay = new ProfileOverlaySettings
        {
            Enabled = ProfileOverlayEnabledCheck.IsChecked == true,
            Position = choice.Position,
            XPercent = Math.Round(ProfileOverlayXSlider.Value, 1),
            YPercent = Math.Round(ProfileOverlayYSlider.Value, 1)
        };
        if (!Persist()) { settings.ProfileOverlay = previous; return; }
        ShowProfileOverlay(settings.ActiveProfile.Name, preview: true);
        StatusText.Text = "Notification de profil enregistrée · Aperçu affiché.";
    }

    private void ShowProfileOverlay(string profileName, bool preview = false)
    {
        if (!preview && !settings.ProfileOverlay.Enabled) return;
        try
        {
            profileOverlay ??= new ProfileOverlayWindow();
            profileOverlay.ShowProfile(profileName, settings.ProfileOverlay, settings.ReduceMotion);
        }
        catch (Exception ex)
        {
            ReportError("Notification de profil indisponible : " + ex.Message);
        }
    }
}
