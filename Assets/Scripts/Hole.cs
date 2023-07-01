using UnityEngine;

public class Hole
{
    public Vector3 Position { get; set; }
    public bool IsTaken { get; set; }

    public Hole(Vector3 pos)
    {
        Position = pos;
        IsTaken = false;
    }
}