namespace Gehenna
{
    public class AudioParam : ManagerParam
    {
        public PoolingManager PoolingManager { get; private set; }

        public AudioParam(GameConfig gameConfig, PoolingManager poolingManager) : base(gameConfig)
        {
            this.PoolingManager = poolingManager;
        }
    }
}