using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct CursorData
{
    public int X;
    public int Y;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<Tile> board;

    public event Action OnNewTilesDraw;
    private CursorData _tileUnderCursor;
    
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }
    }

    private void OnMouseButtonDown()
    {
        var currentTile = GetTile(_tileUnderCursor.X, _tileUnderCursor.Y);
        if (currentTile.IsTaken) Debug.Log("Taken");
        else Debug.Log("Not Taken");
    }

    private void DrawTiles()
    {
        OnNewTilesDraw?.Invoke();
    }

    private Tile GetTile(int x, int y)
    {
        var tile = board.Single(tile => tile.X == x && tile.Y == y);

        return tile;
    }

    public void SetTileUnderMouse(int x, int y)
    {
        _tileUnderMouse = new Tuple<int, int>(x, y);
    }
}
