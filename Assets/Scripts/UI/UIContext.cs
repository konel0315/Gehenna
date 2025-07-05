using UnityEngine;

namespace Gehenna
{
    public class UIContext : ManagerContext
    {
        public ResourceManager ResourceManager { get; }
        public PoolingManager PoolingManager { get; }

        public UIContext
        (
            GameConfig gameConfig,
            ResourceManager resourceManager,
            PoolingManager poolingManager
        ) : base(gameConfig)
        {
            ResourceManager = resourceManager;
            PoolingManager = poolingManager;
        }
    }
}
