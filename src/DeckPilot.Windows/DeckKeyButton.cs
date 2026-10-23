using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DeckPilot.Windows;

public enum DeckKeyFeedback { Ready, Running, Success, Error }

// Each key keeps its feedback independently: refreshing the editor must not erase it.
public sealed class DeckKeyButton : Button
{
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(DeckKeyButton), new PropertyMetadata(""));
    public static readonly DependencyProperty KeyCaptionProperty = DependencyProperty.Register(nameof(KeyCaption), typeof(string), typeof(DeckKeyButton), new PropertyMetadata(""));
    public static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(DeckKeyButton));
    public static readonly DependencyProperty KindSourceProperty = DependencyProperty.Register(nameof(KindSource), typeof(ImageSource), typeof(DeckKeyButton));
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(DeckKeyButton), new PropertyMetadata(false));
    public static readonly DependencyProperty IsHardwarePressedProperty = DependencyProperty.Register(nameof(IsHardwarePressed), typeof(bool), typeof(DeckKeyButton), new PropertyMetadata(false, VisualChanged));
    public static readonly DependencyProperty FeedbackProperty = DependencyProperty.Register(nameof(Feedback), typeof(DeckKeyFeedback), typeof(DeckKeyButton), new PropertyMetadata(DeckKeyFeedback.Ready, VisualChanged));
    public static readonly DependencyProperty IsDropTargetProperty = DependencyProperty.Register(nameof(IsDropTarget), typeof(bool), typeof(DeckKeyButton), new PropertyMetadata(false));
    public bool IsDropTarget { get => (bool)GetValue(IsDropTargetProperty); set => SetValue(IsDropTargetProperty, value); }
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public string KeyCaption { get => (string)GetValue(KeyCaptionProperty); set => SetValue(KeyCaptionProperty, value); }
    public ImageSource IconSource { get => (ImageSource)GetValue(IconSourceProperty); set => SetValue(IconSourceProperty, value); }
    public ImageSource KindSource { get => (ImageSource)GetValue(KindSourceProperty); set => SetValue(KindSourceProperty, value); }
    public bool IsSelected { get => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }
    public bool IsHardwarePressed { get => (bool)GetValue(IsHardwarePressedProperty); set => SetValue(IsHardwarePressedProperty, value); }
    public DeckKeyFeedback Feedback { get => (DeckKeyFeedback)GetValue(FeedbackProperty); set => SetValue(FeedbackProperty, value); }
    private static void VisualChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var key = (DeckKeyButton)sender;
        if (e.Property == FeedbackProperty && key.Feedback != DeckKeyFeedback.Ready && key.Template?.FindName("StateBadge", key) is UIElement badge)
            Motion.Reveal(badge);
    }
}
