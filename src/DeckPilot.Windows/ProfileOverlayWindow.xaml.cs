using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DeckPilot.Core;
using Forms = System.Windows.Forms;

namespace DeckPilot.Windows;

public partial class ProfileOverlayWindow : Window
{
    [StructLayout(LayoutKind.Sequential)]
    private struct ScreenPoint
    {
        public int X;
        public int Y;
        public ScreenPoint(int x, int y) { X = x; Y = y; }
    }
    private const int ExtendedStyle = -20;
    private const int Transparent = 0x20;
    private const int ToolWindow = 0x80;
    private const int NoActivate = 0x08000000;
    private const uint NoSize = 0x0001;
    private const uint NoActivatePosition = 0x0010;
    private readonly DispatcherTimer hideTimer = new() { Interval = TimeSpan.FromSeconds(2.8) };
    private int revision;
    private bool reducedMotion;

    public ProfileOverlayWindow()
    {
        InitializeComponent();
        SourceInitialized += (_, _) =>
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            SetWindowLong(handle, ExtendedStyle, GetWindowLong(handle, ExtendedStyle) | Transparent | ToolWindow | NoActivate);
        };
        hideTimer.Tick += (_, _) => HideAfterDelay();
        Closed += (_, _) => { revision++; hideTimer.Stop(); };
    }

    public void ShowProfile(string profileName, ProfileOverlaySettings settings, bool reduceMotion)
    {
        revision++;
        hideTimer.Stop();
        BeginAnimation(OpacityProperty, null);
        reducedMotion = reduceMotion || !SystemParameters.ClientAreaAnimation;
        ProfileNameText.Text = profileName;

        IntPtr foreground = GetForegroundWindow();
        Forms.Screen screen = foreground != IntPtr.Zero ? Forms.Screen.FromHandle(foreground) : Forms.Screen.PrimaryScreen!;
        if (!IsVisible) Show();
        UpdateLayout();
        Place(screen, settings);
        int current = revision;
        Dispatcher.BeginInvoke(() => { if (current == revision && IsVisible) Place(screen, settings); }, DispatcherPriority.Loaded);

        if (reducedMotion) Opacity = 1;
        else BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(170)) { FillBehavior = FillBehavior.Stop });
        hideTimer.Start();
    }

    private void HideAfterDelay()
    {
        hideTimer.Stop();
        int current = revision;
        if (reducedMotion) { Hide(); return; }
        var fade = new DoubleAnimation(Opacity, 0, TimeSpan.FromMilliseconds(190));
        fade.Completed += (_, _) => { if (current == revision) { BeginAnimation(OpacityProperty, null); Hide(); } };
        BeginAnimation(OpacityProperty, fade);
    }

    private void Place(Forms.Screen screen, ProfileOverlaySettings settings)
    {
        IntPtr handle = new WindowInteropHelper(this).Handle;
        var bounds = screen.Bounds;
        IntPtr monitor = MonitorFromPoint(new ScreenPoint(bounds.Left + bounds.Width / 2, bounds.Top + bounds.Height / 2), 2);
        uint dpi = 96;
        if (monitor != IntPtr.Zero && GetDpiForMonitor(monitor, 0, out uint xDpi, out _) == 0) dpi = xDpi;
        double scale = dpi / 96d;
        int width = Math.Max(1, (int)Math.Ceiling(ActualWidth * scale));
        int height = Math.Max(1, (int)Math.Ceiling(ActualHeight * scale));
        var area = screen.WorkingArea;
        var (x, y) = ProfileOverlayLayout.Place(area.Left, area.Top, area.Width, area.Height,
            width, height, (int)Math.Round(24 * scale), settings);
        if (!SetWindowPos(handle, new IntPtr(-1), x, y, 0, 0, NoSize | NoActivatePosition))
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] private static extern IntPtr MonitorFromPoint(ScreenPoint point, uint flags);
    [DllImport("Shcore.dll")] private static extern int GetDpiForMonitor(IntPtr monitor, int dpiType, out uint dpiX, out uint dpiY);
    [DllImport("user32.dll", EntryPoint = "GetWindowLongW")] private static extern int GetWindowLong(IntPtr handle, int index);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongW")] private static extern int SetWindowLong(IntPtr handle, int index, int value);
    [DllImport("user32.dll", SetLastError = true)] private static extern bool SetWindowPos(IntPtr handle, IntPtr insertAfter, int x, int y, int width, int height, uint flags);
}
