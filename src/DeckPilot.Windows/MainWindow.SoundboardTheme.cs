using System;
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
        try
        {
            var inputs = Audio.Microphones(); var outputs = Audio.Outputs();
            soundboardRefreshing = true;
            try
            {
                SoundboardMicrophonePicker.ItemsSource = inputs;
                SoundboardOutputPicker.ItemsSource = outputs;
                SoundboardMicrophonePicker.SelectedItem = inputs.FirstOrDefault(x => x.Id == settings.Soundboard.MicrophoneId);
                SoundboardOutputPicker.SelectedItem = outputs.FirstOrDefault(x => x.Id == settings.Soundboard.VirtualOutputId);
            }
            finally { soundboardRefreshing = false; }
            if (outputs.All(x => !x.Name.Contains("CABLE", StringComparison.OrdinalIgnoreCase) && !x.Name.Contains("virtual", StringComparison.OrdinalIgnoreCase) && !x.Name.Contains("Voicemeeter", StringComparison.OrdinalIgnoreCase)))
                SoundboardStatus.Text = "Aucun câble audio virtuel détecté. Installe VB-CABLE ou un équivalent, puis actualise.";
        }
        catch (Exception ex) { SoundboardStatus.Text = "Périphériques audio indisponibles : " + ex.Message; }
    }
    private void SoundboardDevicesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || soundboardRefreshing) return;
        if (soundboard?.IsRunning == true) { soundboard.Stop(); SoundboardStartButton.Content = "Activer le micro virtuel"; }
        settings.Soundboard.MicrophoneId = (SoundboardMicrophonePicker.SelectedItem as AudioEndpoint)?.Id ?? "";
        settings.Soundboard.VirtualOutputId = (SoundboardOutputPicker.SelectedItem as AudioEndpoint)?.Id ?? "";
        SaveSoundboardSettings();
        SoundboardStatus.Text = "Périphériques sélectionnés. Active le micro virtuel pour envoyer la voix et les sons.";
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
                var selected = SoundboardOutputPicker.SelectedItem as AudioEndpoint;
                if (selected != null && !selected.Name.Contains("CABLE", StringComparison.OrdinalIgnoreCase) && !selected.Name.Contains("virtual", StringComparison.OrdinalIgnoreCase) && !selected.Name.Contains("Voicemeeter", StringComparison.OrdinalIgnoreCase))
                {
                    if (MessageBox.Show(this, "La sortie choisie ne semble pas être un câble audio virtuel. La voix pourrait sortir par les haut-parleurs et produire un larsen. Continuer ?", "Vérifier la sortie audio", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) != MessageBoxResult.Yes) return;
                }
                Audio.Start(settings.Soundboard.MicrophoneId, settings.Soundboard.VirtualOutputId);
                SoundboardStartButton.Content = "Arrêter le micro virtuel";
                SoundboardStatus.Text = "Voix et sons envoyés vers la sortie virtuelle choisie. Dans le jeu, choisis la capture correspondante comme micro.";
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
        settings.Soundboard.Clips.Add(new SoundboardClip { Name = Path.GetFileNameWithoutExtension(dialog.FileName), FilePath = dialog.FileName });
        SaveSoundboardSettings(); RefreshSoundboardClips();
    }
    private void RefreshSoundboardClips()
    {
        SoundboardClipList.Children.Clear();
        SoundboardClipPicker.ItemsSource = null; SoundboardClipPicker.ItemsSource = settings.Soundboard.Clips;
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
