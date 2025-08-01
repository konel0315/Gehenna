using System;

namespace Gehenna
{
    public class MapParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(MapManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new MapParam
            (
                gameConfig: config, 
                resourceManager: container.Resolve<ResourceManager>()
            );
        }
    }
}