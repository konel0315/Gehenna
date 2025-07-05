using System;

namespace Gehenna
{
    public class DialogueContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(DialogueManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new DialogueContext
            (
                gameConfig: config,
                uiManager:container.Resolve<UIManager>(),
                inputManager:container.Resolve<InputManager>()
                
            );
        }
    }
}