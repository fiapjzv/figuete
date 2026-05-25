using UnityEngine;

public partial class Asteroid : GameBehavior
{
    public Vector3 Velocity { get; set; }
    public Vector3 Rotation { get; set; }

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
}
