using Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public class EventObject : MonoBehaviour
    {
        [SerializeField] private EventData eventData;
        [SerializeField] private AnimationHandler animationHandler;
        [SerializeField] private MovementHandler movementHandler;
        [SerializeField] private SpriteHandler spriteHandler;

        private ContentIterator contentIterator;
        
        public void Initialize()                                                    
        {                                                                           
            animationHandler?.Initialize(gameObject);                                         
            movementHandler?.Initialize(gameObject);                                          
            spriteHandler?.Initialize(gameObject);

            contentIterator = new ContentIterator();
            contentIterator.Initialize(this, eventData);
        }                                                                           
                                                                                    
        public void CleanUp()                                                       
        {                                                                           
            animationHandler?.CleanUp();                                            
            movementHandler?.CleanUp();                                             
            spriteHandler?.CleanUp();      
            contentIterator?.CleanUp();
        }                                                                           
                                                                                    
        public void ManualUpdate()                                                  
        {                                                                           
            animationHandler?.ManualUpdate();                                       
            movementHandler?.ManualUpdate();                                        
            spriteHandler?.ManualUpdate(); 
            contentIterator?.ManualUpdate();
        }                                                                           
                                                                                    
        public void ManualLateUpdate()
        {
            animationHandler?.ManualLateUpdate();                                   
            movementHandler?.ManualLateUpdate();                                    
            spriteHandler?.ManualLateUpdate();
            contentIterator?.ManualLateUpdate();
        }                                                                           
                                                                                    
        public void ManualFixedUpdate()                                             
        {                                                                           
            animationHandler?.ManualFixedUpdate();                                  
            movementHandler?.ManualFixedUpdate();                                   
            spriteHandler?.ManualFixedUpdate();     
            contentIterator?.ManualFixedUpdate();
        }
    }
}