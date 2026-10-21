using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public static class VectorIcons
{
    // SVG-style path geometry, rendered natively by WPF at any scale.
    private static readonly string[] Paths = [
        "M3,5 L21,5 21,19 3,19 Z M6,9 L7,9 M10,9 L11,9 M14,9 L15,9 M18,9 L19,9 M6,12 L7,12 M10,12 L11,12 M14,12 L15,12 M18,12 L19,12 M7,16 L17,16",
        "M4,4 L20,4 20,20 4,20 Z M4,9 L20,9 M7,6.5 L8,6.5 M10,6.5 L11,6.5 M8,13 L16,13 M8,16 L13,16",
        "M3,7 L9,7 11,10 21,10 21,20 3,20 Z M3,7 L3,5 10,5 12,7 20,7 20,10",
        "M12,2 A10,10 0 1 1 11.99,2 M2,12 L22,12 M12,2 C5,8 5,16 12,22 M12,2 C19,8 19,16 12,22",
        "M5,5 L10,5 10,10 5,10 Z M14,14 L19,14 19,19 14,19 Z M10,7.5 L16.5,7.5 16.5,14 M13,11 L16.5,14 20,11",
        "M8,4 L20,12 8,20 Z"
    ];
    private static readonly ImageSource[] Images = Create();
    private static ImageSource[] Create()
    {
        var images = new List<ImageSource>();
        foreach (string path in Paths)
        {
            var geometry = Geometry.Parse(path);
            var drawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(Color.FromRgb(202, 213, 207)), 1.35) { StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round, LineJoin = PenLineJoin.Round }, geometry);
            var group = new DrawingGroup();
            group.Children.Add(new GeometryDrawing(Brushes.Transparent, null, new RectangleGeometry(new Rect(0, 0, 24, 24))));
            group.Children.Add(drawing);
            var image = new DrawingImage(group); image.Freeze(); images.Add(image);
        }
        return images.ToArray();
    }
    public static ImageSource ForKind(ActionKind kind) => Images[(int)kind];
}
public static class ApplicationIcons
{
    private static readonly Dictionary<string, ImageSource> Cache = new(StringComparer.OrdinalIgnoreCase);
    public static ImageSource ForTarget(string target)
    {
        if (string.IsNullOrWhiteSpace(target) || ApplicationTarget.IsPackaged(target) || !File.Exists(target)) return VectorIcons.ForKind(ActionKind.Application);
        if (Cache.TryGetValue(target, out var cached)) return cached;
        var info = new ShellFileInfo();
        if (SHGetFileInfo(target, 0, ref info, (uint)Marshal.SizeOf<ShellFileInfo>(), 0x100) != IntPtr.Zero && info.Icon != IntPtr.Zero)
        {
            try
            {
                var bitmap = Imaging.CreateBitmapSourceFromHIcon(info.Icon, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(32, 32));
                bitmap.Freeze(); Cache[target] = bitmap; return bitmap;
            }
            finally { DestroyIcon(info.Icon); }
        }
        return VectorIcons.ForKind(ActionKind.Application);
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)] private struct ShellFileInfo
    {
        public IntPtr Icon; public int IconIndex; public uint Attributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string DisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string TypeName;
    }
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr SHGetFileInfo(string path, uint attributes, ref ShellFileInfo info, uint size, uint flags);
    [DllImport("user32.dll")] private static extern bool DestroyIcon(IntPtr icon);
}
public sealed class ApplicationIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ApplicationIcons.ForTarget(value as string ?? "");
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
}
public static class Motion
{
    public static void Reveal(UIElement element)
    {
        if (!SystemParameters.ClientAreaAnimation) return;
        element.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(0.55, 1, TimeSpan.FromMilliseconds(180)) { EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut } });
    }
}

public static class DarkChrome
{
    public static void Attach(Window window)
    {
        window.SourceInitialized += (_, _) =>
        {
            int enabled = 1;
            var handle = new WindowInteropHelper(window).Handle;
            if (DwmSetWindowAttribute(handle, 20, ref enabled, sizeof(int)) != 0)
                DwmSetWindowAttribute(handle, 19, ref enabled, sizeof(int));
        };
    }
    [DllImport("dwmapi.dll")] private static extern int DwmSetWindowAttribute(IntPtr window, int attribute, ref int value, int size);
}

public static class BrandAssets
{
    public static System.Drawing.Icon CreateTrayIcon()
    {
        using var stream = Application.GetResourceStream(new Uri("pack://application:,,,/Assets/DeckPilot.png"))!.Stream;
        using var original = new System.Drawing.Bitmap(stream);
        using var small = new System.Drawing.Bitmap(original, new System.Drawing.Size(32, 32));
        IntPtr handle = small.GetHicon();
        try { using var borrowed = System.Drawing.Icon.FromHandle(handle); return (System.Drawing.Icon)borrowed.Clone(); }
        finally { DestroyIcon(handle); }
    }
    [DllImport("user32.dll")] private static extern bool DestroyIcon(IntPtr icon);
}
