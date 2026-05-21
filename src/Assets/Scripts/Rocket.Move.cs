using UnityEngine;
using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket
{
    [SerializeField]
    private float _dodgeSpeed = 35f;
    private Quadrant? _targetQuadrant;

    private void SteerRocket(MoveRocketEvent evt)
    {
        var result = _gameGrid.Move(_currQuadrant, evt);
        if (!result.IsOk(out var targetQuadrant, out var error))
        {
            Logger.Error?.Log(error);
            return;
        }

        Logger.Debug?.Log($"Going to {targetQuadrant}");
        _targetQuadrant = targetQuadrant;
    }

    private void MoveRocketTo()
    {
        if (_targetQuadrant is null)
        {
            return;
        }

        var targetPos = _targetQuadrant.Value.Pos;
        if (transform.MoveTowards(targetPos, _dodgeSpeed))
        {
            Logger.Debug?.Log($"Moving to {targetPos} @ {transform.position}");
            return;
        }

        Logger.Debug?.Log($"Arrived at new quadrant {_targetQuadrant.Value}");
        _currQuadrant = _targetQuadrant.Value;
        _targetQuadrant = null;
    }
}
