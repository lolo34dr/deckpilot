using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using DeckPilot.Core;
using Microsoft.Win32;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private SoundboardEngine? soundboard;
    private SoundboardEngine Audio
    {
        get
        {
            if (soundboard == null)
            {
                soundboard = new SoundboardEngine();
                soundboard.StateChanged += () => Dispatcher.BeginInvoke(() =>
                {
                    if (lifetime.IsCancellationRequested) return;
                    try
                    {
                        string? error = soundboard.ProcessEvents();
                        SoundboardStartButton.Content = soundboard.IsRunning ? "Arrêter le micro virtuel" : "Activer le micro virtuel";
                        if (error != null) SoundboardStatus.Text = "Audio interrompu : " + error;
                        if (!string.IsNullOrWhiteSpace(soundboard.MonitorError)) SoundboardMonitorStatus.Text = soundboard.MonitorError;
                        RefreshSoundboardClips();
                    }
                    catch (Exception ex) { SoundboardStatus.Text = "Audio indisponible : " + ex.Message; }
                });
                ApplySoundboardMonitorSettings();
            }
            return soundboard;
        }
    }
    private readonly DispatcherTimer soundboardRetry = new() { Interval = TimeSpan.FromSeconds(5) };
    private readonly DispatcherTimer monitorSaveTimer = new() { Interval = TimeSpan.FromMilliseconds(300) };
    private readonly AsyncSnapshotCache<SoundboardEndpointSnapshot> soundboardDeviceCache =
        new(() => Task.Run(SoundboardEngine.DiscoverEndpoints), TimeSpan.FromSeconds(15));
    private bool soundboardRefreshing;
    private AudioEndpoint? virtualCableInput;
    private void InitializeThemeAndSoundboard()
    {
        ThemeBackgroundInput.Text = settings.Theme.Background;
        ThemePanelInput.Text = settings.Theme.Panel;
        ThemeAccentInput.Text = settings.Theme.Accent;
        ApplyTheme();
        SoundboardAutoStartCheck.IsChecked = settings.Soundboard.AutoStart;
        SoundboardMonitorCheck.IsChecked = settings.Soundboard.MonitorEnabled;
        SoundboardMonitorVolumeSlider.Value = settings.Soundboard.MonitorVolumePercent;
        UpdateSoundboardMonitorStatus();
        soundboardRetry.Tick += async (_, _) => await TryAutoStartSoundboardAsync();
        monitorSaveTimer.Tick += (_, _) => { monitorSaveTimer.Stop(); SaveSoundboardSettings(); };
        Loaded += async (_, _) =>
        {
            if (!IsOperational) return;
            soundboardRetry.Start();
            await RefreshSoundboardDevicesAsync();
            await TryAutoStartSoundboardAsync();
        };
        Closed += (_, _) =>
        {
            soundboardRetry.Stop();
            if (monitorSaveTimer.IsEnabled) { monitorSaveTimer.Stop(); SaveSoundboardSettings(); }
        };
        SoundboardStatus.Text = "Recherche des périphériques audio…";
        RefreshSoundboardClips();
    }
    private void ApplyTheme()
    {
        SetBrush("AppBackgroundBrush", settings.Theme.Background);
        SetBrush("PanelBrush", settings.Theme.Panel);
        SetBrush("AccentBrush", settings.Theme.Accent);
        var panel = (Color)ColorConverter.ConvertFromString(settings.Theme.Panel)!;
        var card = Color.FromRgb((byte)Math.Min(255, panel.R + 7), (byte)Math.Min(255, panel.G + 7), (byte)Math.Min(255, panel.B + 7));
        SetBrush("CardBrush", card.ToString());
    }
    private static void SetBrush(string key, string color)
    {
        var resources = Application.Current.Resources;
        var brush = (SolidColorBrush)resources[key];
        var parsed = (Color)ColorConverter.ConvertFromString(color)!;
        if (brush.IsFrozen) resources[key] = new SolidColorBrush(parsed);
        else brush.Color = parsed;
    }
    private void ApplyThemeClick(object sender, RoutedEventArgs e)
    {
        var theme = new ThemeSettings { Background = ThemeBackgroundInput.Text.Trim(), Panel = ThemePanelInput.Text.Trim(), Accent = ThemeAccentInput.Text.Trim() };
        if (!theme.IsValid()) { StatusText.Text = "Couleurs invalides : entre trois codes #RRGGBB."; return; }
        settings.Theme = theme;
        ApplyTheme(); SaveSoundboardSettings();
        StatusText.Text = "Thème enregistré et appliqué.";
    }
    private async void RefreshSoundboardDevicesClick(object sender, RoutedEventArgs e)
    {
        SoundboardDeviceRefreshButton.IsEnabled = false;
        SoundboardStatus.Text = "Actualisation des périphériques audio…";
        try
        {
            await RefreshSoundboardDevicesAsync(forceRefresh: true);
            await TryAutoStartSoundboardAsync();
        }
        finally { if (!lifetime.IsCancellationRequested) SoundboardDeviceRefreshButton.IsEnabled = true; }
    }

    private async Task RefreshSoundboardDevicesAsync(bool forceRefresh = false)
    {
        try
        {
            var snapshot = await soundboardDeviceCache.GetAsync(lifetime.Token, forceRefresh);
            if (lifetime.IsCancellationRequested) return;
            virtualCableInput = snapshot.Outputs.FirstOrDefault(x => x.IsVirtualCable);
            var inputs = snapshot.Microphones.Where(x => !x.IsVirtualCable).ToArray();
            var monitorOutputs = snapshot.Outputs.Where(x => !x.IsVirtualCable).ToArray();
            soundboardRefreshing = true;
            try
            {
                SoundboardMicrophonePicker.ItemsSource = inputs;
                SoundboardMicrophonePicker.SelectedItem = inputs.FirstOrDefault(x => x.Id == settings.Soundboard.MicrophoneId);
                SoundboardMonitorOutputPicker.ItemsSource = monitorOutputs;
                SoundboardMonitorOutputPicker.SelectedItem = monitorOutputs.FirstOrDefault(x => x.Id == settings.Soundboard.MonitorOutputId);
            }
            finally { soundboardRefreshing = false; }
            UpdateSoundboardMonitorStatus();
            ApplySoundboardMonitorSettings();
            string outputId = virtualCableInput?.Id ?? "";
            if (settings.Soundboard.VirtualOutputId != outputId)
            {
                settings.Soundboard.VirtualOutputId = outputId;
                SaveSoundboardSettings();
            }
            if (!string.IsNullOrWhiteSpace(snapshot.Error))
                SoundboardStatus.Text = "Périphériques audio indisponibles : " + snapshot.Error;
            else if (virtualCableInput == null)
                SoundboardStatus.Text = "VB-CABLE n’est pas détecté. Installe le pilote depuis le site officiel, puis actualise les périphériques.";
            else if (!inputs.Any(x => x.Id == settings.Soundboard.MicrophoneId))
                SoundboardStatus.Text = "VB-CABLE est prêt. Choisis ton microphone physique.";
            else
                SoundboardStatus.Text = "VB-CABLE est prêt. Dans Discord ou le jeu, choisis CABLE Output comme microphone.";
        }
        catch (OperationCanceledException) when (lifetime.IsCancellationRequested) { }
        catch (Exception ex) { SoundboardStatus.Text = "Périphériques audio indisponibles : " + ex.Message; }
    }

    private async void SoundboardDevicesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || soundboardRefreshing) return;
        if (soundboard?.IsRunning == true) { soundboard.Stop(); SoundboardStartButton.Content = "Activer le micro virtuel"; }
        settings.Soundboard.MicrophoneId = (SoundboardMicrophonePicker.SelectedItem as AudioEndpoint)?.Id ?? "";
        SaveSoundboardSettings();
        SoundboardStatus.Text = virtualCableInput == null
            ? "VB-CABLE n’est pas détecté. Installe le pilote depuis le site officiel, puis actualise les périphériques."
            : "Microphone sélectionné. Active le micro virtuel pour envoyer la voix et les sons.";
        await TryAutoStartSoundboardAsync();
    }
    private void InstallVirtualCableClick(object sender, RoutedEventArgs e)
    {
        string script = Path.Combine(AppContext.BaseDirectory, "Install-VBCABLE.ps1");
        if (!File.Exists(script))
        {
            SoundboardStatus.Text = "Le programme d’installation intégré est absent. Le site officiel VB-Audio va s’ouvrir.";
            try { Process.Start(new ProcessStartInfo("https://vb-audio.com/Cable/") { UseShellExecute = true }); }
            catch (Exception ex) { SoundboardStatus.Text = "Impossible d’ouvrir le site VB-Audio : " + ex.Message; }
            return;
        }
        try
        {
            string powershell = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "WindowsPowerShell", "v1.0", "powershell.exe");
            var start = new ProcessStartInfo(powershell, $"-NoProfile -ExecutionPolicy Bypass -File \"{script}\"")
            {
                UseShellExecute = true,
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(start);
            SoundboardStatus.Text = "DeckPilot vérifie la signature puis installe VB-CABLE en arrière-plan. Autorise Windows si demandé, puis actualise les périphériques.";
        }
        catch (Exception ex) { SoundboardStatus.Text = "Installation de VB-CABLE annulée ou impossible : " + ex.Message; }
    }
    private bool soundboardAutoStartBusy;
    private async Task TryAutoStartSoundboardAsync()
    {
        if (soundboardAutoStartBusy || loading || !IsOperational || lifetime.IsCancellationRequested || !settings.Soundboard.AutoStart || string.IsNullOrWhiteSpace(settings.Soundboard.MicrophoneId) || soundboard?.IsRunning == true) return;
        soundboardAutoStartBusy = true;
        try
        {
            // Reuse the recent device snapshot. Windows endpoint discovery runs
            // off the UI thread and is repeated at most once per cache lifetime.
            await RefreshSoundboardDevicesAsync();
            if (lifetime.IsCancellationRequested || !settings.Soundboard.AutoStart || soundboard?.IsRunning == true
                || virtualCableInput == null || SoundboardMicrophonePicker.SelectedItem is not AudioEndpoint microphone) return;
            Audio.Start(microphone.Id, virtualCableInput.Id);
            ApplySoundboardMonitorSettings();
            SoundboardStartButton.Content = "Arrêter le micro virtuel";
            SoundboardStatus.Text = "Micro virtuel actif automatiquement · Voix et sons envoyés vers VB-CABLE.";
        }
        catch (Exception ex) { SoundboardStatus.Text = "Micro virtuel en attente · Nouvel essai automatique : " + ex.Message; }
        finally { soundboardAutoStartBusy = false; }
    }
    private async void SoundboardAutoStartChanged(object sender, RoutedEventArgs e)
    {
        if (loading) return;
        settings.Soundboard.AutoStart = SoundboardAutoStartCheck.IsChecked == true;
        SaveSoundboardSettings();
        if (settings.Soundboard.AutoStart) await TryAutoStartSoundboardAsync();
        else
        {
            soundboard?.Stop(); SoundboardStartButton.Content = "Activer le micro virtuel";
            SoundboardStatus.Text = "Micro virtuel arrêté · Démarrage automatique désactivé.";
            RefreshSoundboardClips();
        }
    }
    private async void ToggleSoundboardClick(object sender, RoutedEventArgs e)
    {
        bool enable = soundboard?.IsRunning != true;
        if (SoundboardAutoStartCheck.IsChecked != enable) SoundboardAutoStartCheck.IsChecked = enable;
        else await TryAutoStartSoundboardAsync();
    }
    private void SoundboardMonitorChanged(object sender, RoutedEventArgs e)
    {
        if (loading) return;
        settings.Soundboard.MonitorEnabled = SoundboardMonitorCheck.IsChecked == true;
        monitorSaveTimer.Stop(); SaveSoundboardSettings();
        UpdateSoundboardMonitorStatus();
        ApplySoundboardMonitorSettings();
    }
    private void SoundboardMonitorDeviceChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || soundboardRefreshing) return;
        settings.Soundboard.MonitorOutputId = (SoundboardMonitorOutputPicker.SelectedItem as AudioEndpoint)?.Id ?? "";
        monitorSaveTimer.Stop(); SaveSoundboardSettings();
        UpdateSoundboardMonitorStatus();
        ApplySoundboardMonitorSettings();
    }
    private void SoundboardMonitorVolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (loading) return;
        int value = (int)Math.Round(e.NewValue);
        if (settings.Soundboard.MonitorVolumePercent == value) return;
        settings.Soundboard.MonitorVolumePercent = value;
        monitorSaveTimer.Stop(); monitorSaveTimer.Start();
        ApplySoundboardMonitorSettings();
    }
    private void UpdateSoundboardMonitorStatus()
    {
        SoundboardMonitorStatus.Text = !settings.Soundboard.MonitorEnabled
            ? "Retour coupé · Les sons restent envoyés au micro virtuel."
            : string.IsNullOrWhiteSpace(settings.Soundboard.MonitorOutputId)
                ? "Choisis le casque ou les haut-parleurs pour entendre les sons."
                : SoundboardMonitorOutputPicker.SelectedItem is not AudioEndpoint
                    ? "La sortie choisie est débranchée. Sélectionne un autre périphérique ou actualise."
                    : "Seuls les sons du soundboard sont audibles ici ; ta voix reste dans le micro virtuel.";
    }
    private void ApplySoundboardMonitorSettings()
    {
        if (soundboard == null) return;
        string outputId = (SoundboardMonitorOutputPicker.SelectedItem as AudioEndpoint)?.Id ?? "";
        bool enabled = settings.Soundboard.MonitorEnabled && outputId.Length > 0;
        try
        {
            soundboard.ConfigureMonitor(enabled, outputId, settings.Soundboard.MonitorVolumePercent);
            UpdateSoundboardMonitorStatus();
            if (!string.IsNullOrWhiteSpace(soundboard.MonitorError)) SoundboardMonitorStatus.Text = soundboard.MonitorError;
        }
        catch (Exception ex) { SoundboardMonitorStatus.Text = "Retour des sons indisponible : " + ex.Message; }
    }
    private void AddSoundClipClick(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog { Filter = "Audio MP3 ou WAV|*.mp3;*.wav", Title = "Ajouter un son au soundboard" };
        if (dialog.ShowDialog(this) != true) return;
        if (settings.Soundboard.Clips.Count >= 48) { SoundboardStatus.Text = "Limite de 48 sons atteinte."; return; }
        string name = Path.GetFileNameWithoutExtension(dialog.FileName);
        settings.Soundboard.Clips.Add(new SoundboardClip { Name = name[..Math.Min(64, name.Length)], FilePath = dialog.FileName });
        SaveSoundboardSettings(); RefreshSoundboardClips();
    }
    private void RefreshSoundboardClips()
    {
        Guid selectedClipId = (SoundboardClipPicker.SelectedItem as SoundboardClip)?.Id ?? Guid.Empty;
        SoundboardClipList.Children.Clear();
        SoundboardClipPicker.ItemsSource = null; SoundboardClipPicker.ItemsSource = settings.Soundboard.Clips;
        SoundboardClipPicker.SelectedItem = settings.Soundboard.Clips.FirstOrDefault(c => c.Id == selectedClipId);
        foreach (var clip in settings.Soundboard.Clips)
        {
            var row = new DockPanel { Margin = new Thickness(0, 3, 0, 3) };
            var remove = new Button { Content = "×", ToolTip = "Retirer ce son", MinWidth = 28, Padding = new Thickness(7, 3, 7, 3) };
            remove.Click += (_, _) =>
            {
                if (settings.Profiles.SelectMany(p => p.Buttons).SelectMany(a => a.LongPressAction == null ? new[] { a } : new[] { a, a.LongPressAction })
                    .Any(a => a.Kind == ActionKind.Soundboard && Guid.TryParse(a.Target, out var id) && id == clip.Id))
                { SoundboardStatus.Text = "Ce son est affecté à un bouton. Retire d’abord ses actions avant de supprimer le son."; return; }
                soundboard?.StopClip(clip.Id); settings.Soundboard.Clips.Remove(clip); SaveSoundboardSettings(); RefreshSoundboardClips();
            };
            DockPanel.SetDock(remove, Dock.Right); row.Children.Add(remove);
            var relink = new Button { Content = "…", ToolTip = "Remplacer ou retrouver le fichier audio", MinWidth = 28, Padding = new Thickness(7, 3, 7, 3) };
            relink.Click += (_, _) =>
            {
                var dialog = new OpenFileDialog { Filter = "Audio MP3 ou WAV|*.mp3;*.wav", Title = "Retrouver le son · " + clip.Name };
                if (dialog.ShowDialog(this) != true) return;
                soundboard?.StopClip(clip.Id); clip.FilePath = dialog.FileName; SaveSoundboardSettings(); RefreshSoundboardClips();
            };
            DockPanel.SetDock(relink, Dock.Right); row.Children.Add(relink);
            var play = new Button { Content = Audio.IsPlaying(clip.Id) ? "■ " + clip.Name : clip.Name, ToolTip = clip.FilePath, HorizontalContentAlignment = HorizontalAlignment.Left };
            play.Click += (_, _) =>
            {
                try { play.Content = Audio.Toggle(clip.Id, clip.FilePath) ? "■ " + clip.Name : clip.Name; }
                catch (Exception ex) { SoundboardStatus.Text = "Son indisponible : " + ex.Message; }
            };
            row.Children.Add(play); SoundboardClipList.Children.Add(row);
        }
    }
    private void SaveSoundboardSettings()
    {
        try { store.Save(settings); }
        catch (Exception ex) { SoundboardStatus.Text = "Impossible d’enregistrer les préférences : " + ex.Message; }
    }
}
