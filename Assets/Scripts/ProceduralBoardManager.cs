using UnityEngine;

public class ProceduralBoardManager : MonoBehaviour
{
    private const float RowOffset = 1.02f;
    private const float ColumnOffset = 1.02f;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameObject boardTile;
    [SerializeField] private BoardData boardData;

    private Tile[,] _board;

    private void Start()
    {
        boardData.Init();
        _board = new Tile[boardData.NumberOfTileRows, boardData.NumberOfTileColumns];

        InstantiateBoard();
        gameCamera.Setup(boardData.NumberOfTileRows, boardData.NumberOfTileColumns, RowOffset);
    }

    private void InstantiateBoard()
    {
        for (var x = 0; x < boardData.NumberOfTileRows; x++)
        {
            for (var y = 0; y < boardData.NumberOfTileColumns; y++)
            {
                var tile = Instantiate(boardTile, new Vector3(x * RowOffset, 0f, y * ColumnOffset), Quaternion.identity,
                        transform)
                    .AddComponent<Tile>();
                tile.X = x;
                tile.Y = y;

                _board[x, y] = tile;
            }
        }
    }
}