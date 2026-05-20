using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <inheritdoc cref="IEvents"/>
/// <remarks>In unity, we need to ensure that the events happen on the main thread.</remarks>
public class UnityEvents : Events
{
    private readonly SynchronizationContext _syncContext;

    /// <inheritdoc cref="UnityEvents" />
    public UnityEvents(SynchronizationContext syncContext, IGameLogger? logger = null)
        : base(logger)
    {
        _syncContext = syncContext;
    }

    protected override void ScheduleSyncHandlersRun<TEvt>(List<Action<TEvt>> handlers, TEvt evt) =>
        _syncContext.Post(_ => SyncHandlersRun(handlers, evt), null);

    protected override void ScheduleAsyncHandlersRun<TEvt>(List<Func<TEvt, Task>> handlers, TEvt evt) =>
        _syncContext.Post(_ => _ = AsyncHandlersRun(handlers, evt), null);
}
