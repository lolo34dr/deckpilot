using DeckPilot.Core;
using DeckPilot.Windows;

public static class ApplicationCatalogChecks
{
    public static async Task<int> RunAsync()
    {
        int passed = 0;
        async Task Check(string name, Func<Task> test) { await test(); passed++; Console.WriteLine("PASS " + name); }
        void Expect(bool condition) { if (!condition) throw new Exception("Application catalog cache assertion failed"); }
        async Task Cancelled(Task task) { bool cancelled = false; try { await task; } catch (OperationCanceledException) { cancelled = true; } Expect(cancelled); }
        ApplicationCatalogResult Catalog(string name = "Calculator", string? warning = null) => new(new[] { new InstalledApplication(name, "appsfolder:test", "Windows") }, warning);
        TaskCompletionSource<ApplicationCatalogResult> Pending() => new(TaskCreationOptions.RunContinuationsAsynchronously);

        await Check("Concurrent application requests share discovery while each caller keeps its own cancellation", async () =>
        {
            int calls = 0; var source = Pending(); var cache = new ApplicationCatalogCache(() => { calls++; return source.Task; });
            using var cancelled = new CancellationTokenSource(); cancelled.Cancel();
            await Cancelled(cache.GetAsync(cancelled.Token)); Expect(calls == 0);
            using var firstLifetime = new CancellationTokenSource();
            var first = cache.GetAsync(firstLifetime.Token); var second = cache.GetAsync(CancellationToken.None);
            Expect(calls == 1); firstLifetime.Cancel(); await Cancelled(first);
            source.SetResult(Catalog()); var result = await second.WaitAsync(TimeSpan.FromSeconds(3));
            Expect(result.Applications[0].Name == "Calculator" && calls == 1);
            await Cancelled(cache.GetAsync(cancelled.Token));
            Expect(ReferenceEquals(result, await cache.GetAsync(CancellationToken.None)) && calls == 1);
        });
        await Check("Application snapshots expire after five minutes without depending on wall-clock changes", async () =>
        {
            int calls = 0; var clock = new TestClock();
            var cache = new ApplicationCatalogCache(() => Task.FromResult(Catalog("Catalog " + ++calls)), clock);
            var original = await cache.GetAsync(CancellationToken.None);
            clock.Advance(TimeSpan.FromMinutes(5) - TimeSpan.FromTicks(1));
            Expect(ReferenceEquals(original, await cache.GetAsync(CancellationToken.None)) && calls == 1);
            clock.Advance(TimeSpan.FromTicks(1));
            Expect((await cache.GetAsync(CancellationToken.None)).Applications[0].Name == "Catalog 2" && calls == 2);
        });
        await Check("Explicit catalog refresh bypasses a fresh cache and overlapping refreshes coalesce", async () =>
        {
            int calls = 0; var source = Pending();
            var cache = new ApplicationCatalogCache(() => ++calls == 1 ? Task.FromResult(Catalog("Old")) : source.Task);
            var original = await cache.GetAsync(CancellationToken.None);
            var first = cache.GetAsync(CancellationToken.None, true); var second = cache.GetAsync(CancellationToken.None, true);
            Expect(calls == 2 && !first.IsCompleted && !second.IsCompleted);
            Expect(ReferenceEquals(original, await cache.GetAsync(CancellationToken.None)));
            source.SetResult(Catalog("New"));
            Expect((await first.WaitAsync(TimeSpan.FromSeconds(3))).Applications[0].Name == "New");
            Expect(ReferenceEquals(await first, await second) && calls == 2);
        });
        await Check("Failed synchronous catalog refreshes retain a good snapshot and allow the next refresh", async () =>
        {
            int calls = 0;
            var cache = new ApplicationCatalogCache(() => ++calls == 2 ? throw new IOException("Discovery failed") : Task.FromResult(Catalog("Catalog " + calls)));
            var original = await cache.GetAsync(CancellationToken.None);
            bool failed = false; try { await cache.GetAsync(CancellationToken.None, true); } catch (IOException) { failed = true; }
            Expect(failed && ReferenceEquals(original, await cache.GetAsync(CancellationToken.None)));
            Expect((await cache.GetAsync(CancellationToken.None, true)).Applications[0].Name == "Catalog 3" && calls == 3);
        });
        await Check("Partial application catalogs expire after fifteen seconds instead of five minutes", async () =>
        {
            int calls = 0; var clock = new TestClock();
            var cache = new ApplicationCatalogCache(() => Task.FromResult(Catalog("Partial " + ++calls, "Windows list unavailable")), clock);
            var original = await cache.GetAsync(CancellationToken.None); clock.Advance(TimeSpan.FromSeconds(14));
            Expect(ReferenceEquals(original, await cache.GetAsync(CancellationToken.None)));
            clock.Advance(TimeSpan.FromSeconds(1));
            Expect((await cache.GetAsync(CancellationToken.None)).Applications[0].Name == "Partial 2" && calls == 2);
        });
        await Check("Cached application snapshots cannot be mutated by the factory or a consumer", async () =>
        {
            var source = new List<InstalledApplication> { new("Original", "appsfolder:test", "Windows") };
            var cache = new ApplicationCatalogCache(() => Task.FromResult(new ApplicationCatalogResult(source, null)));
            var result = await cache.GetAsync(CancellationToken.None); source.Clear();
            bool refused = false;
            try { ((IList<InstalledApplication>)result.Applications).Clear(); } catch (NotSupportedException) { refused = true; }
            Expect(refused && result.Applications.Count == 1 && result.Applications[0].Name == "Original");
        });
        return passed;
    }
    private sealed class TestClock : TimeProvider
    {
        private long timestamp;
        public override long TimestampFrequency => TimeSpan.TicksPerSecond;
        public override long GetTimestamp() => timestamp;
        public void Advance(TimeSpan amount) => timestamp += amount.Ticks;
    }
}
