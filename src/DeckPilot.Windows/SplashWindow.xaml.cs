using System.Threading.Tasks;
using System.Windows;

namespace DeckPilot.Windows;

public partial class SplashWindow : Window
{
    public SplashWindow()
    {
        InitializeComponent();
        VersionText.Text = "v" + UpdateService.DisplayVersion;
    }

    public void SetReducedMotion(bool enabled) =>
        LoadingProgress.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;

    public Task CloseWhenReadyAsync()
    {
        if (IsVisible) Close();
        return Task.CompletedTask;
    }
}
