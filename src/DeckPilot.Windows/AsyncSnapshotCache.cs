using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeckPilot.Windows;

// Shares an in-progress background discovery and reuses its immutable snapshot
// for a short period. A canceled window wait does not cancel discovery for other
// callers.
internal sealed class AsyncSnapshotCache<T> where T : class
{
    private readonly object gate = new();
    private readonly Func<Task<T>> discover;
    private readonly TimeProvider clock;
    private readonly TimeSpan lifetime;
    private T? snapshot;
    private long publishedAt;
    private Task<T>? inFlight;

    public AsyncSnapshotCache(Func<Task<T>> discover, TimeSpan lifetime, TimeProvider? clock = null)
    {
        this.discover = discover ?? throw new ArgumentNullException(nameof(discover));
        if (lifetime <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(lifetime));
        this.lifetime = lifetime;
        this.clock = clock ?? TimeProvider.System;
    }

    public async Task<T> GetAsync(CancellationToken cancellation, bool forceRefresh = false)
    {
        cancellation.ThrowIfCancellationRequested();
        TaskCompletionSource<T>? launch = null;
        Task<T> pending;
        lock (gate)
        {
            if (inFlight != null) pending = inFlight;
            else if (!forceRefresh && snapshot != null && clock.GetElapsedTime(publishedAt) < lifetime) return snapshot;
            else
            {
                launch = new(TaskCreationOptions.RunContinuationsAsynchronously);
                inFlight = pending = launch.Task;
            }
        }

        if (launch != null) _ = DiscoverAsync(launch);
        return await pending.WaitAsync(cancellation).ConfigureAwait(false);
    }

    private async Task DiscoverAsync(TaskCompletionSource<T> completion)
    {
        try
        {
            var result = await discover().ConfigureAwait(false);
            lock (gate)
            {
                snapshot = result;
                publishedAt = clock.GetTimestamp();
                inFlight = null;
            }
            completion.TrySetResult(result);
        }
        catch (Exception error)
        {
            lock (gate) inFlight = null;
            completion.TrySetException(error);
            _ = completion.Task.Exception;
        }
    }
}
