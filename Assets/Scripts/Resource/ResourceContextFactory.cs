using System;

namespace Gehenna
{
    public class ResourceContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(ResourceManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new ResourceContext(config);
        }
    }
}