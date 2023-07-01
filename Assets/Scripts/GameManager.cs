using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Timeline.Actions;
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

    [SerializeField] private List<Transform> holesPositions;
    [SerializeField] private List<Mole> molesObj;

    private List<Hole> _holes;
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
        _holes = new List<Hole>();

        foreach (var holePos in holesPositions)
        {
            var hole = new Hole(holePos.position);
            _holes.Add(hole);
        }
    }

    public Vector3 GetRandomHolePosition()
    {
        int id;
        do
        {
            id = Random.Range(0, _holes.Capacity);
        } while (_holes[id].IsTaken);

        return _holes[id].Position;
    }
}