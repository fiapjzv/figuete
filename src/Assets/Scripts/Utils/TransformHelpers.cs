using UnityEngine;

public static class TransformHelpers
{
    /// <summary>
    /// Move the <paramref name="transform"/> towards the <paramref name="target"/> and returns false if arrived.
    /// </summary>
    public static bool MoveTowards(this Transform transform, Vector3 target, float speed)
    {
        var step = Time.deltaTime * speed;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        var distance = Vector3.Distance(transform.position, target);
        return distance > step;
    }
}
