using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DeckPilot.Core;
using Microsoft.Win32;

namespace DeckPilot.Windows;

public partial class MainWindow
{
    private readonly PressGestures gestures = new();
    private readonly DispatcherTimer gestureTimer = new() { Interval = TimeSpan.FromMilliseconds(20) };
    private readonly DispatcherTimer audioTimer = new() { Interval = TimeSpan.FromSeconds(2) };
    private AudioSnapshot audioState = new([], "", MicrophoneState.Unavailable);
    private bool readingAudio, audioRefreshRequested, audioRendered, togglingMicrophone;
    private Task audioRefreshTask = Task.CompletedTask;
    private void InitializeAudioGestures()
    {
        gestureTimer.Tick += (_, _) =>
        {
            foreach (var invocation in gestures.Tick(Environment.TickCount64)) RunGesture(invocation);
            if (!gestures.HasPending) gestureTimer.Stop();
        };
        audioTimer.Tick += async (_, _) => await RefreshAudioAsync();
        IsVisibleChanged += async (_, _) => await InterfaceActivityChangedAsync();
        StateChanged += async (_, _) => await InterfaceActivityChangedAsync();
        Loaded += async (_, _) =>
        {
            if (!IsOperational) return;
            audioTimer.Interval = IsInterfaceActive ? TimeSpan.FromSeconds(2) : TimeSpan.FromSeconds(10);
            audioTimer.Start();
            if (!audioRendered && !readingAudio) await RefreshAudioAsync();
        };
        SystemEvents.SessionSwitch += SessionChanged;
        SystemEvents.PowerModeChanged += PowerChanged;
        Closed += (_, _) =>
        {
            gestureTimer.Stop(); audioTimer.Stop(); gestures.CancelAll();
            SystemEvents.SessionSwitch -= SessionChanged; SystemEvents.PowerModeChanged -= PowerChanged;
        };
    }
    private void SessionChanged(object sender, SessionSwitchEventArgs e) => Dispatcher.BeginInvoke(CancelPendingGestures);
    private void PowerChanged(object sender, PowerModeChangedEventArgs e) { if (e.Mode == PowerModes.Suspend) Dispatcher.BeginInvoke(CancelPendingGestures); }
    private void CancelPendingGestures() { gestures.CancelAll(); gestureTimer.Stop(); }
    private void OnDeckRelease(object? sender, DeckKeyEventArgs e)
    {
        if (gestures.Release(e.Key, Environment.TickCount64) is { } invocation) RunGesture(invocation);
    }
    private void RunGesture(GestureInvocation invocation) => Dispatcher.BeginInvoke(async () => await ExecuteActionAsync(invocation.Index, invocation.Action));
    private async Task InterfaceActivityChangedAsync()
    {
        if (!IsOperational || lifetime.IsCancellationRequested) return;
        audioTimer.Interval = IsInterfaceActive ? TimeSpan.FromSeconds(2) : TimeSpan.FromSeconds(10);
        RefreshFeedback();
        if (IsInterfaceActive) await RefreshAudioAsync();
    }
    private Task RefreshAudioAsync()
    {
        if (lifetime.IsCancellationRequested) return Task.CompletedTask;
        audioRefreshRequested = true;
        if (!readingAudio)
        {
            readingAudio = true;
            // Queue before starting the loop so even a synchronous completion
            // cannot leave a completed task installed as the active refresh.
            audioRefreshTask = Dispatcher.InvokeAsync(ReadAudioUntilCurrentAsync).Task.Unwrap();
        }
        return audioRefreshTask;
    }
    private async Task ReadAudioUntilCurrentAsync()
    {
        try
        {
            do
            {
                if (lifetime.IsCancellationRequested) return;
                audioRefreshRequested = false;
                var snapshot = await Task.Run(WindowsAudio.Read);
                if (lifetime.IsCancellationRequested) return;
                ApplyAudioSnapshot(snapshot);
            } while (audioRefreshRequested);
        }
        finally { readingAudio = false; }
    }
    private void ApplyAudioSnapshot(AudioSnapshot snapshot)
    {
        bool outputsChanged = !snapshot.Outputs.SequenceEqual(audioState.Outputs);
        bool changed = !audioRendered || outputsChanged || snapshot.Microphones != audioState.Microphones
            || snapshot.DefaultOutput != audioState.DefaultOutput || snapshot.Error != audioState.Error;
        audioState = snapshot;
        MicrophoneToggleButton.IsEnabled = !togglingMicrophone && (snapshot.Microphones is MicrophoneState.Muted or MicrophoneState.Active or MicrophoneState.Mixed);
        if (!changed) return;
        audioRendered = true;
        MicrophoneStatus.Text = snapshot.Microphones switch
        {
            MicrophoneState.Muted => "● Micros coupés dans Windows",
            MicrophoneState.Active => "● Micros actifs dans Windows",
            MicrophoneState.Mixed => "● Certains micros restent actifs",
            MicrophoneState.None => "Aucun microphone actif",
            _ => "Microphones indisponibles"
        };
        HeaderMicrophoneStatus.Text = snapshot.Microphones switch { MicrophoneState.Muted => "● Micro coupé", MicrophoneState.Active => "● Micro actif", MicrophoneState.Mixed => "● Micro partiel", MicrophoneState.None => "Micro : aucun", _ => "Micro : indisponible" };
        MicrophoneStatus.Foreground = Brush(snapshot.Microphones == MicrophoneState.Muted ? "#F1AFA5" : snapshot.Microphones == MicrophoneState.Mixed ? "#E1C294" : "#B9D5C2");
        HeaderMicrophoneStatus.Foreground = MicrophoneStatus.Foreground;
        MicrophoneStatus.ToolTip = snapshot.Error.Length > 0 ? snapshot.Error : "État des périphériques de capture Windows actifs ; actualisation toutes les 2 secondes au premier plan, 10 secondes en arrière-plan.";
        MicrophoneToggleButton.Content = snapshot.Microphones == MicrophoneState.Muted ? "Réactiver les micros" : "Couper les micros";
        string current = snapshot.Outputs.FirstOrDefault(d => d.Id == snapshot.DefaultOutput)?.Name ?? "Aucune sortie active";
        CurrentAudioOutput.Text = "Sortie : " + current;
        if (outputsChanged && ActionPicker.SelectedIndex == (int)ActionKind.AudioOutput)
        {
            var choice = EditedAction.CopyBasic(); choice.Kind = ActionKind.AudioOutput;
            choice.Target = (PrimaryOutputPicker.SelectedItem as AudioDevice)?.Id ?? choice.Target;
            choice.Arguments = (SecondaryOutputPicker.SelectedItem as AudioDevice)?.Id ?? choice.Arguments;
            PopulateAudioPickers(choice);
        }
    }
    private void PopulateAudioPickers(ButtonAction action)
    {
        if (action.Kind != ActionKind.AudioOutput) return;
        bool previous = loading; loading = true;
        try
        {
            string first = action.Target, second = action.Arguments;
            if (first.Length == 0) first = audioState.DefaultOutput;
            if (second.Length == 0) second = audioState.Outputs.FirstOrDefault(d => d.Id != first)?.Id ?? "";
            var devices = audioState.Outputs.ToList();
            foreach (string id in new[] { first, second }.Where(id => id.Length > 0))
                if (!devices.Any(d => d.Id == id)) devices.Add(new(id, "Sortie indisponible · À remplacer sur ce PC"));
            PrimaryOutputPicker.ItemsSource = devices; SecondaryOutputPicker.ItemsSource = devices;
            PrimaryOutputPicker.SelectedItem = devices.FirstOrDefault(d => d.Id == first);
            SecondaryOutputPicker.SelectedItem = devices.FirstOrDefault(d => d.Id == second);
        }
        finally { loading = previous; }
    }
    private async void ToggleMicrophoneClick(object sender, RoutedEventArgs e)
    {
        if (togglingMicrophone) return;
        togglingMicrophone = true; MicrophoneToggleButton.IsEnabled = false;
        try { await Task.Run(WindowsAudio.ToggleMicrophones); }
        catch (Exception ex) { if (!lifetime.IsCancellationRequested) ReportError("Microphone : " + ex.Message); }
        finally
        {
            togglingMicrophone = false;
            await RefreshAudioAsync();
        }
    }
}
