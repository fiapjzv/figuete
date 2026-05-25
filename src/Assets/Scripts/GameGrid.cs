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

    /// <summary>Returns the <see cref="Quadrant"/> data for a specific position on the grid.</summary>
    Quadrant Quadrant(int row, int col);

    /// <summary>A random quadrant.</summary>
    Quadrant RandomQuadrant();

    /// <summary>
    /// Returns the <see cref="Quadrant"/> for the desired rocket position based on the current position and the
    /// event direction.
    /// </summary>
    Result<Quadrant> Move(Quadrant rocketCurrQuadrant, Controls.RailShooter.MoveRocketEvent evt);
}

public partial class GameGrid : IGameGrid
{
    private readonly IGameLogger _logger;

    public GameGrid(IGameLogger logger)
    {
        _logger = logger;
    }

    public int Rows => 2;
    public int Columns => 3;

    public Quadrant Quadrant(int row, int col)
    {
        var index = row * Columns + col;
        _logger.Debug?.Log($"Quadrant data for ({col}, {row}) @ {index}");
        return _quadrantTransforms[index];
    }

    public Quadrant RandomQuadrant()
    {
        var row = Random.Range(0, Rows);
        var col = Random.Range(0, Columns);
        return Quadrant(row, col);
    }

    /// <summary>Quadrants matrix indexes start at the top left, going to bottom right.</summary>
    private static readonly Quadrant[] _quadrantTransforms =
    {
        new(row: 0, col: 0, new Vector3(-8f, 4f, ROCKET_BASE_PLANE_Z), Quaternion.Euler(120, 90, 80)),
        new(row: 0, col: 1, new Vector3(0f, 4f, ROCKET_BASE_PLANE_Z), Quaternion.Euler(180, 90, 80)),
        new(row: 0, col: 2, new Vector3(8f, 4f, ROCKET_BASE_PLANE_Z), Quaternion.Euler(-120, 90, 80)),
        new(row: 1, col: 0, new Vector3(-8f, -3f, ROCKET_BASE_PLANE_Z), Quaternion.Euler(30, 90, 80)),
        new(row: 1, col: 1, new Vector3(0f, -3f, ROCKET_BASE_PLANE_Z), Quaternion.Euler(0, 90, 80)),
        new(row: 1, col: 2, new Vector3(8f, -3f, ROCKET_BASE_PLANE_Z), Quaternion.Euler(-30, 90, 80)),
    };

    public const float ROCKET_BASE_PLANE_Z = 0f;
    public const float ASTEROIDS_SPAWN_PLANE_Z = 50f;

    public static bool AfterCollisionZone(Vector3 pos) => pos.z < ROCKET_BASE_PLANE_Z;
}
