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
    private static readonly Brush DefaultFrameBrush = new SolidColorBrush(Color.FromRgb(0x54, 0x50, 0x60));
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
                Width = double.NaN, Height = 90, Margin = new Thickness(4),
                KeyCaption = $"{i + 1:00}"
            };
            key.Click += (_, _) => ConfigureRequested?.Invoke(Board, index);
            CardKeys.Children.Add(key);
        }
    }

    public void SetWide(bool wide)
    {
        GuideColumn.Width = wide ? new GridLength(260) : new GridLength(0);
        Grid.SetColumn(CardGuide, wide ? 1 : 0);
        Grid.SetRow(CardGuide, wide ? 0 : 1);
        CardGuide.Margin = wide ? new Thickness(14, 0, 0, 0) : new Thickness(0, 12, 0, 0);
        CardGuide.Visibility = wide ? Visibility.Visible : Visibility.Collapsed;
        CardModeCompact.Visibility = wide ? Visibility.Collapsed : Visibility.Visible;
    }

    public void Refresh(BoardLinkState board, BoardBinding? binding, Profile? profile, bool shared,
        bool firmwareOpen, bool editing, int selectedKey, string title, Func<string, Brush> accentFor)
    {
        Board = board;
        CardTitle.Text = title;
        CardProfile.Text = profile == null ? "Profil de touches · attribué au premier choix" : $"Profil de touches · {profile.Name}";
        CardPort.Text = board.Device.Port;
        if (editing) CardFrame.SetResourceReference(Border.BorderBrushProperty, "AccentBrush");
        else CardFrame.BorderBrush = DefaultFrameBrush;
        AutomationProperties.SetName(this, $"{title} · {board.Device.Port}");
        AutomationProperties.SetAutomationId(this, $"Macropad{board.Device.Port}");
        bool supported = board.Info?.SupportsSerialInput == true;
        firmwareRequired = board.Info != null && !supported || board.Kind == BoardConnectionKind.Unidentified;
        CardStatus.Text = (board, binding?.Enabled) switch
        {
            ({ SerialInput: true }, true) => "Connecté · Prêt à utiliser",
            _ when firmwareRequired => "Mise à jour du firmware nécessaire",
            _ when supported && binding?.Enabled != true => "Connecté · Mode clavier F13 à F24",
            _ when supported && board.Message.StartsWith("Mode clavier", StringComparison.Ordinal) => "Activation du mode DeckPilot…",
            _ => board.Message
        };
        StatusDot.Fill = board.SerialInput && binding?.Enabled == true ? Brushes.LightGreen
            : firmwareRequired ? Brushes.Orange : Brushes.LightSteelBlue;
        int assigned = profile?.Buttons.Count(action => action.Kind != ActionKind.Passthrough) ?? 0;
        CardProgress.Text = shared ? $"{assigned} / 12 touches · Profil partagé"
            : $"{assigned} / 12 touches configurées";
        CardModeHint.Text = firmwareRequired ? "Installez le firmware compatible pour attribuer des actions indépendantes à ce macropad."
            : binding?.Enabled != true ? "Au premier choix d’une touche, le mode DeckPilot sera activé pour cet appareil. Les touches libres n’enverront alors plus F13 à F24."
            : shared ? "Ce profil est utilisé par plusieurs macropads. La première modification en créera une copie pour celui-ci."
            : "Chaque modification est enregistrée automatiquement. Les autres macropads gardent leurs propres actions.";
        CardModeCompact.Text = firmwareRequired ? "Firmware compatible requis avant de configurer les touches."
            : binding?.Enabled != true ? "Premier choix : active le mode DeckPilot ; les touches libres n’enverront plus F13 à F24."
            : shared ? "Profil partagé · La prochaine modification en fera une copie pour cet appareil."
            : "Cliquez une touche pour modifier son action ; les changements sont enregistrés sur ce PC.";
        bool editable = !firmwareOpen && !firmwareRequired && (supported || binding?.Enabled == true && profile != null);
        ConfigureButton.Content = firmwareRequired ? "Mettre à jour le firmware" : shared ? "Séparer et configurer les touches"
            : assigned == 0 ? "Configurer la touche 01" : "Modifier la touche 01";
        ConfigureButton.IsEnabled = editable || firmwareRequired && !firmwareOpen;
        CalibrateCardButton.IsEnabled = board.SerialInput && !firmwareOpen;
        CalibrateCardButton.Visibility = board.SerialInput ? Visibility.Visible : Visibility.Collapsed;
        BoardOptionsButton.IsEnabled = !firmwareOpen;
        AutomationProperties.SetName(ConfigureButton, $"{ConfigureButton.Content} · {title} · {board.Device.Port}");
        AutomationProperties.SetName(CalibrateCardButton, $"Calibrer {title} · {board.Device.Port}");
        AutomationProperties.SetName(BoardOptionsButton, $"Réglages de {title} · {board.Device.Port}");

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
            key.KindSource = VectorIcons.ForKind(kind);
            key.AccentBrush = kind == ActionKind.Passthrough ? Brushes.Transparent : accentFor(action!.Accent);
            key.IsEnabled = editable;
            key.IsSelected = editing && i == selectedKey;
            string name = $"{title} · {board.Device.Port} · Touche {i + 1:00} · {key.Label}";
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
