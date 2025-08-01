namespace Gehenna
{
    public class GameContextManager : ISubManager
    {
        private GameContextParam param;
        private WorldService worldService;
        private SequenceService sequenceService;
        
        private GameContextType currentContextType;
        
        public void Initialize(ManagerParam param)
        {
            if (param is not GameContextParam gameContextParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = gameContextParam;
            
            worldService = new WorldService(this.param);
            sequenceService = new SequenceService(this.param);
            
            worldService.Initialize();
            sequenceService.Initialize();
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            worldService?.CleanUp();
            sequenceService?.CleanUp();
        }

        public void ManualUpdate(float deltaTime)
        {
            worldService?.ManualUpdate(deltaTime);
            sequenceService?.ManualUpdate(deltaTime);
        }

        public void ManualFixedUpdate()
        {
            worldService?.ManualFixedUpdate();
            sequenceService?.ManualFixedUpdate();
        }

        public void TransitionToWorld(WorldParam param)
        {
            if (currentContextType == GameContextType.World)
            {
                GehennaLogger.Log(this, LogType.Warning, "Already in the requested context");
                return;
            }
            currentContextType = GameContextType.World;
            
            param.SetService(worldService);
            sequenceService.Exit();
            worldService.Enter(param);
        }

        public void TransitionToSequence(SequenceParam param)
        {
            if (currentContextType == GameContextType.Sequence)
            {
                GehennaLogger.Log(this, LogType.Warning, "Already in the requested context");
                return;
            }
            currentContextType = GameContextType.Sequence;

            param.SetService(sequenceService);
            worldService.Exit();
            sequenceService.Enter(param);
        }
    }
}