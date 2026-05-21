using UnityEngine;

/// <summary>
/// We divide the game screen into quadrants.
/// The ship is always on a specific quadrant and should wobble around the quadrant's center.
/// </summary>
public interface IGameGrid
{
    /// <summary>How many columns we are dividing the game screen.</summary>
    int Columns { get; }

    /// <summary>How many rows we are dividing the game screen.</summary>
    int Rows { get; }

    /// <summary>
    /// The base position and rotation that the ship should be in on the current quadrant.
    /// </summary>
    Quadrant CurrQuadrant();

    /// <summary>
    /// Returns the <see cref="Quadrant"/> for the desired rocket position based on the current position and the
    /// event direction.
    /// </summary>
    Result<Quadrant> MoveTo(Controls.RailShooter.MoveRocketEvent evt);
}

public partial class GameGrid : IGameGrid
{
    private readonly IGameLogger _logger;

    public GameGrid(IGameLogger logger)
    {
        _logger = logger;
    }

    public int Columns => 3;
    public int Rows => 2;

    private Quadrant TransformFor(int col, int row)
    {
        var index = row * Columns + col;
        _logger.Debug?.Log($"Transform for ({col}, {row}) @ {index}");
        return _quadrantTransforms[index];
    }

    /// <summary>Quadrants matrix indexes start at the top left, going to bottom right.</summary>
    private static readonly Quadrant[] _quadrantTransforms =
    {
        new(new Vector3(-8f, 4f, 0f), Quaternion.Euler(120, 90, 80)),
        new(new Vector3(0f, 4f, 0f), Quaternion.Euler(180, 90, 80)),
        new(new Vector3(8f, 4f, 0f), Quaternion.Euler(-120, 90, 80)),
        new(new Vector3(-8f, -3f, 0f), Quaternion.Euler(30, 90, 80)),
        new(new Vector3(0f, -3f, 0f), Quaternion.Euler(0, 90, 80)),
        new(new Vector3(8f, -3f, 0f), Quaternion.Euler(-30, 90, 80)),
    };
}

public readonly struct Quadrant
{
    public Vector3 Pos { get; }
    public Quaternion Rot { get; }

    public Quadrant(Vector3 pos, Quaternion rot)
    {
        Pos = pos;
        Rot = rot;
    }

    public void Deconstruct(out Vector3 basePosition, out Quaternion baseRotation)
    {
        basePosition = Pos;
        baseRotation = Rot;
    }

    public override string ToString()
    {
        return $"pos: {Pos}, rot: {Rot.eulerAngles}";
    }
}
