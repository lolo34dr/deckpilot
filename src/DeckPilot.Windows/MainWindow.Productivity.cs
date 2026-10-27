using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    // An application-local clipboard avoids replacing the user's Windows clipboard.
    private ButtonAction? copiedKey;
    private bool shortcutRecording;
    private readonly List<ActivityEntry> recentActivity = [];
    private bool activityDirty = true;
    private sealed record ActivityEntry(DateTime Time, string Title, bool Success, string Detail);

    private void InitializeProductivityTools()
    {
        for (int i = 0; i < deckButtons.Count; i++)
        {
            int index = i;
            var menu = new ContextMenu();
            var copy = KeyMenuItem("Copier la touche", "Ctrl+C", "CopyDeckKey", () => CopyDeckKey(false));
            var cut = KeyMenuItem("Couper la touche", "Ctrl+X", "CutDeckKey", () => CopyDeckKey(true));
            var paste = KeyMenuItem("Coller la touche", "Ctrl+V", "PasteDeckKey", PasteDeckKey);
            var clear = KeyMenuItem("Réinitialiser la touche", "Suppr", "ResetDeckKey", ResetDeckKey);
            menu.Items.Add(copy); menu.Items.Add(cut); menu.Items.Add(paste);
            menu.Items.Add(new Separator()); menu.Items.Add(clear);
            deckButtons[i].ContextMenu = menu;
            deckButtons[i].ContextMenuOpening += (_, e) =>
            {
                if (calibrating || firmwareOpen || !SaveEditor()) { e.Handled = true; return; }
                CancelShortcutRecording();
                selected = index; editingLong = false; LoadEditor();
                paste.IsEnabled = copiedKey != null;
                clear.IsEnabled = cut.IsEnabled = !settings.ActiveProfile.Buttons[index].Equals(new ButtonAction());
            };
        }
        PreviewKeyDown += ProductivityKeyDown;
        Deactivated += (_, _) => CancelShortcutRecording();
        IsVisibleChanged += (_, _) => { if (!IsInterfaceActive) CancelShortcutRecording(); else RenderActivity(); };
        StateChanged += (_, _) => { if (!IsInterfaceActive) CancelShortcutRecording(); else RenderActivity(); };
        RecordShortcutButton.LostKeyboardFocus += (_, _) => CancelShortcutRecording();
        ActivityExpander.Expanded += (_, _) => RenderActivity();
        Closed += (_, _) => { copiedKey = null; recentActivity.Clear(); };
        ActivityList.Children.Add(ActivityEmptyState());
    }

    private static MenuItem KeyMenuItem(string label, string gesture, string id, Action invoke)
    {
        var item = new MenuItem { Header = label, InputGestureText = gesture };
        AutomationProperties.SetName(item, label);
        AutomationProperties.SetAutomationId(item, id);
        item.Click += (_, _) => invoke();
        return item;
    }

    private void ProductivityKeyDown(object sender, KeyEventArgs e)
    {
        if (shortcutRecording)
        {
            CaptureShortcut(e);
            return;
        }
        if (e.Handled || calibrating || firmwareOpen || OwnedWindows.Count > 0) return;
        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.K)
        {
            FocusActionSearchClick(this, e); e.Handled = true; return;
        }
        // A focused text field, picker or menu keeps its ordinary edit shortcuts.
        int index = deckButtons.FindIndex(key => key.IsKeyboardFocusWithin);
        if (index < 0) return;
        bool copy = Keyboard.Modifiers == ModifierKeys.Control && e.Key is Key.C or Key.X;
        bool paste = Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.V;
        bool clear = Keyboard.Modifiers == ModifierKeys.None && e.Key == Key.Delete;
        if (!copy && !paste && !clear) return;
        e.Handled = true;
        if (!SaveEditor()) return;
        selected = index; editingLong = false; LoadEditor();
        if (copy) CopyDeckKey(e.Key == Key.X);
        else if (paste) PasteDeckKey();
        else ResetDeckKey();
    }

    private void CopyDeckKey(bool cut)
    {
        if (calibrating || firmwareOpen || !SaveEditor()) return;
        copiedKey = settings.ActiveProfile.Buttons[selected].Copy();
        if (cut)
        {
            if (ReplaceSelectedKey(new()))
                StatusText.Text = "Touche coupée · Collez-la sur une autre touche ou annulez avec Ctrl+Z.";
        }
        else StatusText.Text = "Touche copiée · Appui long et couleur inclus · Ctrl+V sur une touche pour la coller.";
    }

    private void PasteDeckKey()
    {
        if (copiedKey == null || calibrating || firmwareOpen || !SaveEditor()) return;
        if (ReplaceSelectedKey(copiedKey.Copy()))
            StatusText.Text = "Touche collée · Annulation possible · Calibrage conservé.";
    }

    private void ResetDeckKey()
    {
        if (calibrating || firmwareOpen || !SaveEditor()) return;
        if (ReplaceSelectedKey(new()))
            StatusText.Text = "Touche réinitialisée · F13–F24 reste libre · Annulation possible.";
    }

    private bool ReplaceSelectedKey(ButtonAction next)
    {
        var previous = settings.ActiveProfile.Buttons[selected];
        ResetActionFeedback(selected);
        settings.ActiveProfile.Buttons[selected] = next;
        editingLong = false; LoadEditor();
        if (Persist()) return true;
        // Keep the editor and the last saved configuration aligned on a failed write.
        settings.ActiveProfile.Buttons[selected] = previous;
        LoadEditor();
        return false;
    }

    private void RecordShortcutClick(object sender, RoutedEventArgs e)
    {
        if (shortcutRecording) { CancelShortcutRecording(); return; }
        if (calibrating || firmwareOpen || (ActionKind)ActionPicker.SelectedIndex != ActionKind.Shortcut) return;
        CancelPendingGestures();
        RecordShortcutButton.Focus();
        shortcutRecording = true;
        RecordShortcutButton.Content = "Annuler l’enregistrement";
        ShortcutRecordingHint.Text = "Appuyez sur votre combinaison · Échap pour annuler. Les raccourcis réservés à Windows se règlent manuellement.";
        AutomationProperties.SetItemStatus(RecordShortcutButton, "Enregistrement du raccourci en cours");
    }

    private void CaptureShortcut(KeyEventArgs e)
    {
        if (calibrating || firmwareOpen || !IsInterfaceActive) { CancelShortcutRecording(); return; }
        e.Handled = true;
        var key = e.Key == Key.System ? e.SystemKey : e.Key == Key.ImeProcessed ? e.ImeProcessedKey : e.Key;
        if (key == Key.Escape) { CancelShortcutRecording(); return; }
        if (e.IsRepeat || key is Key.LeftCtrl or Key.RightCtrl or Key.LeftAlt or Key.RightAlt or Key.LeftShift or Key.RightShift or Key.LWin or Key.RWin) return;
        int virtualKey = KeyInterop.VirtualKeyFromKey(key);
        if (Calibration.IsDeckKey(virtualKey))
        {
            ShortcutRecordingHint.Text = "F13–F24 est réservé au macropad. Choisissez une autre combinaison, ou Échap pour annuler.";
            return;
        }
        var choice = ((IEnumerable<Choice>)ShortcutPicker.ItemsSource).FirstOrDefault(item => item.Key == virtualKey);
        if (choice == null)
        {
            ShortcutRecordingHint.Text = "Cette touche n’est pas disponible. Choisissez une lettre, un chiffre, F1–F12 ou une touche de navigation.";
            return;
        }
        var modifiers = Keyboard.Modifiers;
        CtrlCheck.IsChecked = modifiers.HasFlag(ModifierKeys.Control);
        AltCheck.IsChecked = modifiers.HasFlag(ModifierKeys.Alt);
        ShiftCheck.IsChecked = modifiers.HasFlag(ModifierKeys.Shift);
        WinCheck.IsChecked = modifiers.HasFlag(ModifierKeys.Windows);
        ShortcutPicker.SelectedItem = choice;
        CancelShortcutRecording();
        ShortcutRecordingHint.Text = "Combinaison capturée · Enregistrez pour l’appliquer à cette touche.";
        UpdateHistoryState();
    }

    private void CancelShortcutRecording()
    {
        if (!shortcutRecording) return;
        shortcutRecording = false;
        RecordShortcutButton.Content = "Enregistrer un raccourci";
        ShortcutRecordingHint.Text = "Cliquez puis appuyez sur une combinaison, ou choisissez les touches ci-dessus.";
        AutomationProperties.SetItemStatus(RecordShortcutButton, "");
    }

    private void FocusActionSearchClick(object sender, RoutedEventArgs e)
    {
        if (calibrating || firmwareOpen) return;
        CancelShortcutRecording();
        ActionSearchBox.BringIntoView();
        ActionSearchBox.Focus();
        ActionSearchBox.SelectAll();
    }

    private void AddActivity(string title, bool success, string detail)
    {
        if (lifetime.IsCancellationRequested) return;
        recentActivity.Insert(0, new(DateTime.Now, ActivityText(title, 100), success, ActivityText(detail, 160)));
        if (recentActivity.Count > 30) recentActivity.RemoveAt(recentActivity.Count - 1);
        activityDirty = true;
        RenderActivity();
    }

    private static string ActivityText(string value, int maximum)
    {
        string text = new(value.Select(character => char.IsControl(character) ? ' ' : character).ToArray());
        return text.Length <= maximum ? text : text[..maximum] + "…";
    }

    private static TextBlock ActivityEmptyState() => new()
    {
        Text = "Les 30 dernières actions de cette session apparaîtront ici. Cet historique n’est pas enregistré.",
        TextWrapping = TextWrapping.Wrap, Foreground = Brush("#AAB7B3"), FontSize = 11,
        Margin = new Thickness(0, 2, 0, 10)
    };

    private void RenderActivity()
    {
        if (!activityDirty || !IsInterfaceActive || !ActivityExpander.IsExpanded) return;
        ActivityList.Children.Clear();
        if (recentActivity.Count == 0) ActivityList.Children.Add(ActivityEmptyState());
        foreach (var entry in recentActivity)
        {
            var content = new StackPanel();
            content.Children.Add(new TextBlock
            {
                Text = entry.Time.ToString("HH:mm:ss") + (entry.Success ? " · Envoyée" : " · Échec"),
                FontSize = 10, Foreground = Brush(entry.Success ? "#8BCBBC" : "#F2A69C")
            });
            content.Children.Add(new TextBlock { Text = entry.Title, TextWrapping = TextWrapping.Wrap, FontWeight = FontWeights.SemiBold, FontSize = 12, Margin = new Thickness(0, 3, 0, 0) });
            if (entry.Detail.Length > 0)
                content.Children.Add(new TextBlock { Text = entry.Detail, TextWrapping = TextWrapping.Wrap, FontSize = 11, Foreground = Brush("#AAB7B3"), Margin = new Thickness(0, 3, 0, 0) });
            ActivityList.Children.Add(new Border { Child = content, BorderBrush = Brush("#35413F"), BorderThickness = new Thickness(0, 0, 0, 1), Padding = new Thickness(0, 8, 0, 10) });
        }
        activityDirty = false;
    }

    private void ClearActivityClick(object sender, RoutedEventArgs e)
    {
        recentActivity.Clear(); activityDirty = true; RenderActivity();
    }
}
