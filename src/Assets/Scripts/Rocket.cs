using UnityEngine;

public partial class Rocket : MonoBehaviour
{
    public Transform pivotPoint;

    private int _currQuadrant = 0;

    public float _wobbleSpeed = 1.5f;
    public float _wobbleAmount = 15f;

    private float _randomOffset1;
    private float _randomOffset2;

    private Quaternion _initialRotation;
    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        // Generate random offsets so the wobble is unique every time you play
        _randomOffset1 = Random.Range(0f, 100f);
        _randomOffset2 = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (pivotPoint is null)
        {
            // TODO: Panic
            return;
        }

        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        // 2. Calculate the raw angles from Perlin Noise
        var xAngle =
            (Mathf.PerlinNoise(Time.time * _wobbleSpeed + _randomOffset1, 0f) * 2f - 1f)
            * _wobbleAmount;
        var zAngle =
            (Mathf.PerlinNoise(0f, Time.time * _wobbleSpeed + _randomOffset2) * 2f - 1f)
            * _wobbleAmount;

        transform.RotateAround(pivotPoint.position, transform.right, xAngle);
        transform.RotateAround(pivotPoint.position, transform.forward, zAngle);
    }
}
