using UnityEngine;

public partial class Asteroid
{
    public static Asteroid Create(Asteroids pool, Material material)
    {
        var asteroidObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        asteroidObj.transform.SetParent(pool.transform);
        asteroidObj.GetComponent<MeshRenderer>().material = material;
        asteroidObj.SetActive(false);

        var meshFilter = asteroidObj.GetComponent<MeshFilter>();
        var mesh = meshFilter.mesh;
        var vertices = mesh.vertices;

        var randomOffset = Random.Range(0f, 100f);

        for (var v = 0; v < vertices.Length; v++)
        {
            var vertex = vertices[v];
            var noise = Mathf.PerlinNoise(vertex.x * 2f + randomOffset, vertex.y * 2f + randomOffset);
            vertices[v] = vertex + (vertex.normalized * (noise * 0.3f));
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        var asteroid = asteroidObj.AddComponent<Asteroid>();
        return asteroid;
    }
}
