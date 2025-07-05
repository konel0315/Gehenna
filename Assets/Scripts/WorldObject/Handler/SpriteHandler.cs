using UnityEngine;

namespace Shared
{
    public class SpriteHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer SpriteRenderer;
        
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
    }
}