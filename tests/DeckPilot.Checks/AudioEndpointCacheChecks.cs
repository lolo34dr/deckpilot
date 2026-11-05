using DeckPilot.Windows;

internal static class AudioEndpointCacheChecks
{
    public static async Task<int> RunAsync()
    {
        int count = 0;
        async Task Check(string name, Func<Task> test)
        {
            await test(); count++; Console.WriteLine("PASS " + name);
        }
        static void Expect(bool value) { if (!value) throw new Exception("Audio endpoint cache regression"); }

        await Check("Concurrent audio device refreshes share one background scan and reuse its snapshot", async () =>
        {
            int scans = 0;
            var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var release = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
            var cache = new AsyncSnapshotCache<Snapshot>(async () =>
            {
                Interlocked.Increment(ref scans); started.TrySetResult();
                return new(await release.Task);
            }, TimeSpan.FromSeconds(30));
            var first = cache.GetAsync(CancellationToken.None);
            await started.Task;
            var second = cache.GetAsync(CancellationToken.None);
            release.SetResult(12);
            Expect((await first).Value == 12 && (await second).Value == 12);
            Expect((await cache.GetAsync(CancellationToken.None)).Value == 12 && scans == 1);
        });

        await Check("Manual audio refresh bypasses the cached snapshot", async () =>
        {
            int scans = 0;
            var cache = new AsyncSnapshotCache<Snapshot>(() => Task.FromResult(new Snapshot(++scans)), TimeSpan.FromMinutes(1));
            Expect((await cache.GetAsync(CancellationToken.None)).Value == 1);
            Expect((await cache.GetAsync(CancellationToken.None, forceRefresh: true)).Value == 2);
            Expect(scans == 2);
        });

        await Check("Audio endpoint snapshots expire without blocking the UI", async () =>
        {
            int scans = 0; var clock = new FakeClock();
            var cache = new AsyncSnapshotCache<Snapshot>(() => Task.FromResult(new Snapshot(++scans)), TimeSpan.FromSeconds(10), clock);
            await cache.GetAsync(CancellationToken.None); clock.Advance(TimeSpan.FromSeconds(11));
            Expect((await cache.GetAsync(CancellationToken.None)).Value == 2 && scans == 2);
        });

        await Check("Canceling one refresh waiter does not cancel another window's shared scan", async () =>
        {
            int scans = 0;
            var release = new TaskCompletionSource<Snapshot>(TaskCreationOptions.RunContinuationsAsynchronously);
            var cache = new AsyncSnapshotCache<Snapshot>(() => { Interlocked.Increment(ref scans); return release.Task; }, TimeSpan.FromSeconds(30));
            using var cancelled = new CancellationTokenSource();
            var first = cache.GetAsync(cancelled.Token); cancelled.Cancel();
            try { await first; throw new Exception("Canceled waiter unexpectedly completed"); }
            catch (OperationCanceledException) { }
            var second = cache.GetAsync(CancellationToken.None); release.SetResult(new Snapshot(7));
            Expect((await second).Value == 7 && scans == 1);
        });

        return count;
    }

    private sealed record Snapshot(int Value);

    private sealed class FakeClock : TimeProvider
    {
        private long timestamp;
        public override long TimestampFrequency => TimeSpan.TicksPerSecond;
        public override long GetTimestamp() => timestamp;
        public void Advance(TimeSpan duration) => timestamp += duration.Ticks;
    }
}
