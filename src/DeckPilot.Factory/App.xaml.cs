using System.Windows;
using System.Threading;
using System.Windows.Threading;
namespace DeckPilot.Factory;
public partial class App : System.Windows.Application
{
    private Mutex? instance;
    private EventWaitHandle? installerExit;
    private RegisteredWaitHandle? installerWait;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (e.Args.Length == 1 && e.Args[0] == "--exit-for-install")
        {
            try { using var signal = EventWaitHandle.OpenExisting(@"Local\DeckPilot.Factory.InstallExit"); signal.Set(); }
            catch (WaitHandleCannotBeOpenedException) { }
            catch (UnauthorizedAccessException) { Shutdown(1); return; }
            Shutdown(); return;
        }
        instance = new Mutex(true, @"Local\DeckPilot.Factory", out bool first);
        if (!first) { Shutdown(); return; }
        DispatcherUnhandledException += (_, args) => FactoryDiagnostics.Write(args.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, args) => FactoryDiagnostics.Write(args.ExceptionObject as Exception);
        var window = new MainWindow();
        installerExit = new EventWaitHandle(false, EventResetMode.AutoReset, @"Local\DeckPilot.Factory.InstallExit");
        installerWait = ThreadPool.RegisterWaitForSingleObject(installerExit, (_, _) => Dispatcher.BeginInvoke(window.RequestInstallerExit), null, Timeout.Infinite, false);
        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        installerWait?.Unregister(null); installerExit?.Dispose(); instance?.Dispose(); base.OnExit(e);
    }
}

internal static class FactoryDiagnostics
{
    public static void Write(Exception? exception)
    {
        if (exception == null) return;
        try
        {
            string folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilotFactory");
            System.IO.Directory.CreateDirectory(folder);
            System.IO.File.AppendAllText(System.IO.Path.Combine(folder, "startup-errors.log"), $"{DateTimeOffset.Now:O}\n{exception}\n\n");
        }
        catch { }
    }
}
