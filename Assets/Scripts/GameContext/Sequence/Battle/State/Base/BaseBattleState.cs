namespace Gehenna
{
    public abstract class BaseBattleState : IState<RoundController>
    {
        protected RoundController Owner { get; private set; }
        protected StateMachine<RoundController> StateMachine => Owner.StateMachine;
        
        protected BattleConfig Config => Owner.BattleSequence.Config;
        protected BattleCache Cache => Owner.BattleSequence.Cache;
        protected SequenceService Service => Config.Service;
        
        public abstract RoundState RoundState { get; }

        public BaseBattleState(RoundController owner)
        {
            this.Owner = owner;
        }
        
        public virtual void Enter()
        {
            GehennaLogger.Log(this, LogType.Info, "Entered");
        }

        public virtual void Exit()
        {
            GehennaLogger.Log(this, LogType.Info, "Exit");
        }

        public abstract void Update();
        public abstract void FixedUpdate();
    }
}