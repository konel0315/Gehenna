using Gehenna;
using UnityEngine;

namespace Gehenna
{
    public abstract class BaseGameState : IState<GameFlowManager>
    {
        protected GameFlowManager owner { get; private set; }
        protected GameFlowParam param { get; private set; }
        protected StateMachine<GameFlowManager> stateMachine => owner.GetStateMachine();

        public BaseGameState(GameFlowManager owner, GameFlowParam param)
        {
            this.owner = owner;
            this.param = param;
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