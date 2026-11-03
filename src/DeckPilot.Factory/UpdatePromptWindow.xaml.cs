using System.ComponentModel;
using System.IO;
using System.Windows;
using DeckPilot.Core;

namespace DeckPilot.Factory;

public partial class UpdatePromptWindow : Window
{
    private readonly FactoryUpdateService service;
    private readonly FactoryUpdate update;
    private readonly Version currentApp, currentFirmware;
    private CancellationTokenSource? download;
    private bool launching;

    public UpdatePromptWindow(FactoryUpdateService service, FactoryUpdate update, Version currentApp, Version currentFirmware)
    {
        InitializeComponent(); this.service = service; this.update = update; this.currentApp = currentApp; this.currentFirmware = currentFirmware;
        ChangesText.Text = update.Description(currentApp, currentFirmware);
        SizeText.Text = $"Téléchargement vérifié par SHA-256 · {update.Size / 1024d / 1024d:0.#} Mo";
        Closing += OnClosing;
    }

    private async void Install_Click(object sender, RoutedEventArgs e)
    {
        if (download != null) return;
        download = new CancellationTokenSource(); InstallButton.IsEnabled = false; LaterButton.Content = "Annuler";
        DownloadProgress.Visibility = Visibility.Visible; ErrorText.Text = ""; DownloadStatus.Text = "Téléchargement de l’installeur sécurisé…";
        try
        {
            var progress = new Progress<double>(value => { DownloadProgress.Value = value * 100; DownloadStatus.Text = $"Téléchargement · {value:P0}"; });
            var staged = await service.DownloadAsync(update, progress, download.Token);
            download.Dispose(); download = null;
            DownloadStatus.Text = "Téléchargement vérifié. Ouverture de l’installeur Windows…";
            FactoryUpdateService.LaunchInstaller(staged).Dispose();
            launching = true; DialogResult = true;
        }
        catch (OperationCanceledException) { DownloadStatus.Text = "Téléchargement annulé. Atelier reste ouvert."; }
        catch (Exception ex) { ErrorText.Text = ex.Message; DownloadStatus.Text = "La mise à jour n’a pas été lancée."; }
        finally
        {
            download?.Dispose(); download = null;
            if (!launching) { InstallButton.IsEnabled = true; LaterButton.Content = "Plus tard"; }
        }
    }

    private void Later_Click(object sender, RoutedEventArgs e)
    {
        if (download != null) { download.Cancel(); return; }
        DialogResult = false;
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (download == null) return;
        e.Cancel = true; download.Cancel();
    }
}
