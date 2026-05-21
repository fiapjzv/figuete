using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Rocket : GameBehavior
{
    [SerializeField]
    private Transform _tipPivot = null!;

    private IGameGrid _gameGrid = null!;

    private Quadrant _currQuadrant;

    protected override void Init()
    {
        _gameGrid = Service.Get<IGameGrid>();
        PopulateWobbleRandomOffsets();
        _currQuadrant = _gameGrid.CurrQuadrant();
        Logger.Debug?.Log($"Rocket starting @ {_currQuadrant}");
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<Controls.RailShooter.MoveRocketEvent>(MoveRocket);
    }

    public void Update()
    {
        WobbleAroundQuadrant();
        HandleMoveIntention();
    }
}
