using System;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

internal static class Program
{
    [STAThread]
    private static int Main()
    {
        try { return Capture(); }
        catch (Exception error) { Console.WriteLine("::error::" + error.ToString().Replace("\r", " ").Replace("\n", " ")); return 1; }
    }
    private static int Capture()
    {
        RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
        var app = new DeckPilot.Windows.App();
        app.DispatcherUnhandledException += (_, e) => { Console.WriteLine("::error::" + e.Exception.ToString().Replace("\r", " ").Replace("\n", " ")); e.Handled = true; app.Shutdown(1); };
        app.InitializeComponent();
        var window = new DeckPilot.Windows.MainWindow();
        window.MinWidth = 960; window.MinHeight = 720;
        window.Width = 1080; window.Height = 980;
        window.WindowStartupLocation = WindowStartupLocation.Manual;
        window.Left = 0; window.Top = 0;
        window.Show();
        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(4) };
        int result = 1;
        timer.Tick += (_, _) =>
        {
            timer.Stop();
            try
            {
                window.UpdateLayout();
                var content = (FrameworkElement)window.Content;
                var image = new RenderTargetBitmap((int)Math.Ceiling(content.ActualWidth), (int)Math.Ceiling(content.ActualHeight), 96, 96, PixelFormats.Pbgra32);
                image.Render(content);
                var png = new PngBitmapEncoder(); png.Frames.Add(BitmapFrame.Create(image));
                Directory.CreateDirectory("docs/previews");
                using var output = File.Create("docs/previews/deckpilot-1.6.1.png");
                png.Save(output);
                Console.WriteLine($"Captured actual WPF interface: {image.PixelWidth} x {image.PixelHeight}");
                result = 0;
            }
            catch (Exception error) { Console.WriteLine("::error::" + error); }
            app.Shutdown();
            Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
        };
        timer.Start();
        Dispatcher.Run();
        return result;
    }
}
