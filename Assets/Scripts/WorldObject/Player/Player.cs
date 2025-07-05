using Shared;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Gehenna
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private MovementHandler movementHandler;
        [SerializeField] private SpriteHandler spriteHandler;
        [SerializeField] private InteractionHandler interactionHandler;
        
        public void Initialize()
        {
            animationHandler?.Initialize(gameObject);
            movementHandler?.Initialize(gameObject);
            spriteHandler?.Initialize(gameObject);
            interactionHandler?.Initialize(gameObject);
        }

        public void CleanUp()
        {
            animationHandler?.CleanUp();  
            movementHandler?.CleanUp();   
            spriteHandler?.CleanUp();     
            interactionHandler?.CleanUp();
        }

        public void ManualUpdate()
        {
            animationHandler?.ManualUpdate();           
            movementHandler?.ManualUpdate();            
            spriteHandler?.ManualUpdate();              
            interactionHandler?.ManualUpdate();         
        }

        public void ManualLateUpdate()
        {
            animationHandler?.ManualLateUpdate();           
            movementHandler?.ManualLateUpdate();            
            spriteHandler?.ManualLateUpdate();              
            interactionHandler?.ManualLateUpdate();
        }

        public void ManualFixedUpdate()
        {
            animationHandler?.ManualFixedUpdate();           
            movementHandler?.ManualFixedUpdate();            
            spriteHandler?.ManualFixedUpdate();              
            interactionHandler?.ManualFixedUpdate();
        }
    }
}