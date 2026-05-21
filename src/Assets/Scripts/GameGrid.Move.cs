using System;

public partial class GameGrid
{
    // NOTE: zero indexed current rocket position.
    private int _currCol = 1;
    private int _currRow = 1;

    public Quadrant CurrQuadrant()
    {
        return TransformFor(_currCol, _currRow);
    }

    public Result<Quadrant> MoveTo(Controls.RailShooter.MoveRocketEvent evt)
    {
        var (desiredCol, desiredRow) = evt switch
        {
            Controls.RailShooter.MoveRocketEvent.UP => (_currCol, _currRow - 1),
            Controls.RailShooter.MoveRocketEvent.DOWN => (_currCol, _currRow + 1),
            Controls.RailShooter.MoveRocketEvent.LEFT => (_currCol - 1, _currRow),
            Controls.RailShooter.MoveRocketEvent.RIGHT => (_currCol + 1, _currRow),
            _ => throw new ArgumentOutOfRangeException(nameof(evt), evt, null),
        };

        if (!Valid(desiredCol, desiredRow))
        {
            return Result.Err<Quadrant>($"Could not move to ({desiredCol}, {desiredRow}). Already on the grid bounds.");
        }

        _currCol = desiredCol;
        _currRow = desiredRow;
        return Result.Ok(TransformFor(desiredCol, desiredRow));
    }

    private bool Valid(int desiredCol, int desiredRow)
    {
        return desiredCol >= 0 && desiredRow >= 0 && desiredCol < Columns && desiredRow < Rows;
    }
}
