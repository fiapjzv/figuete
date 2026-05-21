using System.Runtime.CompilerServices;
using UnityEngine;

public partial class Rocket
{
    [SerializeField]
    private float _posWobbleSpeed = 0.3f;

    [SerializeField]
    private float _posWobbleAmount = 3f;

    [SerializeField]
    private float _rotWobbleSpeed = 0.3f;

    [SerializeField]
    private float _rotWobbleAmount = 15f;

    private Vector3 _posOffsets;
    private Vector3 _rotOffsets;

    private void PopulateWobbleRandomOffsets()
    {
        _posOffsets = new(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        _rotOffsets = new(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    private (Vector3 unstablePos, Quaternion unstableRot) WobbleAround(Vector3 stablePos, Quaternion stableRot)
    {
        var time = Time.time;

        var drift = Wobble(_posOffsets, _posWobbleAmount, _posWobbleSpeed, time);
        var driftedPos = stablePos + drift;

        var rotAngle = Wobble(_rotOffsets, _rotWobbleAmount, _rotWobbleSpeed, time);
        var wobbleRotation = Quaternion.Euler(rotAngle);
        wobbleRotation.ToAngleAxis(out var totalAngle, out var localAxis);

        var worldAxis = stableRot * localAxis;
        var rotationDelta = Quaternion.AngleAxis(totalAngle, worldAxis);
        var pivotWorldPos = driftedPos + (stableRot * _tipPivot.localPosition);

        return driftedPos.RotateAround(stableRot, pivotWorldPos, rotationDelta);
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
