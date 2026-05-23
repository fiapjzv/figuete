using UnityEngine;
using UnityEngine.Pool;

public partial class Asteroids : GameBehavior
{
    [SerializeField]
    private Material _asteroidMaterial = null!;

    [SerializeField]
    private float _minSpeed;

    [SerializeField]
    private float _maxSpeed;

    private IObjectPool<Asteroid> _pool = null!;

    private Asteroid[] _activeAsteroids = new Asteroid[POOL_SIZE];
    private int _activeCount = 0;

    protected override void Init()
    {
        Guard.NotNull(_asteroidMaterial, Logger);

        _pool = CreateObjectPool(POOL_SIZE);
    }

    void Update() { }

    private const int POOL_SIZE = 10;
}
