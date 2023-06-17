using UnityEngine;

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;

    public bool IsTaken { get; set; }

    private void OnMouseEnter()
    {
        GameManager.Instance.SetTileUnderMouse(X, Y);
    }
}