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

public partial class GameGrid : MonoBehaviour, IGameGrid
{
    [SerializeField]
    private Camera _cam = null!;

    public int Columns { get; } = 3;
    public int Rows { get; } = 2;

    private float _distanceFromCamera = 10f;
    private int _currColumn = 1;
    private int _currRow = 0;

    public BaseTransform CurrBaseTransform()
    {
        throw new NotImplementedException();
    }
}

public readonly struct BaseTransform
{
    public Vector3 BasePosition { get; }
    public Vector3 BaseRotation { get; }

    public BaseTransform(Vector3 basePosition, Vector3 baseRotation)
    {
        BasePosition = basePosition;
        BaseRotation = baseRotation;
    }

    public void Deconstruct(out Vector3 basePosition, out Vector3 baseRotation)
    {
        basePosition = BasePosition;
        baseRotation = BaseRotation;
    }
}
