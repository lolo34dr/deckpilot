using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private readonly Dictionary<string, MacropadCard> boardCards = new(StringComparer.OrdinalIgnoreCase);
    private string? editingBoardId;

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
                card = new MacropadCard(board) { Width = 252, Margin = new Thickness(0, 0, 10, 10) };
                card.ConfigureRequested += ConfigureBoard;
                card.CalibrateRequested += board => BeginCalibration(board.Device.DeviceId);
                card.OptionsRequested += board =>
                {
                    SelectBoardDetails(board.Device.DeviceId);
                    BoardSerialCheck.BringIntoView();
                    if (!BoardSerialCheck.Focus()) FirmwareButton.Focus();
                };
                card.FirmwareRequested += async board => await OpenFirmwareAsync(board.Device.DeviceId);
                boardCards.Add(id, card);
                BoardCardsPanel.Children.Add(card);
            }
            var binding = settings.BoardBindings.GetValueOrDefault(id);
            var profile = binding == null ? null : settings.Profiles.FirstOrDefault(profile => profile.Id == binding.ProfileId);
            card.Refresh(board, binding, profile, profile != null && BoardSetup.IsShared(settings, id, profile.Id), firmwareOpen);
        }
        UpdateBoardWorkspace();
        RefreshBoardEditBanner();
    }

    private void UpdateBoardWorkspace()
    {
        if (BoardOverviewScroll == null || ControlTabs == null) return;
        bool show = ControlTabs.SelectedItem == DeviceTab && boardCards.Count > 0;
        BoardOverviewScroll.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        DeckEditorScroll.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
    }

    private BoardBinding? CurrentEditingBinding() =>
        editingBoardId != null && settings.BoardBindings.GetValueOrDefault(editingBoardId) is { Enabled: true } binding
            && binding.ProfileId == settings.ActiveProfile.Id ? binding : null;

    private BoardLinkState? CurrentEditingBoard() => CurrentEditingBinding() == null ? null
        : boardState?.Boards.FirstOrDefault(board => string.Equals(board.Device.DeviceId, editingBoardId, StringComparison.OrdinalIgnoreCase));

    private string EditorKeyName(int index)
    {
        var map = CurrentEditingBinding()?.KeyMap ?? settings.KeyMap;
        return map.Count == 12 ? Calibration.KeyName(map[index]) : $"{index + 1:00}";
    }

    private void RefreshBoardEditBanner()
    {
        if (BoardEditBanner == null) return;
        bool editing = CurrentEditingBinding() != null;
        BoardEditBanner.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;
        if (editing)
            BoardEditTitle.Text = CurrentEditingBoard() is { } board ? $"Touches du macropad {board.Device.Port}" : "Touches du macropad débranché";
        ProfilePickerLabel.Text = editing ? "Profil du macropad" : "Profil actif";
        AutomationProperties.SetName(ProfilePicker, ProfilePickerLabel.Text);
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
        Guid previousProfile = settings.ActiveProfileId;
        string previousDevice = settings.PreferredBoardDeviceId;
        if (setup.AddProfile) settings.Profiles.Add(setup.Profile);
        settings.BoardBindings[id] = setup.Binding;
        settings.ActiveProfileId = setup.Profile.Id;
        settings.PreferredBoardDeviceId = id;
        if (!Persist())
        {
            if (setup.AddProfile) settings.Profiles.Remove(setup.Profile);
            if (previousBinding == null) settings.BoardBindings.Remove(id);
            else settings.BoardBindings[id] = previousBinding;
            settings.ActiveProfileId = previousProfile;
            settings.PreferredBoardDeviceId = previousDevice;
            return;
        }

        if (previousBinding?.Enabled != true) boardConnection?.SetSerialDevices(EnabledSerialDevices());
        if (previousBinding?.ProfileId != setup.Binding.ProfileId && serialDecks.TryGetValue(id, out var deck)) deck.Cancel();
        boardConnection?.Select(id);
        CancelPendingGestures();
        editingBoardId = id;
        selected = index;
        editingLong = false;
        RefreshProfiles();
        LoadEditor();
        ControlTabs.SelectedItem = ActionsTab;
        deckButtons[index].BringIntoView();
        deckButtons[index].Focus();
        StatusText.Text = $"Macropad {board.Device.Port} · Touche {index + 1:00} · Choisissez une action";
    }
}
