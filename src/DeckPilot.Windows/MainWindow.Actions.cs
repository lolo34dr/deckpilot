using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private readonly DispatcherTimer searchTimer = new() { Interval = TimeSpan.FromMilliseconds(180) };
    private IReadOnlyList<InstalledApplication> actionApplications = [];
    private bool actionToolsReady, applicationsLoading, applicationsLoaded;
    private string applicationSearchWarning = "";
    private long ignoreClickUntil;
    private const string ActionDragFormat = "DeckPilot.PrivateAction";
    private sealed record ActionDragPayload(ButtonAction Action, Guid? Profile = null, int Source = -1);

    private void InitializeActionTools()
    {
        actionToolsReady = true;
        searchTimer.Tick += (_, _) => { searchTimer.Stop(); RefreshActionSearch(); };
        Closed += (_, _) => searchTimer.Stop();
        for (int i = 0; i < actionButtons.Count; i++)
        {
            var kind = (ActionKind)i;
            WireDragSource(actionButtons[i], () => new(new() { Kind = kind, Key = kind is ActionKind.Media or ActionKind.Spotify ? 0xB3 : 0x41 }));
        }
        for (int i = 0; i < deckButtons.Count; i++)
        {
            int index = i;
            var key = deckButtons[i];
            key.AllowDrop = true;
            WireDragSource(key, () => SaveEditor() ? new(settings.ActiveProfile.Buttons[index].Copy(), settings.ActiveProfile.Id, index) : null);
            key.DragOver += (_, e) =>
            {
                bool allowed = !calibrating && !firmwareOpen && CanDropAction(e.Data);
                e.Effects = allowed ? e.Data.GetData(ActionDragFormat, false) is ActionDragPayload { Source: >= 0 } ? DragDropEffects.Move : DragDropEffects.Copy : DragDropEffects.None;
                key.IsDropTarget = allowed; e.Handled = true;
            };
            key.DragLeave += (_, _) => key.IsDropTarget = false;
            key.Drop += (_, e) =>
            {
                key.IsDropTarget = false; e.Handled = true;
                if (calibrating || firmwareOpen) return;
                try
                {
                    if (e.Data.GetData(ActionDragFormat, false) is ActionDragPayload payload)
                    {
                        if (!SaveEditor()) return;
                        if (payload.Source >= 0)
                        {
                            if (payload.Profile != settings.ActiveProfile.Id || payload.Source >= 12 || payload.Source == index) return;
                            var buttons = settings.ActiveProfile.Buttons;
                            (buttons[index], buttons[payload.Source]) = (buttons[payload.Source], buttons[index]);
                            ResetActionFeedback(payload.Source); ResetActionFeedback(index);
                            editingLong = false; selected = index; LoadEditor(); Persist();
                            StatusText.Text = "Actions échangées · Le calibrage des touches ne change pas.";
                        }
                        else AssignAction(index, payload.Action);
                    }
                    else if (ExternalAction(e.Data) is { } action) AssignAction(index, action);
                    else StatusText.Text = "Déposez une action, une application, un raccourci, un dossier ou une adresse https://.";
                }
                catch (Exception ex) { ReportError("Glisser-déposer : " + ex.Message); }
            };
        }
    }
    private void ResetActionFeedback(int index)
    {
        gestures.CancelIndex(index); executionTickets[index]++; feedback[index] = DeckKeyFeedback.Ready;
        feedbackMessages[index] = ""; feedbackUntil[index] = pressedUntil[index] = 0;
    }
    private void AssignAction(int index, ButtonAction action, bool currentGesture = false)
    {
        if (calibrating || firmwareOpen || !SaveEditor()) return;
        if (currentGesture && editingLong && index == selected)
        {
            if (action.Kind == ActionKind.Passthrough) { ReportError("Choisissez une action configurée pour l’appui long."); return; }
            settings.ActiveProfile.Buttons[index].LongPressAction = action.CopyBasic();
        }
        else { settings.ActiveProfile.Buttons[index] = action.Copy(); editingLong = false; }
        ResetActionFeedback(index);
        selected = index; LoadEditor(); Persist();
        StatusText.Text = "Action affectée · Complétez ses réglages si nécessaire. Rien n’a été exécuté.";
    }
    private void WireDragSource(FrameworkElement element, Func<ActionDragPayload?> getAction)
    {
        Point origin = default;
        bool armed = false;
        element.PreviewMouseLeftButtonDown += (_, e) => { origin = e.GetPosition(element); armed = true; };
        element.PreviewMouseLeftButtonUp += (_, _) => armed = false;
        element.PreviewMouseMove += (_, e) =>
        {
            if (!armed || e.LeftButton != MouseButtonState.Pressed || calibrating || firmwareOpen) return;
            var current = e.GetPosition(element);
            if (Math.Abs(current.X - origin.X) < SystemParameters.MinimumHorizontalDragDistance && Math.Abs(current.Y - origin.Y) < SystemParameters.MinimumVerticalDragDistance) return;
            if (getAction() is not { } payload) return;
            armed = false;
            var data = new DataObject(); data.SetData(ActionDragFormat, payload);
            try { DragDrop.DoDragDrop(element, data, payload.Source >= 0 ? DragDropEffects.Move : DragDropEffects.Copy); }
            finally
            {
                ignoreClickUntil = Environment.TickCount64 + 250;
                foreach (var key in deckButtons) key.IsDropTarget = false;
            }
        };
    }
    private static bool CanDropAction(IDataObject data)
    {
        if (data.GetData(ActionDragFormat, false) is ActionDragPayload) return true;
        if (data.GetData(DataFormats.FileDrop) is string[] { Length: 1 } paths)
            return Path.GetExtension(paths[0]).ToLowerInvariant() is ".exe" or ".lnk" or ".appref-ms" || Directory.Exists(paths[0]);
        return data.GetData(DataFormats.UnicodeText) is string text && Uri.TryCreate(text.Trim(), UriKind.Absolute, out var uri) && uri.Scheme is "http" or "https";
    }
    private static ButtonAction? ExternalAction(IDataObject data)
    {
        if (data.GetData(DataFormats.FileDrop) is string[] { Length: 1 } paths)
        {
            string path = paths[0]; bool folder = Directory.Exists(path);
            if (!folder && (!File.Exists(path) || Path.GetExtension(path).ToLowerInvariant() is not (".exe" or ".lnk" or ".appref-ms"))) return null;
            string label = folder ? new DirectoryInfo(path).Name : Path.GetFileNameWithoutExtension(path);
            return new() { Kind = folder ? ActionKind.Folder : ActionKind.Application, Label = label[..Math.Min(50, label.Length)], Target = path };
        }
        if (data.GetData(DataFormats.UnicodeText) is string text && Uri.TryCreate(text.Trim(), UriKind.Absolute, out var uri) && uri.Scheme is "http" or "https")
            return new() { Kind = ActionKind.Website, Label = uri.Host[..Math.Min(50, uri.Host.Length)], Target = uri.AbsoluteUri };
        return null;
    }
    private void ActionSearchChanged(object sender, TextChangedEventArgs e)
    {
        if (!actionToolsReady) return;
        searchTimer.Stop(); searchTimer.Start();
        if (!string.IsNullOrWhiteSpace(ActionSearchBox.Text) && !applicationsLoading && !applicationsLoaded) _ = LoadActionApplicationsAsync();
    }
    private async Task LoadActionApplicationsAsync()
    {
        applicationsLoading = true; RefreshActionSearch();
        try
        {
            var result = await ApplicationCatalog.FindAsync(lifetime.Token);
            actionApplications = result.Applications; applicationSearchWarning = result.Warning ?? "";
        }
        catch (OperationCanceledException) { }
        catch (Exception) { applicationSearchWarning = "Catalogue des applications indisponible. Le choix manuel reste possible."; }
        finally
        {
            applicationsLoading = false; applicationsLoaded = true;
            if (!lifetime.IsCancellationRequested) RefreshActionSearch();
        }
    }
    private void RefreshActionSearch()
    {
        string query = ActionSearchBox.Text.Trim(); bool searching = query.Length > 0;
        ActionCatalog.Visibility = searching ? Visibility.Collapsed : Visibility.Visible;
        ActionSearchResults.Visibility = searching ? Visibility.Visible : Visibility.Collapsed;
        ActionSearchResults.Children.Clear();
        if (!searching) { ActionSearchStatus.Text = "Actions ou applications · Glissez-les sur une touche."; return; }
        var results = ActionSearch.Find(query, actionApplications);
        foreach (var preset in results)
        {
            var content = new Grid();
            content.ColumnDefinitions.Add(new() { Width = new GridLength(26) }); content.ColumnDefinitions.Add(new());
            content.Children.Add(new Image { Width = 18, Height = 18, VerticalAlignment = VerticalAlignment.Center, Source = preset.Action.Kind == ActionKind.Application ? ApplicationIcons.ForTarget(preset.Action.Target) : VectorIcons.ForKind(preset.Action.Kind) });
            var text = new TextBlock { Text = preset.Name, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(text, 1); content.Children.Add(text);
            var button = new Button { Content = content, Padding = new Thickness(8, 9, 8, 9), Margin = new Thickness(0, 0, 0, 4), HorizontalContentAlignment = HorizontalAlignment.Stretch, ToolTip = preset.Details };
            System.Windows.Automation.AutomationProperties.SetName(button, preset.Name);
            button.Click += (_, _) => { if (Environment.TickCount64 >= ignoreClickUntil) AssignAction(selected, preset.Action, true); };
            WireDragSource(button, () => new(preset.Action.Copy())); ActionSearchResults.Children.Add(button);
        }
        ActionSearchStatus.Text = $"{results.Count} résultat(s)" + (applicationsLoading ? " · Recherche des applications…" : applicationSearchWarning.Length > 0 ? " · " + applicationSearchWarning : " · Cliquez ou glissez sur une touche.");
    }
    private void RefreshStartupState()
    {
        try { StartupCheck.IsChecked = StartupService.Enabled; StartupStatus.Text = StartupService.Status; }
        catch (Exception ex) { StartupCheck.IsChecked = false; StartupStatus.Text = "État du démarrage inaccessible : " + ex.Message; }
    }
}
