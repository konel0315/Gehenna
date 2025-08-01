using System;

namespace Gehenna
{
    public class DialogueParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(DialogueManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new DialogueParam
            (
                gameConfig: config,
                uiManager:container.Resolve<UIManager>(),
                inputManager:container.Resolve<InputManager>()
            );
        }
    }
}