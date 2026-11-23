using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using DeckPilot.Core;

namespace DeckPilot.Windows;

internal partial class MacropadCard : UserControl
{
    private readonly Button[] keys = new Button[12];
    private readonly TextBlock[] labels = new TextBlock[12];
    private static readonly Brush HeldBrush = Brushes.DarkSlateBlue;

    public BoardLinkState Board { get; private set; }
    public event Action<BoardLinkState, int>? ConfigureRequested;
    public event Action<BoardLinkState>? CalibrateRequested, OptionsRequested, FirmwareRequested;

    public MacropadCard(BoardLinkState board)
    {
        Board = board;
        InitializeComponent();
        for (int i = 0; i < keys.Length; i++)
        {
            int index = i;
            var number = new TextBlock { Text = $"{i + 1:00}", FontWeight = FontWeights.SemiBold };
            var label = labels[i] = new TextBlock { FontSize = 10, TextTrimming = TextTrimming.CharacterEllipsis };
            var content = new StackPanel();
            content.Children.Add(number);
            content.Children.Add(label);
            var key = keys[i] = new Button
            {
                MinHeight = 42,
                Margin = new Thickness(0, 0, 3, 3),
                Padding = new Thickness(3, 1, 3, 1),
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Content = content
            };
            key.Click += (_, _) => ConfigureRequested?.Invoke(Board, index);
            CardKeys.Children.Add(key);
        }
    }

    public void Refresh(BoardLinkState board, BoardBinding? binding, Profile? profile, bool shared, bool firmwareOpen)
    {
        Board = board;
        string title = $"Macropad {board.Device.Port}";
        CardTitle.Text = title;
        AutomationProperties.SetName(this, title);
        AutomationProperties.SetAutomationId(this, $"Macropad{board.Device.Port}");
        string firmware = board.Info is { } info ? $" · Firmware {info.Firmware.Version}" : "";
        bool supported = board.Info?.SupportsSerialInput == true;
        bool needsFirmware = board.Info != null && !supported || board.Kind == BoardConnectionKind.Unidentified;
        CardStatus.Text = (board.SerialInput && binding?.Enabled == true ? "Mode DeckPilot actif"
            : needsFirmware ? "Firmware 1.2.0 requis"
            : supported && binding?.Enabled != true ? "Prêt à configurer"
            : supported && board.Message.StartsWith("Mode clavier", StringComparison.Ordinal) ? "Activation du mode DeckPilot…"
            : board.Message) + firmware;
        CardProfile.Text = profile == null ? "Aucun profil attribué" : profile.Name + (shared ? " · Profil partagé" : "");
        bool editable = !firmwareOpen && (supported || binding?.Enabled == true && profile != null);
        ConfigureButton.Content = shared ? "Séparer et configurer" : profile == null || binding?.Enabled != true
            ? "Activer et configurer" : "Configurer les touches";
        ConfigureButton.IsEnabled = editable;
        CalibrateCardButton.IsEnabled = board.SerialInput && !firmwareOpen;
        BoardOptionsButton.IsEnabled = !firmwareOpen;
        BoardFirmwareButton.Content = needsFirmware ? "Installer le firmware…" : "Firmware…";
        BoardFirmwareButton.IsEnabled = !firmwareOpen;
        AutomationProperties.SetName(ConfigureButton, $"{ConfigureButton.Content} · {title}");
        AutomationProperties.SetName(CalibrateCardButton, $"Calibrer {title}");
        AutomationProperties.SetName(BoardOptionsButton, $"Réglages de {title}");
        AutomationProperties.SetName(BoardFirmwareButton, $"Firmware de {title}");

        var map = binding?.KeyMap;
        int mask = board.Telemetry?.Mask ?? 0;
        for (int i = 0; i < keys.Length; i++)
        {
            var action = profile?.Buttons[i];
            string detail = action == null || action.Kind == ActionKind.Passthrough ? "Libre"
                : string.IsNullOrWhiteSpace(action.Label) ? ActionSearch.KindName(action.Kind) : action.Label;
            labels[i].Text = detail;
            string name = $"{title} · Touche {i + 1:00} · {detail}";
            keys[i].IsEnabled = editable;
            keys[i].ToolTip = name;
            AutomationProperties.SetName(keys[i], name);
            AutomationProperties.SetAutomationId(keys[i], $"Macropad{board.Device.Port}Key{i + 1:00}");
            int bit = map?.Count == 12 ? map[i] - Calibration.FirstKey : i;
            if ((mask & (1 << bit)) != 0) keys[i].Background = HeldBrush;
            else keys[i].ClearValue(BackgroundProperty);
        }
    }

    private void ConfigureClick(object sender, RoutedEventArgs e) => ConfigureRequested?.Invoke(Board, 0);
    private void CalibrateClick(object sender, RoutedEventArgs e) => CalibrateRequested?.Invoke(Board);
    private void OptionsClick(object sender, RoutedEventArgs e) => OptionsRequested?.Invoke(Board);
    private void FirmwareClick(object sender, RoutedEventArgs e) => FirmwareRequested?.Invoke(Board);
}
