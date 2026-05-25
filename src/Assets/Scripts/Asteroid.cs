using UnityEngine;

public partial class Asteroid : GameBehavior
{
    public float Speed { get; set; }
    public Vector3 EulerAngSpeed { get; set; }

    public static Vector3 RandomAngSpeed()
    {
        return new Vector3(
            x: Random.Range(MIN_ANG_SPEED, MAX_ANG_SPEED),
            y: Random.Range(MIN_ANG_SPEED, MAX_ANG_SPEED),
            z: Random.Range(MIN_ANG_SPEED, MAX_ANG_SPEED)
        );
    }

    private const float MIN_ANG_SPEED = 0;
    private const float MAX_ANG_SPEED = 90;
}
