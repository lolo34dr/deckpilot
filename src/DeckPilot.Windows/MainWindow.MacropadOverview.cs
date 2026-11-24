using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Threading;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private readonly Dictionary<string, MacropadCard> boardCards = new(StringComparer.OrdinalIgnoreCase);
    private string? editingBoardId;
    private Guid? editingProfileId;
    private bool boardOverviewVisible;
    private readonly Dictionary<string, Button> boardEditorButtons = new(StringComparer.OrdinalIgnoreCase);
    private Profile EditorProfile => editingProfileId is { } id
        ? settings.Profiles.FirstOrDefault(profile => profile.Id == id) ?? settings.ActiveProfile
        : settings.ActiveProfile;

    private void UseKeyboardProfileInEditor()
    {
        editingBoardId = null;
        editingProfileId = null;
    }

    private void ShowKeyboardProfilesClick(object sender, RoutedEventArgs e)
    {
        if (!SaveEditor()) return;
        UseKeyboardProfileInEditor();
        editingLong = false;
        RefreshProfiles();
        LoadEditor();
        ProfilePicker.Focus();
    }

    private void RefreshBoardCards()
    {
        if (BoardCardsPanel == null) return;
        var boards = boardState?.Boards ?? [];
        foreach (var retired in boardCards.Keys.Except(boards.Select(board => board.Device.DeviceId), StringComparer.OrdinalIgnoreCase).ToArray())
        {
            BoardCardsPanel.Children.Remove(boardCards[retired]);
            boardCards.Remove(retired);
        }
        foreach (var board in boards)
        {
            string id = board.Device.DeviceId;
            if (!boardCards.TryGetValue(id, out var card))
            {
                card = new MacropadCard(board) { Margin = new Thickness(6, 0, 6, 12) };
                card.ConfigureRequested += ConfigureBoard;
                card.CalibrateRequested += board => BeginCalibration(board.Device.DeviceId);
                card.OptionsRequested += board =>
                {
                    if (!SaveEditor()) return;
                    SelectBoardDetails(board.Device.DeviceId);
                    BoardSettingsExpander.IsExpanded = true;
                    BoardSettingsExpander.BringIntoView();
                    BoardSettingsExpander.Focus();
                };
                card.FirmwareRequested += async board => await OpenFirmwareAsync(board.Device.DeviceId);
                boardCards.Add(id, card);
                BoardCardsPanel.Children.Add(card);
            }
            var binding = settings.BoardBindings.GetValueOrDefault(id);
            var profile = binding == null ? null : settings.Profiles.FirstOrDefault(profile => profile.Id == binding.ProfileId);
            card.Refresh(board, binding, profile, profile != null && BoardSetup.IsShared(settings, id, profile.Id),
                firmwareOpen, string.Equals(editingBoardId, id, StringComparison.OrdinalIgnoreCase), selected,
                BoardDisplayName(board), AccentBrushFor);
        }
        UpdateBoardWorkspace();
        RefreshBoardEditBanner();
    }

    private void UpdateBoardWorkspace()
    {
        if (BoardOverviewPanel == null || ControlTabs == null || ConfigurationPanel == null) return;
        bool hasBoards = boardCards.Count > 0;
        BoardOverviewPanel.Visibility = hasBoards ? Visibility.Visible : Visibility.Collapsed;
        bool overview = ControlTabs.SelectedItem == DeviceTab && hasBoards;
        if (overview != boardOverviewVisible)
        {
            boardOverviewVisible = overview;
            BoardSettingsExpander.IsExpanded = !overview;
        }
        DeckEditorScroll.Visibility = overview ? Visibility.Collapsed : Visibility.Visible;
        UpdateConfigurationLayout(overview);
        BoardOverviewCount.Text = boardCards.Count == 1 ? "1 appareil détecté" : $"{boardCards.Count} appareils détectés";
        BoardSettingsCard.MaxWidth = boardCards.Count == 1 ? 940 : 1450;
        BoardOverviewTitle.Text = boardCards.Count == 1 ? "Votre macropad" : "Vos macropads";
        BoardOverviewIntro.Text = boardCards.Count == 1
            ? "Choisissez une touche sur le macropad ci-dessous, puis l’action qu’elle doit lancer. Aucun réglage avancé n’est nécessaire pour commencer."
            : "Chaque macropad a ses propres touches et son profil. Choisissez d’abord l’appareil, puis cliquez sur la touche à personnaliser.";
        ResizeBoardCards(BoardOverviewPanel.ActualWidth);
    }

    private void BoardOverviewSizeChanged(object sender, SizeChangedEventArgs e) => ResizeBoardCards(e.NewSize.Width);

    private void ResizeBoardCards(double width)
    {
        if (BoardCardsPanel == null) return;
        BoardCardsPanel.Columns = Math.Min(Math.Max(boardCards.Count, 1), width >= 1230 ? 3 : width >= 750 ? 2 : 1);
        BoardCardsPanel.MaxWidth = BoardCardsPanel.Columns == 1 ? 940 : double.PositiveInfinity;
        BoardCardsPanel.HorizontalAlignment = BoardCardsPanel.Columns == 1 ? HorizontalAlignment.Center : HorizontalAlignment.Stretch;
        BoardCardsPanel.Width = BoardCardsPanel.Columns == 1 && width > 0 ? Math.Min(940, width) : double.NaN;
        bool wideCard = BoardCardsPanel.Columns == 1 && width >= 820;
        foreach (var card in boardCards.Values) card.SetWide(wideCard);
    }

    private string BoardDisplayName(BoardLinkState board)
    {
        var boards = boardState?.Boards;
        if (boards == null || boards.Count <= 1) return "Votre macropad";
        int index = boards.ToList().FindIndex(item => string.Equals(item.Device.DeviceId, board.Device.DeviceId, StringComparison.OrdinalIgnoreCase));
        return $"Macropad {Math.Max(index, 0) + 1}";
    }

    private BoardBinding? CurrentEditingBinding() =>
        editingBoardId == null ? null : settings.BoardBindings.GetValueOrDefault(editingBoardId);

    private BoardLinkState? CurrentEditingBoard() => editingBoardId == null ? null
        : boardState?.Boards.FirstOrDefault(board => string.Equals(board.Device.DeviceId, editingBoardId, StringComparison.OrdinalIgnoreCase));

    private string EditorKeyName(int index)
    {
        var map = CurrentEditingBinding()?.KeyMap ?? settings.KeyMap;
        return map.Count == 12 ? Calibration.KeyName(map[index]) : $"{index + 1:00}";
    }

    private void RefreshBoardEditBanner()
    {
        if (BoardEditBanner == null) return;
        bool editing = editingBoardId != null && editingProfileId == EditorProfile.Id;
        var board = CurrentEditingBoard();
        bool assigned = CurrentEditingBinding()?.ProfileId == EditorProfile.Id;
        var port = board?.Device.Port;
        BoardEditBanner.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;
        ActionBoardTarget.Visibility = BoardEditBanner.Visibility;
        if (editing)
        {
            BoardEditTitle.Text = port == null ? "Configurer les touches du macropad" : $"Configurer les touches · {BoardDisplayName(board!)}";
            BoardEditProfile.Text = $"Profil de touches · {EditorProfile.Name}";
            BoardEditStatus.Text = !assigned ? "Ce profil n’est plus celui utilisé par ce macropad."
                : port == null ? "Macropad débranché · Les modifications restent enregistrées sur ce PC."
                : $"Touche {selected + 1:00} sélectionnée · Choisissez une action dans la bibliothèque, puis revenez au macropad.";
            ActionBoardTarget.Text = port == null ? $"Étape 2 · Action de la touche {selected + 1:00}"
                : $"{port} · Étape 2 · Action de la touche {selected + 1:00}";
        }

        ProfilePickerLabel.Text = editing ? "Profil affiché" : "Profil actif";
        ProfilePicker.IsEnabled = !editing;
        ProfilePickerLabel.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
        ProfilePicker.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
        AutomationProperties.SetName(ProfilePicker, ProfilePickerLabel.Text);
        DeckSurfaceTitle.Text = editing && port != null ? $"APERÇU DES TOUCHES · {port}" : "MACROPAD / 3 × 4";
        RefreshBoardEditorSwitches();
    }

    private void RefreshBoardEditorSwitches()
    {
        var boards = boardState?.Boards ?? [];
        foreach (var retired in boardEditorButtons.Keys.Except(boards.Select(board => board.Device.DeviceId), StringComparer.OrdinalIgnoreCase).ToArray())
        {
            BoardEditorSwitches.Children.Remove(boardEditorButtons[retired]);
            boardEditorButtons.Remove(retired);
        }
        foreach (var board in boards)
        {
            var device = board.Device;
            if (!boardEditorButtons.TryGetValue(device.DeviceId, out var button))
            {
                button = new Button { MinHeight = 36, Padding = new Thickness(10, 4, 10, 4) };
                button.Click += (_, _) =>
                {
                    var current = boardState?.Boards.FirstOrDefault(item => item.Device.DeviceId == device.DeviceId);
                    if (current != null) ConfigureBoard(current, selected);
                };
                boardEditorButtons.Add(device.DeviceId, button);
                BoardEditorSwitches.Children.Add(button);
            }
            button.Content = $"{BoardDisplayName(board)} · {device.Port}";
            bool selectedBoard = string.Equals(device.DeviceId, editingBoardId, StringComparison.OrdinalIgnoreCase);
            button.IsEnabled = (!selectedBoard || CurrentEditingBinding()?.ProfileId != EditorProfile.Id)
                && (board.Info?.SupportsSerialInput == true || settings.BoardBindings.GetValueOrDefault(device.DeviceId)?.Enabled == true);
            AutomationProperties.SetName(button, $"Modifier le macropad {device.Port}");
        }
        BoardEditorSwitches.Visibility = boards.Count > 1 || CurrentEditingBinding()?.ProfileId != EditorProfile.Id
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void SelectBoardDetails(string id)
    {
        if (string.Equals(settings.PreferredBoardDeviceId, id, StringComparison.OrdinalIgnoreCase)) return;
        string previous = settings.PreferredBoardDeviceId;
        settings.PreferredBoardDeviceId = id;
        if (Persist()) boardConnection?.Select(id);
        else settings.PreferredBoardDeviceId = previous;
    }

    private void ConfigureBoard(BoardLinkState board, int index)
    {
        if (firmwareOpen || calibrating || profileSelectionLocks > 0 || OwnedWindows.Count > 0 || !SaveEditor()) return;
        string id = board.Device.DeviceId;
        var previousBinding = settings.BoardBindings.GetValueOrDefault(id);
        if (board.Info?.SupportsSerialInput != true && previousBinding?.Enabled != true) return;
        BoardSetup setup;
        try { setup = BoardSetup.Prepare(settings, id, board.Device.Port); }
        catch (Exception ex) when (ex is InvalidOperationException or System.IO.IOException)
        {
            ReportError(ex.Message);
            return;
        }
        string previousDevice = settings.PreferredBoardDeviceId;
        if (setup.AddProfile) settings.Profiles.Add(setup.Profile);
        settings.BoardBindings[id] = setup.Binding;
        settings.PreferredBoardDeviceId = id;
        bool changed = setup.AddProfile || previousBinding != setup.Binding ||
            !string.Equals(previousDevice, id, StringComparison.OrdinalIgnoreCase);
        if (changed && !Persist())
        {
            if (setup.AddProfile) settings.Profiles.Remove(setup.Profile);
            if (previousBinding == null) settings.BoardBindings.Remove(id);
            else settings.BoardBindings[id] = previousBinding;
            settings.PreferredBoardDeviceId = previousDevice;
            return;
        }

        if (previousBinding?.Enabled != true) boardConnection?.SetSerialDevices(EnabledSerialDevices());
        if (previousBinding?.ProfileId != setup.Binding.ProfileId && serialDecks.TryGetValue(id, out var deck)) deck.Cancel();
        boardConnection?.Select(id);
        CancelPendingGestures();
        editingBoardId = id;
        editingProfileId = setup.Profile.Id;
        selected = index;
        editingLong = false;
        RefreshProfiles();
        LoadEditor();
        ControlTabs.SelectedItem = ActionsTab;
        Dispatcher.BeginInvoke(() =>
        {
            deckButtons[index].BringIntoView();
            ActionSearchBox.Focus();
        }, DispatcherPriority.Loaded);
        StatusText.Text = $"Macropad {board.Device.Port} · Touche {index + 1:00} · Choisissez une action";
    }
}
