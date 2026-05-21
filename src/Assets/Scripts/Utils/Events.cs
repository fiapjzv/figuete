using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Event hub used to decouple systems using the pattern.
/// <example>
/// <code>
/// var events = Events.Instance;
///
/// var sub = events.Subscribe&lt;CardPlayed&gt;(evt =&gt;
/// {
///     Debug.Log($&quot;Card played: {evt.Card}&quot;);
/// });
///
/// events.Publish(new CardPlayed(card));
///
/// sub.Dispose(); // optionally
/// </code>
/// </example>
/// </summary>
/// <remarks>
/// Prefer events that are a <pre>readonly struct</pre> to avoid heap allocations during publication.
/// </remarks>
public interface IEvents
{
    /// <summary>Publishes an event to all subscribers of the event type.</summary>
    /// <inheritdoc cref="IEvents"/>
    void Publish<TEvt>(in TEvt evt);

    /// <summary>
    /// Event subscription.<br />
    /// Registers a <paramref name="handler"/>, which is a function to be executed when an event occurs.
    /// Returns an <see cref="IDisposable"/> that removes the subscription when called.
    /// </summary>
    /// <inheritdoc cref="IEvents"/>
    IDisposable Subscribe<TEvt>(Action<TEvt> handler);

    /// <inheritdoc cref="IEvents.Subscribe{TEvt}(Action{TEvt})"/>
    IDisposable Subscribe<TEvt>(Func<TEvt, Task> handler);
}

/// <inheritdoc cref="IEvents"/>
public abstract partial class Events : IEvents
{
    protected readonly IGameLogger _logger;

    // NOTE: making it thread-safe by locking the handlers dictionary only during Subscribe
    private readonly object _subLock = new();
    private Dictionary<Type, object> _syncHandlers = new();
    private Dictionary<Type, object> _asyncHandlers = new();

    protected Events(IGameLogger? logger = null)
    {
        _logger = logger ?? NullLogger.Instance;
    }
}
