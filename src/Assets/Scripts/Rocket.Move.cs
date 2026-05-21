using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket
{
    private Quadrant? _quadrantIntention;

    private void MoveRocket(MoveRocketEvent evt)
    {
        var result = _gameGrid.Move(_currQuadrant, evt);
        if (!result.IsOk(out var quadrantIntention, out var error))
        {
            Logger.Error?.Log(error);
            return;
        }

        Logger.Debug?.Log($"Going to {quadrantIntention}");
        _currQuadrant = quadrantIntention;
    }

    private void HandleMoveIntention()
    {
        // throw new System.NotImplementedException();
    }
}
