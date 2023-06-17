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
            GameManager.Instance.OnNewTilesDraw += Function; 
        }

        // TODO: change function name according to its functionality, now its a placeholder name
        private void Function()
        {
            if (isStunned) return;

            // Hide under the board and draw new tile
            this.currentTile.IsTaken = false;

            var currentTile = DrawTile();

            while (currentTile.IsTaken)
            {
                // draw until its free
            }
        }

        private Tile DrawTile()
        {
            return null;
        }
    }
}