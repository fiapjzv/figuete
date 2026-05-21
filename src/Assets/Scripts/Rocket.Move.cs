public partial class Rocket
{
    private Quadrant? _quadrantIntention;

    private void MoveRocket(Controls.RailShooter.MoveRocketEvent evt)
    {
        var result = _gameGrid.MoveTo(evt);
        if (!result.IsOk(out var quadrantIntention, out var error))
        {
            Logger.Error?.Log(error);
            return;
        }

        // _transformIntention = transformIntention;
        Logger.Debug?.Log($"Going to {quadrantIntention}");
        _currQuadrant = quadrantIntention;
    }

    private void HandleMoveIntention()
    {
        // throw new System.NotImplementedException();
    }
}
