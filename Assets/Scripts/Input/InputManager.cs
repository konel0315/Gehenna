using UnityEngine;
using UnityEngine.InputSystem;

namespace Gehenna
{
    public class InputManager : ISubManager
    {
        private InputParam param;
        private InputConfig config;
        
        private InputSystem_Actions inputActions;
        private IInputHandler currentHandler;
        private IInputHandler prevHandler;

        public void Initialize(ManagerParam param)
        {
            if (param is not InputParam inputParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = inputParam;

            if (!inputParam.GameConfig.TryGetConfig<InputConfig>(out config))
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid config");
                return;
            }
            
            inputActions = new InputSystem_Actions();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Submit.performed += OnSubmit;
            inputActions.Player.Cancel.performed += OnCancel;
            
            inputActions.UI.Submit.performed += OnSubmit;
            inputActions.UI.Cancel.started += OnCancelStarted;
            inputActions.UI.Cancel.canceled += OnCancelCanceled;
            inputActions.UI.Auto.performed += OnAuto;
            inputActions.UI.Log.performed += OnLog;
            
            inputActions.Enable();
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }
        
        public void CleanUp()
        {
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Submit.performed -= OnSubmit;
            inputActions.Player.Cancel.performed -= OnCancel;
            
            inputActions.UI.Submit.performed -= OnSubmit;
            inputActions.UI.Cancel.started -= OnCancelStarted;
            inputActions.UI.Cancel.canceled -= OnCancelCanceled;
            inputActions.UI.Auto.performed -= OnAuto;
            inputActions.UI.Log.performed -= OnLog;
            
            inputActions.Disable();
            
            param = null;
            config = null;
            currentHandler = null;
            inputActions = null;
        }
        
        public void SwitchToUIMap()
        {
            inputActions.Player.Disable();
            inputActions.UI.Enable();
        }

        public void SwitchToPlayerMap()
        {
            inputActions.UI.Disable();
            inputActions.Player.Enable();
        }

        
        public void ManualUpdate(float deltaTime) { }
        public void ManualFixedUpdate() { }

        public void SetHandler(IInputHandler handler)
        {
            prevHandler = currentHandler;
            currentHandler = handler;
        }

        public void RevertHandler()
        {
            if (prevHandler == null)
            {
                GehennaLogger.Log(this, LogType.Warning, "No previous handler to revert to");
                return;
            }
            currentHandler = prevHandler;
        }
        
        public void SetInputEnabled(bool enabled)
        {
            if (enabled) 
                inputActions.Enable();
            else 
                inputActions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            currentHandler?.Move(context.ReadValue<Vector2>());
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
                currentHandler.Submit();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            currentHandler?.Cancel();
        }
        private void OnCancelStarted(InputAction.CallbackContext ctx)
        {
            if (currentHandler is DialogueInputHandler handler)
            {
                handler.BeginSkipHold();
            }
        }

        private void OnCancelCanceled(InputAction.CallbackContext ctx)
        {
            if (currentHandler is DialogueInputHandler handler)
            {
                handler.CancelSkipHold();
            }
        }
        public void OnAuto(InputAction.CallbackContext context)
        {
            if (currentHandler is DialogueInputHandler handler)
            {
                handler.Auto();
            }
        }
        public void OnLog(InputAction.CallbackContext context)
        {
            if (currentHandler is DialogueInputHandler handler)
            {
                handler.Log();
            }
        }
    }
}