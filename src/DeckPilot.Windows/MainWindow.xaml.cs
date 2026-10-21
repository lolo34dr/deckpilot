using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;
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
    private System.Drawing.Icon? trayIcon;
    private bool loading = true, quitting, calibrating;
    private bool layoutChanged;
    private int selected;
    private readonly List<int> captured = [];
    private readonly List<Button> deckButtons = [];
    private readonly List<TextBlock> deckLabels = [], deckKeys = [];
    private readonly List<Image> deckIcons = [];
    private readonly UpdateService updateService = new();
    private readonly CancellationTokenSource lifetime = new();
    private readonly DispatcherTimer updateTimer = new() { Interval = TimeSpan.FromHours(6) };
    private bool updateBusy, firmwareOpen;
    private DateTimeOffset lastUpdateCheck;
    private string pickedApplicationName = "";
    public bool IsOperational => hook != null;
    private readonly List<Button> actionButtons = [];
    private readonly List<Border> calibrationCells = [];
    private readonly List<TextBlock> calibrationLabels = [];
    private readonly DispatcherTimer flashTimer = new() { Interval = TimeSpan.FromMilliseconds(220) };
    private record Choice(string Name, int Key);
    private static readonly string[] ActionNames = ["Touche libre", "Ouvrir une application", "Ouvrir un dossier", "Ouvrir un site web", "Raccourci clavier", "Multimédia"];

    public MainWindow(bool startInBackground = false)
    {
        InitializeComponent();
        SourceInitialized += (_, _) => EnableDarkTitleBar();
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
        layoutChanged = settings.MigrateLayout();
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
        try { StartupService.Initialize(); }
        catch (Exception ex) { StatusText.Text = "Démarrage Windows : " + ex.Message; }
        StartupCheck.IsChecked = StartupService.Enabled;
        AutoUpdateCheck.IsChecked = settings.AutoCheckUpdates;
        VersionLabel.Text = "v" + UpdateService.DisplayVersion;
        UpdateStatus.Text = "Recherche au démarrage, puis toutes les 6 h.";
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
        Closed += (_, _) => { flashTimer.Stop(); updateTimer.Stop(); lifetime.Cancel(); updateService.Dispose(); hook?.Dispose(); tray?.Dispose(); trayIcon?.Dispose(); };
        loading = false;
        StatusText.Text = "Prêt · Choisissez un bouton pour le personnaliser.";
        updateTimer.Tick += async (_, _) => await CheckForUpdatesAsync(false);
        updateTimer.Start();
        Loaded += async (_, _) =>
        {
            if (!settings.IsCalibrated) BeginCalibration(); else if (startInBackground) Hide();
            Motion.Reveal(WindowContent);
            try { await Task.Delay(8000, lifetime.Token); await CheckForUpdatesAsync(false); }
            catch (OperationCanceledException) { }
        };
        _ = Task.Run(UpdateService.CleanOldDownloads);
    }
    private void BuildGrid()
    {
        for (int i = 0; i < 12; i++)
        {
            int index = i;
            var content = new Grid();
            var key = new TextBlock { FontSize = 9, Foreground = Brush("#919191"), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top };
            var icon = new Image { Width = 27, Height = 27, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 0, 7) };
            var label = new TextBlock { FontSize = 10, HorizontalAlignment = HorizontalAlignment.Stretch, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Bottom, TextTrimming = TextTrimming.CharacterEllipsis };
            content.Children.Add(icon); content.Children.Add(key); content.Children.Add(label);
            var button = new Button { Content = content, Width = 76, Height = 76, HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch, Padding = new Thickness(7), Margin = new Thickness(5) };
            button.Click += (_, _) => { SaveEditor(); selected = index; LoadEditor(); };
            DeckGrid.Children.Add(button); deckButtons.Add(button); deckLabels.Add(label); deckKeys.Add(key); deckIcons.Add(icon);
            var calibrationLabel = new TextBlock { Text = $"{i + 1:00}", FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            var cell = new Border { CornerRadius = new CornerRadius(6), BorderThickness = new Thickness(1), Margin = new Thickness(6), Child = calibrationLabel };
            CalibrationGrid.Children.Add(cell); calibrationCells.Add(cell); calibrationLabels.Add(calibrationLabel);
        }
        for (int i = 0; i < ActionNames.Length; i++)
        {
            int index = i;
            var row = new StackPanel { Orientation = Orientation.Horizontal };
            row.Children.Add(new Image { Source = VectorIcons.ForKind((ActionKind)i), Width = 18, Height = 18, Margin = new Thickness(0, 0, 12, 0), VerticalAlignment = VerticalAlignment.Center });
            row.Children.Add(new TextBlock { Text = ActionNames[i], VerticalAlignment = VerticalAlignment.Center, FontSize = 12 });
            var button = new Button { Content = row, HorizontalContentAlignment = HorizontalAlignment.Stretch, Padding = new Thickness(10, 12, 8, 12), Margin = new Thickness(0, 0, 0, 3) };
            button.Click += (_, _) => { ActionPicker.SelectedIndex = index; SaveEditor(); };
            System.Windows.Automation.AutomationProperties.SetName(button, ActionNames[i]);
            ActionCatalog.Children.Add(button); actionButtons.Add(button);
        }
    }
    private void EnableDarkTitleBar()
    {
        int enabled = 1;
        var handle = new WindowInteropHelper(this).Handle;
        if (DwmSetWindowAttribute(handle, 20, ref enabled, sizeof(int)) != 0)
            DwmSetWindowAttribute(handle, 19, ref enabled, sizeof(int));
    }
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr window, int attribute, ref int value, int size);
    private static SolidColorBrush Brush(string color) => new((Color)ColorConverter.ConvertFromString(color));
    private void RenderGrid()
    {
        for (int i = 0; i < 12; i++)
        {
            var action = settings.ActiveProfile.Buttons[i];
            string keyName = settings.IsCalibrated ? Calibration.KeyName(settings.KeyMap[i]) : $"{i + 1:00}";
            deckLabels[i].Text = string.IsNullOrWhiteSpace(action.Label) ? keyName : action.Label;
            deckKeys[i].Text = string.IsNullOrWhiteSpace(action.Label) ? "" : keyName;
            deckIcons[i].Source = action.Kind == ActionKind.Application ? ApplicationIcons.ForTarget(action.Target) : VectorIcons.ForKind(action.Kind);
            deckIcons[i].Opacity = action.Kind == ActionKind.Passthrough ? 0.6 : 1;
            deckButtons[i].Background = Brush(i == selected ? "#303632" : "#191B1A");
            deckButtons[i].BorderBrush = Brush(i == selected ? "#81A58B" : "#505350");
            deckButtons[i].ToolTip = $"Ligne {i / 4 + 1}, colonne {i % 4 + 1} · {deckLabels[i].Text} · {keyName} · {ActionNames[(int)action.Kind]}";
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
        SelectedKey.Text = $"Ligne {selected / 4 + 1}, colonne {selected % 4 + 1} · " + (settings.IsCalibrated ? Calibration.KeyName(settings.KeyMap[selected]) : "Calibrage nécessaire");
        LabelEditor.Text = action.Label;
        ActionPicker.SelectedIndex = (int)action.Kind;
        pickedApplicationName = action.ApplicationName;
        TargetEditor.Text = action.Target;
        ArgumentsEditor.Text = action.Arguments;
        CtrlCheck.IsChecked = action.Ctrl; AltCheck.IsChecked = action.Alt; ShiftCheck.IsChecked = action.Shift; WinCheck.IsChecked = action.Win;
        ShortcutPicker.SelectedItem = ((IEnumerable<Choice>)ShortcutPicker.ItemsSource).FirstOrDefault(c => c.Key == action.Key) ?? ((IEnumerable<Choice>)ShortcutPicker.ItemsSource).First();
        MediaPicker.SelectedItem = ((IEnumerable<Choice>)MediaPicker.ItemsSource).FirstOrDefault(c => c.Key == action.Key) ?? ((IEnumerable<Choice>)MediaPicker.ItemsSource).First();
        UpdateActionFields(); RenderGrid(); loading = previous; Motion.Reveal(EditorPanel);
    }
    private bool SaveEditor()
    {
        if (loading || ActionPicker.SelectedIndex < 0) return true;
        var action = settings.ActiveProfile.Buttons[selected];
        action.Label = LabelEditor.Text.Trim(); action.Kind = (ActionKind)ActionPicker.SelectedIndex;
        action.Target = TargetEditor.Text.Trim(); action.Arguments = ArgumentsEditor.Text; action.ApplicationName = pickedApplicationName;
        action.Ctrl = CtrlCheck.IsChecked == true; action.Alt = AltCheck.IsChecked == true;
        action.Shift = ShiftCheck.IsChecked == true; action.Win = WinCheck.IsChecked == true;
        if (action.Kind == ActionKind.Shortcut && ShortcutPicker.SelectedItem is Choice shortcut) action.Key = shortcut.Key;
        if (action.Kind == ActionKind.Media && MediaPicker.SelectedItem is Choice media) action.Key = media.Key;
        bool saved = Persist(); RenderGrid(); return saved;
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
        EditorActionTitle.Text = ActionNames[(int)kind];
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].Background = Brush(i == (int)kind ? "#343434" : "#222222");
            actionButtons[i].BorderBrush = Brush(i == (int)kind ? "#545454" : "#222222");
        }
        ApplicationPanel.Visibility = kind == ActionKind.Application ? Visibility.Visible : Visibility.Collapsed;
        ManualTargetExpander.Visibility = kind is ActionKind.Application or ActionKind.Folder or ActionKind.Website ? Visibility.Visible : Visibility.Collapsed;
        ManualTargetExpander.Header = kind == ActionKind.Application ? "Choisir un fichier manuellement" : "Destination";
        ManualTargetExpander.IsExpanded = kind != ActionKind.Application;
        TargetPanel.Visibility = kind is ActionKind.Application or ActionKind.Folder or ActionKind.Website ? Visibility.Visible : Visibility.Collapsed;
        ArgumentsPanel.Visibility = kind == ActionKind.Application && !ApplicationTarget.IsPackaged(TargetEditor.Text) ? Visibility.Visible : Visibility.Collapsed;
        ShortcutPanel.Visibility = kind == ActionKind.Shortcut ? Visibility.Visible : Visibility.Collapsed;
        MediaPanel.Visibility = kind == ActionKind.Media ? Visibility.Visible : Visibility.Collapsed;
        BrowseButton.Visibility = kind == ActionKind.Website ? Visibility.Collapsed : Visibility.Visible;
        TargetLabel.Text = kind == ActionKind.Website ? "Adresse du site" : kind == ActionKind.Folder ? "Chemin du dossier" : "Chemin du programme";
        ActionHelp.Text = kind switch
        {
            ActionKind.Passthrough => "La touche F13–F24 est transmise normalement. Attribuez-la directement dans votre jeu.",
            ActionKind.Application => "Choisissez une application installée, ou sélectionnez son fichier manuellement.",
            ActionKind.Folder => "Ouvre le dossier dans l’Explorateur Windows.",
            ActionKind.Website => "Ouvre une adresse http:// ou https:// dans votre navigateur par défaut.",
            ActionKind.Shortcut => "Envoie ce raccourci à la fenêtre active. Les touches F13–F24 simulées ne déclenchent pas d’autres actions DeckPilot.",
            _ => "Contrôle la lecture ou le volume de Windows."
        };
        UpdateApplicationSummary();
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
        if (firmwareOpen) return;
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
            deckButtons[index].Background = Brush("#4A6150"); flashTimer.Stop(); flashTimer.Start();
            try
            {
                ActionRunner.Run(action);
                if (action.Kind != ActionKind.Passthrough) StatusText.Text = $"Action exécutée · {deckLabels[index].Text}";
            }
            catch (Exception ex) { ReportError("Action impossible : " + ex.Message); }
        });
    }
    private void FirmwareClick(object sender, RoutedEventArgs e)
    {
        if (updateBusy || firmwareOpen || calibrating) return;
        if (!SaveEditor()) return;
        firmwareOpen = true;
        try { new FirmwareWindow { Owner = this }.ShowDialog(); }
        finally { firmwareOpen = false; }
    }
    private void CalibrateClick(object sender, RoutedEventArgs e) => BeginCalibration();
    private void BeginCalibration()
    {
        SaveEditor(); captured.Clear(); calibrating = true;
        CalibrationOverlay.Visibility = Visibility.Visible; CalibrationError.Text = layoutChanged ? "Disposition corrigée : 3 lignes de 4. Recalibrez les positions ; vos profils sont conservés." : "Les actions personnalisées sont suspendues pendant le calibrage.";
        UpdateCalibration();
    }
    private void CaptureKey(int key)
    {
        if (!calibrating) return;
        if (captured.Contains(key)) { CalibrationError.Text = $"{Calibration.KeyName(key)} est déjà attribuée. Appuyez sur un autre bouton physique."; return; }
        captured.Add(key);
        if (captured.Count == 12)
        {
            settings.KeyMap = [.. captured]; layoutChanged = false; calibrating = false; CalibrationOverlay.Visibility = Visibility.Collapsed;
            bool saved = Persist(); LoadEditor(); if (saved) StatusText.Text = "Calibrage terminé · Vos 12 boutons sont prêts !";
            _ = CheckForUpdatesAsync(false);
        }
        else { CalibrationError.Text = "Bien reçu. Passez au bouton suivant."; UpdateCalibration(); }
    }
    private void UpdateCalibration()
    {
        CalibrationPrompt.Text = $"Étape {captured.Count + 1}/12 · Appuyez sur le bouton ligne {captured.Count / 4 + 1}, colonne {captured.Count % 4 + 1}.";
        for (int i = 0; i < 12; i++)
        {
            calibrationCells[i].Background = Brush(i == captured.Count ? "#3C4A3F" : i < captured.Count ? "#2D332E" : "#1C1D1C");
            calibrationCells[i].BorderBrush = Brush(i == captured.Count ? "#91B39A" : "#505350");
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
        trayIcon = BrandAssets.CreateTrayIcon();
        tray = new Forms.NotifyIcon { Icon = trayIcon, Text = "DeckPilot", ContextMenuStrip = menu, Visible = true };
        tray.DoubleClick += (_, _) => Dispatcher.Invoke(ShowMain);
    }
    private void ShowMain() { Show(); WindowState = WindowState.Normal; Activate(); }
    private void WindowClosing(object? sender, CancelEventArgs e)
    {
        if (firmwareOpen) { e.Cancel = true; return; }
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
    private void ChooseApplicationClick(object sender, RoutedEventArgs e)
    {
        var picker = new ApplicationPicker { Owner = this };
        if (picker.ShowDialog() != true || picker.SelectedApplication is not { } app) return;
        TargetEditor.Text = app.Target;
        pickedApplicationName = app.Name;
        if (string.IsNullOrWhiteSpace(LabelEditor.Text)) LabelEditor.Text = app.Name[..Math.Min(50, app.Name.Length)];
        UpdateApplicationSummary(); SaveEditor();
    }
    private void TargetChanged(object sender, TextChangedEventArgs e)
    {
        if (loading) return;
        pickedApplicationName = ""; UpdateApplicationSummary();
    }
    private void UpdateApplicationSummary()
    {
        string target = TargetEditor.Text.Trim();
        SelectedApplicationName.Text = !string.IsNullOrWhiteSpace(pickedApplicationName) ? pickedApplicationName :
            target.Length == 0 ? "Parcourir les applications installées" : ApplicationTarget.IsPackaged(target) ? "Application Windows sélectionnée" : Path.GetFileNameWithoutExtension(target);
        SelectedApplicationIcon.Source = ApplicationIcons.ForTarget(target);
        bool packaged = ActionPicker.SelectedIndex == (int)ActionKind.Application && ApplicationTarget.IsPackaged(target);
        TargetEditor.Visibility = packaged ? Visibility.Collapsed : Visibility.Visible;
        if (ActionPicker.SelectedIndex == (int)ActionKind.Application)
        {
            ArgumentsPanel.Visibility = packaged ? Visibility.Collapsed : Visibility.Visible;
            TargetLabel.Text = packaged ? "Remplacer par un fichier" : "Chemin du programme";
        }
    }
    private async void CheckUpdateClick(object sender, RoutedEventArgs e) => await CheckForUpdatesAsync(true);
    private void AutoUpdateClick(object sender, RoutedEventArgs e)
    {
        settings.AutoCheckUpdates = AutoUpdateCheck.IsChecked == true; Persist();
        UpdateStatus.Text = settings.AutoCheckUpdates ? "Recherche automatique activée." : "Recherche automatique désactivée.";
        if (settings.AutoCheckUpdates) _ = CheckForUpdatesAsync(false);
    }
    private async Task CheckForUpdatesAsync(bool manual)
    {
        if (updateBusy || firmwareOpen || calibrating || lifetime.IsCancellationRequested || (!manual && (!settings.AutoCheckUpdates || settings.UpdateSnoozedUntil > DateTimeOffset.UtcNow || DateTimeOffset.UtcNow - lastUpdateCheck < TimeSpan.FromMinutes(5)))) return;
        lastUpdateCheck = DateTimeOffset.UtcNow;
        updateBusy = true; UpdateCheckButton.IsEnabled = false; UpdateStatus.Text = "Recherche d’une mise à jour…";
        try
        {
            var release = await updateService.CheckAsync(lifetime.Token);
            if (lifetime.IsCancellationRequested) return;
            if (release == null) { UpdateStatus.Text = "Vous utilisez la dernière version."; return; }
            UpdateStatus.Text = $"Version {release.Version} disponible.";
            var dialog = new UpdateWindow(updateService, release);
            if (IsVisible) { dialog.Owner = this; dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner; }
            if (dialog.ShowDialog() != true || dialog.Staged == null)
            {
                settings.UpdateSnoozedUntil = DateTimeOffset.UtcNow.AddHours(24); Persist();
                UpdateStatus.Text = "Rappel demain, ou vérification manuelle."; return;
            }
            if (!SaveEditor()) { UpdateStatus.Text = "Mise à jour suspendue : vos réglages n’ont pas pu être enregistrés."; return; }
            UpdateStatus.Text = "Redémarrage pour installer la mise à jour…";
            bool shutdown = await updateService.StartInstallerAsync(dialog.Staged, !IsVisible);
            if (shutdown) { quitting = true; Application.Current.Shutdown(); }
            else UpdateStatus.Text = "L’installateur prendra le relais. Vos réglages seront conservés.";
        }
        catch (OperationCanceledException) { if (!lifetime.IsCancellationRequested) UpdateStatus.Text = "La vérification a pris trop de temps. Réessayez plus tard."; }
        catch (Exception ex)
        {
            if (!lifetime.IsCancellationRequested)
            {
                UpdateStatus.Text = "Mise à jour indisponible. Réessayez plus tard.";
                if (manual) ReportError(ex.Message);
            }
        }
        finally { updateBusy = false; if (!quitting) UpdateCheckButton.IsEnabled = true; }
    }
    public void ReportUpdateFailure() => ReportError("La mise à jour n’a pas pu démarrer. L’ancienne version a été conservée ou restaurée.");
    public void RequestInstallerExit()
    {
        if (firmwareOpen || updateBusy || !SaveEditor()) return;
        quitting = true; Close();
    }
    private void StartupClick(object sender, RoutedEventArgs e)
    {
        try
        {
            StartupService.Set(StartupCheck.IsChecked == true);
            StatusText.Text = StartupCheck.IsChecked == true ? "Démarrage avec Windows activé." : "Démarrage avec Windows désactivé.";
        }
        catch (Exception ex) { StartupCheck.IsChecked = StartupService.Enabled; ReportError("Démarrage automatique : " + ex.Message); }
    }
}
