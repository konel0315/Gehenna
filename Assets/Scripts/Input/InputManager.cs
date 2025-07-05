using UnityEngine;
using UnityEngine.InputSystem;

namespace Gehenna
{
    public class InputManager : ISubManager
    {
        private InputContext context;
        private InputActionAsset inputActionAsset;
        
        private InputSystem_Actions inputActions;
        private IInputHandler currentHandler;

        public void Initialize(ManagerContext context)
        {
            if (context is not InputContext inputContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = inputContext;
            this.inputActionAsset = inputContext.GameConfig.InputActionAsset;
            
            inputActions = new InputSystem_Actions();
            inputActions.Player.Move.performed += _ =>
            {
                currentHandler?.Example();
            };
            inputActions.Player.Next.performed += _ =>
            {
                currentHandler?.OnNextDialogue();
            };
            inputActions.Enable();
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }
        
        public void CleanUp()
        {
            context = null;
            inputActionAsset = null;
            inputActions.Disable();
            currentHandler = null;
        }

        public void SetHandler(IInputHandler handler)
        {
            currentHandler = handler;
        }
        
        public void SetInputEnabled(bool enabled)
        {
            if (enabled) 
                inputActions.Enable();
            else 
                inputActions.Disable();
        }
        
        public void ManualUpdate() { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }
    }
}