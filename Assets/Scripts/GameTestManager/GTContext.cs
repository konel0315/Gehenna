using System;

namespace Gehenna
{
    public class GTContext : ManagerContext
    {
        public DialogueManager DialogueManager { get; }

        public GTContext(GameConfig gameConfig, DialogueManager dialogueManager )
            : base(gameConfig)
        {
            DialogueManager = dialogueManager;
        }
    }
}