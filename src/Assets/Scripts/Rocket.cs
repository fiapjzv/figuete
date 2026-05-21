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
        UpdateStableTransform(_currQuadrant);
        Logger.Debug?.Log($"Rocket starting @ {_currQuadrant}");
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<MoveRocketEvent>(SteerRocket);
    }

    public void Update()
    {
        if (_currMovement is not null)
        {
            var (newPos, newRot, arrived) = MoveRocketTo(_currMovement.Value, _stablePosition, _stableRot);

            if (arrived)
            {
                Logger.Debug?.Log($"Arrived at new quadrant {_currMovement.Value.TargetQuadrant}");
                _currQuadrant = _currMovement.Value.TargetQuadrant;
                _currMovement = null;
            }
            (_stablePosition, _stableRot) = (newPos, newRot);
        }

        var (unstablePos, unstableRot) = WobbleAround(_stablePosition, _stableRot);

        transform.position = unstablePos;
        transform.rotation = unstableRot;
    }
}
