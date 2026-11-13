using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private sealed record ProfileDestination(string Name, string Id) { public override string ToString() => Name; }
    private int profileSelectionLocks;
    private T WithProfileSelectionLocked<T>(Func<T> showDialog)
    {
        profileSelectionLocks++;
        try { return showDialog(); }
        finally { profileSelectionLocks--; }
    }

    private void InitializeWorkspaceFeatures()
    {
        AccentPicker.ItemsSource = ActionAppearance.Accents;
        SystemCommandPicker.ItemsSource = SystemActions.All;
        ProfileTemplatePicker.ItemsSource = ProfileTemplates.All;
        ProfileTemplatePicker.SelectedIndex = 0;
        ReduceMotionCheck.IsChecked = settings.ReduceMotion;
        Motion.ReduceMotion = settings.ReduceMotion;
        InitializeProfileOverlayPreferences();
        AccentPicker.SelectionChanged += (_, _) =>
        {
            if (loading || AccentPicker.SelectedItem is not ActionAccent accent) return;
            deckButtons[selected].AccentBrush = AccentBrushFor(accent.Value);
            UpdateHistoryState();
        };
    }

    private static Brush AccentBrushFor(string accent) => string.IsNullOrEmpty(accent) ? Brushes.Transparent : Brush(accent);

    private void PopulateWorkspaceEditor(ButtonAction action)
    {
        SetKeyColor(settings.ActiveProfile.Buttons[selected].Accent);
        var destinations = new[] { new ProfileDestination("Profil suivant", "next"), new ProfileDestination("Profil précédent", "previous") }
            .Concat(settings.Profiles.Select(p => new ProfileDestination(p.Name, p.Id.ToString()))).ToList();
        string destination = action.Kind == ActionKind.Profile && action.Target.Length > 0 ? action.Target : "next";
        if (!destinations.Any(d => d.Id.Equals(destination, StringComparison.OrdinalIgnoreCase)))
            destinations.Add(new("Profil indisponible · À remplacer", destination));
        ProfileTargetPicker.ItemsSource = destinations;
        ProfileTargetPicker.SelectedItem = destinations.First(d => d.Id.Equals(destination, StringComparison.OrdinalIgnoreCase));
        SystemCommandPicker.SelectedItem = SystemActions.All.FirstOrDefault(c => c.Id == action.Target);
        if (action.Kind != ActionKind.System || action.Target.Length == 0) SystemCommandPicker.SelectedIndex = 0;
    }

    private void SetKeyColor(string value)
    {
        var choices = ActionAppearance.Accents.ToList();
        var choice = choices.FirstOrDefault(c => c.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        if (choice == null) { choice = new ActionAccent(value, "Personnalisée"); choices.Add(choice); }
        AccentPicker.ItemsSource = choices;
        AccentPicker.SelectedItem = choice;
    }

    private void ChooseKeyColorClick(object sender, RoutedEventArgs e)
    {
        var value = (AccentPicker.SelectedItem as ActionAccent)?.Value ?? "";
        var current = TryReadThemeColor(value, out var parsed) ? parsed : (Color)ColorConverter.ConvertFromString("#AD91E8")!;
        using var dialog = new System.Windows.Forms.ColorDialog
        {
            Color = System.Drawing.Color.FromArgb(current.R, current.G, current.B), AnyColor = true, FullOpen = true
        };
        if (dialog.ShowDialog(new ThemeDialogOwner(new System.Windows.Interop.WindowInteropHelper(this).Handle)) != System.Windows.Forms.DialogResult.OK) return;
        SetKeyColor($"#{dialog.Color.R:X2}{dialog.Color.G:X2}{dialog.Color.B:X2}");
    }

    private void RefreshProfileSummary()
    {
        int count = settings.ActiveProfile.Buttons.Count(a => a.Kind != ActionKind.Passthrough);
        ProfileSummary.Text = $"{count} / 12 touches configurées · {settings.Profiles.Count} profil(s)";
    }

    private void AddTemplateClick(object sender, RoutedEventArgs e)
    {
        if (calibrating || firmwareOpen || ProfileTemplatePicker.SelectedItem is not ProfileTemplate template) return;
        if (settings.Profiles.Count >= 100) { ReportError("Limite de 100 profils atteinte."); return; }
        if (!SaveEditor()) return;
        var profile = ProfileTemplates.Create(template.Id);
        string name = profile.Name;
        int suffix = 2;
        while (settings.Profiles.Any(p => p.Name.Equals(profile.Name, StringComparison.OrdinalIgnoreCase))) profile.Name = name + " " + suffix++;
        Guid previous = settings.ActiveProfile.Id;
        settings.Profiles.Add(profile); settings.ActiveProfileId = profile.Id;
        if (!Persist())
        {
            settings.Profiles.Remove(profile); settings.ActiveProfileId = previous;
            RefreshProfiles(); LoadEditor(); return;
        }
        CancelPendingGestures(); editingLong = false; selected = 0;
        RefreshProfiles(); LoadEditor();
        StatusText.Text = $"Modèle « {profile.Name} » ajouté · Vos autres profils sont conservés.";
    }

    private void ReduceMotionChanged(object sender, RoutedEventArgs e)
    {
        if (loading || (ReduceMotionCheck.IsChecked == true) == settings.ReduceMotion) return;
        bool previous = settings.ReduceMotion;
        settings.ReduceMotion = ReduceMotionCheck.IsChecked == true;
        if (!Persist()) { settings.ReduceMotion = previous; ReduceMotionCheck.IsChecked = previous; }
        Motion.ReduceMotion = settings.ReduceMotion;
        if (settings.ReduceMotion)
        {
            WindowContent.BeginAnimation(OpacityProperty, null);
            EditorPanel.BeginAnimation(OpacityProperty, null);
            ActionExecutionStatus.BeginAnimation(OpacityProperty, null);
        }
    }

    private bool SwitchProfile(Profile destination)
    {
        if (profileSelectionLocks > 0 || OwnedWindows.Count > 0) { ReportError("Fermez la fenêtre de configuration avant de changer de profil."); return false; }
        if (calibrating || firmwareOpen || !SaveEditor()) return false;
        Guid previous = settings.ActiveProfile.Id;
        settings.ActiveProfileId = destination.Id;
        if (!Persist())
        {
            settings.ActiveProfileId = previous;
            RefreshProfiles(); LoadEditor(); return false;
        }
        CancelPendingGestures(); editingLong = false;
        RefreshProfiles(); LoadEditor();
        if (tray != null) tray.Text = "DeckPilot · " + destination.Name[..Math.Min(destination.Name.Length, 45)];
        StatusText.Text = "Profil actif · " + destination.Name;
        if (previous != destination.Id) ShowProfileOverlay(destination.Name);
        return true;
    }
}
