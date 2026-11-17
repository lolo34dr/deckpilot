using DeckPilot.Core;

public static class ActionToolsChecks
{
    public static async Task<int> RunAsync()
    {
        int passed = 0;
        void Check(bool condition, string name) { if (!condition) throw new Exception(name); Console.WriteLine("PASS " + name); passed++; }
        var youtube = new Session("chrome.exe", true);
        var spotify = new Session("Spotify.exe", false);
        foreach (int key in new[] { 0xB0, 0xB1, 0xB3 }) await SpotifyPlayback.SendAsync([youtube, spotify], key, default);
        Check(youtube.Commands.Count == 0 && spotify.Commands.SequenceEqual(new[] { 0xB0, 0xB1, 0xB3 }), "Spotify controls only its own session while a browser is playing");
        var active = new Session("SpotifyAB.SpotifyMusic_zpdnekdrzrea0!Spotify", true);
        await SpotifyPlayback.SendAsync([spotify, youtube, active], 0xB0, default);
        Check(active.Commands.SequenceEqual(new[] { 0xB0 }) && spotify.Commands.Count == 3, "Active packaged Spotify takes priority over an idle Spotify session");
        bool missing = false;
        try { await SpotifyPlayback.SendAsync([youtube, new Session("browser-spotify.exe", true)], 0xB0, default); }
        catch (InvalidOperationException) { missing = true; }
        Check(missing && youtube.Commands.Count == 0 && !SpotifyPlayback.IsSpotify("SpotifyAB.SpotifyMusic_fake!App"), "Missing Spotify or a lookalike never falls back to browser/global media controls");
        var refusing = new Session("Spotify.exe", true) { Accept = false };
        bool refused = false;
        try { await SpotifyPlayback.SendAsync([youtube, refusing], 0xB3, default); }
        catch (InvalidOperationException) { refused = true; }
        Check(refused && youtube.Commands.Count == 0, "A refused Spotify command remains an error without controlling another player");
        bool invalid = false;
        try { await SpotifyPlayback.SendAsync([spotify], 0xAF, default); } catch (ArgumentException) { invalid = true; }
        using var cancellation = new CancellationTokenSource(); cancellation.Cancel();
        bool cancelled = false;
        try { await SpotifyPlayback.SendAsync([spotify], 0xB0, cancellation.Token); } catch (OperationCanceledException) { cancelled = true; }
        Check(invalid && cancelled && spotify.Commands.Count == 3, "Unsupported Spotify commands and cancellation cannot send a media command");
        var deezerIdle = new DeezerTestSession("org.deezer.desktop", false);
        var deezer = new DeezerTestSession("Deezer.Desktop_123!App", true);
        var deezerBrowser = new DeezerTestSession("chrome.exe", true);
        foreach (int key in new[] { 0xB3, 0xB0, 0xB1 }) await DeezerPlayback.SendAsync([deezerBrowser, deezerIdle, deezer], key, default);
        Check(deezerBrowser.Commands.Count == 0 && deezerIdle.Commands.Count == 0 && deezer.Commands.SequenceEqual(new[] { 0xB3, 0xB0, 0xB1 })
            && DeezerPlayback.IsDeezer("Deezer.exe") && DeezerPlayback.IsDeezer("org.deezer.desktop")
            && DeezerPlayback.IsDeezer("Deezer.Desktop_123!App") && !DeezerPlayback.IsDeezer("DeezerBrowser.exe")
            && !DeezerPlayback.IsDeezer("chrome.exe"), "Deezer targets recognized Windows media sessions, preferring playback and excluding browsers/lookalikes");
        bool missingDeezer = false, invalidDeezerKey = false, cancelledDeezer = false, refusedDeezer = false;
        try { await DeezerPlayback.SendAsync([deezerBrowser], 0xB0, default); } catch (InvalidOperationException) { missingDeezer = true; }
        try { await DeezerPlayback.SendAsync([deezer], 0xAF, default); } catch (ArgumentException) { invalidDeezerKey = true; }
        using (var deezerCancellation = new CancellationTokenSource())
        {
            deezerCancellation.Cancel();
            try { await DeezerPlayback.SendAsync([deezer], 0xB0, deezerCancellation.Token); } catch (OperationCanceledException) { cancelledDeezer = true; }
        }
        var refusingDeezer = new DeezerTestSession("Deezer.exe", true) { Accept = false };
        try { await DeezerPlayback.SendAsync([deezerBrowser, refusingDeezer], 0xB3, default); } catch (InvalidOperationException) { refusedDeezer = true; }
        Check(missingDeezer && invalidDeezerKey && cancelledDeezer && refusedDeezer && deezerBrowser.Commands.Count == 0,
            "Missing, refused, unsupported or cancelled Deezer commands never fall back to another player");
        var spotifyVolume = new VolumeSession("Spotify", true, .50f);
        var spotifyIdle = new VolumeSession("SpotifyAB.SpotifyMusic_123", false, .30f);
        var youtubeVolume = new VolumeSession("chrome", true, .80f);
        Check(SpotifyVolume.Adjust([spotifyVolume, spotifyIdle, youtubeVolume], 0xAF) == 1
            && Math.Abs(spotifyVolume.Volume - .55f) < .001f && Math.Abs(spotifyIdle.Volume - .30f) < .001f && Math.Abs(youtubeVolume.Volume - .80f) < .001f,
            "Spotify volume changes only its active Windows audio session, never the browser or master volume");
        Check(SpotifyVolume.Adjust([spotifyIdle], 0xAE) == 1 && Math.Abs(spotifyIdle.Volume - .25f) < .001f,
            "Spotify volume remains adjustable from its inactive session when playback is paused");
        var minimum = new VolumeSession("Spotify.exe", false, 0f);
        var maximum = new VolumeSession("Spotify", true, 1f);
        SpotifyVolume.Adjust([minimum], 0xAE); SpotifyVolume.Adjust([maximum], 0xAF);
        bool missingSpotifyVolume = false, invalidVolumeKey = false;
        try { SpotifyVolume.Adjust([youtubeVolume], 0xAF); } catch (InvalidOperationException) { missingSpotifyVolume = true; }
        try { SpotifyVolume.Adjust([spotifyVolume], 0xB3); } catch (ArgumentException) { invalidVolumeKey = true; }
        Check(minimum.Volume == 0f && maximum.Volume == 1f && missingSpotifyVolume && invalidVolumeKey
            && youtubeVolume.Volume == .80f && SpotifyVolume.IsSpotifyProcess("SpotifyAB.SpotifyMusic") && !SpotifyVolume.IsSpotifyProcess("chrome"),
            "Spotify volume clamps safely and never falls back to global media volume");
        var deezerVolume = new DeezerTestVolumeSession("Deezer", true, .50f);
        var deezerPausedVolume = new DeezerTestVolumeSession("Deezer_Desktop", false, .30f);
        var otherVolume = new DeezerTestVolumeSession("chrome", true, .80f);
        int deezerTargets = DeezerVolume.Adjust([deezerVolume, deezerPausedVolume, otherVolume], 0xAF);
        DeezerVolume.Adjust([deezerPausedVolume], 0xAE);
        var deezerMinimum = new DeezerTestVolumeSession("Deezer.exe", false, 0f);
        var deezerMaximum = new DeezerTestVolumeSession("org.deezer.desktop", true, 1f);
        DeezerVolume.Adjust([deezerMinimum], 0xAE); DeezerVolume.Adjust([deezerMaximum], 0xAF);
        bool missingDeezerVolume = false, invalidDeezerVolumeKey = false;
        try { DeezerVolume.Adjust([otherVolume], 0xAF); } catch (InvalidOperationException) { missingDeezerVolume = true; }
        try { DeezerVolume.Adjust([deezerVolume], 0xB3); } catch (ArgumentException) { invalidDeezerVolumeKey = true; }
        Check(deezerTargets == 1 && Math.Abs(deezerVolume.Volume - .55f) < .001f && Math.Abs(deezerPausedVolume.Volume - .25f) < .001f
            && deezerMinimum.Volume == 0f && deezerMaximum.Volume == 1f && Math.Abs(otherVolume.Volume - .80f) < .001f
            && missingDeezerVolume && invalidDeezerVolumeKey && DeezerVolume.IsDeezerProcess("Deezer_Desktop")
            && DeezerVolume.IsDeezerProcess("org.deezer.desktop") && !DeezerVolume.IsDeezerProcess("DeezerBrowser")
            && !DeezerVolume.IsDeezerProcess("chrome"), "Deezer volume changes only matched app sessions, handles pause and bounds, and never changes browser/master volume");
        var volume = ActionSearch.Find("volume", []);
        var apps = new[] { new InstalledApplication("Discord", @"C:\Apps\Discord.lnk", "Menu"), new InstalledApplication("Éditeur", @"C:\Apps\edit.exe", "Menu") };
        var discord = ActionSearch.Find("DISCORD", apps);
        int spotifyPosition = Array.IndexOf(ActionSearch.LibraryOrder, ActionKind.Spotify);
        Check(volume.Where(p => p.Action.Kind == ActionKind.Media).Select(p => p.Action.Key).Order().SequenceEqual(new[] { 0xAD, 0xAE, 0xAF })
            && volume.Where(p => p.Action.Kind == ActionKind.Spotify).Select(p => p.Action.Key).Order().SequenceEqual(new[] { 0xAE, 0xAF })
            && volume.Where(p => p.Action.Kind == ActionKind.Deezer).Select(p => p.Action.Key).Order().SequenceEqual(new[] { 0xAE, 0xAF })
            && spotifyPosition >= 0 && spotifyPosition + 1 < ActionSearch.LibraryOrder.Length && ActionSearch.LibraryOrder[spotifyPosition + 1] == ActionKind.Deezer
            && ActionSearch.LibraryOrder.Order().SequenceEqual(Enum.GetValues<ActionKind>().Where(ActionSearch.IsAvailable).Order())
            && volume.Any(p => p.Action.Kind == ActionKind.System && p.Action.Target == "sound-settings") && discord.Single().Action.Target == apps[0].Target
            && ActionSearch.Find("editeur", apps).Single().Action.ApplicationName == "Éditeur", "Action search finds volume presets and installed applications regardless of case or accents");
        var next = ActionSearch.Find("skip", []).Where(p => p.Action.Kind == ActionKind.Spotify).Single();
        var copy = next.Action.Copy(); copy.Label = "Changed";
        Check(next.Action.Key == 0xB0 && next.Action.Label != copy.Label && ActionSearch.Find("", apps).Count == 0, "Spotify search presets are specific, copyable and separate from general media");
        string path = Path.Combine(Path.GetTempPath(), "deckpilot-tools-" + Guid.NewGuid() + ".json");
        try
        {
            var settings = new Settings(); settings.ActiveProfile.Buttons[0] = next.Action.Copy();
            var store = new SettingsStore(path); store.Save(settings);
            Check(store.Load().ActiveProfile.Buttons[0].Kind == ActionKind.Spotify && store.Load().ActiveProfile.Buttons[0].Key == 0xB0, "Spotify assignments survive a settings round-trip");
        }
        finally { File.Delete(path); }
        return passed;
    }
    private sealed class Session(string appId, bool playing) : ISpotifyMediaSession
    {
        public string AppId => appId;
        public bool IsPlaying => playing;
        public bool Accept { get; init; } = true;
        public List<int> Commands { get; } = [];
        public Task<bool> SendAsync(int key, CancellationToken cancellation) { cancellation.ThrowIfCancellationRequested(); Commands.Add(key); return Task.FromResult(Accept); }
    }
    private sealed class DeezerTestSession(string appId, bool playing) : IDeezerMediaSession
    {
        public string AppId => appId;
        public bool IsPlaying => playing;
        public bool Accept { get; init; } = true;
        public List<int> Commands { get; } = [];
        public Task<bool> SendAsync(int key, CancellationToken cancellation) { cancellation.ThrowIfCancellationRequested(); Commands.Add(key); return Task.FromResult(Accept); }
    }
    private sealed class DeezerTestVolumeSession(string processName, bool isActive, float volume) : IDeezerVolumeSession
    {
        public string ProcessName => processName;
        public bool IsActive => isActive;
        public float Volume { get; private set; } = volume;
        public void SetVolume(float value) => Volume = value;
    }
    private sealed class VolumeSession(string processName, bool isActive, float volume) : ISpotifyVolumeSession
    {
        public string ProcessName => processName;
        public bool IsActive => isActive;
        public float Volume { get; private set; } = volume;
        public void SetVolume(float value) => Volume = value;
    }
}
