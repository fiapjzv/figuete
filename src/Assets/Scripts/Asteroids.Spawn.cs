public partial class Asteroids
{
    private bool ShouldSpawnNewAsteroid()
    {
        return _activeCount == 0;
    }

    private void SpawnNewAsteroid()
    {
        _activeAsteroids[_activeCount] = _pool.Get();
        _activeCount++;
    }
}
