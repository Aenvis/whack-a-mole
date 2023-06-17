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
        
        //TODO: move that to some service class to draw and set tiles synchronously, atm there is a bug where moles completely skip this method on draw - not intended
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