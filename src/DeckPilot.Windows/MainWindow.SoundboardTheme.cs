using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Threading;
using DeckPilot.Core;
using Microsoft.Win32;
using Forms = System.Windows.Forms;

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
                        UpdateNoiseSuppressionStatus();
                        UpdateSoundboardPlaybackButtons();
                    }
                    catch (Exception ex) { SoundboardStatus.Text = "Audio indisponible : " + ex.Message; }
                });
                soundboard.ConfigureNoiseSuppression(settings.Soundboard.NoiseSuppressionEnabled, settings.Soundboard.NoiseSuppressionStrengthPercent);
                ApplySoundboardMonitorSettings();
            }
            return soundboard;
        }
    }
    private readonly DispatcherTimer soundboardRetry = new() { Interval = TimeSpan.FromSeconds(5) };
    private readonly DispatcherTimer monitorSaveTimer = new() { Interval = TimeSpan.FromMilliseconds(300) };
    private readonly DispatcherTimer noiseSuppressionSaveTimer = new() { Interval = TimeSpan.FromMilliseconds(300) };
    private readonly AsyncSnapshotCache<SoundboardEndpointSnapshot> soundboardDeviceCache =
        new(() => Task.Run(SoundboardEngine.DiscoverEndpoints), TimeSpan.FromSeconds(15));
    private bool soundboardRefreshing;
    private bool copyingSoundClip;
    private readonly Dictionary<Guid, Button> soundPlaybackButtons = [];
    private readonly HashSet<string> retiredSoundPaths = new(StringComparer.OrdinalIgnoreCase);
    private AudioEndpoint? virtualCableInput;
    private void InitializeThemeAndSoundboard()
    {
        ThemeBackgroundInput.Text = settings.Theme.Background;
        ThemePanelInput.Text = settings.Theme.Panel;
        ThemeAccentInput.Text = settings.Theme.Accent;
        RefreshThemeColorSwatches();
        ApplyTheme();
        SoundboardAutoStartCheck.IsChecked = settings.Soundboard.AutoStart;
        SoundboardNoiseSuppressionCheck.IsChecked = settings.Soundboard.NoiseSuppressionEnabled;
        SoundboardNoiseSuppressionSlider.Value = settings.Soundboard.NoiseSuppressionStrengthPercent;
        SoundboardMonitorCheck.IsChecked = settings.Soundboard.MonitorEnabled;
        SoundboardMonitorVolumeSlider.Value = settings.Soundboard.MonitorVolumePercent;
        UpdateNoiseSuppressionStatus();
        UpdateSoundboardMonitorStatus();
        soundboardRetry.Tick += async (_, _) => await TryAutoStartSoundboardAsync();
        monitorSaveTimer.Tick += (_, _) => { monitorSaveTimer.Stop(); SaveSoundboardSettings(); };
        noiseSuppressionSaveTimer.Tick += (_, _) => { noiseSuppressionSaveTimer.Stop(); SaveSoundboardSettings(); };
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
            if (noiseSuppressionSaveTimer.IsEnabled) { noiseSuppressionSaveTimer.Stop(); SaveSoundboardSettings(); }
            PruneRetiredSounds(keepHistory: false);
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
        var theme = new ThemeSettings
        {
            Background = ThemeBackgroundInput.Text.Trim(),
            Panel = ThemePanelInput.Text.Trim(),
            Accent = ThemeAccentInput.Text.Trim()
        };
        if (!theme.IsValid())
        {
            StatusText.Text = "Vérifiez les codes couleur : utilisez le format #RRGGBB, ou choisissez une couleur dans la palette.";
            return;
        }
        settings.Theme = theme;
        ApplyTheme(); SaveSoundboardSettings();
        StatusText.Text = "Couleurs enregistrées et appliquées.";
    }
    private void ThemeColorCodeChanged(object sender, TextChangedEventArgs e)
    {
        if (loading) return;
        RefreshThemeColorSwatches();
    }
    private void RefreshThemeColorSwatches()
    {
        UpdateThemeColorSwatch(ThemeBackgroundInput, ThemeBackgroundSwatch);
        UpdateThemeColorSwatch(ThemePanelInput, ThemePanelSwatch);
        UpdateThemeColorSwatch(ThemeAccentInput, ThemeAccentSwatch);
    }
    private static void UpdateThemeColorSwatch(TextBox input, Border swatch)
    {
        string value = input.Text.Trim();
        bool valid = value.Length == 7 && value[0] == '#' && value.Skip(1).All(Uri.IsHexDigit);
        swatch.Background = valid ? Brush(value) : Brush("#1A1C20");
        swatch.BorderBrush = valid ? Brush("#AEB5C0") : Brush("#D98383");
        System.Windows.Automation.AutomationProperties.SetItemStatus(input, valid ? "Code couleur valide" : "Saisissez un code comme #9B8CFF");
    }
    private void ChooseThemeBackgroundClick(object sender, RoutedEventArgs e) => ChooseThemeColor(ThemeBackgroundInput);
    private void ChooseThemePanelClick(object sender, RoutedEventArgs e) => ChooseThemeColor(ThemePanelInput);
    private void ChooseThemeAccentClick(object sender, RoutedEventArgs e) => ChooseThemeColor(ThemeAccentInput);
    private static bool TryReadThemeColor(string value, out Color color)
    {
        color = Colors.Transparent;
        if (value.Length != 7 || value[0] != '#' || !value.Skip(1).All(Uri.IsHexDigit)) return false;
        color = (Color)ColorConverter.ConvertFromString(value)!;
        return true;
    }
    private sealed class ThemeDialogOwner(IntPtr handle) : Forms.IWin32Window
    {
        public IntPtr Handle => handle;
    }
    private void ChooseThemeColor(TextBox input)
    {
        string defaultValue = ReferenceEquals(input, ThemeBackgroundInput) ? "#08090B"
            : ReferenceEquals(input, ThemePanelInput) ? "#14161A" : "#9B8CFF";
        Color current = TryReadThemeColor(input.Text.Trim(), out var parsed)
            ? parsed
            : (Color)ColorConverter.ConvertFromString(defaultValue)!;
        using var dialog = new Forms.ColorDialog
        {
            Color = System.Drawing.Color.FromArgb(current.R, current.G, current.B),
            AnyColor = true,
            FullOpen = true
        };
        if (dialog.ShowDialog(new ThemeDialogOwner(new WindowInteropHelper(this).Handle)) != Forms.DialogResult.OK) return;
        input.Text = $"#{dialog.Color.R:X2}{dialog.Color.G:X2}{dialog.Color.B:X2}";
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
            UpdateNoiseSuppressionStatus();
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
            UpdateSoundboardPlaybackButtons();
        }
    }
    private async void ToggleSoundboardClick(object sender, RoutedEventArgs e)
    {
        bool enable = soundboard?.IsRunning != true;
        if (SoundboardAutoStartCheck.IsChecked != enable) SoundboardAutoStartCheck.IsChecked = enable;
        else await TryAutoStartSoundboardAsync();
    }

    private void SoundboardNoiseSuppressionChanged(object sender, RoutedEventArgs e)
    {
        if (loading) return;
        bool previous = settings.Soundboard.NoiseSuppressionEnabled;
        bool enabled = SoundboardNoiseSuppressionCheck.IsChecked == true;
        try
        {
            Audio.ConfigureNoiseSuppression(enabled, settings.Soundboard.NoiseSuppressionStrengthPercent);
            settings.Soundboard.NoiseSuppressionEnabled = enabled;
            SaveSoundboardSettings();
            UpdateNoiseSuppressionStatus();
        }
        catch (Exception ex)
        {
            settings.Soundboard.NoiseSuppressionEnabled = previous;
            bool wasLoading = loading; loading = true;
            SoundboardNoiseSuppressionCheck.IsChecked = previous;
            loading = wasLoading;
            SoundboardNoiseSuppressionStatus.Text = "Réduction de bruit indisponible : " + ex.Message;
        }
    }

    private void SoundboardNoiseSuppressionStrengthChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (loading) return;
        int value = (int)Math.Round(e.NewValue);
        if (settings.Soundboard.NoiseSuppressionStrengthPercent == value) return;
        int previous = settings.Soundboard.NoiseSuppressionStrengthPercent;
        settings.Soundboard.NoiseSuppressionStrengthPercent = value;
        try
        {
            soundboard?.ConfigureNoiseSuppression(settings.Soundboard.NoiseSuppressionEnabled, value);
            noiseSuppressionSaveTimer.Stop(); noiseSuppressionSaveTimer.Start();
            UpdateNoiseSuppressionStatus();
        }
        catch (Exception ex)
        {
            settings.Soundboard.NoiseSuppressionStrengthPercent = previous;
            bool wasLoading = loading; loading = true;
            SoundboardNoiseSuppressionSlider.Value = previous;
            loading = wasLoading;
            SoundboardNoiseSuppressionStatus.Text = "Réduction de bruit indisponible : " + ex.Message;
        }
    }

    private void UpdateNoiseSuppressionStatus()
    {
        string? error = soundboard?.NoiseSuppressionError;
        if (!string.IsNullOrWhiteSpace(error))
            SoundboardNoiseSuppressionStatus.Text = error;
        else if (settings.Soundboard.NoiseSuppressionEnabled)
            SoundboardNoiseSuppressionStatus.Text = "RNNoise filtre la voix envoyée à CABLE Output. Sélectionne cette entrée dans tes applications ; les sons du soundboard restent intacts.";
        else
            SoundboardNoiseSuppressionStatus.Text = "Réduction désactivée · Le micro est transmis sans filtre.";
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
    private async void AddSoundClipClick(object sender, RoutedEventArgs e)
    {
        if (copyingSoundClip) return;
        if (settings.Soundboard.Clips.Count >= SoundboardSettings.MaximumClips)
        {
            SoundboardStatus.Text = "Limite de 48 sons atteinte.";
            return;
        }
        var dialog = new OpenFileDialog { Filter = "Audio MP3 ou WAV|*.mp3;*.wav", Title = "Ajouter un son au soundboard" };
        if (dialog.ShowDialog(this) != true) return;
        await CopySoundClipAsync(dialog.FileName);
    }

    private async Task CopySoundClipAsync(string sourcePath, Guid? replaceId = null)
    {
        if (copyingSoundClip) return;
        copyingSoundClip = true;
        SoundboardClip? copied = null;
        bool saved = false;
        try
        {
            SoundboardStatus.Text = "Copie du son dans DeckPilot…";
            var cancellation = lifetime.Token;
            copied = await Task.Run(() => SoundboardLibrary.CopyAsync(store.SoundsPath, sourcePath, cancellation), cancellation);
            if (lifetime.IsCancellationRequested) return;
            var clips = settings.Soundboard.Clips.ToList();
            string? previousPath = null;
            if (replaceId is Guid id)
            {
                int index = clips.FindIndex(clip => clip.Id == id);
                if (index < 0) throw new IOException("Le son à remplacer n’est plus dans la liste.");
                previousPath = clips[index].FilePath;
                clips[index] = clips[index] with { FilePath = copied.FilePath };
            }
            else
            {
                if (clips.Count >= SoundboardSettings.MaximumClips) throw new InvalidDataException("Limite de 48 sons atteinte.");
                clips.Add(copied);
            }
            SoundboardLibrary.SaveClips(settings, clips, store.Save);
            saved = true;
            if (replaceId is Guid clipId) soundboard?.StopClip(clipId);
            if (previousPath != null) retiredSoundPaths.Add(previousPath);
            profileHistory?.Record(settings);
            PruneRetiredSounds();
            RefreshSoundboardClips();
            SoundboardStatus.Text = "Son enregistré dans DeckPilot.";
        }
        catch (OperationCanceledException) when (lifetime.IsCancellationRequested) { }
        catch (Exception ex) { SoundboardStatus.Text = "Impossible d’enregistrer le son : " + ex.Message; }
        finally
        {
            if (!saved && copied != null) SoundboardLibrary.TryDeleteManagedFile(store.SoundsPath, copied.FilePath);
            copyingSoundClip = false;
        }
    }

    private void PruneRetiredSounds(bool keepHistory = true)
    {
        if (retiredSoundPaths.Count == 0) return;
        var retained = settings.Soundboard.Clips.Select(clip => clip.FilePath).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (keepHistory && profileHistory != null) retained.UnionWith(profileHistory.RetainedSoundPaths);
        foreach (string path in retiredSoundPaths.Where(path => !retained.Contains(path)).ToArray())
        {
            SoundboardLibrary.TryDeleteManagedFile(store.SoundsPath, path);
            retiredSoundPaths.Remove(path);
        }
    }

    private void UpdateSoundboardPlaybackButtons()
    {
        foreach (var clip in settings.Soundboard.Clips)
            if (soundPlaybackButtons.TryGetValue(clip.Id, out var button))
            {
                string label = soundboard?.IsPlaying(clip.Id) == true ? "■ " + clip.Name : clip.Name;
                if (!Equals(button.Content, label)) button.Content = label;
            }
    }

    private void RefreshSoundboardClips()
    {
        Guid selectedClipId = (SoundboardClipPicker.SelectedItem as SoundboardClip)?.Id ?? Guid.Empty;
        SoundboardClipList.Children.Clear();
        soundPlaybackButtons.Clear();
        SoundboardClipPicker.ItemsSource = null; SoundboardClipPicker.ItemsSource = settings.Soundboard.Clips;
        SoundboardClipPicker.SelectedItem = settings.Soundboard.Clips.FirstOrDefault(c => c.Id == selectedClipId);
        foreach (var clip in settings.Soundboard.Clips)
        {
            var row = new DockPanel { Margin = new Thickness(0, 3, 0, 3) };
            var remove = new Button { Content = "×", ToolTip = "Retirer ce son", MinWidth = 28, Padding = new Thickness(7, 3, 7, 3) };
            remove.Click += (_, _) =>
            {
                if (copyingSoundClip) return;
                if (settings.Profiles.SelectMany(p => p.Buttons).SelectMany(a => a.LongPressAction == null ? new[] { a } : new[] { a, a.LongPressAction })
                    .Any(a => a.Kind == ActionKind.Soundboard && Guid.TryParse(a.Target, out var id) && id == clip.Id))
                { SoundboardStatus.Text = "Ce son est affecté à un bouton. Retire d’abord ses actions avant de supprimer le son."; return; }
                try
                {
                    var remaining = settings.Soundboard.Clips.Where(item => item.Id != clip.Id).ToList();
                    SoundboardLibrary.SaveClips(settings, remaining, store.Save);
                    soundboard?.StopClip(clip.Id);
                    retiredSoundPaths.Add(clip.FilePath);
                    profileHistory?.Record(settings);
                    PruneRetiredSounds();
                    RefreshSoundboardClips();
                }
                catch (Exception ex) { SoundboardStatus.Text = "Impossible de retirer le son : " + ex.Message; }
            };
            DockPanel.SetDock(remove, Dock.Right); row.Children.Add(remove);
            var relink = new Button { Content = "…", ToolTip = "Remplacer ou retrouver le fichier audio", MinWidth = 28, Padding = new Thickness(7, 3, 7, 3) };
            relink.Click += async (_, _) =>
            {
                if (copyingSoundClip) return;
                var dialog = new OpenFileDialog { Filter = "Audio MP3 ou WAV|*.mp3;*.wav", Title = "Retrouver le son · " + clip.Name };
                if (dialog.ShowDialog(this) != true) return;
                await CopySoundClipAsync(dialog.FileName, clip.Id);
            };
            DockPanel.SetDock(relink, Dock.Right); row.Children.Add(relink);
            var play = new Button { Content = clip.Name, ToolTip = clip.FilePath, HorizontalContentAlignment = HorizontalAlignment.Left };
            play.Click += (_, _) =>
            {
                try
                {
                    bool isPlaying = Audio.Toggle(clip.Id, clip.FilePath);
                    play.Content = isPlaying ? "■ " + clip.Name : clip.Name;
                    if (isPlaying) ShowSoundboardOverlay(clip.Name);
                }
                catch (Exception ex) { SoundboardStatus.Text = "Son indisponible : " + ex.Message; }
            };
            soundPlaybackButtons.Add(clip.Id, play);
            row.Children.Add(play); SoundboardClipList.Children.Add(row);
        }
        UpdateSoundboardPlaybackButtons();
    }
    private void SaveSoundboardSettings()
    {
        try { store.Save(settings); }
        catch (Exception ex) { SoundboardStatus.Text = "Impossible d’enregistrer les préférences : " + ex.Message; }
    }
}
