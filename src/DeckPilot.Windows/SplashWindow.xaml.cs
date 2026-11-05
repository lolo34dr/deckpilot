using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace DeckPilot.Windows;

public partial class SplashWindow : Window
{
    private readonly long shownAt = Stopwatch.GetTimestamp();
    private static readonly TimeSpan MinimumVisibleTime = TimeSpan.FromMilliseconds(600);
    private bool reducedMotion = true;

    public SplashWindow()
    {
        InitializeComponent();
        VersionText.Text = "v" + UpdateService.DisplayVersion;
    }

    public void SetReducedMotion(bool enabled)
    {
        reducedMotion = enabled;
        LoadingProgress.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;
        ReducedMotionProgress.Visibility = enabled ? Visibility.Visible : Visibility.Collapsed;
    }

    public async Task CloseWhenReadyAsync()
    {
        var remaining = MinimumVisibleTime - Stopwatch.GetElapsedTime(shownAt);
        if (remaining > TimeSpan.Zero) await Task.Delay(remaining);
        if (!reducedMotion && SystemParameters.ClientAreaAnimation && IsVisible)
        {
            var completed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var fade = new DoubleAnimation(0, TimeSpan.FromMilliseconds(150));
            fade.Completed += (_, _) => completed.TrySetResult();
            BeginAnimation(OpacityProperty, fade);
            await completed.Task;
        }
        if (IsVisible) Close();
    }
}
