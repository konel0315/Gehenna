using System;

namespace Gehenna
{
    public class AudioParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(AudioManager);

        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new AudioParam(config, container.Resolve<PoolingManager>());
        }
    }
}