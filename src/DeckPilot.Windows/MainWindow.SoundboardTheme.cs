using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DeckPilot.Core;
using Microsoft.Win32;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private SoundboardEngine? soundboard;
    private SoundboardEngine Audio => soundboard ??= new SoundboardEngine();
    private bool soundboardRefreshing;
    private AudioEndpoint? virtualCableInput;
    private void InitializeThemeAndSoundboard()
    {
        ThemeBackgroundInput.Text = settings.Theme.Background;
        ThemePanelInput.Text = settings.Theme.Panel;
        ThemeAccentInput.Text = settings.Theme.Accent;
        ApplyTheme();
        RefreshSoundboardDevices();
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
    private void RefreshSoundboardDevicesClick(object sender, RoutedEventArgs e) => RefreshSoundboardDevices();
    private void RefreshSoundboardDevices()
    {
        virtualCableInput = null;
        try
        {
            var inputs = Audio.Microphones().Where(x => !VirtualCableEndpoints.IsRecordingEndpoint(x.Name)).ToArray();
            virtualCableInput = Audio.Outputs().FirstOrDefault(x => VirtualCableEndpoints.IsPlaybackEndpoint(x.Name));
            soundboardRefreshing = true;
            try
            {
                SoundboardMicrophonePicker.ItemsSource = inputs;
                SoundboardMicrophonePicker.SelectedItem = inputs.FirstOrDefault(x => x.Id == settings.Soundboard.MicrophoneId);
            }
            finally { soundboardRefreshing = false; }
            string outputId = virtualCableInput?.Id ?? "";
            if (settings.Soundboard.VirtualOutputId != outputId)
            {
                settings.Soundboard.VirtualOutputId = outputId;
                SaveSoundboardSettings();
            }
            if (virtualCableInput == null)
                SoundboardStatus.Text = "VB-CABLE n’est pas détecté. Installe le pilote depuis le site officiel, puis actualise les périphériques.";
            else if (string.IsNullOrWhiteSpace(settings.Soundboard.MicrophoneId))
                SoundboardStatus.Text = "VB-CABLE est prêt. Choisis ton microphone physique.";
            else
                SoundboardStatus.Text = "VB-CABLE est prêt. Dans Discord ou le jeu, choisis CABLE Output comme microphone.";
        }
        catch (Exception ex) { SoundboardStatus.Text = "Périphériques audio indisponibles : " + ex.Message; }
    }
    private void SoundboardDevicesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || soundboardRefreshing) return;
        if (soundboard?.IsRunning == true) { soundboard.Stop(); SoundboardStartButton.Content = "Activer le micro virtuel"; }
        settings.Soundboard.MicrophoneId = (SoundboardMicrophonePicker.SelectedItem as AudioEndpoint)?.Id ?? "";
        SaveSoundboardSettings();
        SoundboardStatus.Text = virtualCableInput == null
            ? "VB-CABLE n’est pas détecté. Installe le pilote depuis le site officiel, puis actualise les périphériques."
            : "Microphone sélectionné. Active le micro virtuel pour envoyer la voix et les sons.";
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
            SoundboardStatus.Text = "L’installation sécurisée de VB-CABLE a démarré. Autorise Windows, suis l’installateur, puis actualise les périphériques.";
        }
        catch (Exception ex) { SoundboardStatus.Text = "Installation de VB-CABLE annulée ou impossible : " + ex.Message; }
    }
    private void ToggleSoundboardClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (soundboard?.IsRunning == true)
            {
                soundboard.Stop(); SoundboardStartButton.Content = "Activer le micro virtuel";
                SoundboardStatus.Text = "Micro virtuel arrêté.";
            }
            else
            {
                if (virtualCableInput == null) { RefreshSoundboardDevices(); if (virtualCableInput == null) return; }
                Audio.Start(settings.Soundboard.MicrophoneId, virtualCableInput.Id);
                SoundboardStartButton.Content = "Arrêter le micro virtuel";
                SoundboardStatus.Text = "Voix et sons envoyés vers VB-CABLE. Dans Discord ou le jeu, choisis CABLE Output comme microphone.";
            }
            RefreshSoundboardClips();
        }
        catch (Exception ex) { SoundboardStatus.Text = "Impossible de démarrer le mixage : " + ex.Message; }
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
            remove.Click += (_, _) => { soundboard?.StopClip(clip.Id); settings.Soundboard.Clips.Remove(clip); SaveSoundboardSettings(); RefreshSoundboardClips(); };
            DockPanel.SetDock(remove, Dock.Right); row.Children.Add(remove);
            var play = new Button { Content = clip.Name, ToolTip = clip.FilePath, HorizontalContentAlignment = HorizontalAlignment.Left };
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
