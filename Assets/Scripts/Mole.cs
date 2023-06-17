using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Mole : MonoBehaviour
    {
        public Tile currentTile;
        public bool isStunned;

        private void Start()
        {
            currentTile = GameManager.Instance.DrawTile();
            GameManager.Instance.OnNewTilesDraw += OnTilesDraw; 
        }
        
        private void OnTilesDraw()
        {
            if (isStunned) return;
            
            currentTile.IsTaken = false;
            currentTile = GameManager.Instance.DrawTile();
            currentTile.IsTaken = true;
            Debug.Log($"{gameObject.name} tile: {currentTile?.gameObject.name}");
        }
    }
}