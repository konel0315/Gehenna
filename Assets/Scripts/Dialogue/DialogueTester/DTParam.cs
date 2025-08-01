using System;

namespace Gehenna
{
    public class DTParam : ManagerParam
    {
        public DialogueManager DialogueManager { get; }

        public DTParam(GameConfig gameConfig, DialogueManager dialogueManager )
            : base(gameConfig)
        {
            DialogueManager = dialogueManager;
        }
    }
}