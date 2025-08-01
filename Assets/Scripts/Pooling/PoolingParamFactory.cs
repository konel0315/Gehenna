using System;

namespace Gehenna
{
    public class PoolingParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(PoolingManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new PoolingParam(config);
        }
    }
}