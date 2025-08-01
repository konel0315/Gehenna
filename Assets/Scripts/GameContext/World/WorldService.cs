namespace Gehenna
{
    public class WorldService
    {
        private World currentWorld;
        private WorldConfig currentConfig;
        
        public GameContextParam ContextParam { get; private set; }

        public WorldService(GameContextParam contextParam)
        {
            this.ContextParam = contextParam;
        }
        
        public void Initialize()
        {
            GehennaLogger.Log(this, LogType.Success, "Initialized");
        }

        public void CleanUp()
        {
            currentWorld = null;
            currentConfig = null;
        }
        
        public void ManualUpdate(float deltaTime)
        {
            currentWorld?.ManualUpdate(deltaTime);
        }
        
        public void ManualFixedUpdate()
        {
            currentWorld?.ManualFixedUpdate();
        }

        public void Enter(WorldParam param)
        {
            if (param.World == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Enter failed: World is null");
                return;
            }

            if (param.Config == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Enter failed: WorldData is null");
                return;
            }
            
            currentWorld = param.World;
            currentConfig = param.Config;
        }

        public void Exit()
        {
            currentWorld?.Exit();
        }
    }
}