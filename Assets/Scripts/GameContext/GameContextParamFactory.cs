using System;

namespace Gehenna
{
    public class GameContextParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(GameContextManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new GameContextParam
            (
                gameConfig: config,
                resource: container.Resolve<ResourceManager>(),
                pooling: container.Resolve<PoolingManager>(),
                ui: container.Resolve<UIManager>(),
                camera: container.Resolve<CameraManager>()
            );
        }
    }
}