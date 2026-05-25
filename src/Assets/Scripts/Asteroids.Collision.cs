using UnityEngine;

public partial class Asteroids
{
    private void SwapTargetQuadrants(Asteroid.CollisionEvt collision)
    {
        var (a, b) = collision;
        Logger.Debug?.Log($"Swapping target quadrants for {a} and {b}");
        if (a.TargetQuadrant.Equals(a.TargetQuadrant))
        {
            a.TargetQuadrant = _grid.RandomQuadrant();
            b.TargetQuadrant = _grid.RandomQuadrant();
        }
        else
        {
            (a.TargetQuadrant, b.TargetQuadrant) = (a.TargetQuadrant, b.TargetQuadrant);
        }

        a.RecalculateVelocity(a.LinSpeed * (1 + COLLISION_SPEED_CHANGE));
        b.RecalculateVelocity(b.LinSpeed * (1 - COLLISION_SPEED_CHANGE));
    }

    private void ReverseRotation(Asteroid.CollisionEvt collision)
    {
        var (a, b) = collision;
        Logger.Debug?.Log($"Flipping asteroid rotation for {a} and {b}");

        var newRotA = -ROTATION_SPEED_CHANGE * a.Rotation;
        var newRotB = -ROTATION_SPEED_CHANGE * b.Rotation;

        newRotA = Vector3.ClampMagnitude(newRotA, MAX_ROTATION_SPEED);
        newRotB = Vector3.ClampMagnitude(newRotB, MAX_ROTATION_SPEED);

        a.UpdateRotation(newRotA);
        a.UpdateRotation(newRotB);
    }

    private const float COLLISION_SPEED_CHANGE = 0.1f;
    private const float ROTATION_SPEED_CHANGE = 5f;
    private const float MAX_ROTATION_SPEED = 360f;
}
