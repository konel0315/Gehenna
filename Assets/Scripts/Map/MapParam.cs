namespace Gehenna
{
    public class MapParam : ManagerParam
    {
        public ResourceManager ResourceManager { get; }
        
        public MapParam
        (
            GameConfig gameConfig,
            ResourceManager resourceManager
        ) : base(gameConfig)
        {
            ResourceManager = resourceManager;
        }
    }
}