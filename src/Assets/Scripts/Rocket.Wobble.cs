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

    private void PopulateWobbleRandomSeed()
    {
        _posOffsets = new(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        _rotOffsets = new(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    private void WobbleAroundQuadrantCenter()
    {
        var (quadCenter, quadRot) = _gameGrid.CurrBaseTransform();

        var noiseSeed = Time.time;
        var drift = Wobble(quadCenter, _posWobbleAmount, _posWobbleSpeed, noiseSeed);
        var rot = Wobble(quadRot, _rotWobbleAmount, _rotWobbleSpeed, noiseSeed);

        var wobbleRotation = Quaternion.Euler(rot);
        wobbleRotation.ToAngleAxis(out var totalAngle, out var localAxis);

        var worldAxis = transform.rotation * localAxis;
        transform.RotateAround(_tipPivot.position, worldAxis, totalAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector3 Wobble(Vector3 basis, float amount, float speed, float time)
    {
        return new Vector3(
                x: Noise.NormalizedPerlin(basis.x, speed, time),
                y: Noise.NormalizedPerlin(basis.y, speed, time),
                z: Noise.NormalizedPerlin(basis.z, speed, time)
            ) * amount;
    }
}
