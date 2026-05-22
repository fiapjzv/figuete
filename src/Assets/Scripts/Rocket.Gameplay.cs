public partial class Rocket
{
    private void HandleGameplay()
    {
        if (_currMoveCmd is not null)
        {
            var (newPos, newRot, arrived) = MoveRocketTo(_currMoveCmd.Value, _stablePosition, _stableRot);

            if (arrived)
            {
                Logger.Debug?.Log($"Arrived at new quadrant {_currMoveCmd.Value.TargetQuadrant}");
                _currQuadrant = _currMoveCmd.Value.TargetQuadrant;
                _currMoveCmd = null;
            }
            (_stablePosition, _stableRot) = (newPos, newRot);
        }

        var (unstablePos, unstableRot) = WobbleAround(_stablePosition, _stableRot);

        transform.position = unstablePos;
        transform.rotation = unstableRot;
    }

    private void GameplayParamsInit()
    {
        PopulateWobbleRandomOffsets();
    }

    private void StartGamePlay(RocketInPositionToStart _)
    {
        _currState = State.GAMEPLAY;
    }
}
