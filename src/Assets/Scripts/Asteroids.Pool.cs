using System.Diagnostics;
using UnityEngine;
using UnityEngine.Pool;

public partial class Asteroids
{
    private IObjectPool<Asteroid> CreateObjectPool(int poolSize)
    {
        Logger.Debug?.Log("Creating asteroid pool and pre-warming objects");
        var sw = Stopwatch.StartNew();
        var pool = new ObjectPool<Asteroid>(
            createFunc: CreateAsteroid,
            actionOnGet: ActivateAsteroid,
            actionOnRelease: ReleaseAsteroid,
            actionOnDestroy: DestroyAsteroid,
            collectionCheck: true,
            defaultCapacity: poolSize,
            maxSize: poolSize
        );

        var prewarmed = new Asteroid[poolSize];
        for (var i = 0; i < poolSize; i++)
        {
            prewarmed[i] = pool.Get();
        }
        for (var i = 0; i < poolSize; i++)
        {
            pool.Release(prewarmed[i]);
        }
        Logger.Debug?.Log($"Asteroid ready after {sw.ElapsedMilliseconds}ms");
        return pool;
    }

    private Asteroid CreateAsteroid() => Asteroid.Create(this, _asteroidPrefab, Logger);

    private void ActivateAsteroid(Asteroid asteroid)
    {
        Logger.Debug?.Log($"Asteroid activated: {asteroid}!");
        asteroid.Speed = Random.Range(1f, 2f);
        asteroid.gameObject.SetActive(true);
    }

    private void ReleaseAsteroid(Asteroid asteroid)
    {
        Logger.Debug?.Log($"Asteroid released: {asteroid}!");
        asteroid.gameObject.SetActive(false);
    }

    private void DestroyAsteroid(Asteroid m)
    {
        Logger.Error?.Log("Asteroids being destroyed! This should not happen.");
        Destroy(m.gameObject);
    }
}
