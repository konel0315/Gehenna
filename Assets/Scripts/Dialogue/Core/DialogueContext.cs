namespace Gehenna
{
    public class DialogueContext : ManagerContext
    {
        public UIManager UIManager { get; }
        public InputManager InputManager { get; }
        
        public DialogueContext(GameConfig gameConfig,UIManager uiManager,InputManager inputManager): base(gameConfig)
        {
            UIManager = uiManager;
            InputManager = inputManager;
        }
    }
}