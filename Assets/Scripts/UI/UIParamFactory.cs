using System;

namespace Gehenna
{
    public class UIParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(UIManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new UIParam
            (
                gameConfig: config,
                resource: container.Resolve<ResourceManager>(),
                pooling: container.Resolve<PoolingManager>()
            );
        }
    }
}