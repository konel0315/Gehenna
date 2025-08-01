using System;

namespace Gehenna
{
    public class GameFlowParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(GameFlowManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new GameFlowParam
            (
                gameConfig: config, 
                resource: container.Resolve<ResourceManager>(),
                audio: container.Resolve<AudioManager>(),
                input: container.Resolve<InputManager>(),
                ui: container.Resolve<UIManager>()
            );
        }
    }
}