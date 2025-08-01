namespace Gehenna
{
    public class DialogueParam : ManagerParam
    {
        public UIManager UIManager { get; }
        public InputManager InputManager { get; }
        
        public DialogueParam
        (
            GameConfig gameConfig, 
            UIManager uiManager, 
            InputManager inputManager
        ): base(gameConfig)
        {
            UIManager = uiManager;
            InputManager = inputManager;
        }
    }
}