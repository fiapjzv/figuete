using UnityEngine;

public partial class Rocket
{
    [SerializeField]
    private float _startPosTimeInSecs = 2f;
    private MoveCommand _startAnimationMove;

    private void HandleStartAnimation()
    {
        var (newPos, newRot, arrived) = MoveRocketTo(_startAnimationMove, _stablePosition, _stableRot);
        (_stablePosition, _stableRot) = (newPos, newRot);
        var (unstablePos, unstableRot) = WobbleAround(_stablePosition, _stableRot);

        transform.position = unstablePos;
        transform.rotation = unstableRot;

        if (!arrived)
        {
            return;
        }

        Logger.Info?.Log(
            $"Rocket in position to start {transform.position} @ {transform.rotation.eulerAngles} "
                + $"=> {_currQuadrant} - {_stablePosition} x {_stableRot.eulerAngles}"
        );

        _stablePosition = _currQuadrant.Pos;
        _stableRot = _currQuadrant.Rot;

        Events.Publish(new RocketInPositionToStart());
    }

    private void StartAnimationParamsInit()
    {
        _startAnimationMove = new MoveCommand(transform, _currQuadrant, _startPosTimeInSecs);
        _stablePosition = transform.transform.position;
        _stableRot = transform.rotation;
    }
}
