using UnityEngine;
using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket
{
    [SerializeField]
    private float _dodgeTimeInSecs = 0.5f;

    private MoveCommand? _currMovement;

    private void SteerRocket(MoveRocketEvent evt)
    {
        var result = _gameGrid.Move(_currQuadrant, evt);
        if (!result.IsOk(out var targetQuadrant, out var error))
        {
            Logger.Error?.Log(error);
            return;
        }

        Logger.Debug?.Log($"Going to {targetQuadrant}");
        _currMovement = new MoveCommand(transform, targetQuadrant, _dodgeTimeInSecs);
    }

    private void MoveRocketTo()
    {
        if (_currMovement is null)
        {
            return;
        }

        var moveCmd = _currMovement.Value;

        var (targetPos, targetRot) = moveCmd.TargetQuadrant.Transform();
        var isMoving = transform.MoveTowards(targetPos, moveCmd.TranslateSpeed);
        var isRotating = transform.RotateTo(targetRot, moveCmd.RotateSpeed);

        if (isMoving || isRotating)
        {
            Logger.Debug?.Log($"Moving to {moveCmd} @ {transform}");
            return;
        }

        Logger.Debug?.Log($"Arrived at new quadrant {moveCmd.TargetQuadrant}");
        _currQuadrant = moveCmd.TargetQuadrant;
        _currMovement = null;
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
    }
}
