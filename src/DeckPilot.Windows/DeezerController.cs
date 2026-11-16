using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;
using Windows.Media.Control;

namespace DeckPilot.Windows;

public static class DeezerController
{
    public static async Task RunAsync(int key, CancellationToken cancellation)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        timeout.CancelAfter(TimeSpan.FromSeconds(8));
        try
        {
            var manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync().AsTask(timeout.Token);
            var sessions = manager.GetSessions().Select(s => (IDeezerMediaSession)new Session(s));
            await DeezerPlayback.SendAsync(sessions, key, timeout.Token);
        }
        catch (OperationCanceledException) when (!cancellation.IsCancellationRequested)
        { throw new InvalidOperationException("Deezer ne répond pas. Réessayez après avoir lancé une lecture dans l’application."); }
        catch (Exception ex) when (ex is COMException or UnauthorizedAccessException)
        { throw new InvalidOperationException("La session Deezer est indisponible. Ouvrez Deezer pour Windows et lancez une lecture.", ex); }
    }
    private sealed class Session(GlobalSystemMediaTransportControlsSession session) : IDeezerMediaSession
    {
        public string AppId => session.SourceAppUserModelId;
        public bool IsPlaying => session.GetPlaybackInfo()?.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
        public async Task<bool> SendAsync(int key, CancellationToken cancellation) => key switch
        {
            0xB3 => await session.TryTogglePlayPauseAsync().AsTask(cancellation),
            0xB0 => await session.TrySkipNextAsync().AsTask(cancellation),
            0xB1 => await session.TrySkipPreviousAsync().AsTask(cancellation),
            _ => throw new ArgumentException("Commande Deezer invalide.")
        };
    }
}
