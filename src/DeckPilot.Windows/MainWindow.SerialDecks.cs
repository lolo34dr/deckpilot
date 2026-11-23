using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private sealed class SerialDeck(string id, Guid session)
    {
        public string Id { get; } = id;
        public Guid Session { get; } = session;
        public PressGestures Gestures { get; } = new();
        public int Mask { get; set; }
        public int Revision { get; private set; }
        public void Cancel()
        {
            Revision++;
            Gestures.CancelAll();
        }
    }

    private readonly Dictionary<string, SerialDeck> serialDecks = new(StringComparer.OrdinalIgnoreCase);
    private string? serialCalibrationId;
    private bool loadingBoardBinding;

    private IEnumerable<string> EnabledSerialDevices() =>
        settings.BoardBindings.Where(pair => pair.Value.Enabled).Select(pair => pair.Key);

    private void OnBoardInput(BoardInputFrame frame)
    {
        if (lifetime.IsCancellationRequested) return;
        serialDecks.TryGetValue(frame.DeviceId, out var deck);
        if (frame.Mask is not int mask)
        {
            if (deck?.Session != frame.Session) return;
            deck.Cancel();
            serialDecks.Remove(frame.DeviceId);
            if (string.Equals(serialCalibrationId, frame.DeviceId, StringComparison.OrdinalIgnoreCase))
            {
                EndCalibration();
                ReportError("Macropad déconnecté. Son calibrage précédent est conservé.");
            }
            return;
        }

        if (boardConnection?.IsInputCurrent(frame.DeviceId, frame.Session) != true ||
            !settings.BoardBindings.TryGetValue(frame.DeviceId, out var binding) || !binding.Enabled) return;
        if (deck?.Session != frame.Session)
        {
            deck?.Cancel();
            serialDecks[frame.DeviceId] = deck = new(frame.DeviceId, frame.Session);
        }

        int previous = deck.Mask;
        deck.Mask = mask;
        int pressed = mask & ~previous;
        if (calibrating)
        {
            if (string.Equals(serialCalibrationId, deck.Id, StringComparison.OrdinalIgnoreCase) && pressed != 0)
            {
                if ((pressed & (pressed - 1)) != 0 || previous != 0)
                    CalibrationError.Text = "Relâchez les touches, puis appuyez sur un seul bouton.";
                else CaptureKey(Calibration.FirstKey + System.Numerics.BitOperations.TrailingZeroCount((uint)pressed));
            }
            return;
        }
        if (firmwareOpen || shortcutRecording || SerialProfile(deck.Id) is not { } profile) return;

        int released = previous & ~mask;
        for (int bit = 0; bit < 12; bit++)
        {
            int flag = 1 << bit;
            if (((pressed | released) & flag) == 0) continue;
            int index = binding.ButtonIndex(bit);
            GestureInvocation? invocation = (pressed & flag) != 0
                ? deck.Gestures.Begin(bit, index, profile.Buttons[index], frame.At)
                : (released & flag) != 0 ? deck.Gestures.Release(bit, frame.At) : null;
            if (invocation != null) RunSerialGesture(deck, profile.Id, invocation);
        }
        if (deck.Gestures.HasPending) gestureTimer.Start();
    }

    private Profile? SerialProfile(string id) => settings.BoardBindings.TryGetValue(id, out var binding) && binding.Enabled
        ? settings.Profiles.FirstOrDefault(profile => profile.Id == binding.ProfileId) : null;

    private bool IsSerialActionCurrent(SerialDeck deck, Guid profile, int revision) =>
        deck.Revision == revision && SerialProfile(deck.Id)?.Id == profile
        && boardConnection?.IsInputCurrent(deck.Id, deck.Session) == true;

    private void RunSerialGesture(SerialDeck deck, Guid profile, GestureInvocation invocation)
    {
        int revision = deck.Revision;
        Dispatcher.BeginInvoke(async () =>
        {
            if (calibrating || firmwareOpen || shortcutRecording || lifetime.IsCancellationRequested || !IsSerialActionCurrent(deck, profile, revision)) return;
            await ExecuteActionAsync(invocation.Index, invocation.Action, deck, profile, revision);
        });
    }

    private bool TickSerialGestures(long now)
    {
        bool pending = false;
        foreach (var deck in serialDecks.Values)
        {
            if (!deck.Gestures.HasPending) continue;
            var profile = SerialProfile(deck.Id);
            if (profile == null || boardConnection?.IsInputCurrent(deck.Id, deck.Session) != true)
            {
                deck.Cancel();
                continue;
            }
            foreach (var invocation in deck.Gestures.Tick(now)) RunSerialGesture(deck, profile.Id, invocation);
            pending |= deck.Gestures.HasPending;
        }
        return pending;
    }

    private void CancelSerialGestures()
    {
        foreach (var deck in serialDecks.Values) deck.Cancel();
    }

    private void ApplyBoardInputSettings(bool refreshProfiles = false)
    {
        if (BoardProfilePicker == null) return;
        loadingBoardBinding = true;
        try
        {
            var device = boardState?.Device;
            BoardBinding? binding = null;
            if (device != null) settings.BoardBindings.TryGetValue(device.DeviceId, out binding);
            bool enabled = binding?.Enabled == true;
            bool supported = device != null && boardState?.Info?.SupportsSerialInput == true;
            bool active = enabled && boardState?.SerialInput == true;
            BoardInputSettings.Visibility = device == null ? Visibility.Collapsed : Visibility.Visible;
            BoardSerialCheck.IsEnabled = supported || enabled;
            BoardSerialCheck.IsChecked = enabled;
            BoardInputHint.Text = active ? "Les appuis utilisent le profil ci-dessous. Les touches libres ne produisent aucun raccourci clavier."
                : enabled ? "Le mode DeckPilot est en attente. Vérifiez le firmware et relâchez toutes les touches."
                : supported ? "Activez ce mode pour utiliser plusieurs macropads avec des profils distincts."
                : "Le mode DeckPilot nécessite le firmware 1.2.0 ou plus récent.";
            BoardProfileSettings.Visibility = enabled ? Visibility.Visible : Visibility.Collapsed;
            if (refreshProfiles || BoardProfilePicker.ItemsSource == null)
                BoardProfilePicker.ItemsSource = settings.Profiles.ToArray();
            var profile = enabled && device != null ? SerialProfile(device.DeviceId) : null;
            BoardProfilePicker.SelectedItem = profile;
            if (enabled && profile == null && active)
                BoardInputHint.Text = "Choisissez un profil pour que ce macropad puisse déclencher des actions.";
            CalibrateButton.Content = enabled ? "Calibrer ce macropad" : "Calibrer les touches clavier";
            CalibrateButton.IsEnabled = !enabled || active;
        }
        finally { loadingBoardBinding = false; }
    }

    private bool SaveBoardBinding(string id, BoardBinding binding)
    {
        settings.BoardBindings.TryGetValue(id, out var previous);
        settings.BoardBindings[id] = binding;
        if (!Persist())
        {
            if (previous == null) settings.BoardBindings.Remove(id);
            else settings.BoardBindings[id] = previous;
            ApplyBoardInputSettings();
            return false;
        }
        if (serialDecks.TryGetValue(id, out var deck)) deck.Cancel();
        if ((previous?.Enabled == true) != binding.Enabled) boardConnection?.SetSerialDevices(EnabledSerialDevices());
        ApplyBoardInputSettings();
        RefreshBoardCards();
        ApplyHardwareHighlights();
        return true;
    }

    private void BoardSerialClick(object sender, RoutedEventArgs e)
    {
        if (loadingBoardBinding || firmwareOpen || boardState?.Device is not { } device) return;
        bool enable = BoardSerialCheck.IsChecked == true;
        var binding = settings.BoardBindings.GetValueOrDefault(device.DeviceId);
        if (enable)
        {
            var board = boardState?.Boards.FirstOrDefault(link => link.Device.DeviceId == device.DeviceId);
            if (board != null) ConfigureBoard(board, 0);
        }
        else if (binding != null) SaveBoardBinding(device.DeviceId, binding with { Enabled = false });
        ApplyBoardInputSettings();
    }

    private void BoardProfileChanged(object sender, SelectionChangedEventArgs e)
    {
        if (loadingBoardBinding || loading || firmwareOpen || boardState?.Device is not { } device
            || BoardProfilePicker.SelectedItem is not Profile profile
            || !settings.BoardBindings.TryGetValue(device.DeviceId, out var binding) || profile.Id == binding.ProfileId) return;
        SaveBoardBinding(device.DeviceId, binding with { ProfileId = profile.Id });
    }

}
