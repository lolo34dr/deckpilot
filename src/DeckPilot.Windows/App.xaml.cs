using System;
using System.Threading;
using System.Linq;
using System.Windows;

namespace DeckPilot.Windows;
public partial class App : Application
{
    private Mutex? instance;
    private EventWaitHandle? installerExit;
    private RegisteredWaitHandle? installerWait;
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (e.Args.Length == 2 && e.Args[0] == "--apply-update")
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            int result = await UpdateInstaller.ApplyAsync(e.Args[1]);
            Shutdown(result); return;
        }
        if (e.Args.Length == 1 && e.Args[0] == "--exit-for-install")
        {
            try { using var signal = EventWaitHandle.OpenExisting(@"Local\DeckPilot.InstallExit"); signal.Set(); }
            catch (WaitHandleCannotBeOpenedException) { }
            Shutdown(); return;
        }
        if (e.Args.Length == 2 && e.Args[0] == "--configure-startup")
        {
            try { StartupService.Set(e.Args[1] == "on"); Shutdown(); }
            catch { Shutdown(1); }
            return;
        }
        if (e.Args.Length == 1 && e.Args[0] == "--remove-startup")
        {
            try { StartupService.RemoveInstalledEntry(); Shutdown(); }
            catch { Shutdown(1); }
            return;
        }
        instance = new Mutex(true, @"Local\DeckPilot.Windows", out bool first);
        if (!first)
        {
            MessageBox.Show("DeckPilot fonctionne déjà. Ouvrez-le depuis son icône près de l’horloge.", "DeckPilot");
            Shutdown();
            return;
        }
        var window = new MainWindow(e.Args.Contains("--background"));
        installerExit = new EventWaitHandle(false, EventResetMode.AutoReset, @"Local\DeckPilot.InstallExit");
        installerWait = ThreadPool.RegisterWaitForSingleObject(installerExit, (_, _) => Dispatcher.BeginInvoke(window.RequestInstallerExit), null, Timeout.Infinite, false);
        int healthArgument = Array.IndexOf(e.Args, "--update-health");
        window.Loaded += (_, _) =>
        {
            if (window.IsOperational && healthArgument >= 0 && healthArgument + 1 < e.Args.Length)
                try { UpdateService.WriteHealth(e.Args[healthArgument + 1]); } catch (System.IO.IOException) { } catch (UnauthorizedAccessException) { }
            if (e.Args.Contains("--update-failed")) window.ReportUpdateFailure();
        };
        window.Show();
    }
    protected override void OnExit(ExitEventArgs e) { installerWait?.Unregister(null); installerExit?.Dispose(); instance?.Dispose(); base.OnExit(e); }
}
