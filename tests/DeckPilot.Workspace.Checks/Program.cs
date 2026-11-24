using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DeckPilot.Core;
using DeckPilot.Windows;

internal static class Program
{
    private const BindingFlags InstanceFields = BindingFlags.Instance | BindingFlags.NonPublic;

    [STAThread]
    private static void Main(string[] args)
    {
        string? capturePrefix = CapturePrefix(args);
        var directory = Directory.CreateTempSubdirectory("deckpilot-workspace-");
        try
        {
            var app = new App();
            app.InitializeComponent();
            var window = new MainWindow();
            var store = new SettingsStore(Path.Combine(directory.FullName, "settings.json"));
            var settings = new Settings();
            typeof(MainWindow).GetField("store", InstanceFields)!.SetValue(window, store);
            typeof(MainWindow).GetField("settings", InstanceFields)!.SetValue(window, settings);
            Invoke(window, "RefreshProfiles");
            Invoke(window, "LoadEditor");

            var a = CreateBoard("COM3", "board-a");
            var b = CreateBoard("COM4", "board-b");
            var state = new BoardConnectionState(BoardConnectionKind.Connected, a.Message,
                [a.Device, b.Device], a.Device, a.Info, ConnectedCount: 2) { Boards = [a, b] };
            var tabs = Find<TabControl>(window, "ControlTabs");
            var deviceTab = Find<TabItem>(window, "DeviceTab");
            var singleState = state with { Devices = [a.Device], Boards = [a], ConnectedCount = 1 };
            if (capturePrefix != null)
            {
                Invoke(window, "OnBoardStateChanged", singleState);
                tabs.SelectedItem = deviceTab;
                CaptureOverview(window, capturePrefix, "one-overview-wide", 1180, 900);
                CaptureOverview(window, capturePrefix, "one-overview-narrow", 720, 820);
                CaptureOverview(window, capturePrefix, "one-settings", 1180, 900, showSettings: true);
            }

            Invoke(window, "OnBoardStateChanged", state);
            tabs.SelectedItem = deviceTab;
            var cards = Find<UniformGrid>(window, "BoardCardsPanel");
            Expect(cards.Children.Count == 2, "Both devices need a card.");
            Expect(Find<Border>(window, "ConfigurationPanel").GetValue(Grid.ColumnSpanProperty) is 2,
                "The overview needs the full workspace.");
            CaptureOverview(window, capturePrefix, "two-overview-wide", 1440, 900);
            CaptureOverview(window, capturePrefix, "two-overview-narrow", 720, 820);
            CaptureOverview(window, capturePrefix, "two-settings", 1440, 900, showSettings: true);
            LayoutWindow(window, 1440, 900);
            var viewport = Find<ScrollViewer>(window, "DeviceScroll");
            var overview = Find<StackPanel>(window, "BoardOverviewPanel");
            Expect(viewport.ViewportWidth > 1100 && overview.ActualWidth >= viewport.ViewportWidth - 30 && cards.Columns == 2,
                "Two macropads must use the available width and appear side by side.");
            LayoutWindow(window, 720, 820);
            Expect(cards.Columns == 1, "A narrow window must place the macropads in one column.");
            Invoke(window, "ResizeBoardCards", 1100d);
            Expect(cards.Columns == 2, "Wide view needs two cards per row.");
            Invoke(window, "ResizeBoardCards", 620d);
            Expect(cards.Columns == 1, "Narrow view needs one card per row.");

            ClickFirstKey(cards, 0);
            Expect(tabs.SelectedItem == Find<TabItem>(window, "ActionsTab"), "Selecting a key opens the library.");
            Expect(Find<TextBlock>(window, "ActionBoardTarget").Text.StartsWith("COM3", StringComparison.Ordinal), "Wrong editing target.");
            Invoke(window, "AssignAction", 0, new ButtonAction { Kind = ActionKind.Media, Key = 0xB3, Label = "Pause" }, false);
            CaptureWindow(window, capturePrefix, "first-editor-wide", 1180, 900);
            CaptureWindow(window, capturePrefix, "first-editor-narrow", 720, 820);

            tabs.SelectedItem = deviceTab;
            ClickFirstKey(cards, 1);
            Expect(Find<TextBlock>(window, "ActionBoardTarget").Text.StartsWith("COM4", StringComparison.Ordinal), "Wrong editing target.");
            Invoke(window, "AssignAction", 0, new ButtonAction { Kind = ActionKind.Media, Key = 0xB0, Label = "Suivante" }, false);
            CaptureWindow(window, capturePrefix, "second-editor-wide", 1180, 900);
            CaptureWindow(window, capturePrefix, "second-editor-narrow", 720, 820);

            var saved = store.Load();
            var first = saved.Profiles.Single(profile => profile.Id == saved.BoardBindings["board-a"].ProfileId);
            var second = saved.Profiles.Single(profile => profile.Id == saved.BoardBindings["board-b"].ProfileId);
            Expect(first.Id != second.Id && first.Buttons[0].Label == "Pause" && second.Buttons[0].Label == "Suivante",
                "Each device must retain its own action.");
            Expect(saved.ActiveProfileId == first.Id, "Editing the second board must preserve the active keyboard profile.");
            Expect(saved.BoardBindings.Values.All(binding => binding.Enabled), "Both boards must enter DeckPilot mode.");

            tabs.SelectedItem = deviceTab;
            var activeA = a with { Message = "Mode DeckPilot actif", SerialInput = true };
            var activeB = b with { Message = "Mode DeckPilot actif", SerialInput = true };
            Invoke(window, "OnBoardStateChanged", state with
            {
                Message = activeA.Message, SerialInput = true, Boards = [activeA, activeB]
            });
            CaptureOverview(window, capturePrefix, "two-configured", 1440, 900);
            Invoke(window, "OnBoardStateChanged", singleState with
            {
                Message = activeA.Message, SerialInput = true, Boards = [activeA]
            });
            Expect(cards.Children.Count == 1 && CardKeys(cards, 0).Children.Count == 12,
                "The remaining deck keeps its twelve keys after disconnect.");
            CaptureOverview(window, capturePrefix, "one-configured", 1180, 900);
            if (capturePrefix != null)
            {
                ClickFirstKey(cards, 0);
                CaptureWindow(window, capturePrefix, "one-editor-wide", 1180, 900);
                CaptureWindow(window, capturePrefix, "one-editor-narrow", 720, 820);
            }
            Console.WriteLine("PASS Macropad workspace: two responsive cards, direct key editing, independent actions and unplugging.");
        }
        finally { directory.Delete(recursive: true); }
    }

    private static BoardLinkState CreateBoard(string port, string id) =>
        new(new(port, "DeckPilot", id, "", false), BoardConnectionKind.Connected, "Mode clavier · F13 à F24", new(new("1.2.0"), 2, 7));

    private static string? CapturePrefix(string[] args)
    {
        if (args.Length == 0) return null;
        if (args.Length != 1 || !args[0].StartsWith("--capture-prefix=", StringComparison.Ordinal))
            throw new ArgumentException("Usage: --capture-prefix=before (or after)");
        string prefix = args[0]["--capture-prefix=".Length..];
        if (prefix.Length is < 1 or > 40 || !prefix.All(c => char.IsAsciiLetterOrDigit(c) || c is '-' or '_'))
            throw new ArgumentException("The capture prefix must use only ASCII letters, digits, '-' or '_'.");
        return prefix;
    }

    private static void CaptureOverview(MainWindow window, string? prefix, string name, int width, int height, bool showSettings = false)
    {
        if (prefix == null) return;
        var expander = Find<Expander>(window, "BoardSettingsExpander");
        var scroll = Find<ScrollViewer>(window, "DeviceScroll");
        bool wasExpanded = expander.IsExpanded;
        try
        {
            expander.IsExpanded = showSettings;
            CaptureWindow(window, prefix, name, width, height, () =>
            {
                if (showSettings)
                {
                    // Keep the settings header and controls together in the viewport.
                    double top = expander.TranslatePoint(new Point(), scroll).Y + scroll.VerticalOffset;
                    scroll.ScrollToVerticalOffset(Math.Max(0, top - 24));
                }
                else scroll.ScrollToTop();
            });
        }
        finally
        {
            expander.IsExpanded = wasExpanded;
            scroll.ScrollToTop();
        }
    }

    private static void CaptureWindow(MainWindow window, string? prefix, string name, int width, int height, Action? afterLayout = null)
    {
        if (prefix == null) return;
        var root = LayoutWindow(window, width, height);
        afterLayout?.Invoke();
        root.UpdateLayout();

        var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(root);
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmap));
        string folder = Path.Combine(Environment.CurrentDirectory, "artifacts", "ui");
        Directory.CreateDirectory(folder);
        string path = Path.Combine(folder, $"{prefix}-{name}.png");
        using var output = File.Create(path);
        encoder.Save(output);
        Console.WriteLine($"CAPTURE {path} ({width} × {height})");
    }

    private static Grid LayoutWindow(MainWindow window, int width, int height)
    {
        var root = Find<Grid>(window, "WindowContent");
        root.Background = window.Background;
        root.Width = width;
        root.Height = height;
        var size = new Size(width, height);
        root.Measure(size);
        root.Arrange(new Rect(size));
        root.UpdateLayout();
        return root;
    }

    private static UniformGrid CardKeys(UniformGrid cards, int index) =>
        (UniformGrid)((FrameworkElement)cards.Children[index]).FindName("CardKeys");

    private static void ClickFirstKey(UniformGrid cards, int index)
    {
        var key = (Button)CardKeys(cards, index).Children[0];
        Expect(key.IsEnabled, "The card should allow direct configuration.");
        key.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, key));
    }

    private static T Find<T>(MainWindow window, string name) where T : FrameworkElement =>
        (T)window.FindName(name);

    private static void Invoke(MainWindow window, string method, params object[] args) =>
        typeof(MainWindow).GetMethod(method, InstanceFields)!.Invoke(window, args);

    private static void Expect(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
