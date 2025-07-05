using System;

namespace Gehenna
{
    public class UIContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(UIManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new UIContext
            (
                gameConfig: config,
                resourceManager: container.Resolve<ResourceManager>(),
                poolingManager: container.Resolve<PoolingManager>()
            );
        }
    }
}