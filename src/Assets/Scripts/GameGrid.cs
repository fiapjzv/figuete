using System;
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
    BaseTransform CurrBaseTransform();
}

public partial class GameGrid : IGameGrid
{
    private readonly Camera _cam;

    public int Columns { get; } = 3;
    public int Rows { get; } = 2;

    private float _distanceFromCamera = 10f;
    private int _currColumn = 1;
    private int _currRow = 0;

    private readonly static BaseTransform[] _quadrantTransforms =
    {
        new(new Vector3(0f, -3f, 0f), Quaternion.Euler(0, 90, 80)),
    };

    public GameGrid(Camera cam)
    {
        _cam = cam;
    }

    public BaseTransform CurrBaseTransform()
    {
        return _quadrantTransforms[0];
    }
}

public readonly struct BaseTransform
{
    public Vector3 BasePosition { get; }
    public Quaternion BaseRotation { get; }

    public BaseTransform(Vector3 basePosition, Quaternion baseRotation)
    {
        BasePosition = basePosition;
        BaseRotation = baseRotation;
    }

    public void Deconstruct(out Vector3 basePosition, out Quaternion baseRotation)
    {
        basePosition = BasePosition;
        baseRotation = BaseRotation;
    }
}
