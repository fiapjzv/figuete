using UnityEngine;
using UnityEngine.Pool;

public partial class Asteroids : GameBehavior
{
    [SerializeField]
    private GameObject _asteroidPrefab = null!;

    [SerializeField]
    private float _minSpeed;

    [SerializeField]
    private float _maxSpeed;

    private IObjectPool<Asteroid> _pool = null!;

    protected override void Init()
    {
        Guard.NotNull(_asteroidPrefab, Logger);

        _pool = CreateObjectPool(POOL_SIZE);
    }

    public void Update()
    {
        if (ShouldSpawnNewAsteroid())
        {
            SpawnNewAsteroid();
        }
    }

    private const int POOL_SIZE = 10;
}
