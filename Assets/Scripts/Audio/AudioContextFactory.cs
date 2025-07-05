using System;

namespace Gehenna
{
    public class AudioContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(AudioManager);

        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new AudioContext(config, container.Resolve<PoolingManager>());
        }
    }
}