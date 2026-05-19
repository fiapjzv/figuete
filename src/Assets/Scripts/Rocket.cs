using UnityEngine;

public class DrunkRocketOptimized : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public Transform pivotPoint;

    [Header("Grid Setup (6 Parts)")]
    public int columns = 3;
    public int rows = 2;
    public float distanceFromCamera = 10f;

    [Header("Current Position")]
    public int currentColumn = 1;
    public int currentRow = 0;

    [Header("Positional Drift")]
    public float positionWobbleSpeed = 1.2f;
    public float positionWobbleAmount = 0.5f;

    [Header("Rotational Sway")]
    public float rotationWobbleSpeed = 1.5f;
    public float rotationWobbleAmount = 15f;

    // Storing offsets as Vector3s makes the inspector and code much cleaner
    private Vector3 posOffsets;
    private Vector3 rotOffsets;
    private Quaternion baseRotation;

    public void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        baseRotation = transform.rotation;

        // Populate the Vector3s with random offsets for the Perlin noise
        posOffsets = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );
        rotOffsets = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );
    }

    public void Update()
    {
        if (pivotPoint == null)
            return;

        // 1. Calculate Quadrant Center
        Vector3 quadrantCenter = GetQuadrantCenter(currentColumn, currentRow);

        // 2. Calculate 3D Positional Drift using a helper method
        Vector3 drift = new Vector3(
            GetNoise(posOffsets.x, positionWobbleSpeed) * positionWobbleAmount,
            GetNoise(posOffsets.y, positionWobbleSpeed) * positionWobbleAmount,
            0f // Kept at 0 so it doesn't drift forward/backward, but you can add Z drift if you want!
        );

        // 3. Reset Position and Rotation
        transform.position = quadrantCenter + drift;
        transform.rotation = baseRotation;

        // 4. Calculate 3D Rotational Wobble (All 3 axes!)
        Vector3 wobbleAngles =
            new Vector3(
                GetNoise(rotOffsets.x, rotationWobbleSpeed),
                GetNoise(rotOffsets.y, rotationWobbleSpeed),
                GetNoise(rotOffsets.z, rotationWobbleSpeed)
            ) * rotationWobbleAmount;

        // 5. Convert the 3-axis Euler rotation into a single Angle and Axis
        Quaternion wobbleRotation = Quaternion.Euler(wobbleAngles);
        wobbleRotation.ToAngleAxis(out float totalAngle, out Vector3 localAxis);

        // 6. Convert the local axis to a world axis, then rotate once!
        Vector3 worldAxis = transform.rotation * localAxis;
        transform.RotateAround(pivotPoint.position, worldAxis, totalAngle);
    }

    /// <summary>
    /// Helper method to keep Perlin Noise math clean. Returns a value from -1 to 1.
    /// </summary>
    float GetNoise(float offset, float speed)
    {
        return (Mathf.PerlinNoise(Time.time * speed + offset, 0f) * 2f) - 1f;
    }

    Vector3 GetQuadrantCenter(int col, int row)
    {
        float viewportX = (col + 0.5f) / columns;
        float viewportY = (row + 0.5f) / rows;
        return mainCamera.ViewportToWorldPoint(
            new Vector3(viewportX, viewportY, distanceFromCamera)
        );
    }
}
