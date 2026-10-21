using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public partial class FirmwareWindow : Window
{
    private PreparedFirmware? package;
    private bool busy;
    private bool discovering;
    private bool identifying;
    private int selectionRevision;
    public FirmwareWindow()
    {
        InitializeComponent();
        discovering = true;
        Loaded += async (_, _) =>
        {
            try
            {
                package = await Task.Run(FirmwareService.Prepare);
                PackageStatus.Text = $"Firmware inclus : {package.Manifest.Version}. Fourni avec cette version de DeckPilot, sans téléchargement supplémentaire.";
                await RefreshAsync();
            }
            catch (Exception ex) { PackageStatus.Text = ex.Message; }
            finally { discovering = false; UpdateInstallButton(); }
        };
        Closing += (_, e) =>
        {
            if (busy || discovering || identifying) { e.Cancel = true; return; }
            selectionRevision++;
            package?.Dispose();
        };
    }
    private async Task RefreshAsync()
    {
        discovering = true; RefreshButton.IsEnabled = false; Devices.IsEnabled = false;
        InstallButton.IsEnabled = false; DeviceStatus.Text = "Recherche des cartes Leonardo…";
        selectionRevision++;
        try
        {
            var devices = await Task.Run(FirmwareService.Discover);
            Devices.ItemsSource = devices.FindAll(d => !d.Bootloader);
            Devices.SelectedIndex = -1;
            DeviceStatus.Text = devices.Count == 0 ? "Aucune carte compatible détectée. Vérifiez le câble USB et le pilote Windows." : "Sélectionnez votre macropad. Les identifiants Leonardo sont partagés par d’autres cartes.";
        }
        catch (Exception ex) { DeviceStatus.Text = "Détection impossible : " + ex.Message; }
        finally { discovering = false; RefreshButton.IsEnabled = true; Devices.IsEnabled = true; UpdateInstallButton(); }
    }
    private async void RefreshClick(object sender, RoutedEventArgs e) => await RefreshAsync();
    private async void DeviceChanged(object sender, SelectionChangedEventArgs e)
    {
        int revision = ++selectionRevision;
        BoardConfirmed.IsChecked = false;
        UpdateInstallButton();
        if (Devices.SelectedItem is not FirmwareDevice device) return;
        identifying = true; Devices.IsEnabled = RefreshButton.IsEnabled = false; UpdateInstallButton();
        DeviceStatus.Text = "Lecture de l’identité du firmware…";
        var identity = await Task.Run(() => FirmwareService.Identify(device));
        if (revision != selectionRevision) return;
        identifying = false; Devices.IsEnabled = RefreshButton.IsEnabled = true;
        DeviceStatus.Text = identity != null ? $"DeckPilot identifié · firmware {identity.Version} · {device.Port}" : $"Firmware d’origine ou non identifié · {device.Port}. Une première installation remplacera son programme.";
        if (device.Location.Length == 0) DeviceStatus.Text += " Connexion physique non identifiable : installation bloquée.";
        UpdateInstallButton();
    }
    private void ConfirmationChanged(object sender, RoutedEventArgs e) => UpdateInstallButton();
    private void UpdateInstallButton()
    {
        InstallButton.IsEnabled = !busy && !discovering && !identifying && package != null && BoardConfirmed.IsChecked == true && Devices.SelectedItem is FirmwareDevice { Location.Length: > 0 };
    }
    private async void InstallClick(object sender, RoutedEventArgs e)
    {
        if (package == null || Devices.SelectedItem is not FirmwareDevice device) return;
        if (MessageBox.Show(this, $"Installer DeckPilot {package.Manifest.Version} sur {device.Port} ?\n\nLe programme actuel de cette carte sera remplacé. Ne débranchez pas le câble pendant l’opération. Les profils enregistrés sur le PC sont conservés.", "Confirmer le flash", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes) return;
        busy = true; Devices.IsEnabled = RefreshButton.IsEnabled = CloseButton.IsEnabled = BoardConfirmed.IsEnabled = false;
        UpdateInstallButton(); Progress.IsIndeterminate = true;
        try
        {
            await FirmwareService.FlashAsync(device, package, new Progress<string>(text => ProgressText.Text = text));
            BoardConfirmed.IsChecked = false;
        }
        catch (Exception ex)
        {
            ProgressText.Text = ex.Message + "\n\nEn cas de carte non reconnue : utilisez l’IDE Arduino avec la carte Leonardo et le programme d’origine. Un double contact bref RST–GND peut ouvrir le bootloader ; lancez alors le téléversement. Ne reliez jamais VCC à GND.";
        }
        finally
        {
            busy = false; Progress.IsIndeterminate = false;
            Devices.IsEnabled = RefreshButton.IsEnabled = CloseButton.IsEnabled = BoardConfirmed.IsEnabled = true;
            UpdateInstallButton();
        }
    }
    private void CloseClick(object sender, RoutedEventArgs e) => Close();
}
