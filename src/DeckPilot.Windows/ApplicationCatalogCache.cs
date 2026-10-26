using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeckPilot.Core;

namespace DeckPilot.Windows;

public sealed record ApplicationCatalogResult(IReadOnlyList<InstalledApplication> Applications, string? Warning);

// One read-only snapshot shared by search and picker windows. Consumers own only
// their wait: closing a picker must not cancel another consumer's discovery.
internal sealed class ApplicationCatalogCache
{
    private readonly object gate = new();
    private readonly Func<Task<ApplicationCatalogResult>> discover;
    private readonly TimeProvider clock;
    private ApplicationCatalogResult? snapshot;
    private long publishedAt;
    private Task<ApplicationCatalogResult>? inFlight;
    private static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan PartialLifetime = TimeSpan.FromSeconds(15);

    public ApplicationCatalogCache(Func<Task<ApplicationCatalogResult>> discover, TimeProvider? clock = null)
    {
        this.discover = discover;
        this.clock = clock ?? TimeProvider.System;
    }

    public async Task<ApplicationCatalogResult> GetAsync(CancellationToken cancellation, bool forceRefresh = false)
    {
        cancellation.ThrowIfCancellationRequested();
        TaskCompletionSource<ApplicationCatalogResult>? launch = null;
        Task<ApplicationCatalogResult> pending;
        lock (gate)
        {
            var lifetime = snapshot?.Warning == null ? Lifetime : PartialLifetime;
            if (!forceRefresh && snapshot != null && clock.GetElapsedTime(publishedAt) < lifetime) return snapshot;
            if (inFlight == null)
            {
                launch = new(TaskCreationOptions.RunContinuationsAsynchronously);
                inFlight = launch.Task;
            }
            pending = inFlight;
        }
        // Publish the placeholder before calling the factory: even a synchronous
        // completion/failure must not leave a completed task stuck in the cache.
        if (launch != null) _ = DiscoverAsync(launch);
        return await pending.WaitAsync(cancellation).ConfigureAwait(false);
    }

    private async Task DiscoverAsync(TaskCompletionSource<ApplicationCatalogResult> completion)
    {
        try
        {
            var result = await discover().ConfigureAwait(false);
            var immutable = new ApplicationCatalogResult(Array.AsReadOnly(result.Applications.ToArray()), result.Warning);
            lock (gate)
            {
                snapshot = immutable;
                publishedAt = clock.GetTimestamp();
                inFlight = null;
            }
            completion.TrySetResult(immutable);
        }
        catch (Exception error)
        {
            lock (gate) inFlight = null;
            completion.TrySetException(error);
            // The last waiting window may have closed before discovery failed.
            _ = completion.Task.Exception;
        }
    }
}
