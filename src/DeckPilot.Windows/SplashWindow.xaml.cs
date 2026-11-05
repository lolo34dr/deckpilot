using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DeckPilot.Windows;

public partial class SplashWindow : Window
{
    private readonly long shownAt = Stopwatch.GetTimestamp();
    private static readonly TimeSpan MinimumVisibleTime = TimeSpan.FromMilliseconds(600);
    private bool reducedMotion = true;

    public SplashWindow()
    {
        InitializeComponent();
        if (SplashBanner.Source is not BitmapSource bitmap || bitmap.PixelWidth <= 0 || bitmap.PixelHeight <= 0)
            throw new InvalidDataException("The DeckPilot splash banner resource could not be loaded.");
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
