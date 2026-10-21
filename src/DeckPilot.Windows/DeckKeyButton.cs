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
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    public string KeyCaption { get => (string)GetValue(KeyCaptionProperty); set => SetValue(KeyCaptionProperty, value); }
    public ImageSource IconSource { get => (ImageSource)GetValue(IconSourceProperty); set => SetValue(IconSourceProperty, value); }
    public ImageSource KindSource { get => (ImageSource)GetValue(KindSourceProperty); set => SetValue(KindSourceProperty, value); }
    public bool IsSelected { get => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }
    public bool IsHardwarePressed { get => (bool)GetValue(IsHardwarePressedProperty); set => SetValue(IsHardwarePressedProperty, value); }
    public DeckKeyFeedback Feedback { get => (DeckKeyFeedback)GetValue(FeedbackProperty); set => SetValue(FeedbackProperty, value); }
    public override void OnApplyTemplate() { base.OnApplyTemplate(); AnimatePress(); }
    protected override void OnMouseEnter(MouseEventArgs e) { base.OnMouseEnter(e); AnimatePress(); }
    protected override void OnMouseLeave(MouseEventArgs e) { base.OnMouseLeave(e); AnimatePress(); }
    private static void VisualChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var key = (DeckKeyButton)sender;
        key.AnimatePress();
        if (e.Property == FeedbackProperty && key.Feedback != DeckKeyFeedback.Ready && key.Template?.FindName("StateBadge", key) is UIElement badge)
            Motion.Reveal(badge);
    }
    private void AnimatePress()
    {
        if (Template?.FindName("KeyScale", this) is not ScaleTransform scale) return;
        double target = IsHardwarePressed ? 0.965 : IsMouseOver ? 1.015 : 1;
        foreach (var property in new[] { ScaleTransform.ScaleXProperty, ScaleTransform.ScaleYProperty })
        {
            if (SystemParameters.ClientAreaAnimation)
                scale.BeginAnimation(property, new DoubleAnimation(target, TimeSpan.FromMilliseconds(110)) { EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut } });
            else { scale.BeginAnimation(property, null); scale.SetValue(property, target); }
        }
    }
}
