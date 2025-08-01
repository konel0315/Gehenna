using JetBrains.Annotations;
using UnityEngine;

namespace Gehenna
{
    public class IntroState : BaseGameState
    {
        private IntroStateConfig config;
        private IntroUI introUI;
        
        public IntroState(GameFlowManager owner, GameFlowParam param) : base(owner, param) { }

        public override void Enter()
        {
            base.Enter();
            
            if (!param.GameConfig.TryGetGameStateConfig<IntroStateConfig>(out config))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to load {nameof(IntroStateConfig)}");
                return;
            }
            
            IntroUIModel model = new IntroUIModel()
            {
                FadeInDuration = config.FadeInDuration,
                HoldDuration = config.HoldDuration,
                FadeOutDuration = config.FadeOutDuration,
                OnComplete = () => stateMachine.ChangeState<TitleState>()
            };

            if (!param.UI.TryOpenUI<IntroUIModel>(UIKey.IntroUI, model, out var introUI))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to open intro UI");
                return;
            }

            this.introUI = introUI as IntroUI;
        }

        public override void Exit()
        {
            base.Exit();

            config = null;
            param.UI.TryCloseUI<IntroUIModel>(UIKey.IntroUI);
        }

        public override void Update() { }
        public override void FixedUpdate() { }
    }
}
