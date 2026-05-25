using UnityEngine;

public partial class Asteroids
{
    private readonly Asteroid[] _activeAsteroids = new Asteroid[POOL_SIZE];
    private int _activeCount;

    private bool ShouldSpawnNewAsteroid()
    {
        return _activeCount == 0;
    }

    private void SpawnNewAsteroid()
    {
        var newAsteroid = _activeAsteroids[_activeCount] = _pool.Get();
        _activeCount++;
        newAsteroid.transform.position = new Vector3(0, 0, 0);
    }
}
