using System.Runtime.CompilerServices;
using UnityEngine;

public static class TransformHelpers
{
    /// <summary>
    /// Move the <paramref name="from"/> towards the <paramref name="target"/> and returns false if arrived.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MoveTowards(this Vector3 from, Vector3 target, float speed, out Vector3 newPos)
    {
        var step = Time.deltaTime * speed;
        newPos = Vector3.MoveTowards(from, target, step);
        var distance = Vector3.Distance(newPos, target);
        return distance > step;
    }

    /// <summary>
    /// Rotates the <paramref name="from"/> towards the <paramref name="target"/> rotation and returns false if arrived.
    /// <paramref name="speed"/> is in degrees per seconds.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RotateTo(this Quaternion from, Quaternion target, float speed, out Quaternion newRot)
    {
        var step = Time.deltaTime * speed;
        newRot = Quaternion.RotateTowards(from, target, step);
        var angle = Quaternion.Angle(newRot, target);
        return angle > 0.01f;
    }

    /// <summary>
    /// Calculates a new position and rotation as if the object orbited around a pivot point.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (Vector3 pos, Quaternion rot) RotateAround(
        this Vector3 objPos,
        Quaternion objRot,
        Vector3 pivot,
        Quaternion rotDelta
    )
    {
        var finalRotation = rotDelta * objRot;
        var offsetFromPivot = objPos - pivot;
        var finalPosition = pivot + (rotDelta * offsetFromPivot);
        return (finalPosition, finalRotation);
    }
}
