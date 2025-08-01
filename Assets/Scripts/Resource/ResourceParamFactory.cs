using System;

namespace Gehenna
{
    public class ResourceParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(ResourceManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new ResourceParam(config);
        }
    }
}