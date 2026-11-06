using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace DeckPilot.Windows;

public static class SpotifyVolumeController
{
    public static Task RunAsync(int key, CancellationToken cancellation) =>
        Task.Run(() => Run(key, cancellation), cancellation);

    private static void Run(int key, CancellationToken cancellation)
    {
        if (key is not (0xAF or 0xAE)) throw new ArgumentException("Commande de volume Spotify invalide.");
        cancellation.ThrowIfCancellationRequested();

        using var enumerator = new MMDeviceEnumerator();
        var endpoints = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        var matches = new List<SpotifySession>();
        try
        {
            foreach (var endpoint in endpoints)
            {
                cancellation.ThrowIfCancellationRequested();
                using (endpoint)
                {
                    var manager = endpoint.AudioSessionManager;
                    try
                    {
                        var sessions = manager.Sessions;
                        for (int i = 0; i < sessions.Count; i++)
                        {
                            AudioSessionControl? control = null;
                            try
                            {
                                control = sessions[i];
                                uint processId = control.GetProcessID;
                                if (processId == 0 || processId > int.MaxValue) continue;

                                string processName;
                                try
                                {
                                    using var process = Process.GetProcessById((int)processId);
                                    processName = process.ProcessName;
                                }
                                catch (ArgumentException) { continue; }
                                catch (InvalidOperationException) { continue; }
                                catch (Win32Exception) { continue; }

                                if (!SpotifyVolume.IsSpotifyProcess(processName)) continue;
                                var state = control.State;
                                if (state == AudioSessionState.AudioSessionStateExpired) continue;

                                var audioVolume = control.SimpleAudioVolume;
                                matches.Add(new SpotifySession(processName, state == AudioSessionState.AudioSessionStateActive, control, audioVolume));
                                control = null;
                            }
                            finally { control?.Dispose(); }
                        }
                    }
                    finally { manager.Dispose(); }
                }
            }

            cancellation.ThrowIfCancellationRequested();
            SpotifyVolume.Adjust(matches, key);
        }
        catch (InvalidOperationException ex) when (ex.Message.StartsWith("Aucune session audio Spotify", StringComparison.Ordinal))
        {
            throw;
        }
        catch (Exception ex) when (ex is not OperationCanceledException and not ArgumentException)
        {
            throw new InvalidOperationException("Impossible de régler le volume de Spotify. Vérifiez que Spotify est ouvert et qu’une sortie audio est active.", ex);
        }
        finally
        {
            foreach (var session in matches) session.Dispose();
        }
    }

    private sealed class SpotifySession(string processName, bool isActive, AudioSessionControl control, SimpleAudioVolume volume) : ISpotifyVolumeSession, IDisposable
    {
        public string ProcessName => processName;
        public bool IsActive => isActive;
        public float Volume => volume.Volume;
        public void SetVolume(float value) => volume.Volume = value;
        public void Dispose() { volume.Dispose(); control.Dispose(); }
    }
}
