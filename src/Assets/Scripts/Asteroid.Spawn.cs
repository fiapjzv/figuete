using UnityEngine;

public partial class Asteroid
{
    public void Spawn(IGameGrid grid)
    {
        var speed = Random.Range(MIN_LIN_SPEED, MAX_LIN_SPEED);
        transform.position = RandomStartPosition(grid);
        TargetQuadrant = grid.RandomQuadrant();
        Velocity = (TargetQuadrant.Pos - transform.position).normalized * speed;

        Rotation = new Vector3(
            x: Random.Range(MIN_ANG_SPEED, MAX_ANG_SPEED),
            y: Random.Range(MIN_ANG_SPEED, MAX_ANG_SPEED),
            z: Random.Range(MIN_ANG_SPEED, MAX_ANG_SPEED)
        );

        // NOTE: generating perceived shape variance
        transform.rotation = Random.rotation;
        var randomScale = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    private static Vector3 RandomStartPosition(IGameGrid grid)
    {
        var quadrant = grid.RandomQuadrant();
        return new Vector3(quadrant.Pos.x, quadrant.Pos.y, GameGrid.ASTEROIDS_SPAWN_PLANE_Z);
    }
}
