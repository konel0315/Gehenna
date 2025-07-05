namespace Gehenna
{
    public class AudioContext : ManagerContext
    {
        public PoolingManager PoolingManager { get; private set; }

        public AudioContext(GameConfig gameConfig, PoolingManager poolingManager) : base(gameConfig)
        {
            this.PoolingManager = poolingManager;
        }
    }
}