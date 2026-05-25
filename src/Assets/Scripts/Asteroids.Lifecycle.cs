using UnityEngine;

public partial class Asteroids
{
    private float _deadZoneZ;

    private void AsteroidsLifetime(float dt)
    {
        // NOTE: Loops backwards to wap and Pop
        for (var i = _activeCount - 1; i >= 0; i--)
        {
            var asteroid = _activeAsteroids[i];
            if (asteroid.transform.position.z < _deadZoneZ)
            {
                RemoveAsteroidFromScreen(asteroid, i);
                continue;
            }

            UpdateAsteroidTransform(asteroid, dt);
        }
    }

    private static void UpdateAsteroidTransform(Asteroid asteroid, float dt)
    {
        asteroid.transform.Translate(Vector3.back * (asteroid.Speed * dt));
    }

    private void RemoveAsteroidFromScreen(Asteroid asteroid, int i)
    {
        _pool.Release(asteroid);
        _activeCount--;
    }

    private static float CalculateZIndexForBehindCamera(Camera cam)
    {
        // NOTE: behind the camera assuming that the camera is pointing into +z, from -z without any angle
        return cam.transform.position.z - 0.5f;
    }
}
