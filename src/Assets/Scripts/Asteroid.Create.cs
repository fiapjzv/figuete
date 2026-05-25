using UnityEngine;

public partial class Asteroid
{
    public static Asteroid Create(Asteroids pool, GameObject asteroidPrefab, IGameLogger logger)
    {
        var asteroidObj = Instantiate(asteroidPrefab, pool.transform, worldPositionStays: true);
        var meshFilter = asteroidObj.GetComponentInChildren<MeshFilter>();

        var mesh = meshFilter.mesh;
        var vertices = mesh.vertices;

        var randomOffset = Random.Range(0f, 100f);

        for (var v = 0; v < vertices.Length; v++)
        {
            var vertex = vertices[v];

            // NOTE: Because Perlin noise is based on the spatial position (vertex.x, vertex.y),
            //       the duplicated flat-shaded vertices sitting in the exact same spot
            //       will be pushed in the exact same direction.
            var noise = Mathf.PerlinNoise(
                vertex.x * NOISE_FACTOR + randomOffset,
                vertex.y * NOISE_FACTOR + randomOffset
            );
            vertices[v] = vertex + (vertex.normalized * (noise * NOISE_FACTOR));
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        var asteroid = asteroidObj.GetComponent<Asteroid>();
        return asteroid;
    }

    public const float NOISE_FACTOR = 1.2f;
}
