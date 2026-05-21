using System;
using static Controls.RailShooter.MoveRocketEvent;

public partial class GameGrid
{
    public Result<Quadrant> Move(Quadrant rocketCurrQuadrant, Controls.RailShooter.MoveRocketEvent evt)
    {
        var (row, col) = rocketCurrQuadrant.Indexes();
        var (desiredRow, desiredCol) = evt switch
        {
            UP => (row - 1, col),
            DOWN => (row + 1, col),
            LEFT => (row, col - 1),
            RIGHT => (row, col + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(evt), evt, null),
        };

        return Valid(desiredRow, desiredCol)
            ? Result.Ok(Quadrant(desiredRow, desiredCol))
            : Result.Err<Quadrant>($"Could not move to ({desiredRow}, {desiredCol}). Already on the grid bounds.");
    }

    private bool Valid(int desiredRow, int desiredCol)
    {
        return desiredCol >= 0 && desiredRow >= 0 && desiredCol < Columns && desiredRow < Rows;
    }
}
