using System;
using System.Threading;
using System.Linq;
using System.Windows;

namespace DeckPilot.Windows;
public partial class App : Application
{
    private Mutex? instance;
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (e.Args.Length == 2 && e.Args[0] == "--apply-update")
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            int result = await UpdateInstaller.ApplyAsync(e.Args[1]);
            Shutdown(result); return;
        }
        instance = new Mutex(true, @"Local\DeckPilot.Windows", out bool first);
        if (!first)
        {
            MessageBox.Show("DeckPilot fonctionne déjà. Ouvrez-le depuis son icône près de l’horloge.", "DeckPilot");
            Shutdown();
            return;
        }
        var window = new MainWindow(e.Args.Contains("--background"));
        int healthArgument = Array.IndexOf(e.Args, "--update-health");
        window.Loaded += (_, _) =>
        {
            if (window.IsOperational && healthArgument >= 0 && healthArgument + 1 < e.Args.Length)
                try { UpdateService.WriteHealth(e.Args[healthArgument + 1]); } catch (System.IO.IOException) { } catch (UnauthorizedAccessException) { }
            if (e.Args.Contains("--update-failed")) window.ReportUpdateFailure();
        };
        window.Show();
    }
    protected override void OnExit(ExitEventArgs e) { instance?.Dispose(); base.OnExit(e); }
}
