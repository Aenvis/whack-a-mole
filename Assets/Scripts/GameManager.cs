using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public struct CursorData
{
    public int X;
    public int Y;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private List<Tile> board;
    [SerializeField] private List<Mole> moles;
    
    private const float TimeBetweenDraws = 2f;
    public event Action OnNewTilesDraw;
    
    private CursorData _tileUnderCursor;
    private float _time;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= TimeBetweenDraws)
        {
            Debug.Log($"draw at: {_time}");
            OnNewTilesDraw?.Invoke();
            _time -= TimeBetweenDraws;
        }
    }

    public Tile DrawTile()
    {
        var freeTiles = board.Where(tile => tile.IsTaken == false).ToList();
        var random = Random.Range(0, freeTiles.Count);
        return freeTiles[random];
    }

    public void SetTileUnderMouse(int x, int y)
    {
        _tileUnderCursor.X = x;
        _tileUnderCursor.Y = y;
    }
}