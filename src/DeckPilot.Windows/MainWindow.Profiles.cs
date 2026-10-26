using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using DeckPilot.Core;
using Microsoft.Win32;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private ProfileHistory? profileHistory;
    private bool restoringHistory, editingLong;
    private void InitializeProfileTools()
    {
        profileHistory = new(settings);
        GesturePicker.ItemsSource = new[] { "Appui court", "Appui long" };
        GesturePicker.SelectedIndex = 0;
        PreviewKeyDown += (_, e) =>
        {
            if (OwnedWindows.Count > 0 || Keyboard.FocusedElement is TextBoxBase || Keyboard.Modifiers != ModifierKeys.Control) return;
            if (e.Key is Key.Z or Key.Y) { RestoreHistory(e.Key == Key.Y); e.Handled = true; }
        };
        AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler((_, _) => UpdateHistoryState()), true);
        AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler((_, _) => UpdateHistoryState()), true);
        AddHandler(ToggleButton.CheckedEvent, new RoutedEventHandler((_, _) => UpdateHistoryState()), true);
        AddHandler(ToggleButton.UncheckedEvent, new RoutedEventHandler((_, _) => UpdateHistoryState()), true);
        LongPressThreshold.ValueChanged += (_, _) => UpdateHistoryState();
    }
    private ButtonAction EditedAction => editingLong ? settings.ActiveProfile.Buttons[selected].LongPressAction ?? settings.ActiveProfile.Buttons[selected].CopyBasic() : settings.ActiveProfile.Buttons[selected];
    private ButtonAction ReadEditorConfiguration()
    {
        var root = settings.ActiveProfile.Buttons[selected].Copy();
        var action = editingLong ? root.LongPressAction ?? root.CopyBasic() : root;
        action.Label = LabelEditor.Text.Trim(); action.Kind = (ActionKind)ActionPicker.SelectedIndex;
        action.Target = TargetEditor.Text.Trim(); action.Arguments = ArgumentsEditor.Text; action.ApplicationName = pickedApplicationName;
        action.Ctrl = CtrlCheck.IsChecked == true; action.Alt = AltCheck.IsChecked == true;
        action.Shift = ShiftCheck.IsChecked == true; action.Win = WinCheck.IsChecked == true;
        if (action.Kind == ActionKind.Shortcut && ShortcutPicker.SelectedItem is Choice shortcut) action.Key = shortcut.Key;
        if (action.Kind == ActionKind.Media && MediaPicker.SelectedItem is Choice media) action.Key = media.Key;
        if (action.Kind == ActionKind.Spotify && SpotifyPicker.SelectedItem is Choice spotify) action.Key = spotify.Key;
        if (action.Kind == ActionKind.AudioOutput)
        {
            action.Target = (PrimaryOutputPicker.SelectedItem as AudioDevice)?.Id ?? action.Target;
            action.Arguments = (SecondaryOutputPicker.SelectedItem as AudioDevice)?.Id ?? action.Arguments;
        }
        if (editingLong) root.LongPressAction = action.CopyBasic();
        root.LongPressEnabled = LongPressCheck.IsChecked == true;
        root.LongPressMilliseconds = (int)Math.Round(LongPressThreshold.Value);
        if (root.LongPressEnabled && (root.LongPressAction == null || root.LongPressAction.Kind == ActionKind.Passthrough)) root.LongPressAction = root.CopyBasic();
        return root;
    }
    private bool EditorChanged()
    {
        if (loading || ActionPicker.SelectedIndex < 0) return false;
        return !ReadEditorConfiguration().Equals(settings.ActiveProfile.Buttons[selected]);
    }
    private void UpdateHistoryState()
    {
        if (profileHistory == null || loading) return;
        bool changed = EditorChanged();
        UndoProfileButton.IsEnabled = profileHistory.CanUndo || changed;
        RedoProfileButton.IsEnabled = profileHistory.CanRedo && !changed;
    }
    private void UndoProfileClick(object sender, RoutedEventArgs e) => RestoreHistory(false);
    private void RedoProfileClick(object sender, RoutedEventArgs e) => RestoreHistory(true);
    private void RestoreHistory(bool redo)
    {
        if (profileHistory == null || calibrating || firmwareOpen || !SaveEditor()) return;
        if (!(redo ? profileHistory.Redo(settings) : profileHistory.Undo(settings))) return;
        CancelPendingGestures();
        foreach (int i in Enumerable.Range(0, 12)) ResetActionFeedback(i);
        editingLong = false; RefreshProfiles(); LoadEditor();
        restoringHistory = true;
        bool saved;
        try { saved = Persist(); }
        finally { restoringHistory = false; }
        UpdateHistoryState(); if (!saved) return; StatusText.Text = redo ? "Modification rétablie." : "Modification annulée · Calibrage conservé.";
    }
    private void LongPressClick(object sender, RoutedEventArgs e)
    {
        if (loading) return;
        if (!SaveEditor()) { LoadEditor(); return; }
        if (LongPressCheck.IsChecked != true) editingLong = false;
        LoadEditor();
    }
    private void GestureChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || profileHistory == null || GesturePicker.SelectedIndex < 0) return;
        bool next = GesturePicker.SelectedIndex == 1;
        if (next == editingLong) return;
        if (!SaveEditor()) { bool was = loading; loading = true; GesturePicker.SelectedIndex = editingLong ? 1 : 0; loading = was; return; }
        editingLong = next; LoadEditor();
    }
    private void ExportProfileClick(object sender, RoutedEventArgs e) => ExportProfiles(false);
    private void ExportAllProfilesClick(object sender, RoutedEventArgs e) => ExportProfiles(true);
    private void ExportProfiles(bool all)
    {
        if (!SaveEditor()) return;
        var dialog = new SaveFileDialog { Title = all ? "Exporter tous les profils" : "Exporter le profil", Filter = "Profils DeckPilot|*.deckpilot.json", FileName = "DeckPilot-profil.deckpilot.json", AddExtension = true, DefaultExt = ".deckpilot.json" };
        if (dialog.ShowDialog(this) != true) return;
        try
        {
            string json = ProfileExchange.Export(all ? settings.Profiles : new[] { settings.ActiveProfile });
            string temporary = dialog.FileName + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try { File.WriteAllText(temporary, json); File.Move(temporary, dialog.FileName, true); }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
            StatusText.Text = "Profil exporté · Le calibrage et les préférences de ce PC ne sont pas inclus.";
        }
        catch (Exception ex) { ReportError("Export impossible : " + ex.Message); }
    }
    private void ImportProfilesClick(object sender, RoutedEventArgs e) => ReadProfiles(false);
    private void RestoreProfilesClick(object sender, RoutedEventArgs e) => ReadProfiles(true);
    private void ReadProfiles(bool restore)
    {
        if (calibrating || firmwareOpen || !SaveEditor()) return;
        if (restore)
        {
            try { Directory.CreateDirectory(store.BackupsPath); }
            catch (Exception ex) { ReportError("Accès aux sauvegardes impossible : " + ex.Message); return; }
        }
        var dialog = new OpenFileDialog { Title = restore ? "Restaurer les profils depuis une sauvegarde" : "Importer des profils", Filter = "Profils DeckPilot|*.deckpilot.json|JSON|*.json", InitialDirectory = restore ? store.BackupsPath : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) };
        if (dialog.ShowDialog(this) != true) return;
        try
        {
            var package = ProfileExchange.Read(dialog.FileName);
            string names = string.Join("\n", package.Profiles.Take(8).Select(p => "• " + p.Name));
            string message = restore ? $"Remplacer les profils actuels par ces {package.Profiles.Count} profil(s) ?\n\n{names}\n\nLe calibrage est conservé. Les profils actuels seront sauvegardés."
                : $"Ajouter ces {package.Profiles.Count} profil(s) sans remplacer les vôtres ?\n\n{names}\n\nLes programmes et périphériques audio peuvent nécessiter une adaptation sur ce PC. Aucune action ne sera exécutée.";
            if (MessageBox.Show(this, message, "DeckPilot", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes) return;
            if (restore) { settings.Profiles = ProfileExchange.Copy(package.Profiles); settings.ActiveProfileId = settings.Profiles[0].Id; }
            else
            {
                if (settings.Profiles.Count + package.Profiles.Count > 100) throw new InvalidDataException("Limite de 100 profils atteinte.");
                settings.ActiveProfileId = ProfileExchange.AddAsNew(settings, package)[0].Id;
            }
            CancelPendingGestures(); editingLong = false;
            RefreshProfiles(); LoadEditor(); if (!Persist()) return;
            StatusText.Text = restore ? "Profils restaurés · Annulation possible · Calibrage conservé." : "Profils importés · Aucune action exécutée.";
        }
        catch (Exception ex) { ReportError("Lecture des profils impossible : " + ex.Message); }
    }
}
