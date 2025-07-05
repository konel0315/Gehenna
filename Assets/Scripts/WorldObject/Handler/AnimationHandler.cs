using UnityEngine;

namespace Gehenna
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private GameObject owner;
        
        public bool IsPlaying { get; }
        public string CurrentAnimation { get; }

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

        public void Play()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}