using System;

namespace Gehenna
{
    public class MapContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(MapManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new MapContext
            (
                gameConfig: config, 
                resourceManager: container.Resolve<ResourceManager>()
            );
        }
    }
}