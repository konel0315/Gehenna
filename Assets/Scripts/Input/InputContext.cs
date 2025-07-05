namespace Gehenna
{
    public class InputContext : ManagerContext
    {
        public DialogueManager DialogueManager { get; }
        
        public InputContext(GameConfig gameConfig,DialogueManager dialogueManager) : base(gameConfig)
        {
            DialogueManager=dialogueManager;
            
        }
    }
}
