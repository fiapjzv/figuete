using System;
using System.Collections.Generic;
using UnityEngine;
using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket : GameBehavior
{
    private IGameGrid _gameGrid = null!;

    [SerializeField]
    private Transform _tipPivot = null!;

    private State _currState;
    private Quadrant _currQuadrant;

    protected override void Init()
    {
        _gameGrid = Service.Get<IGameGrid>();
        _currQuadrant = _gameGrid.Quadrant(row: 1, col: 1);

        GameplayParamsInit();
        StartAnimationParamsInit();
        Logger.Debug?.Log($"Rocket starting @ {_currQuadrant}");
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<MoveRocketEvent>(SteerRocket);
        yield return Events.Subscribe<RocketInPositionToStart>(StartGamePlay);
    }

    public void Update()
    {
        switch (_currState)
        {
            case State.START_ANIMATION:
                HandleStartAnimation();
                break;
            case State.GAMEPLAY:
                HandleGameplay();
                break;
            case State.GAME_OVER_ANIMATION:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
