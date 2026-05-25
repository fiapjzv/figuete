using UnityEngine;

public partial class Asteroid : GameBehavior
{
    public float LinSpeed { get; private set; }
    public Vector3 Velocity { get; private set; }
    public Vector3 Rotation { get; private set; }

    /// <summary>The quadrant the asteroid is going to collide.</summary>
    public Quadrant TargetQuadrant { get; set; }

    private const float MIN_LIN_SPEED = 5;
    private const float MAX_LIN_SPEED = 10;

    private const float MIN_ANG_SPEED = 0;
    private const float MAX_ANG_SPEED = 90;

    public override string ToString()
    {
        return $"Asteroid Target {TargetQuadrant}; Velocity {Velocity}; Rotation {Rotation};";
    }

    public void RecalculateVelocity(float linSpeed)
    {
        LinSpeed = linSpeed;
        Velocity = (TargetQuadrant.Pos - transform.position).normalized * LinSpeed;
    }

    public void UpdateRotation(Vector3 rotation)
    {
        Rotation = rotation;
    }

    public void Accelerate(float factor)
    {
        Velocity *= factor;
    }
}
