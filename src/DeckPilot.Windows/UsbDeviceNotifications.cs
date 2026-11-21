using System;
using System.Windows;
using System.Windows.Interop;

namespace DeckPilot.Windows;

internal static class UsbDeviceNotifications
{
    internal static void Observe(Window window, Action refresh)
    {
        const int deviceChange = 0x0219;
        const int devNodesChanged = 0x0007;
        HwndSource? source = null;
        HwndSourceHook hook = (IntPtr _, int message, IntPtr change, IntPtr __, ref bool handled) =>
        {
            if (message == deviceChange && change == new IntPtr(devNodesChanged))
                window.Dispatcher.InvokeAsync(refresh);
            return IntPtr.Zero;
        };
        window.SourceInitialized += (_, _) =>
        {
            source = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
            source?.AddHook(hook);
        };
        window.Closed += (_, _) =>
        {
            if (source is { IsDisposed: false }) source.RemoveHook(hook);
        };
    }
}
