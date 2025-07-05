using UnityEngine;

namespace Gehenna
{
    public class MovementHandler : MonoBehaviour
    {
        private GameObject owner;
        
        public void Initialize(GameObject owner)
        {
            this.owner = owner;
        }

        public void CleanUp()
        {
            
        }

        public void ManualUpdate()
        {
            
        }

        public void ManualLateUpdate()
        {
            
        }

        public void ManualFixedUpdate()
        {
            
        }

        public bool TryMove(Vector2Int direction)
        {
            return false;
        }

        public bool CanMoveTo(Vector2Int destination)
        {
            return false;
        }
        
        public void StopImmediately()
        {
            
        }

        public void SnapToGrid()
        {
            
        }
    }
}