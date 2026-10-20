using System;
using System.Threading;
using System.Linq;
using System.Windows;

namespace DeckPilot.Windows;
public partial class App : Application
{
    private Mutex? instance;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        instance = new Mutex(true, @"Local\DeckPilot.Windows", out bool first);
        if (!first)
        {
            MessageBox.Show("DeckPilot fonctionne déjà. Ouvrez-le depuis son icône près de l’horloge.", "DeckPilot");
            Shutdown();
            return;
        }
        new MainWindow(e.Args.Contains("--background")).Show();
    }
    protected override void OnExit(ExitEventArgs e) { instance?.Dispose(); base.OnExit(e); }
}
