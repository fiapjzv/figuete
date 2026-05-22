using UnityEngine;
using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket
{
    [SerializeField]
    private float _dodgeTimeInSecs = 0.5f;

    private Vector3 _stablePosition;
    private Quaternion _stableRot;

    private MoveCommand? _currMoveCmd;

    private void SteerRocket(MoveRocketEvent evt)
    {
        var result = _gameGrid.Move(_currQuadrant, evt);
        if (!result.IsOk(out var targetQuadrant, out var error))
        {
            Logger.Error?.Log(error);
            return;
        }

        Logger.Debug?.Log($"Going to {targetQuadrant}");
        _currMoveCmd = new MoveCommand(transform, targetQuadrant, _dodgeTimeInSecs);
        _currQuadrant = targetQuadrant;
    }

    private (Vector3 pos, Quaternion rot, bool arrived) MoveRocketTo(
        MoveCommand moveCmd,
        Vector3 stablePos,
        Quaternion stableRot
    )
    {
        var (targetPos, targetRot, posSpeed, rotSpeed) = moveCmd;
        var isMoving = stablePos.MoveTowards(targetPos, posSpeed, out var newPos);
        var isRotating = stableRot.RotateTo(targetRot, rotSpeed, out var newRot);

        Logger.Debug?.Log($"Moving to {moveCmd} @ {transform}");
        return (newPos, newRot, arrived: !isMoving && !isRotating);
    }

    public readonly struct MoveCommand
    {
        public float TranslateSpeed { get; }
        public float RotateSpeed { get; }
        public Quadrant TargetQuadrant { get; }

        public MoveCommand(Transform transform, Quadrant target, float moveTimeInSecs)
        {
            var distance = Vector3.Distance(transform.position, target.Pos);
            var angle = Quaternion.Angle(transform.rotation, target.Rot);

            if (moveTimeInSecs < 0.001)
            {
                Guard.Panic("Move time is too small");
            }

            TargetQuadrant = target;
            TranslateSpeed = distance / moveTimeInSecs;
            RotateSpeed = angle / moveTimeInSecs;
        }

        public override string ToString()
        {
            return TargetQuadrant.ToString();
        }

        public void Deconstruct(out Vector3 targetPos, out Quaternion targetRot, out float posSpeed, out float rotSpeed)
        {
            (targetPos, targetRot) = TargetQuadrant.Transform();
            posSpeed = TranslateSpeed;
            rotSpeed = RotateSpeed;
        }
    }
}
