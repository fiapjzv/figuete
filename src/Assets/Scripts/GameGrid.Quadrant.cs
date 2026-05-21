using UnityEngine;

public readonly struct Quadrant
{
    public int Row { get; }
    public int Col { get; }

    public Vector3 Pos { get; }
    public Quaternion Rot { get; }

    public Quadrant(int row, int col, Vector3 pos, Quaternion rot)
    {
        (Col, Row, Pos, Rot) = (col, row, pos, rot);
    }

    public (Vector3 pos, Quaternion rot) Transform()
    {
        return (Pos, Rot);
    }

    public (int row, int col) Indexes()
    {
        return (Row, Col);
    }

    public override string ToString()
    {
        return $"pos: {Pos}, rot: {Rot.eulerAngles}";
    }
}
