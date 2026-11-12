using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private void InitializeStreamingEditor()
    {
        StreamingProviderPicker.ItemsSource = new[] { "OBS Studio", "Streamlabs Desktop" };
    }

    private string StreamingProviderId => StreamingProviderPicker.SelectedIndex == 1 ? "streamlabs" : "obs";

    private void LoadStreamingEditor(ButtonAction action)
    {
        string provider = action.ApplicationName is "obs" or "streamlabs" ? action.ApplicationName : "obs";
        StreamingProviderPicker.SelectedIndex = provider == "streamlabs" ? 1 : 0;
        ConfigureStreamingProvider(provider, action.Target);
        StreamingTargetPicker.Text = action.Arguments;
    }

    private void ConfigureStreamingProvider(string provider, string? selectedCommand)
    {
        StreamingCommandPicker.ItemsSource = StreamingActionCatalog.For(provider);
        StreamingCommandPicker.SelectedItem = StreamingActionCatalog.Find(provider, selectedCommand ?? "") ?? StreamingActionCatalog.For(provider).FirstOrDefault();
        var connection = settings.Streaming;
        bool streamlabs = provider == "streamlabs";
        StreamingConnectionTitle.Text = streamlabs ? "Connexion Streamlabs Desktop" : "Connexion OBS Studio";
        StreamingHostEditor.Text = streamlabs ? connection.StreamlabsHost : connection.ObsHost;
        StreamingPortEditor.Text = (streamlabs ? connection.StreamlabsPort : connection.ObsPort).ToString(CultureInfo.InvariantCulture);
        StreamingSecretLabel.Text = streamlabs ? "Jeton Remote Control" : "Mot de passe WebSocket";
        StreamingSecretEditor.Clear();
        bool hasSecret = !string.IsNullOrEmpty(streamlabs ? connection.StreamlabsTokenProtected : connection.ObsPasswordProtected);
        StreamingSecretStatus.Text = hasSecret ? "Un secret est déjà protégé sur ce PC. Saisissez-en un nouveau pour le remplacer." : "Aucun secret enregistré pour cette application.";
        StreamingConnectionHelp.Text = streamlabs
            ? "Dans Streamlabs Desktop : Paramètres > Remote Control > afficher les détails, puis copiez le jeton."
            : "Dans OBS : Outils > Paramètres du serveur WebSocket. Activez le serveur et définissez un mot de passe.";
        StreamingTargetLabel.Text = StreamingCommandPicker.SelectedItem is StreamingCommand command && command.Id == "scene" ? "Scène à activer" : "Nom exact de la scène, de la source ou de l’entrée audio";
        StreamingTargetPicker.IsEnabled = StreamingCommandPicker.SelectedItem is StreamingCommand selected && selected.NeedsTarget;
    }

    private void StreamingProviderChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || StreamingProviderPicker.SelectedItem == null) return;
        ConfigureStreamingProvider(StreamingProviderId, null);
        StreamingTargetPicker.Text = "";
    }

    private void StreamingCommandChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loading || StreamingProviderPicker.SelectedItem == null) return;
        if (StreamingCommandPicker.SelectedItem is not StreamingCommand command) return;
        StreamingTargetLabel.Text = command.Id == "scene" ? "Scène à activer" : "Nom exact de la scène, de la source ou de l’entrée audio";
        StreamingTargetPicker.IsEnabled = command.NeedsTarget;
        if (!command.NeedsTarget) StreamingTargetPicker.Text = "";
    }

    private bool CaptureStreamingConnection(ButtonAction action, out StreamingConnections previous)
    {
        previous = settings.Streaming;
        string provider = action.ApplicationName;
        bool streamlabs = provider == "streamlabs";
        if (provider is not ("obs" or "streamlabs")) return false;
        if (!int.TryParse(StreamingPortEditor.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out int port) || port is < 1 or > 65535)
        {
            ReportError("Saisissez un port valide entre 1 et 65535."); return false;
        }
        string host = StreamingHostEditor.Text.Trim();
        var current = settings.Streaming;
        var updated = new StreamingConnections
        {
            ObsHost = current.ObsHost,
            ObsPort = current.ObsPort,
            ObsPasswordProtected = current.ObsPasswordProtected,
            StreamlabsHost = current.StreamlabsHost,
            StreamlabsPort = current.StreamlabsPort,
            StreamlabsTokenProtected = current.StreamlabsTokenProtected
        };
        try
        {
            if (streamlabs)
            {
                updated.StreamlabsHost = host;
                updated.StreamlabsPort = port;
                if (!string.IsNullOrEmpty(StreamingSecretEditor.Password)) updated.StreamlabsTokenProtected = StreamingSecretProtection.Protect(StreamingSecretEditor.Password);
            }
            else
            {
                updated.ObsHost = host;
                updated.ObsPort = port;
                if (!string.IsNullOrEmpty(StreamingSecretEditor.Password)) updated.ObsPasswordProtected = StreamingSecretProtection.Protect(StreamingSecretEditor.Password);
            }
        }
        catch (CryptographicException) { ReportError("Windows n’a pas pu protéger le secret. Vérifiez la session Windows puis réessayez."); return false; }
        if (!updated.IsValid())
        {
            ReportError("Saisissez un nom d’hôte valide et un port entre 1 et 65535."); return false;
        }
        settings.Streaming = updated;
        return true;
    }

    private async void RefreshStreamingTargetsClick(object sender, RoutedEventArgs e)
    {
        if (!SaveEditor()) return;
        string provider = StreamingProviderId;
        string existing = StreamingTargetPicker.Text;
        StreamingStatus.Text = "Connexion et récupération des scènes…";
        RefreshStreamingTargetsButton.IsEnabled = false;
        try
        {
            var scenes = await StreamingBridge.GetScenesAsync(provider, settings.Streaming, lifetime.Token);
            StreamingTargetPicker.ItemsSource = scenes;
            StreamingTargetPicker.Text = existing;
            StreamingStatus.Text = scenes.Count == 0 ? "Connexion réussie, mais aucune scène n’a été trouvée." : $"Connexion réussie · {scenes.Count} scène(s) trouvée(s).";
        }
        catch (Exception ex)
        {
            if (!lifetime.IsCancellationRequested) StreamingStatus.Text = "Connexion impossible · " + ex.Message;
        }
        finally { RefreshStreamingTargetsButton.IsEnabled = true; }
    }

    private void ClearStreamingSecretClick(object sender, RoutedEventArgs e)
    {
        if (!SaveEditor()) return;
        var previous = settings.Streaming;
        if (StreamingProviderId == "streamlabs") settings.Streaming.StreamlabsTokenProtected = "";
        else settings.Streaming.ObsPasswordProtected = "";
        StreamingSecretEditor.Clear();
        StreamingSecretStatus.Text = "Le secret enregistré a été effacé.";
        if (!Persist())
        {
            settings.Streaming = previous;
            ConfigureStreamingProvider(StreamingProviderId, (StreamingCommandPicker.SelectedItem as StreamingCommand)?.Id);
        }
    }
}
