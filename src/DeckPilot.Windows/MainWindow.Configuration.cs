using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private bool? compactConfiguration;

    private void ConfigurationWorkspaceSizeChanged(object sender, SizeChangedEventArgs e)
    {
        bool compact = e.NewSize.Width < 1008;
        if (compactConfiguration == compact) return;
        compactConfiguration = compact;
        PanelWorkspaceColumn.Width = new GridLength(compact ? 0 : 440);
        ConfigurationSplitter.Visibility = compact ? Visibility.Visible : Visibility.Collapsed;
        PanelWorkspaceRow.Height = new GridLength(compact ? 8 : 0);
        UpdateConfigurationRows();

        Grid.SetColumnSpan(DeckEditorScroll, compact ? 2 : 1);
        Grid.SetColumnSpan(BoardOverviewScroll, compact ? 2 : 1);
        Grid.SetRow(ConfigurationPanel, compact ? 2 : 0);
        Grid.SetColumn(ConfigurationPanel, compact ? 0 : 1);
        Grid.SetColumnSpan(ConfigurationPanel, compact ? 2 : 1);
        ConfigurationPanel.BorderThickness = compact ? new Thickness(0, 1, 0, 0) : new Thickness(1, 0, 0, 0);
    }

    private void ConfigurationTabChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.Source != ControlTabs) return;
        if (compactConfiguration == true) UpdateConfigurationRows();
        UpdateBoardWorkspace();
        if (ControlTabs.SelectedItem == DeviceTab) RefreshBoardCards();
    }

    private void UpdateConfigurationRows()
    {
        bool compact = compactConfiguration == true;
        bool actions = ControlTabs.SelectedItem == ActionsTab;
        DeckWorkspaceRow.Height = new GridLength(compact && actions ? 3 : 1, GridUnitType.Star);
        ConfigurationBottomRow.Height = compact ? new GridLength(2, GridUnitType.Star) : new GridLength(0);
    }

    private void ShowDeviceClick(object sender, RoutedEventArgs e)
    {
        ControlTabs.SelectedItem = DeviceTab;
        DeviceTab.Focus();
    }

    private void ShowKeyEditorClick(object sender, RoutedEventArgs e)
    {
        LabelEditor.BringIntoView();
        LabelEditor.Focus();
    }

    private void InitializeBuildMode()
    {
        if (!BetaEnvironment.IsEnabled) return;

        Title = BetaEnvironment.DisplayName;
        StartupCheck.IsEnabled = false;
        AutoUpdateCheck.IsEnabled = false;
        UpdateCheckButton.IsEnabled = false;
        AutoUpdateCheck.IsChecked = false;
        StartupStatus.Text = "Le démarrage avec Windows est désactivé pour cette bêta.";
        UpdateStatus.Text = "Cette bêta locale ne recherche pas de mises à jour.";
        var version = typeof(MainWindow).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0];
        const string marker = "-hybrid-beta.";
        var markerIndex = version?.IndexOf(marker, StringComparison.Ordinal) ?? -1;
        VersionLabel.Text += markerIndex >= 0 ? $" · Bêta {version![(markerIndex + marker.Length)..]}" : " · Bêta";
    }
}
