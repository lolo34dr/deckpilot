using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using DeckPilot.Core;

namespace DeckPilot.Windows;
public partial class ApplicationPicker : Window
{
    private readonly CancellationTokenSource lifetime = new();
    private string? warning;
    private ListCollectionView? view;
    private string query = "";
    private readonly DispatcherTimer searchTimer = new() { Interval = TimeSpan.FromMilliseconds(120) };
    public InstalledApplication? SelectedApplication { get; private set; }
    public ApplicationPicker()
    {
        InitializeComponent(); DarkChrome.Attach(this);
        Loaded += async (_, _) => { SearchBox.Focus(); Motion.Reveal(this); await LoadApplications(); };
        searchTimer.Tick += (_, _) => ApplySearch();
        Closed += (_, _) => { searchTimer.Stop(); lifetime.Cancel(); lifetime.Dispose(); };
    }
    private async Task LoadApplications(bool forceRefresh = false)
    {
        RefreshButton.IsEnabled = false; ResultsText.Text = "Recherche des applications installées…";
        try
        {
            var result = await ApplicationCatalog.FindAsync(lifetime.Token, forceRefresh);
            if (lifetime.IsCancellationRequested) return;
            string? selectedTarget = (AppList.SelectedItem as InstalledApplication)?.Target;
            searchTimer.Stop(); query = SearchBox.Text.Trim();
            warning = result.Warning;
            // A cached list's default view would share filters between windows
            // and retain the old picker's SearchBox through its filter delegate.
            view = new ListCollectionView(result.Applications.ToArray());
            view.Filter = item => item is InstalledApplication app && app.Matches(query);
            AppList.ItemsSource = view;
            AppList.SelectedItem = view.Cast<InstalledApplication>().FirstOrDefault(app => app.Target == selectedTarget);
            UpdateCount();
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { if (!lifetime.IsCancellationRequested) ResultsText.Text = "Recherche indisponible : " + ex.Message; }
        finally { if (!lifetime.IsCancellationRequested) RefreshButton.IsEnabled = true; }
    }
    private void SearchChanged(object sender, TextChangedEventArgs e)
    {
        searchTimer.Stop();
        if (view != null) searchTimer.Start();
    }
    private void ApplySearch()
    {
        searchTimer.Stop();
        string next = SearchBox.Text.Trim();
        if (next != query) { query = next; view?.Refresh(); }
        if (view != null) UpdateCount();
    }
    private void UpdateCount()
    {
        int count = view?.Count ?? 0;
        ResultsText.Text = (count == 0 ? "Aucun résultat. Essayez un autre nom ou utilisez le choix manuel." : $"{count} application(s)") + (warning == null ? "" : " · " + warning);
    }
    private void SelectionChanged(object sender, SelectionChangedEventArgs e) => ChooseButton.IsEnabled = AppList.SelectedItem is InstalledApplication;
    private void ChooseClick(object sender, RoutedEventArgs e)
    {
        // Enter/double-click must apply a pending search before accepting a row.
        ApplySearch();
        if (AppList.SelectedItem is not InstalledApplication app || view?.Contains(app) != true) return;
        SelectedApplication = app; DialogResult = true;
    }
    private void AppDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ItemsControl.ContainerFromElement(AppList, e.OriginalSource as DependencyObject) is ListBoxItem) ChooseClick(sender, e);
    }
    private async void RefreshClick(object sender, RoutedEventArgs e) => await LoadApplications(forceRefresh: true);
}
