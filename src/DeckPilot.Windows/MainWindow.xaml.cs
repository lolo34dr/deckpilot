using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using DeckPilot.Core;
using Microsoft.Win32;
using Forms = System.Windows.Forms;

namespace DeckPilot.Windows;
public partial class MainWindow : Window
{
    private static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeckPilot", "settings.json");
    private readonly SettingsStore store = new(ConfigPath);
    private Settings settings = new();
    private KeyboardHook? hook;
    private Forms.NotifyIcon? tray;
    private bool loading = true, quitting, calibrating;
    private int selected;
    private readonly List<int> captured = [];
    private readonly List<Button> deckButtons = [];
    private readonly List<TextBlock> deckLabels = [], deckKeys = [], deckActions = [];
    private readonly List<Border> calibrationCells = [];
    private readonly List<TextBlock> calibrationLabels = [];
    private readonly DispatcherTimer flashTimer = new() { Interval = TimeSpan.FromMilliseconds(220) };
    private record Choice(string Name, int Key);
    private static readonly string[] ActionNames = ["Touche libre", "Ouvrir une application", "Ouvrir un dossier", "Ouvrir un site web", "Raccourci clavier", "Multimédia"];

    public MainWindow(bool startInBackground = false)
    {
        InitializeComponent();
        try { settings = store.Load(); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Text.Json.JsonException)
        {
            MessageBox.Show($"Impossible de lire la configuration : {ex.Message}\nLe fichier original va être conservé. Une configuration neuve sera utilisée.", "DeckPilot");
            if (File.Exists(ConfigPath))
            {
                try { File.Copy(ConfigPath, ConfigPath + ".backup-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff"), overwrite: false); }
                catch (Exception backupError)
                {
                    MessageBox.Show("Impossible de conserver une copie : " + backupError.Message + "\nDeckPilot va se fermer sans modifier le fichier.", "DeckPilot");
                    throw;
                }
            }
        }
        ActionPicker.ItemsSource = ActionNames;
        var keys = new List<Choice>();
        for (int k = 0x41; k <= 0x5A; k++) keys.Add(new(((char)k).ToString(), k));
        for (int k = 0x30; k <= 0x39; k++) keys.Add(new(((char)k).ToString(), k));
        for (int k = 0x70; k <= 0x87; k++) keys.Add(new($"F{k - 0x70 + 1}", k));
        keys.AddRange(new Choice[] { new("Entrée", 0x0D), new("Échap", 0x1B), new("Espace", 0x20), new("Tabulation", 0x09), new("Retour arrière", 0x08), new("Page précédente", 0x21), new("Page suivante", 0x22), new("Fin", 0x23), new("Début", 0x24), new("Gauche", 0x25), new("Haut", 0x26), new("Droite", 0x27), new("Bas", 0x28), new("Insérer", 0x2D), new("Supprimer", 0x2E) });
        ShortcutPicker.ItemsSource = keys;
        MediaPicker.ItemsSource = new Choice[] { new("Lecture / pause", 0xB3), new("Piste suivante", 0xB0), new("Piste précédente", 0xB1), new("Volume +", 0xAF), new("Volume −", 0xAE), new("Muet", 0xAD) };
        BuildGrid();
        RefreshProfiles();
        LoadEditor();
        StartupCheck.IsChecked = StartupEnabled();
        try { hook = new KeyboardHook(); hook.KeyPressed += OnDeckKey; }
        catch (Win32Exception ex)
        {
            MessageBox.Show("Impossible d’écouter les touches : " + ex.Message, "DeckPilot");
            quitting = true;
            Loaded += (_, _) => Close();
            return;
        }
        CreateTray();
        flashTimer.Tick += (_, _) => { flashTimer.Stop(); RenderGrid(); };
        Closing += WindowClosing;
        Closed += (_, _) => { flashTimer.Stop(); hook?.Dispose(); tray?.Dispose(); };
        loading = false;
        StatusText.Text = "Prêt · Choisissez un bouton pour le personnaliser.";
        Loaded += (_, _) => { if (!settings.IsCalibrated) BeginCalibration(); else if (startInBackground) Hide(); };
    }
    private void BuildGrid()
    {
        for (int i = 0; i < 12; i++)
        {
            int index = i;
            var content = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
            var number = new TextBlock { Text = $"{i + 1:00}", FontSize = 12, Foreground = Brush("#93A7FF") };
            var label = new TextBlock { FontSize = 16, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 8, 0, 4), TextTrimming = TextTrimming.CharacterEllipsis };
            var action = new TextBlock { FontSize = 11, Foreground = Brush("#AAB6CD"), TextTrimming = TextTrimming.CharacterEllipsis };
            var key = new TextBlock { FontSize = 11, Foreground = Brush("#93A7FF"), Margin = new Thickness(0, 5, 0, 0) };
            content.Children.Add(number); content.Children.Add(label); content.Children.Add(action); content.Children.Add(key);
            var button = new Button { Content = content, HorizontalContentAlignment = HorizontalAlignment.Stretch, Padding = new Thickness(14), Margin = new Thickness(0, 0, 10, 10) };
            button.Click += (_, _) => { SaveEditor(); selected = index; LoadEditor(); };
            DeckGrid.Children.Add(button); deckButtons.Add(button); deckLabels.Add(label); deckKeys.Add(key); deckActions.Add(action);
            var calibrationLabel = new TextBlock { Text = $"{i + 1:00}", FontSize = 24, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            var cell = new Border { CornerRadius = new CornerRadius(12), Margin = new Thickness(6), Child = calibrationLabel };
            CalibrationGrid.Children.Add(cell); calibrationCells.Add(cell); calibrationLabels.Add(calibrationLabel);
        }
    }
    private static SolidColorBrush Brush(string color) => new((Color)ColorConverter.ConvertFromString(color));
    private void RenderGrid()
    {
        for (int i = 0; i < 12; i++)
        {
            var action = settings.ActiveProfile.Buttons[i];
            deckLabels[i].Text = string.IsNullOrWhiteSpace(action.Label) ? $"Bouton {i + 1}" : action.Label;
            deckActions[i].Text = ActionNames[(int)action.Kind];
            deckKeys[i].Text = settings.IsCalibrated ? Calibration.KeyName(settings.KeyMap[i]) : "À calibrer";
            deckButtons[i].Background = Brush(i == selected ? "#2B3862" : "#171D2C");
            deckButtons[i].BorderBrush = Brush(i == selected ? "#93A7FF" : "#29334A");
            deckButtons[i].ToolTip = $"Ligne {i / 3 + 1}, colonne {i % 3 + 1} · {deckLabels[i].Text} · {deckKeys[i].Text}";
            System.Windows.Automation.AutomationProperties.SetName(deckButtons[i], (string)deckButtons[i].ToolTip);
        }
    }
    private void RefreshProfiles()
    {
        bool previous = loading; loading = true;
        ProfilePicker.ItemsSource = null;
        ProfilePicker.ItemsSource = settings.Profiles;
        ProfilePicker.SelectedItem = settings.ActiveProfile;
        ProfileName.Text = settings.ActiveProfile.Name;
        loading = previous;
    }
    private void LoadEditor()
    {
        bool previous = loading; loading = true;
        var action = settings.ActiveProfile.Buttons[selected];
        SelectedTitle.Text = $"Bouton {selected + 1:00}";
        SelectedKey.Text = $"Ligne {selected / 3 + 1}, colonne {selected % 3 + 1} · " + (settings.IsCalibrated ? Calibration.KeyName(settings.KeyMap[selected]) : "Calibrage nécessaire");
        LabelEditor.Text = action.Label;
        ActionPicker.SelectedIndex = (int)action.Kind;
        TargetEditor.Text = action.Target;
        ArgumentsEditor.Text = action.Arguments;
        CtrlCheck.IsChecked = action.Ctrl; AltCheck.IsChecked = action.Alt; ShiftCheck.IsChecked = action.Shift; WinCheck.IsChecked = action.Win;
        ShortcutPicker.SelectedItem = ((IEnumerable<Choice>)ShortcutPicker.ItemsSource).FirstOrDefault(c => c.Key == action.Key) ?? ((IEnumerable<Choice>)ShortcutPicker.ItemsSource).First();
        MediaPicker.SelectedItem = ((IEnumerable<Choice>)MediaPicker.ItemsSource).FirstOrDefault(c => c.Key == action.Key) ?? ((IEnumerable<Choice>)MediaPicker.ItemsSource).First();
        UpdateActionFields(); RenderGrid(); loading = previous;
    }
    private void SaveEditor()
    {
        if (loading || ActionPicker.SelectedIndex < 0) return;
        var action = settings.ActiveProfile.Buttons[selected];
        action.Label = LabelEditor.Text.Trim(); action.Kind = (ActionKind)ActionPicker.SelectedIndex;
        action.Target = TargetEditor.Text.Trim(); action.Arguments = ArgumentsEditor.Text;
        action.Ctrl = CtrlCheck.IsChecked == true; action.Alt = AltCheck.IsChecked == true;
        action.Shift = ShiftCheck.IsChecked == true; action.Win = WinCheck.IsChecked == true;
        if (action.Kind == ActionKind.Shortcut && ShortcutPicker.SelectedItem is Choice shortcut) action.Key = shortcut.Key;
        if (action.Kind == ActionKind.Media && MediaPicker.SelectedItem is Choice media) action.Key = media.Key;
        Persist(); RenderGrid();
    }
    private bool Persist()
    {
        try { settings.ActiveProfileId = settings.ActiveProfile.Id; store.Save(settings); StatusText.Text = "Configuration enregistrée sur ce PC."; return true; }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { ReportError("Enregistrement impossible : " + ex.Message); return false; }
    }
    private void SaveClick(object sender, RoutedEventArgs e) => SaveEditor();
    private void ActionChanged(object sender, SelectionChangedEventArgs e) { if (!loading) UpdateActionFields(); }
    private void UpdateActionFields()
    {
        if (ActionPicker.SelectedIndex < 0) return;
        var kind = (ActionKind)ActionPicker.SelectedIndex;
        TargetPanel.Visibility = kind is ActionKind.Application or ActionKind.Folder or ActionKind.Website ? Visibility.Visible : Visibility.Collapsed;
        ArgumentsPanel.Visibility = kind == ActionKind.Application ? Visibility.Visible : Visibility.Collapsed;
        ShortcutPanel.Visibility = kind == ActionKind.Shortcut ? Visibility.Visible : Visibility.Collapsed;
        MediaPanel.Visibility = kind == ActionKind.Media ? Visibility.Visible : Visibility.Collapsed;
        BrowseButton.Visibility = kind == ActionKind.Website ? Visibility.Collapsed : Visibility.Visible;
        TargetLabel.Text = kind == ActionKind.Website ? "Adresse du site" : kind == ActionKind.Folder ? "Chemin du dossier" : "Chemin du programme";
        ActionHelp.Text = kind switch
        {
            ActionKind.Passthrough => "La touche F13–F24 est transmise normalement. Attribuez-la directement dans votre jeu.",
            ActionKind.Application => "Lance le programme choisi. La touche physique est interceptée par DeckPilot.",
            ActionKind.Folder => "Ouvre le dossier dans l’Explorateur Windows.",
            ActionKind.Website => "Ouvre une adresse http:// ou https:// dans votre navigateur par défaut.",
            ActionKind.Shortcut => "Envoie ce raccourci à la fenêtre active. Les touches F13–F24 simulées ne déclenchent pas d’autres actions DeckPilot.",
            _ => "Contrôle la lecture ou le volume de Windows."
        };
    }
    private void BrowseClick(object sender, RoutedEventArgs e)
    {
        if ((ActionKind)ActionPicker.SelectedIndex == ActionKind.Folder)
        {
            using var dialog = new Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == Forms.DialogResult.OK) TargetEditor.Text = dialog.SelectedPath;
        }
        else
        {
            var dialog = new OpenFileDialog { Filter = "Applications et raccourcis|*.exe;*.lnk|Tous les fichiers|*.*", CheckFileExists = true };
            if (dialog.ShowDialog(this) == true) TargetEditor.Text = dialog.FileName;
        }
    }
    private void ProfileChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || ProfilePicker.SelectedItem is not Profile next) return;
        SaveEditor(); settings.ActiveProfileId = next.Id; LoadEditor(); ProfileName.Text = next.Name; Persist();
        if (tray != null) tray.Text = "DeckPilot · " + next.Name[..Math.Min(next.Name.Length, 45)];
    }
    private string NewProfileName() => string.IsNullOrWhiteSpace(ProfileName.Text) ? "Nouveau profil" : ProfileName.Text.Trim();
    private void CreateProfile(object sender, RoutedEventArgs e) => AddProfile(false);
    private void DuplicateProfile(object sender, RoutedEventArgs e) => AddProfile(true);
    private void AddProfile(bool duplicate)
    {
        SaveEditor();
        var profile = new Profile { Name = NewProfileName() };
        if (duplicate) { profile.Name += " (copie)"; profile.Buttons = settings.ActiveProfile.Buttons.Select(b => b.Copy()).ToList(); }
        settings.Profiles.Add(profile); settings.ActiveProfileId = profile.Id; RefreshProfiles(); LoadEditor(); Persist();
    }
    private void RenameProfile(object sender, RoutedEventArgs e)
    {
        SaveEditor(); settings.ActiveProfile.Name = NewProfileName(); RefreshProfiles(); Persist();
    }
    private void DeleteProfile(object sender, RoutedEventArgs e)
    {
        if (settings.Profiles.Count == 1) { StatusText.Text = "Conservez au moins un profil."; return; }
        if (MessageBox.Show(this, $"Supprimer le profil « {settings.ActiveProfile.Name} » ?", "DeckPilot", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
        settings.Profiles.Remove(settings.ActiveProfile); settings.ActiveProfileId = settings.Profiles[0].Id;
        RefreshProfiles(); LoadEditor(); Persist();
    }
    private void OnDeckKey(object? sender, DeckKeyEventArgs e)
    {
        if (calibrating)
        {
            e.Handled = true;
            int key = e.Key;
            Dispatcher.BeginInvoke(() => CaptureKey(key));
            return;
        }
        if (!settings.IsCalibrated) return;
        int index = settings.KeyMap.IndexOf(e.Key);
        if (index < 0) return;
        var action = settings.ActiveProfile.Buttons[index].Copy();
        e.Handled = action.Kind != ActionKind.Passthrough;
        Dispatcher.BeginInvoke(() =>
        {
            deckButtons[index].Background = Brush("#416A60"); flashTimer.Stop(); flashTimer.Start();
            try
            {
                ActionRunner.Run(action);
                if (action.Kind != ActionKind.Passthrough) StatusText.Text = $"Action exécutée · {deckLabels[index].Text}";
            }
            catch (Exception ex) { ReportError("Action impossible : " + ex.Message); }
        });
    }
    private void CalibrateClick(object sender, RoutedEventArgs e) => BeginCalibration();
    private void BeginCalibration()
    {
        SaveEditor(); captured.Clear(); calibrating = true;
        CalibrationOverlay.Visibility = Visibility.Visible; CalibrationError.Text = "Les actions personnalisées sont suspendues pendant le calibrage.";
        UpdateCalibration();
    }
    private void CaptureKey(int key)
    {
        if (!calibrating) return;
        if (captured.Contains(key)) { CalibrationError.Text = $"{Calibration.KeyName(key)} est déjà attribuée. Appuyez sur un autre bouton physique."; return; }
        captured.Add(key);
        if (captured.Count == 12)
        {
            settings.KeyMap = [.. captured]; calibrating = false; CalibrationOverlay.Visibility = Visibility.Collapsed;
            bool saved = Persist(); LoadEditor(); if (saved) StatusText.Text = "Calibrage terminé · Vos 12 boutons sont prêts !";
        }
        else { CalibrationError.Text = "Bien reçu. Passez au bouton suivant."; UpdateCalibration(); }
    }
    private void UpdateCalibration()
    {
        CalibrationPrompt.Text = $"Étape {captured.Count + 1}/12 · Appuyez sur le bouton ligne {captured.Count / 3 + 1}, colonne {captured.Count % 3 + 1}.";
        for (int i = 0; i < 12; i++)
        {
            calibrationCells[i].Background = Brush(i == captured.Count ? "#5267CF" : i < captured.Count ? "#20463E" : "#1C2436");
            calibrationLabels[i].Text = i < captured.Count ? Calibration.KeyName(captured[i]) + " ✓" : $"{i + 1:00}";
        }
    }
    private void RestartCalibration(object sender, RoutedEventArgs e) => BeginCalibration();
    private void CancelCalibration(object sender, RoutedEventArgs e)
    {
        calibrating = false; CalibrationOverlay.Visibility = Visibility.Collapsed;
        StatusText.Text = settings.IsCalibrated ? "Calibrage annulé · Ancienne disposition conservée." : "Calibrage requis · Les touches restent libres en attendant.";
    }
    private void CreateTray()
    {
        var menu = new Forms.ContextMenuStrip();
        menu.Items.Add("Ouvrir DeckPilot", null, (_, _) => Dispatcher.Invoke(ShowMain));
        menu.Items.Add("Quitter", null, (_, _) => Dispatcher.Invoke(() => { quitting = true; Close(); }));
        tray = new Forms.NotifyIcon { Icon = System.Drawing.SystemIcons.Application, Text = "DeckPilot", ContextMenuStrip = menu, Visible = true };
        tray.DoubleClick += (_, _) => Dispatcher.Invoke(ShowMain);
    }
    private void ShowMain() { Show(); WindowState = WindowState.Normal; Activate(); }
    private void WindowClosing(object? sender, CancelEventArgs e)
    {
        SaveEditor();
        if (!quitting)
        {
            // Never leave a hidden calibration consuming the user's keys.
            if (calibrating)
            {
                calibrating = false; CalibrationOverlay.Visibility = Visibility.Collapsed;
                StatusText.Text = "Calibrage annulé à la fermeture de la fenêtre.";
            }
            e.Cancel = true; Hide();
        }
    }
    private void ReportError(string message)
    {
        StatusText.Text = message;
        if (!IsVisible && tray != null) { tray.BalloonTipTitle = "DeckPilot"; tray.BalloonTipText = message; tray.ShowBalloonTip(4000); }
    }
    private static bool StartupEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
        return key?.GetValue("DeckPilot") != null;
    }
    private void StartupClick(object sender, RoutedEventArgs e)
    {
        try
        {
            using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (StartupCheck.IsChecked == true)
            {
                var executable = Environment.ProcessPath ?? throw new IOException("Exécutable introuvable.");
                if (!Path.GetFileName(executable).Equals("DeckPilot.exe", StringComparison.OrdinalIgnoreCase))
                    throw new IOException("Utilisez DeckPilot.exe depuis le dossier publié pour activer le démarrage automatique.");
                key.SetValue("DeckPilot", "\"" + executable + "\" --background");
            }
            else key.DeleteValue("DeckPilot", throwOnMissingValue: false);
            StatusText.Text = StartupCheck.IsChecked == true ? "Démarrage avec Windows activé." : "Démarrage avec Windows désactivé.";
        }
        catch (Exception ex) { StartupCheck.IsChecked = StartupEnabled(); ReportError("Démarrage automatique : " + ex.Message); }
    }
}
