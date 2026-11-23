using System;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using DeckPilot.Core;

namespace DeckPilot.Windows;

internal partial class MacropadCard : UserControl
{
    private readonly DeckKeyButton[] keys = new DeckKeyButton[12];
    private bool firmwareRequired;
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
            var key = keys[i] = new DeckKeyButton
            {
                Width = double.NaN, Height = 82, Margin = new Thickness(3),
                KeyCaption = $"{i + 1:00}"
            };
            key.Click += (_, _) => ConfigureRequested?.Invoke(Board, index);
            CardKeys.Children.Add(key);
        }
    }

    public void Refresh(BoardLinkState board, BoardBinding? binding, Profile? profile, bool shared,
        bool firmwareOpen, bool editing, int selectedKey, Brush accent)
    {
        Board = board;
        string title = $"Macropad {board.Device.Port}";
        CardTitle.Text = profile?.Name ?? "Macropad";
        CardPort.Text = board.Device.Port;
        if (editing) CardFrame.SetResourceReference(Border.BorderBrushProperty, "AccentBrush");
        else CardFrame.ClearValue(Border.BorderBrushProperty);
        AutomationProperties.SetName(this, title);
        AutomationProperties.SetAutomationId(this, $"Macropad{board.Device.Port}");
        bool supported = board.Info?.SupportsSerialInput == true;
        firmwareRequired = board.Info != null && !supported || board.Kind == BoardConnectionKind.Unidentified;
        CardStatus.Text = (board, binding?.Enabled) switch
        {
            ({ SerialInput: true }, true) => "Prêt à utiliser",
            _ when firmwareRequired => "Mise à jour du firmware nécessaire",
            _ when supported && binding?.Enabled != true => "Mode clavier · F13 à F24",
            _ when supported && board.Message.StartsWith("Mode clavier", StringComparison.Ordinal) => "Activation du mode DeckPilot…",
            _ => board.Message
        };
        int assigned = profile?.Buttons.Count(action => action.Kind != ActionKind.Passthrough) ?? 0;
        CardProgress.Text = shared ? $"{assigned} / 12 touches · Profil partagé"
            : $"{assigned} / 12 touches configurées";
        bool editable = !firmwareOpen && !firmwareRequired && (supported || binding?.Enabled == true && profile != null);
        ConfigureButton.Content = firmwareRequired ? "Mettre à jour le firmware" : shared ? "Séparer et configurer"
            : profile == null || binding?.Enabled != true ? "Configurer le macropad" : "Modifier les touches";
        ConfigureButton.IsEnabled = editable || firmwareRequired && !firmwareOpen;
        CalibrateCardButton.IsEnabled = board.SerialInput && !firmwareOpen;
        CalibrateCardButton.Visibility = board.SerialInput && binding?.KeyMap.Count == 0 && assigned == 0 ? Visibility.Visible : Visibility.Collapsed;
        BoardOptionsButton.IsEnabled = !firmwareOpen;
        AutomationProperties.SetName(ConfigureButton, $"{ConfigureButton.Content} · {title}");
        AutomationProperties.SetName(CalibrateCardButton, $"Calibrer {title}");
        AutomationProperties.SetName(BoardOptionsButton, $"Options de {title}");

        var map = binding?.KeyMap;
        int mask = board.Telemetry?.Mask ?? 0;
        for (int i = 0; i < keys.Length; i++)
        {
            var key = keys[i];
            var action = profile?.Buttons[i];
            var kind = action?.Kind ?? ActionKind.Passthrough;
            key.Label = kind == ActionKind.Passthrough ? "Libre"
                : string.IsNullOrWhiteSpace(action!.Label) ? ActionSearch.KindName(kind) : action.Label;
            key.IconSource = kind == ActionKind.Application ? ApplicationIcons.ForTarget(action!.Target) : VectorIcons.ForKind(kind);
            key.AccentBrush = kind == ActionKind.Passthrough ? Brushes.Transparent : accent;
            key.IsEnabled = editable;
            key.IsSelected = editing && i == selectedKey;
            string name = $"{title} · Touche {i + 1:00} · {key.Label}";
            key.ToolTip = name;
            AutomationProperties.SetName(key, name);
            AutomationProperties.SetAutomationId(key, $"Macropad{board.Device.Port}Key{i + 1:00}");
            int bit = map?.Count == 12 ? map[i] - Calibration.FirstKey : i;
            key.IsHardwarePressed = (mask & (1 << bit)) != 0;
        }
    }

    private void ConfigureClick(object sender, RoutedEventArgs e)
    {
        if (firmwareRequired) FirmwareRequested?.Invoke(Board);
        else ConfigureRequested?.Invoke(Board, 0);
    }

    private void CalibrateClick(object sender, RoutedEventArgs e) => CalibrateRequested?.Invoke(Board);
    private void OptionsClick(object sender, RoutedEventArgs e) => OptionsRequested?.Invoke(Board);
}
