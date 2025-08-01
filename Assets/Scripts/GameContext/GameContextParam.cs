namespace Gehenna
{
    public class GameContextParam : ManagerParam
    {
        public ResourceManager Resource { get; }
        public PoolingManager Pooling { get; }
        public UIManager UI { get; }
        public CameraManager Camera { get; }
        
        public GameContextParam
        (
            GameConfig gameConfig,
            ResourceManager resource,
            PoolingManager pooling,
            UIManager ui,
            CameraManager camera
        ) : base(gameConfig)
        {
            Resource = resource;
            Pooling = pooling;
            UI = ui;
            Camera = camera;
        }
    }
}