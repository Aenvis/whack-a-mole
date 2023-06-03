using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoardData", menuName = "Board/BoardData", order = 0)]
public class BoardData : ScriptableObject
{
    [field: SerializeField] public int NumberOfTileRows { get; private set; }
    [field: SerializeField] public int NumberOfTileColumns { get; private set; }

    public void Init()
    {
        
    }
}