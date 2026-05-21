using System;
using System.Collections.Generic;
using UnityEngine;
using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket : GameBehavior
{
    private IGameGrid _gameGrid = null!;

    [SerializeField]
    private Transform _tipPivot = null!;
    private Quadrant _currQuadrant;

    protected override void Init()
    {
        _gameGrid = Service.Get<IGameGrid>();
        PopulateWobbleRandomOffsets();

        _currQuadrant = _gameGrid.Quadrant(row: 1, col: 1);
        Logger.Debug?.Log($"Rocket starting @ {_currQuadrant}");
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<MoveRocketEvent>(MoveRocket);
    }

    public void Update()
    {
        WobbleAroundQuadrant();
        HandleMoveIntention();
    }
}
