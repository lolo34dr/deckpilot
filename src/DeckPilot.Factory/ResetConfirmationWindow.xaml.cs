using System.Windows;

namespace DeckPilot.Factory;

public partial class ResetConfirmationWindow : Window
{
    public ResetConfirmationWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => ConfirmationInput.Focus();
    }

    private void ConfirmationInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) =>
        EraseButton.IsEnabled = string.Equals(ConfirmationInput.Text, "EFFACER", StringComparison.Ordinal);

    private void EraseButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;
}
