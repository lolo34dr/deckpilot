using System;
using System.Linq;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public partial class ApplicationPicker : Window
{
    private readonly CancellationTokenSource lifetime = new();
    private string? warning;
    private ICollectionView? view;
    public InstalledApplication? SelectedApplication { get; private set; }
    public ApplicationPicker()
    {
        InitializeComponent(); DarkChrome.Attach(this);
        Loaded += async (_, _) => { SearchBox.Focus(); Motion.Reveal(this); await LoadApplications(); };
        Closed += (_, _) => lifetime.Cancel();
    }
    private async Task LoadApplications()
    {
        RefreshButton.IsEnabled = false; ResultsText.Text = "Recherche des applications installées…";
        try
        {
            var result = await ApplicationCatalog.FindAsync(lifetime.Token);
            warning = result.Warning;
            view = CollectionViewSource.GetDefaultView(result.Applications);
            view.Filter = item => item is InstalledApplication app && app.Matches(SearchBox.Text);
            AppList.ItemsSource = view; UpdateCount();
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { if (!lifetime.IsCancellationRequested) ResultsText.Text = "Recherche indisponible : " + ex.Message; }
        finally { if (!lifetime.IsCancellationRequested) RefreshButton.IsEnabled = true; }
    }
    private void SearchChanged(object sender, TextChangedEventArgs e) { view?.Refresh(); if (view != null) UpdateCount(); }
    private void UpdateCount()
    {
        int count = view?.Cast<object>().Count() ?? 0;
        ResultsText.Text = (count == 0 ? "Aucun résultat. Essayez un autre nom ou utilisez le choix manuel." : $"{count} application(s)") + (warning == null ? "" : " · " + warning);
    }
    private void SelectionChanged(object sender, SelectionChangedEventArgs e) => ChooseButton.IsEnabled = AppList.SelectedItem is InstalledApplication;
    private void ChooseClick(object sender, RoutedEventArgs e)
    {
        if (AppList.SelectedItem is not InstalledApplication app) return;
        SelectedApplication = app; DialogResult = true;
    }
    private void AppDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ItemsControl.ContainerFromElement(AppList, e.OriginalSource as DependencyObject) is ListBoxItem) ChooseClick(sender, e);
    }
    private async void RefreshClick(object sender, RoutedEventArgs e) => await LoadApplications();
}
