using Gehenna;
using UnityEngine;

namespace Shared
{
    public class InteractionHandler : MonoBehaviour
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

        public void TryInteract()
        {
            if (!CanInteract())
            {
                return;
            }
            
            EventObject eventObject = DetectEvent();
            if (eventObject == null)
            {
                return;
            }
        }

        private EventObject DetectEvent()
        {
            return null;
        }

        private bool CanInteract()
        {
            return false;
        }
    }
}