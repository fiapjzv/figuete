using System.Runtime.CompilerServices;
using UnityEngine;

public partial class Rocket
{
    [SerializeField]
    private float _posWobbleSpeed = 1.2f;

    [SerializeField]
    private float _posWobbleAmount = 0.5f;

    [SerializeField]
    private float _rotWobbleSpeed = 1.5f;

    [SerializeField]
    private float _rotWobbleAmount = 15f;

    private Vector3 _posOffsets;
    private Vector3 _rotOffsets;

    private void PopulateWobbleRandomOffsets()
    {
        _posOffsets = new(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        _rotOffsets = new(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    private void WobbleAroundQuadrantCenter()
    {
        var (quadCenter, quadRot) = _gameGrid.CurrBaseTransform();

        var time = Time.time;
        var drift = Wobble(_posOffsets, _posWobbleAmount, _posWobbleSpeed, time);
        transform.position = quadCenter + drift;

        var rot = Wobble(_rotOffsets, _rotWobbleAmount, _rotWobbleSpeed, time);
        var wobbleRotation = Quaternion.Euler(rot);
        wobbleRotation.ToAngleAxis(out var totalAngle, out var localAxis);
        var worldAxis = transform.rotation * localAxis;

        transform.rotation = quadRot;
        transform.RotateAround(_tipPivot.position, worldAxis, totalAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector3 Wobble(Vector3 randomSeed, float amount, float speed, float time)
    {
        return new Vector3(
                x: Noise.NormalizedPerlin(randomSeed.x, speed, time),
                y: Noise.NormalizedPerlin(randomSeed.y, speed, time),
                z: Noise.NormalizedPerlin(randomSeed.z, speed, time)
            ) * amount;
    }
}
