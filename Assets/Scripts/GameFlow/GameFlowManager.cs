namespace Gehenna
{
    public class GameFlowManager : ISubManager
    {
        private GameFlowContext context;
        private GameFlowContainer container;
        private StateMachine<GameFlowManager> stateMachine;
        
        public void Initialize(ManagerContext context)
        {
            if (context is not GameFlowContext gameFlowContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = gameFlowContext;
            
            container = new GameFlowContainer();
            container.Register(() => new BootState());
            container.Register(() => new IntroState());
            container.Register(() => new PlayState());
            container.Register(() => new TitleState());
            
            BaseGameState[] states = new BaseGameState[]
            {
                container.Resolve<BootState>(),
                container.Resolve<IntroState>(),
                container.Resolve<PlayState>(),
                container.Resolve<TitleState>()
            };

            stateMachine = new StateMachine<GameFlowManager>(this);
            foreach (var state in states)
            {
                stateMachine.AddState(state);
            }
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            context = null;
        }

        public void ManualUpdate()
        {
            stateMachine?.ManualUpdate();
        }

        public void ManualLateUpdate()
        {
            stateMachine?.ManualLateUpdate();
        }

        public void ManualFixedUpdate()
        {
            stateMachine?.ManualFixedUpdate();
        }

        public void Run()
        {
            stateMachine.ChangeState<BootState>();
        }

        public BaseGameState GetGameState<T>() where T : BaseGameState
        {
            return container.Resolve<T>();
        }
    }
}