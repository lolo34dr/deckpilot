using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public partial class UpdateWindow : Window
{
    private readonly UpdateService service;
    private readonly UpdateRelease release;
    private CancellationTokenSource? download;
    public StagedUpdate? Staged { get; private set; }
    public UpdateWindow(UpdateService service, UpdateRelease release)
    {
        InitializeComponent(); DarkChrome.Attach(this); this.service = service; this.release = release;
        VersionText.Text = $"Version {UpdateService.DisplayVersion} → {release.Version} · {release.Size / 1024d / 1024d:0.#} Mo";
        Closing += OnClosing;
        Loaded += (_, _) => Motion.Reveal(this);
    }
    private async void InstallClick(object sender, RoutedEventArgs e)
    {
        download = new CancellationTokenSource();
        InstallButton.IsEnabled = false; LaterButton.Content = "Annuler";
        ErrorText.Text = ""; DownloadProgress.Visibility = Visibility.Visible;
        DownloadStatus.Text = "Téléchargement de la mise à jour…";
        try
        {
            var progress = new Progress<double>(value => { DownloadProgress.Value = value * 100; DownloadStatus.Text = $"Téléchargement · {value:P0}"; });
            Staged = await service.StageAsync(release, progress, download.Token);
            DownloadStatus.Text = "Téléchargement vérifié. Préparation du redémarrage…";
            download.Dispose(); download = null;
            DialogResult = true;
        }
        catch (OperationCanceledException) { DownloadStatus.Text = "Téléchargement annulé. Aucun fichier installé n’a été modifié."; }
        catch (Exception ex) { ErrorText.Text = ex.Message; DownloadStatus.Text = "La mise à jour n’a pas été installée."; }
        finally
        {
            download?.Dispose(); download = null;
            InstallButton.IsEnabled = true; LaterButton.Content = "Plus tard";
        }
    }
    private void LaterClick(object sender, RoutedEventArgs e)
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
