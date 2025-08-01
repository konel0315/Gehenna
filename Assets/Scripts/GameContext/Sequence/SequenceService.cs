namespace Gehenna
{
    public class SequenceService
    {
        private ISequence currentSequence;
        private SequenceData currentData;
        
        public GameContextParam ContextParam { get; private set; }

        public SequenceService(GameContextParam contextParam)
        {
            this.ContextParam = contextParam;
        }
        
        public void Initialize()
        {
            GehennaLogger.Log(this, LogType.Success, "Initialized");
        }

        public void CleanUp()
        {
            currentSequence = null;
            currentData = null;
        }
        
        public void ManualUpdate(float deltaTime)
        {
            currentSequence?.ManualUpdate(deltaTime);
        }
        
        public void ManualFixedUpdate()
        {
            currentSequence?.ManualFixedUpdate();
        }

        public void Enter(SequenceParam param)
        {
            if (param.Sequence == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Enter failed: Sequence is null");
                return;
            }

            if (param.Data == null)
            {
                GehennaLogger.Log(this, LogType.Error, "Enter failed: SequenceData is null");
                return;
            }
            
            currentSequence = param.Sequence;
            currentData = param.Data;
            currentSequence.Enter(param.Data);
        }

        public void Exit()
        {
            currentSequence?.Exit();
        }
    }
}