using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DeckPilot.Windows;

internal sealed class MicrophoneSetupWindow : Window
{
    private readonly ComboBox microphonePicker;
    public AudioEndpoint? SelectedMicrophone => microphonePicker.SelectedItem as AudioEndpoint;

    public MicrophoneSetupWindow(IReadOnlyList<AudioEndpoint> microphones, string selectedId)
    {
        Title = "Configurer DeckPilot";
        Width = 480;
        SizeToContent = SizeToContent.Height;
        ResizeMode = ResizeMode.NoResize;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Background = new SolidColorBrush(Color.FromRgb(12, 14, 17));
        Foreground = new SolidColorBrush(Color.FromRgb(231, 237, 241));

        var layout = new StackPanel { Margin = new Thickness(22) };
        layout.Children.Add(new TextBlock
        {
            Text = "Choisis le microphone physique que DeckPilot utilisera pour le soundboard. Tu pourras le changer dans les préférences de l’application.",
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 12),
            Foreground = Foreground
        });

        microphonePicker = new ComboBox { ItemsSource = microphones, DisplayMemberPath = nameof(AudioEndpoint.Name) };
        microphonePicker.SelectedItem = microphones.FirstOrDefault(m => m.Id == selectedId);
        layout.Children.Add(microphonePicker);
        if (microphones.Count == 0)
        {
            microphonePicker.IsEnabled = false;
            layout.Children.Add(new TextBlock
            {
                Text = "Aucun microphone physique n’a été détecté. Branche-le et choisis-le plus tard dans les préférences.",
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Color.FromRgb(170, 184, 194)),
                Margin = new Thickness(0, 0, 0, 12)
            });
        }

        var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        var save = new Button { Content = "Enregistrer", IsDefault = true, IsEnabled = SelectedMicrophone != null };
        microphonePicker.SelectionChanged += (_, _) => save.IsEnabled = SelectedMicrophone != null;
        save.Click += (_, _) => DialogResult = true;
        var later = new Button { Content = "Choisir plus tard", IsCancel = true };
        buttons.Children.Add(save);
        buttons.Children.Add(later);
        layout.Children.Add(buttons);
        Content = layout;
    }
}
