using System;

namespace Gehenna
{
    public class InputContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(InputManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new InputContext(
                
                    gameConfig: config,
                    dialogueManager:container.Resolve<DialogueManager>()
                    );
        }
    }
}