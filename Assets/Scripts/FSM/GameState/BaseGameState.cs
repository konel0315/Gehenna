using Gehenna;
using UnityEngine;

namespace Gehenna
{
    public abstract class BaseGameState : IState<GameFlowManager>
    {
        protected GameFlowManager owner { get; private set; }
        protected StateMachine<GameFlowManager> stateMachine { get; private set; }
    
        public virtual void Initialize(GameFlowManager owner, StateMachine<GameFlowManager> stateMachine)
        {
            this.owner = owner;
            this.stateMachine = stateMachine;
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

        public abstract void LateUpdate();

        public abstract void FixedUpdate();
    }
}