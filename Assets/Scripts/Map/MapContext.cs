namespace Gehenna
{
    public class MapContext : ManagerContext
    {
        public ResourceManager ResourceManager { get; }
        
        public MapContext
        (
            GameConfig gameConfig,
            ResourceManager resourceManager
        ) : base(gameConfig)
        {
            ResourceManager = resourceManager;
        }
    }
}