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
    private const float TimeBetweenDraws = 2f;
    public static GameManager Instance;

    [SerializeField] private List<Tile> board;

    private CursorData _tileUnderCursor;
    private float _time;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= TimeBetweenDraws)
        {
            Debug.Log($"draw at: {_time}");
            _time -= TimeBetweenDraws;
        }

        if (Input.GetMouseButtonDown(0)) OnMouseButtonDown();
    }

    public event Action OnNewTilesDraw;

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

    public Tile GetTile(int x, int y)
    {
        var tile = board.Single(tile => tile.X == x && tile.Y == y);

        return tile;
    }

    public void SetTileUnderMouse(int x, int y)
    {
        _tileUnderCursor.X = x;
        _tileUnderCursor.Y = y;
    }
}