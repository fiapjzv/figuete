using UnityEngine;

public partial class Asteroids
{
    [SerializeField]
    public float _spawnRate = 2f;

    private float _nextSpawnTime;

    private bool ShouldSpawnNewAsteroid(float time)
    {
        var itsTime = time >= _nextSpawnTime;

        if (itsTime)
        {
            _nextSpawnTime = time + _spawnRate;
        }

        return itsTime && _activeCount < POOL_SIZE;
    }

    private void SpawnNewAsteroid()
    {
        _activeAsteroids[_activeCount] = _pool.Get();
        _activeCount++;
    }
}
