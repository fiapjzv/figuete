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
    private IGameGrid _grid = null!;

    private readonly Asteroid[] _activeAsteroids = new Asteroid[POOL_SIZE];
    private int _activeCount;

    protected override void Init()
    {
        Guard.NotNull(_asteroidPrefab, Logger);
        _grid = Guard.NotNull(Service.Get<IGameGrid>(), Logger);
        _pool = CreateObjectPool(POOL_SIZE);
    }

    public void Start()
    {
        var cam = Guard.NotNull(Camera.main, Logger);
        _deadZoneZ = CalculateZIndexForBehindCamera(cam);
    }

    public void Update()
    {
        var time = Time.deltaTime;
        if (ShouldSpawnNewAsteroid())
        {
            SpawnNewAsteroid();
        }

        AsteroidsLifetime(time);
    }

    private const int POOL_SIZE = 10;
}
