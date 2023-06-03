using JetBrains.Annotations;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject boardTile;
    [SerializeField] private BoardData boardData;

    private readonly int[,] _board;
    private const float RowOffset = 1.02f;
    private const float ColumnOffset = 1.02f;

    private void Start()
    {
        boardData.Init();
        InstantiateBoard();
    }

    private void InstantiateBoard()
    {
        for (var y = 0; y < boardData.NumberOfTileRows; y++)
        {
            for (var x = 0; x < boardData.NumberOfTileColumns; x++)
            {
                Instantiate(boardTile, new Vector3(x * RowOffset, 0f, y * ColumnOffset), Quaternion.identity, this.transform);
            }
        }
    }
}