using System.Runtime.CompilerServices;
using UnityEngine;

public static class Noise
{
    /// <summary>
    /// Generates a smooth, continuous landscape of noise values. Returns a value from -1 to 1.
    /// </summary>
    /// <example>
    /// USAGE: Drunk rocket wobble, wind blowing through grass or camera shake.
    /// </example>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NormalizedPerlin(float offset, float speed, float time)
    {
        return Mathf.PerlinNoise(time * speed + offset, 0f) * 2f - 1f;
    }
}
