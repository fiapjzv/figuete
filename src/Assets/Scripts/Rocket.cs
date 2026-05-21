using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Rocket : GameBehavior
{
    [SerializeField]
    private Transform _tipPivot = null!;

    private IGameGrid _gameGrid = null!;

    protected override void Init()
    {
        _gameGrid = Service.Get<IGameGrid>();
        PopulateWobbleRandomOffsets();
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<Controls.RailShooter.MoveRocketEvent>(MoveRocket);
    }

    public void Update()
    {
        WobbleAroundQuadrantCenter();
    }
}
