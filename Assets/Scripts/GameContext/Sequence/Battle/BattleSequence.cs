namespace Gehenna
{
    public class BattleSequence : ISequence
    {
        private RoundController roundController;
        
        public BattleConfig Config { get; private set; }
        public BattleCache Cache { get; private set; }
        
        public void Enter(SequenceData data)
        {
            this.Config = data as BattleConfig;
            if (this.Config == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid Data Type");
                return;
            }

            Cache ??= new BattleCache();
            roundController ??= new RoundController();
            
            Cache.Initialize();
            roundController.Initialize(this);
        }

        public void Exit()
        {
            Cache.CleanUp();
            roundController.CleanUp();
            roundController = null;
        }

        public void ManualUpdate(float deltaTime)
        {
            
        }

        public void ManualFixedUpdate()
        {
            
        }
    }
}