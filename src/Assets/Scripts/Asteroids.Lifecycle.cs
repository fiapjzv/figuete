using UnityEngine;

public partial class Asteroids
{
    private float _deadZoneZ;

    private void AsteroidsLifetime(float dt)
    {
        // NOTE: Loops backwards to use Pop and swap algorithm
        for (var i = _activeCount - 1; i >= 0; i--)
        {
            var asteroid = _activeAsteroids[i];
            if (asteroid.transform.position.z < _deadZoneZ)
            {
                PopAndSwapToRemoveAsteroid(asteroid, i);
                continue;
            }

            UpdateAsteroidTransform(asteroid, dt);
        }
    }

    private static void UpdateAsteroidTransform(Asteroid asteroid, float dt)
    {
        asteroid.transform.Translate(asteroid.Velocity * dt, Space.World);
        asteroid.transform.Rotate(asteroid.Rotation * dt, Space.Self);

        // NOTE: accel on asteroid after collision to avoid the asteroid to obstruct the camera
        if (GameGrid.AfterCollisionZone(asteroid.transform.position))
        {
            asteroid.Velocity *= 1.05f;
        }
    }

    private void PopAndSwapToRemoveAsteroid(Asteroid asteroid, int i)
    {
        _pool.Release(asteroid);
        // NOTE: pop and swap works here because we are iterating backwards
        _activeAsteroids[i] = _activeAsteroids[_activeCount - 1];
        _activeCount--;
    }

    private static float CalculateZIndexForBehindCamera(Camera cam)
    {
        // NOTE: behind the camera assuming that the camera is pointing into +z, from -z without any angle
        return cam.transform.position.z - 0.5f;
    }
}
