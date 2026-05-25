using UnityEngine;

public partial class Asteroid
{
    public Asteroid Spawn(IGameGrid grid)
    {
        var speed = Random.Range(MIN_LIN_SPEED, MAX_LIN_SPEED);
        transform.position = new Vector3(0, 0, GameGrid.ASTEROIDS_SPAWN_PLANE_Z);
        TargetQuadrant = CalculateTarget(grid);
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
        return this;
    }

    private static Quadrant CalculateTarget(IGameGrid grid)
    {
        var (row, col) = (Random.Range(0, grid.Rows), Random.Range(0, grid.Columns));
        return grid.Quadrant(row, col);
    }
}
