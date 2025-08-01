using UnityEngine;

namespace Gehenna
{
    public class UIParam : ManagerParam
    {
        public ResourceManager Resource { get; }
        public PoolingManager Pooling { get; }

        public UIParam
        (
            GameConfig gameConfig,
            ResourceManager resource,
            PoolingManager pooling
        ) : base(gameConfig)
        {
            Resource = resource;
            Pooling = pooling;
        }
    }
}
