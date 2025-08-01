namespace Gehenna
{
    public class GameFlowManager : ISubManager
    {
        private GameFlowParam param;
        private GameFlowContainer container;
        private StateMachine<GameFlowManager> stateMachine;
        
        public void Initialize(ManagerParam param)
        {
            if (param is not GameFlowParam gameFlowParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = gameFlowParam;
            
            container = new GameFlowContainer();
            container.Register(() => new IntroState(this, this.param));
            container.Register(() => new TitleState(this, this.param));
            container.Register(() => new PlayState(this, this.param));
            
            BaseGameState[] states = new BaseGameState[]
            {
                container.Resolve<IntroState>(),
                container.Resolve<TitleState>(),
                container.Resolve<PlayState>(),
            };
            
            stateMachine = new StateMachine<GameFlowManager>(this);
            
            foreach (var state in states)
                stateMachine.AddState(state);
 
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            
        }

        public void ManualUpdate(float deltaTime)
        {
            stateMachine?.ManualUpdate();
        }

        public void ManualFixedUpdate()
        {
            stateMachine?.ManualFixedUpdate();
        }

        public void Run()
        {
            stateMachine.ChangeState<IntroState>();
        }

        public BaseGameState GetGameState<T>() where T : BaseGameState
        {
            return container.Resolve<T>();
        }

        public StateMachine<GameFlowManager> GetStateMachine()
        {
            return stateMachine;
        }
    }
}