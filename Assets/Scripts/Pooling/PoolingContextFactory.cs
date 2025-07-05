using System;

namespace Gehenna
{
    public class PoolingContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(PoolingManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new PoolingContext(config);
        }
    }
}