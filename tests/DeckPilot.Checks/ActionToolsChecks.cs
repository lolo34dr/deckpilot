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
        var volume = ActionSearch.Find("volume", []);
        var apps = new[] { new InstalledApplication("Discord", @"C:\Apps\Discord.lnk", "Menu"), new InstalledApplication("Éditeur", @"C:\Apps\edit.exe", "Menu") };
        var discord = ActionSearch.Find("DISCORD", apps);
        Check(volume.Where(p => p.Action.Kind == ActionKind.Media).Select(p => p.Action.Key).Order().SequenceEqual(new[] { 0xAD, 0xAE, 0xAF })
            && volume.Where(p => p.Action.Kind == ActionKind.Spotify).Select(p => p.Action.Key).Order().SequenceEqual(new[] { 0xAE, 0xAF })
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
    private sealed class VolumeSession(string processName, bool isActive, float volume) : ISpotifyVolumeSession
    {
        public string ProcessName => processName;
        public bool IsActive => isActive;
        public float Volume { get; private set; } = volume;
        public void SetVolume(float value) => Volume = value;
    }
}
