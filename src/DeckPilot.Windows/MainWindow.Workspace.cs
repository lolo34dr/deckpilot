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
        AccentPicker.SelectedItem = ActionAppearance.Accents.FirstOrDefault(c => c.Value.Equals(settings.ActiveProfile.Buttons[selected].Accent, StringComparison.OrdinalIgnoreCase)) ?? ActionAppearance.Accents[0];
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
        return true;
    }
}
