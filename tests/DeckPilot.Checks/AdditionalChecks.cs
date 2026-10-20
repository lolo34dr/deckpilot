using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DeckPilot.Core;

internal static class AdditionalChecks
{
    public static async Task<int> RunAsync()
    {
        int count = 0;
        void Expect(bool value) { if (!value) throw new Exception("Assertion failed"); }
        async Task Check(string name, Func<Task> action) { await action(); count++; Console.WriteLine("PASS " + name); }
        async Task Reject(Func<Task> action)
        {
            try { await action(); } catch (InvalidDataException) { return; }
            throw new Exception("Invalid update was accepted");
        }
        string ReleaseJson(string tag = "v0.2.0", bool prerelease = false, bool draft = false) => JsonSerializer.Serialize(new {
            tag_name = tag, prerelease, draft,
            assets = new[] { new { name = "DeckPilot.exe", browser_download_url = $"https://github.com/{UpdateRelease.Repository}/releases/download/{tag}/DeckPilot.exe", size = 2048 },
                new { name = "SHA256SUMS.txt", browser_download_url = $"https://github.com/{UpdateRelease.Repository}/releases/download/{tag}/SHA256SUMS.txt", size = 160 } }
        });
        var current = new Version(0, 1, 1, 0);
        await Check("Application search ignores accents and case", () => {
            Expect(new InstalledApplication("Éditeur vidéo", "editor.lnk", "Menu Démarrer").Matches("EDITEUR")); return Task.CompletedTask;
        });
        await Check("Start menu entries take priority over duplicate Windows apps", () => {
            var apps = ApplicationTarget.Merge([new("Editor", "editor.lnk", "Menu Démarrer"), new("editor", "appsfolder:Editor", "Windows"), new("Browser", "browser.lnk", "Menu Démarrer")]);
            Expect(apps.Count == 2 && apps[0].Name == "Browser" && apps[1].Target == "editor.lnk"); return Task.CompletedTask;
        });
        await Check("Packaged app IDs round-trip without shell command interpretation", async () => {
            string id = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
            Expect(ApplicationTarget.GetAppId(ApplicationTarget.ForAppId(id)) == id);
            await Reject(() => { ApplicationTarget.ForAppId("bad\" /argument"); return Task.CompletedTask; });
            await Reject(() => { ApplicationTarget.GetAppId("calculator.exe"); return Task.CompletedTask; });
        });
        await Check("Only newer stable releases are offered, using numeric versions", () => {
            Expect(UpdateRelease.Parse(ReleaseJson(), current)?.Version == new Version(0, 2, 0));
            Expect(UpdateRelease.Parse(ReleaseJson(), new Version(0, 2, 0, 0)) == null);
            Expect(UpdateRelease.Parse(ReleaseJson(), new Version(0, 3, 0)) == null);
            Expect(UpdateRelease.Parse(ReleaseJson("v0.10.0"), new Version(0, 9, 0)) != null);
            Expect(UpdateRelease.Parse(ReleaseJson(prerelease: true), current) == null);
            Expect(UpdateRelease.Parse(ReleaseJson(draft: true), current) == null);
            Expect(UpdateRelease.Parse(ReleaseJson("v0.3.0-beta"), current) == null);
            return Task.CompletedTask;
        });
        await Check("Foreign repositories and insecure asset URLs are refused", async () => {
            await Reject(() => { UpdateRelease.Parse(ReleaseJson().Replace("https://github.com/", "http://github.com/"), current); return Task.CompletedTask; });
            await Reject(() => { UpdateRelease.Parse(ReleaseJson().Replace(UpdateRelease.Repository, "someone/other"), current); return Task.CompletedTask; });
            await Reject(() => { UpdateRelease.Parse(ReleaseJson().Replace("https://github.com/", "https://github.com.attacker.test/"), current); return Task.CompletedTask; });
        });
        await Check("An executable without a checksum asset is refused", async () => {
            await Reject(() => { UpdateRelease.Parse(ReleaseJson().Replace("SHA256SUMS.txt", "missing.txt"), current); return Task.CompletedTask; });
        });
        byte[] bytes = new byte[2048]; bytes[0] = (byte)'M'; bytes[1] = (byte)'Z'; bytes[300] = 42;
        string hash = Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
        string manifest = hash + "  DeckPilot.exe\r\n" + new string('0', 64) + "  DeckPilot-win-x64.zip\r\n";
        await Check("Checksum parsing selects exactly the Windows executable", () => {
            Expect(UpdateRelease.ExecutableHash(manifest) == hash);
            Expect(UpdateRelease.ExecutableHash(hash.ToUpperInvariant() + " *DeckPilot.exe\n") == hash); return Task.CompletedTask;
        });
        await Check("Missing and duplicate checksum entries are refused", async () => {
            await Reject(() => { UpdateRelease.ExecutableHash(new string('0', 64) + " DeckPilot-win-x64.zip"); return Task.CompletedTask; });
            await Reject(() => { UpdateRelease.ExecutableHash(manifest + manifest); return Task.CompletedTask; });
        });
        string directory = Path.Combine(Path.GetTempPath(), "DeckPilot-v2-check-" + Guid.NewGuid());
        Directory.CreateDirectory(directory);
        try
        {
            var release = UpdateRelease.Parse(ReleaseJson(), current)!;
            HttpResponseMessage Reply(string url, byte[]? content = null) => new(HttpStatusCode.OK) {
                Content = new ByteArrayContent(url.EndsWith("SHA256SUMS.txt") ? Encoding.UTF8.GetBytes(manifest) : content ?? bytes)
            };
            await Check("A verified download reports progress and preserves the exact bytes", async () => {
                using var client = new ReleaseClient(new FakeHandler(request => Reply(request.RequestUri!.AbsolutePath)));
                string path = Path.Combine(directory, "valid"); var progress = new InlineProgress();
                string downloadedHash = await client.DownloadAsync(release, path, progress, default);
                Expect(downloadedHash == hash && progress.Last == 1);
                Expect(File.ReadAllBytes(Path.Combine(path, "DeckPilot.exe")).SequenceEqual(bytes));
            });
            await Check("A checksum mismatch removes the damaged download", async () => {
                byte[] corrupted = bytes.ToArray(); corrupted[400] = 1;
                using var client = new ReleaseClient(new FakeHandler(request => Reply(request.RequestUri!.AbsolutePath, corrupted)));
                string path = Path.Combine(directory, "corrupted");
                await Reject(() => client.DownloadAsync(release, path, null, default));
                Expect(!File.Exists(Path.Combine(path, "DeckPilot.exe")));
            });
            await Check("A truncated streaming download is rejected and removed", async () => {
                using var client = new ReleaseClient(new FakeHandler(request => request.RequestUri!.AbsolutePath.EndsWith("SHA256SUMS.txt")
                    ? Reply(request.RequestUri.AbsolutePath) : new(HttpStatusCode.OK) { Content = new UnknownLengthContent(bytes[..1000]) }));
                string path = Path.Combine(directory, "truncated");
                await Reject(() => client.DownloadAsync(release, path, null, default));
                Expect(!File.Exists(Path.Combine(path, "DeckPilot.exe")));
            });
            await Check("A changed HTTP content length is rejected", async () => {
                using var client = new ReleaseClient(new FakeHandler(request => Reply(request.RequestUri!.AbsolutePath, bytes[..1000])));
                await Reject(() => client.DownloadAsync(release, Path.Combine(directory, "length"), null, default));
            });
            await Check("A cancelled download leaves the installed executable untouched", async () => {
                using var client = new ReleaseClient(new FakeHandler(request => Reply(request.RequestUri!.AbsolutePath)));
                using var cancel = new CancellationTokenSource(); cancel.Cancel();
                bool cancelled = false;
                try { await client.DownloadAsync(release, Path.Combine(directory, "cancelled"), null, cancel.Token); }
                catch (OperationCanceledException) { cancelled = true; }
                Expect(cancelled);
            });
            await Check("An existing file in a download directory is never overwritten or deleted", async () => {
                string path = Path.Combine(directory, "existing"); Directory.CreateDirectory(path);
                File.WriteAllText(Path.Combine(path, "DeckPilot.exe"), "existing file");
                using var client = new ReleaseClient(new FakeHandler(request => Reply(request.RequestUri!.AbsolutePath)));
                bool refused = false;
                try { await client.DownloadAsync(release, path, null, default); } catch (IOException) { refused = true; }
                Expect(refused && File.ReadAllText(Path.Combine(path, "DeckPilot.exe")) == "existing file");
            });
            await Check("Redirects to another host or unencrypted HTTP are refused", async () => {
                foreach (string destination in new[] { "https://attacker.test/DeckPilot.exe", "http://github.com/DeckPilot.exe" })
                {
                    using var client = new ReleaseClient(new FakeHandler(request => {
                        if (request.RequestUri!.AbsolutePath.EndsWith("SHA256SUMS.txt")) return Reply(request.RequestUri.AbsolutePath);
                        var response = new HttpResponseMessage(HttpStatusCode.Redirect); response.Headers.Location = new(destination); return response;
                    }));
                    await Reject(() => client.DownloadAsync(release, Path.Combine(directory, Guid.NewGuid().ToString()), null, default));
                }
            });
            await Check("A GitHub release asset HTTPS redirect is supported", async () => {
                using var client = new ReleaseClient(new FakeHandler(request => {
                    if (request.RequestUri!.Host == "release-assets.githubusercontent.com") return Reply("exe");
                    if (request.RequestUri.AbsolutePath.EndsWith("SHA256SUMS.txt")) return Reply(request.RequestUri.AbsolutePath);
                    var response = new HttpResponseMessage(HttpStatusCode.Redirect); response.Headers.Location = new("https://release-assets.githubusercontent.com/test"); return response;
                }));
                Expect(await client.DownloadAsync(release, Path.Combine(directory, "redirect"), null, default) == hash);
            });
            await Check("Latest-release checking handles no release and surfaces HTTP errors", async () => {
                using var absent = new ReleaseClient(new FakeHandler(_ => new(HttpStatusCode.NotFound)));
                Expect(await absent.CheckAsync(current) == null);
                using var success = new ReleaseClient(new FakeHandler(_ => new(HttpStatusCode.OK) { Content = new StringContent(ReleaseJson()) }));
                Expect((await success.CheckAsync(current))?.Tag == "v0.2.0");
                using var denied = new ReleaseClient(new FakeHandler(_ => new(HttpStatusCode.Forbidden)));
                bool failed = false; try { await denied.CheckAsync(current); } catch (HttpRequestException) { failed = true; }
                Expect(failed);
            });
            await Check("Non-executable content with a valid checksum is still rejected", async () => {
                byte[] data = bytes.ToArray(); data[0] = 0;
                string otherHash = Convert.ToHexString(SHA256.HashData(data)).ToLowerInvariant();
                using var client = new ReleaseClient(new FakeHandler(request => new(HttpStatusCode.OK) { Content = new ByteArrayContent(request.RequestUri!.AbsolutePath.EndsWith("SHA256SUMS.txt") ? Encoding.UTF8.GetBytes(otherHash + "  DeckPilot.exe") : data) }));
                await Reject(() => client.DownloadAsync(release, Path.Combine(directory, "not-exe"), null, default));
            });
            string target = Path.Combine(directory, "installed.exe"), staged = Path.Combine(directory, "new.exe"), backup = Path.Combine(directory, "previous.exe");
            await Check("Replacement keeps the previous executable until health confirmation", () => {
                File.WriteAllText(target, "old"); File.WriteAllText(staged, "new");
                var transaction = new UpdateTransaction(target, staged, backup); transaction.Replace();
                Expect(File.ReadAllText(target) == "new" && File.ReadAllText(backup) == "old");
                transaction.Rollback(); Expect(File.ReadAllText(target) == "old" && !File.Exists(backup));
                return Task.CompletedTask;
            });
            await Check("Successful startup commits the update and removes only its backup", () => {
                var transaction = new UpdateTransaction(target, staged, backup); transaction.Replace(); transaction.Commit();
                Expect(File.ReadAllText(target) == "new" && !File.Exists(backup) && File.ReadAllText(staged) == "new"); return Task.CompletedTask;
            });
            await Check("A missing download cannot damage the current installation", () => {
                var transaction = new UpdateTransaction(target, Path.Combine(directory, "missing.exe"), backup);
                bool failed = false; try { transaction.Replace(); } catch (IOException) { failed = true; }
                Expect(failed && File.ReadAllText(target) == "new" && !File.Exists(backup)); return Task.CompletedTask;
            });
            await Check("An existing backup is protected from replacement", () => {
                File.WriteAllText(backup, "saved");
                var transaction = new UpdateTransaction(target, staged, backup);
                bool failed = false; try { transaction.Replace(); } catch (IOException) { failed = true; }
                Expect(failed && File.ReadAllText(backup) == "saved" && File.ReadAllText(target) == "new"); File.Delete(backup); return Task.CompletedTask;
            });
            if (OperatingSystem.IsWindows())
                await Check("A Windows file lock preserves the installed file when replacement fails", () => {
                    using var locked = new FileStream(target, FileMode.Open, FileAccess.Read, FileShare.Read);
                    var transaction = new UpdateTransaction(target, staged, backup);
                    bool failed = false; try { transaction.Replace(); } catch (IOException) { failed = true; }
                    Expect(failed && !transaction.Replaced && !File.Exists(backup)); return Task.CompletedTask;
                });
            else Console.WriteLine("SKIP Windows file-lock check (executed by Windows CI)");
            await Check("Application choice and update preferences survive a settings round-trip", () => {
                var settings = new Settings { AutoCheckUpdates = false, UpdateSnoozedUntil = DateTimeOffset.UtcNow.AddDays(1) };
                settings.ActiveProfile.Buttons[0] = new() { Kind = ActionKind.Application, ApplicationName = "Calculatrice", Target = ApplicationTarget.ForAppId("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App") };
                var store = new SettingsStore(Path.Combine(directory, "preferences.json")); store.Save(settings);
                var loaded = store.Load();
                Expect(!loaded.AutoCheckUpdates && loaded.UpdateSnoozedUntil == settings.UpdateSnoozedUntil);
                Expect(loaded.ActiveProfile.Buttons[0].ApplicationName == "Calculatrice"); return Task.CompletedTask;
            });
            await Check("Old settings gain update defaults without losing profiles or calibration", () => {
                var settings = new Settings { KeyMap = Enumerable.Range(Calibration.FirstKey, 12).ToList() };
                settings.ActiveProfile.Name = "Mon profil existant";
                var node = System.Text.Json.Nodes.JsonNode.Parse(JsonSerializer.Serialize(settings))!;
                node.AsObject().Remove("AutoCheckUpdates"); node.AsObject().Remove("UpdateSnoozedUntil");
                foreach (var button in node["Profiles"]![0]!["Buttons"]!.AsArray()) button!.AsObject().Remove("ApplicationName");
                string path = Path.Combine(directory, "legacy.json"); File.WriteAllText(path, node.ToJsonString());
                var loaded = new SettingsStore(path).Load();
                Expect(loaded.AutoCheckUpdates && loaded.IsCalibrated && loaded.ActiveProfile.Name == "Mon profil existant"); return Task.CompletedTask;
            });
        }
        finally { Directory.Delete(directory, recursive: true); }
        return count;
    }
    private sealed class InlineProgress : IProgress<double> { public double Last; public void Report(double value) => Last = value; }
    private sealed class FakeHandler(Func<HttpRequestMessage, HttpResponseMessage> respond) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation) { cancellation.ThrowIfCancellationRequested(); return Task.FromResult(respond(request)); }
    }
    private sealed class UnknownLengthContent(byte[] data) : HttpContent
    {
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) => stream.WriteAsync(data).AsTask();
        protected override bool TryComputeLength(out long length) { length = 0; return false; }
    }
}
