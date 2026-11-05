using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

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
            catch (UnauthorizedAccessException) { Shutdown(1); return; }
            Shutdown(); return;
        }
        if (e.Args.Length == 2 && e.Args[0] == "--configure-startup")
        {
            try { StartupService.Set(e.Args[1] == "on"); Shutdown(); }
            catch { Shutdown(1); }
            return;
        }
        if (e.Args.Length == 3 && e.Args[0] == "--configure-preferences")
        {
            try
            {
                bool autoUpdates = e.Args[1] == "on";
                bool reduceMotion = e.Args[2] == "on";
                if ((e.Args[1] is not ("on" or "off")) || (e.Args[2] is not ("on" or "off"))) throw new ArgumentException("Invalid installer preferences.");
                var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeckPilot", "settings.json");
                var store = new DeckPilot.Core.SettingsStore(path);
                var settings = store.Load();
                settings.AutoCheckUpdates = autoUpdates;
                settings.ReduceMotion = reduceMotion;
                store.Save(settings);
                Shutdown();
            }
            catch { Shutdown(1); }
            return;
        }
        if (e.Args.Length == 1 && (e.Args[0] is "--configure-microphone" or "--configure-microphone-if-needed"))
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            try
            {
                var store = new DeckPilot.Core.SettingsStore(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeckPilot", "settings.json"));
                var settings = store.Load();
                var endpoints = await Task.Run(SoundboardEngine.DiscoverEndpoints);
                if (!string.IsNullOrWhiteSpace(endpoints.Error)) throw new System.IO.IOException(endpoints.Error);
                var physicalMicrophones = endpoints.Microphones.Where(m => !m.IsVirtualCable).ToArray();
                bool configureOnlyIfNeeded = e.Args[0] == "--configure-microphone-if-needed";
                if (configureOnlyIfNeeded && physicalMicrophones.Any(m => string.Equals(m.Id, settings.Soundboard.MicrophoneId, StringComparison.OrdinalIgnoreCase)))
                {
                    Shutdown();
                    return;
                }
                var dialog = new MicrophoneSetupWindow(physicalMicrophones, settings.Soundboard.MicrophoneId);
                if (dialog.ShowDialog() == true && dialog.SelectedMicrophone is { } microphone)
                {
                    settings.Soundboard.MicrophoneId = microphone.Id;
                    store.Save(settings);
                }
                Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le choix du microphone n’a pas pu être enregistré : " + ex.Message, "DeckPilot", MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown(1);
            }
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
            if (!e.Args.Contains("--background")) MessageBox.Show("DeckPilot fonctionne déjà. Ouvrez-le depuis son icône près de l’horloge.", "DeckPilot");
            Shutdown();
            return;
        }
        SplashWindow? splash = null;
        if (!e.Args.Contains("--background"))
        {
            splash = new SplashWindow();
            splash.Show();
            // Let WPF render the branded splash before building the main window.
            await Dispatcher.InvokeAsync(static () => { }, DispatcherPriority.ContextIdle).Task;
        }
        MainWindow window;
        try { window = new MainWindow(e.Args.Contains("--background")) { ShowActivated = !e.Args.Contains("--background") }; }
        catch (Exception ex)
        {
            if (splash?.IsVisible == true) splash.Close();
            try
            {
                var folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DeckPilot");
                System.IO.Directory.CreateDirectory(folder);
                System.IO.File.WriteAllText(System.IO.Path.Combine(folder, "startup-error.log"), ex.ToString());
            }
            catch { }
            throw;
        }
        MainWindow = window;
        if (splash != null)
        {
            splash.SetReducedMotion(window.ReducedMotion);
            window.ContentRendered += async (_, _) => await splash.CloseWhenReadyAsync();
        }
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
