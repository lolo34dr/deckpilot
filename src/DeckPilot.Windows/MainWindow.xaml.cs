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
    private readonly List<DeckKeyButton> deckButtons = [];
    private readonly DeckKeyFeedback[] feedback = new DeckKeyFeedback[12];
    private readonly string[] feedbackMessages = new string[12];
    private readonly long[] feedbackUntil = new long[12], pressedUntil = new long[12];
    private readonly int[] executionTickets = new int[12];
    private Guid feedbackProfile;
    private readonly UpdateService updateService = new();
    private readonly CancellationTokenSource lifetime = new();
    private readonly DispatcherTimer updateTimer = new() { Interval = TimeSpan.FromHours(6) };
    private bool updateBusy, firmwareOpen, firmwarePromptActive, loadingBoardPicker;
    private BoardConnectionService? boardConnection;
    private BoardConnectionState? boardState;
    private FirmwareManifest? bundledFirmware;
    private string bundledFirmwareError = "";
    private readonly DispatcherTimer firmwareTimer = new() { Interval = TimeSpan.FromSeconds(5) };
    private readonly DispatcherTimer boardUiTimer = new() { Interval = TimeSpan.FromMilliseconds(100) };
    private long firmwareQuietUntil;
    private readonly long startedAt = Environment.TickCount64;
    private readonly HashSet<string> firmwareOffered = [];
    private string deviceListSignature = "";
    private Forms.ToolStripMenuItem? trayProfiles;
    private DateTimeOffset lastUpdateCheck;
    private string pickedApplicationName = "";
    public bool IsOperational => hook != null;
    private readonly List<Button> actionButtons = [];
    private readonly List<Border> calibrationCells = [];
    private readonly List<TextBlock> calibrationLabels = [];
    private readonly DispatcherTimer feedbackTimer = new() { Interval = TimeSpan.FromMilliseconds(60) };
    private record Choice(string Name, int Key);
    private static readonly string[] ActionNames = ["Touche libre", "Ouvrir une application", "Ouvrir un dossier", "Ouvrir un site web", "Raccourci clavier", "Multimédia"];

    public MainWindow(bool startInBackground = false)
    {
        InitializeComponent();
        // Fit the first window to the usable desktop; the keypad scales down on smaller screens.
        Width = Math.Min(Width, SystemParameters.WorkArea.Width);
        Height = Math.Min(Height, SystemParameters.WorkArea.Height);
        MinWidth = Math.Min(MinWidth, Width); MinHeight = Math.Min(MinHeight, Height);
        SourceInitialized += (_, _) => EnableDarkTitleBar();
        try { settings = store.Load(); }
        catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException or System.Text.Json.JsonException)
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
        AutoFirmwareCheck.IsChecked = settings.AutoCheckFirmware;
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
        feedbackTimer.Tick += (_, _) => RefreshFeedback();
        Closing += WindowClosing;
        Closed += (_, _) => { boardUiTimer.Stop(); firmwareTimer.Stop(); boardConnection?.Dispose(); feedbackTimer.Stop(); updateTimer.Stop(); lifetime.Cancel(); updateService.Dispose(); hook?.Dispose(); tray?.Dispose(); trayIcon?.Dispose(); };
        loading = false;
        StatusText.Text = "Prêt · Choisissez un bouton pour le personnaliser.";
        updateTimer.Tick += async (_, _) => await CheckForUpdatesAsync(false);
        updateTimer.Start();
        firmwareTimer.Tick += async (_, _) => await OfferFirmwareUpdateAsync();
        firmwareTimer.Start();
        boardUiTimer.Tick += (_, _) => { if (boardConnection is { } connection) { var state = connection.State; if (!ReferenceEquals(state, boardState)) ApplyBoardState(state); } };
        boardUiTimer.Start();
        CommandBindings.Add(new System.Windows.Input.CommandBinding(System.Windows.Input.ApplicationCommands.Save, (_, e) => { SaveEditor(); e.Handled = true; }));
        InputBindings.Add(new System.Windows.Input.KeyBinding(System.Windows.Input.ApplicationCommands.Save, System.Windows.Input.Key.S, System.Windows.Input.ModifierKeys.Control));
        Loaded += async (_, _) =>
        {
            if (!settings.IsCalibrated) BeginCalibration(); else if (startInBackground) Hide();
            Motion.Reveal(WindowContent);
            boardConnection = new BoardConnectionService(settings.PreferredBoardDeviceId);
            try { bundledFirmware = await Task.Run(FirmwareService.ReadBundledManifest); ApplyBoardState(boardConnection.State); }
            catch (Exception ex) { bundledFirmwareError = ex.Message; BoardFirmwareStatus.Text = ex.Message; }
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
            var button = new DeckKeyButton();
            System.Windows.Automation.AutomationProperties.SetAutomationId(button, $"DeckKey{i + 1:00}");
            button.Click += (_, _) => { SaveEditor(); selected = index; LoadEditor(); };
            DeckGrid.Children.Add(button); deckButtons.Add(button);
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
        if (feedbackProfile != settings.ActiveProfile.Id)
        {
            feedbackProfile = settings.ActiveProfile.Id;
            for (int i = 0; i < 12; i++)
            {
                feedback[i] = DeckKeyFeedback.Ready; feedbackMessages[i] = "";
                feedbackUntil[i] = pressedUntil[i] = 0; executionTickets[i]++;
            }
        }
        for (int i = 0; i < 12; i++)
        {
            var action = settings.ActiveProfile.Buttons[i];
            string keyName = settings.IsCalibrated ? Calibration.KeyName(settings.KeyMap[i]) : $"{i + 1:00}";
            deckButtons[i].Label = string.IsNullOrWhiteSpace(action.Label) ? keyName : action.Label;
            deckButtons[i].KeyCaption = keyName;
            deckButtons[i].IconSource = action.Kind == ActionKind.Application ? ApplicationIcons.ForTarget(action.Target) : VectorIcons.ForKind(action.Kind);
            deckButtons[i].KindSource = VectorIcons.ForKind(action.Kind);
            deckButtons[i].IsSelected = i == selected;
            deckButtons[i].ToolTip = $"Ligne {i / 4 + 1}, colonne {i % 4 + 1} · {deckButtons[i].Label} · {keyName} · {ActionNames[(int)action.Kind]}";
            System.Windows.Automation.AutomationProperties.SetName(deckButtons[i], (string)deckButtons[i].ToolTip);
        }
        RefreshFeedback();
    }
    private void ApplyHardwareHighlights()
    {
        int mask = boardState?.Kind == BoardConnectionKind.Connected ? boardState.Telemetry?.Mask ?? 0 : 0;
        for (int i = 0; i < deckButtons.Count; i++)
        {
            int key = settings.IsCalibrated ? settings.KeyMap[i] : Calibration.FirstKey + i;
            bool down = (mask & (1 << (key - Calibration.FirstKey))) != 0;
            deckButtons[i].IsHardwarePressed = down || pressedUntil[i] > Environment.TickCount64;
        }
    }
    private void RefreshFeedback()
    {
        long now = Environment.TickCount64;
        bool pending = false;
        for (int i = 0; i < 12; i++)
        {
            if (feedbackUntil[i] > 0 && feedbackUntil[i] <= now)
            {
                feedback[i] = DeckKeyFeedback.Ready; feedbackMessages[i] = ""; feedbackUntil[i] = 0;
            }
            pending |= feedback[i] == DeckKeyFeedback.Running || feedbackUntil[i] > now || pressedUntil[i] > now;
            deckButtons[i].Feedback = feedback[i];
            string state = feedback[i] switch { DeckKeyFeedback.Running => "Action en cours", DeckKeyFeedback.Success => "Action envoyée", DeckKeyFeedback.Error => "Échec de l’action", _ => "Prêt" };
            System.Windows.Automation.AutomationProperties.SetItemStatus(deckButtons[i], state);
            System.Windows.Automation.AutomationProperties.SetHelpText(deckButtons[i], feedbackMessages[i] ?? "");
        }
        ApplyHardwareHighlights();
        ActionExecutionStatus.Text = !string.IsNullOrEmpty(feedbackMessages[selected]) ? feedbackMessages[selected] :
            settings.ActiveProfile.Buttons[selected].Kind == ActionKind.Passthrough ? "Aucune action à tester pour une touche libre." : "Prêt à tester · L’action sera exécutée sur ce PC.";
        ActionExecutionStatus.Foreground = Brush(feedback[selected] == DeckKeyFeedback.Error ? "#E1A398" : feedback[selected] == DeckKeyFeedback.Success ? "#AED3B8" : "#A8B6AC");
        TestActionButton.IsEnabled = settings.ActiveProfile.Buttons[selected].Kind != ActionKind.Passthrough && feedback[selected] != DeckKeyFeedback.Running && !firmwareOpen && !calibrating;
        if (pending) feedbackTimer.Start(); else feedbackTimer.Stop();
    }
    private void SetFeedback(int index, DeckKeyFeedback state, string message)
    {
        feedback[index] = state; feedbackMessages[index] = message;
        feedbackUntil[index] = state is DeckKeyFeedback.Success or DeckKeyFeedback.Error ? Environment.TickCount64 + (state == DeckKeyFeedback.Error ? 6000 : 3000) : 0;
        RefreshFeedback();
        if (index == selected) Motion.Reveal(ActionExecutionStatus);
    }
    private void RefreshProfiles()
    {
        bool previous = loading; loading = true;
        ProfilePicker.ItemsSource = null;
        ProfilePicker.ItemsSource = settings.Profiles;
        ProfilePicker.SelectedItem = settings.ActiveProfile;
        ProfileName.Text = settings.ActiveProfile.Name;
        loading = previous; RefreshTrayProfiles();
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
            actionButtons[i].Background = Brush(i == (int)kind ? "#344B3B" : "#222622");
            actionButtons[i].BorderBrush = Brush(i == (int)kind ? "#90B89B" : "#222622");
        }
        ApplicationPanel.Visibility = kind == ActionKind.Application ? Visibility.Visible : Visibility.Collapsed;
        AdvancedOptionsExpander.Visibility = kind is ActionKind.Application or ActionKind.Folder or ActionKind.Website ? Visibility.Visible : Visibility.Collapsed;
        AdvancedOptionsExpander.Header = kind == ActionKind.Application ? "Options avancées · fichier et arguments" : "Destination";
        AdvancedOptionsExpander.IsExpanded = kind != ActionKind.Application;
        TargetPanel.Visibility = kind is ActionKind.Application or ActionKind.Folder or ActionKind.Website ? Visibility.Visible : Visibility.Collapsed;
        ArgumentsPanel.Visibility = kind == ActionKind.Application && !ApplicationTarget.IsPackaged(TargetEditor.Text) ? Visibility.Visible : Visibility.Collapsed;
        ShortcutPanel.Visibility = kind == ActionKind.Shortcut ? Visibility.Visible : Visibility.Collapsed;
        MediaPanel.Visibility = kind == ActionKind.Media ? Visibility.Visible : Visibility.Collapsed;
        BrowseButton.Visibility = kind == ActionKind.Website ? Visibility.Collapsed : Visibility.Visible;
        TargetLabel.Text = kind == ActionKind.Website ? "Adresse du site" : kind == ActionKind.Folder ? "Chemin du dossier" : "Chemin du programme";
        ActionHelp.Text = kind switch
        {
            ActionKind.Passthrough => settings.IsCalibrated ? "Cette touche F13–F24 reste libre. Attribuez-la directement dans votre jeu ou application." : "Le clavier F13–F24 fonctionne sans calibrage. Calibrez les positions pour que DeckPilot puisse exécuter vos actions personnalisées.",
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
        RefreshTrayProfiles();
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
        Dispatcher.BeginInvoke(async () =>
        {
            pressedUntil[index] = Environment.TickCount64 + 180; RefreshFeedback();
            await ExecuteActionAsync(index, action);
        });
    }
    private async void TestActionClick(object sender, RoutedEventArgs e)
    {
        if (calibrating || firmwareOpen || !SaveEditor()) return;
        await ExecuteActionAsync(selected, settings.ActiveProfile.Buttons[selected].Copy());
    }
    private async Task ExecuteActionAsync(int index, ButtonAction action)
    {
        if (action.Kind == ActionKind.Passthrough || lifetime.IsCancellationRequested) return;
        int ticket = ++executionTickets[index];
        long dispatchedAt = Environment.TickCount64;
        SetFeedback(index, DeckKeyFeedback.Running, "Action en cours…");
        try
        {
            // Let WPF render feedback before dispatching; keyboard commands stay on the UI thread.
            await Dispatcher.Yield(DispatcherPriority.Render);
            if (action.Kind is ActionKind.Application or ActionKind.Folder or ActionKind.Website)
                await Task.Run(() => ActionRunner.Run(action));
            else ActionRunner.Run(action);
            // Keep short actions readable, without delaying their actual execution.
            int remaining = (int)Math.Max(0, 300 - (Environment.TickCount64 - dispatchedAt));
            if (remaining > 0) await Task.Delay(remaining, lifetime.Token);
            if (lifetime.IsCancellationRequested || executionTickets[index] != ticket) return;
            SetFeedback(index, DeckKeyFeedback.Success, "Action envoyée ✓");
            StatusText.Text = $"Action envoyée · {deckButtons[index].Label}";
        }
        catch (Exception ex)
        {
            if (lifetime.IsCancellationRequested || executionTickets[index] != ticket) return;
            SetFeedback(index, DeckKeyFeedback.Error, "Échec · " + ex.Message);
            ReportError("Action impossible : " + ex.Message);
        }
    }
    private async void FirmwareClick(object sender, RoutedEventArgs e) => await OpenFirmwareAsync();
    private async Task OpenFirmwareAsync()
    {
        if (updateBusy || firmwareOpen || firmwarePromptActive || OwnedWindows.Count > 0 || calibrating || lifetime.IsCancellationRequested) return;
        if (!SaveEditor()) return;
        string preferredId = boardState?.Devices.Any(d => d.DeviceId == settings.PreferredBoardDeviceId) == true
            ? settings.PreferredBoardDeviceId : boardState?.Device?.DeviceId ?? settings.PreferredBoardDeviceId;
        firmwareOpen = true;
        try
        {
            if (boardConnection != null) await boardConnection.PauseAsync();
            ShowMain();
            var dialog = new FirmwareWindow(preferredId) { Owner = this };
            dialog.ShowDialog();
            if (dialog.SelectedDeviceId is { Length: > 0 } id)
            {
                settings.PreferredBoardDeviceId = id; Persist(); boardConnection?.Select(id);
            }
        }
        catch (Exception ex) { ReportError("Firmware : " + ex.Message); }
        finally { firmwareOpen = false; boardConnection?.Resume(); }
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
        trayProfiles = new Forms.ToolStripMenuItem("Profils"); menu.Items.Add(trayProfiles);
        menu.Items.Add("Quitter", null, (_, _) => Dispatcher.Invoke(() => { quitting = true; Close(); }));
        trayIcon = BrandAssets.CreateTrayIcon();
        tray = new Forms.NotifyIcon { Icon = trayIcon, Text = "DeckPilot", ContextMenuStrip = menu, Visible = true };
        tray.DoubleClick += (_, _) => Dispatcher.Invoke(ShowMain);
        RefreshTrayProfiles();
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
    private void RefreshTrayProfiles()
    {
        if (trayProfiles == null) return;
        trayProfiles.DropDownItems.Clear();
        foreach (var profile in settings.Profiles)
        {
            var item = new Forms.ToolStripMenuItem(profile.Name.Replace("&", "&&")) { Checked = profile.Id == settings.ActiveProfile.Id };
            item.Click += (_, _) => Dispatcher.Invoke(() => { if (!firmwareOpen && !calibrating && OwnedWindows.Count == 0) ProfilePicker.SelectedItem = profile; });
            trayProfiles.DropDownItems.Add(item);
        }
    }
    private void ApplyBoardState(BoardConnectionState state)
    {
        if (lifetime.IsCancellationRequested) return;
        boardState = state;
        bool connected = state.Kind == BoardConnectionKind.Connected;
        DeckAvailabilityStatus.Text = state.Kind switch
        {
            BoardConnectionKind.Connected => "Connecté",
            BoardConnectionKind.Disconnected => "Hors ligne",
            BoardConnectionKind.ChooseDevice => "Choisir la carte",
            BoardConnectionKind.Paused => "Firmware",
            BoardConnectionKind.Unidentified => "Carte à identifier",
            BoardConnectionKind.Error => "Liaison indisponible",
            _ => "Connexion…"
        };
        DeckConnectionDot.Fill = Brush(connected ? "#A5D1B1" : "#ADB6AF");
        DeckConnectionPill.Background = Brush(connected ? "#304639" : "#363C37");
        DeckAvailabilityHint.Text = connected ? "Configuration sur ce PC · Cliquez sur une touche pour la modifier."
            : state.Kind == BoardConnectionKind.Disconnected ? "Carte débranchée · Modifiez et testez vos actions sur ce PC."
            : "Vos profils restent modifiables et testables pendant la connexion.";
        BoardConnectionStatus.Text = state.Message;
        BoardConnectionStatus.Foreground = Brush(state.Kind == BoardConnectionKind.Connected ? "#A8C4AF" : state.Kind is BoardConnectionKind.Error or BoardConnectionKind.Unidentified ? "#D1B18C" : "#A5AAA6");
        string signature = string.Join("|", state.Devices.Select(d => d.DeviceId + ":" + d.Port + ":" + d.Location + ":" + d.Name));
        if (signature != deviceListSignature)
        {
            deviceListSignature = signature; loadingBoardPicker = true;
            BoardDevicePicker.ItemsSource = state.Devices;
            BoardDevicePicker.Visibility = state.Devices.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
            if (state.Kind == BoardConnectionKind.ChooseDevice) BoardDiagnosticExpander.IsExpanded = true;
            loadingBoardPicker = false;
        }
        var target = state.Devices.FirstOrDefault(d => d.DeviceId == settings.PreferredBoardDeviceId)
            ?? state.Devices.FirstOrDefault(d => d.DeviceId == state.Device?.DeviceId);
        if ((BoardDevicePicker.SelectedItem as FirmwareDevice)?.DeviceId != target?.DeviceId)
        {
            loadingBoardPicker = true; BoardDevicePicker.SelectedItem = target; loadingBoardPicker = false;
        }
        string installed = state.Info?.Firmware.Version ?? "";
        bool upgrade = bundledFirmware != null && state.Info != null && Version.Parse(installed) < Version.Parse(bundledFirmware.Version);
        BoardFirmwareStatus.Text = installed.Length > 0 ? $"Firmware {installed}" + (upgrade ? $" · {bundledFirmware!.Version} disponible" : bundledFirmware != null && installed == bundledFirmware.Version ? " · À jour" : "") : bundledFirmware == null ? bundledFirmwareError.Length > 0 ? bundledFirmwareError : "Lecture du firmware inclus…" : $"Firmware inclus : {bundledFirmware.Version}";
        FirmwareButton.Content = upgrade ? $"Installer le firmware {bundledFirmware!.Version}…" : "Firmware du macropad…";
        BoardLinkDetails.Text = state.Info?.Protocol == 2
            ? state.Telemetry is { } t ? $"USB actif · {(t.LatencyMilliseconds >= 0 ? t.LatencyMilliseconds + " ms" : "Synchronisation")} · {System.Numerics.BitOperations.PopCount((uint)t.Mask)}/12 enfoncés" : "Liaison USB identifiée · Synchronisation…"
            : state.Info != null ? "Liaison classique · Diagnostic en direct disponible avec le nouveau firmware."
            : state.Kind == BoardConnectionKind.Error ? "Fermez les autres outils qui utilisent la carte. DeckPilot tente de se reconnecter automatiquement."
            : "Les touches F13–F24 restent utilisables en mode clavier.";
        ApplyHardwareHighlights();
    }
    private void BoardDeviceChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loadingBoardPicker || BoardDevicePicker.SelectedItem is not FirmwareDevice device || firmwareOpen) return;
        settings.PreferredBoardDeviceId = device.DeviceId; Persist(); boardConnection?.Select(device.DeviceId);
        if (boardConnection != null) ApplyBoardState(boardConnection.State);
    }
    private void AutoFirmwareClick(object sender, RoutedEventArgs e)
    {
        settings.AutoCheckFirmware = AutoFirmwareCheck.IsChecked == true; Persist();
    }
    private async Task OfferFirmwareUpdateAsync()
    {
        if (Environment.TickCount64 < firmwareQuietUntil || Environment.TickCount64 - startedAt < 15000 || !settings.AutoCheckFirmware || settings.FirmwareSnoozedUntil > DateTimeOffset.UtcNow || bundledFirmware == null || boardState?.Kind != BoardConnectionKind.Connected || boardState.Info == null || calibrating || updateBusy || firmwareOpen || firmwarePromptActive || OwnedWindows.Count > 0 || lifetime.IsCancellationRequested) return;
        var current = boardState.Info.Firmware;
        if (Version.Parse(current.Version) >= Version.Parse(bundledFirmware.Version)) return;
        string offer = boardState.Device!.DeviceId + "|" + bundledFirmware.Version;
        if (!firmwareOffered.Add(offer)) return;
        firmwarePromptActive = true;
        try
        {
            string message = $"Le firmware {bundledFirmware.Version} est disponible pour votre DeckPilot (version actuelle : {current.Version}).\n\nOuvrir l’installation ? Le flash nécessitera votre confirmation.";
            var answer = IsVisible ? MessageBox.Show(this, message, "Mise à jour du firmware", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No)
                : MessageBox.Show(message, "Mise à jour du firmware", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            settings.FirmwareSnoozedUntil = DateTimeOffset.UtcNow.AddHours(24); Persist();
            if (answer == MessageBoxResult.Yes) await OpenFirmwareAsync();
        }
        finally { firmwarePromptActive = false; }
    }
    private void CopyDiagnosticClick(object sender, RoutedEventArgs e)
    {
        var state = boardState;
        string latency = state?.Telemetry is { LatencyMilliseconds: >= 0 } telemetry ? telemetry.LatencyMilliseconds.ToString() : "Inconnue";
        string text = $"DeckPilot {UpdateService.DisplayVersion}\nÉtat : {state?.Message ?? "Initialisation"}\nPort : {state?.Device?.Port ?? "Aucun"}\nFirmware : {state?.Info?.Firmware.Version ?? "Non identifié"}\nFirmware inclus : {bundledFirmware?.Version ?? "Indisponible"}\nProtocole : {state?.Info?.Protocol.ToString() ?? "Non identifié"}\nLatence : {latency} ms\nTemps de fonctionnement : {TimeSpan.FromMilliseconds(state?.Telemetry?.Uptime ?? 0)}\n";
        try { Clipboard.SetText(text); StatusText.Text = "Diagnostic copié. Vous pouvez le joindre à un signalement de problème."; }
        catch (Exception ex) { ReportError("Copie impossible : " + ex.Message); }
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
        if (updateBusy || firmwareOpen || firmwarePromptActive || OwnedWindows.Count > 0 || calibrating || lifetime.IsCancellationRequested || (!manual && (!settings.AutoCheckUpdates || settings.UpdateSnoozedUntil > DateTimeOffset.UtcNow || DateTimeOffset.UtcNow - lastUpdateCheck < TimeSpan.FromMinutes(5)))) return;
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
            else
            {
                firmwareQuietUntil = Environment.TickCount64 + 300000;
                UpdateStatus.Text = "L’installateur prendra le relais. Vos réglages seront conservés.";
            }
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
