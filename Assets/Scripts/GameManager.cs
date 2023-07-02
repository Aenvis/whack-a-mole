using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public struct CursorData
{
    public int X;
    public int Y;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<Transform> holesPositions;
    [SerializeField] private List<Mole> moles;

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

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var random = new System.Random();
            var id = random.Next(moles.Count-1);
            StartCoroutine(moles[id].Stun());
        }
    }
}