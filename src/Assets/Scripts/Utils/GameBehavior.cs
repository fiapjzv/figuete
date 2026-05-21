using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Base class for components. Integrates logging, events, and automatic unsubscription.</summary>
public abstract partial class GameBehavior : MonoBehaviour
{
    protected IGameLogger Logger = null!;
    protected IEvents Events = null!;

    private readonly List<IDisposable> _subs = new();

    public void Awake()
    {
        Logger = Service.Get<IGameLogger>();
        Events = Service.Get<IEvents>();

        Init();
    }

    public void OnEnable()
    {
        _subs.AddRange(SubscribeEvents());
        WhenEnabled();
    }

    public void OnDisable()
    {
        EnsureUnsub();
        WhenDisabled();
    }

    public void OnDestroy()
    {
        EnsureUnsub();
        WhenDestroyed();
    }

    /// <summary>Entry point for initializing references and custom Awake logic.</summary>
    protected virtual void Init() { }

    /// <summary>Return the event subscriptions you wish to make in the component's OnEnable.</summary>
    protected virtual IEnumerable<IDisposable> SubscribeEvents() => Enumerable.Empty<IDisposable>();

    /// <summary>Custom logic that runs after subscriptions are made.</summary>
    protected virtual void WhenEnabled() { }

    /// <summary>Custom logic that runs after canceling subscriptions.</summary>
    protected virtual void WhenDisabled() { }

    /// <summary>Custom logic that runs after canceling subscriptions.</summary>
    protected virtual void WhenDestroyed() { }

    private void EnsureUnsub()
    {
        if (_subs.Count == 0)
        {
            return;
        }

        Logger.Debug?.Log($"Unsubscribing {_subs.Count} game events");
        foreach (var sub in _subs)
        {
            sub.Dispose();
        }
        _subs.Clear();
    }
}