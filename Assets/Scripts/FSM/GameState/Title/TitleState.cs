namespace Gehenna
{
    public class TitleState : BaseGameState
    {
        private TitleStateConfig config;
        private TitleUI titleUI;
        
        public TitleState(GameFlowManager owner, GameFlowParam param) : base(owner, param) { }

        public override void Enter()
        {
            base.Enter();
            
            if (!param.GameConfig.TryGetGameStateConfig<TitleStateConfig>(out config))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to load {nameof(TitleStateConfig)}");
                return;
            }
            
            TitleUIModel model = new TitleUIModel()
            {
                OnNewGame = OnNewGame,
                OnContinueGame = OnContinueGame,
                OnSetting = OnSetting,
                RegisterInputHandler = param.Input.SetHandler
            };
            
            if (!param.UI.TryOpenUI<TitleUIModel>(UIKey.TitleUI, model, out var titleUI))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to open title UI");
                return;
            }
            
            this.titleUI = titleUI as TitleUI;
        }

        public override void Update() { }
        public override void FixedUpdate() { }

        private void OnNewGame()
        {
            GehennaLogger.Log(this, LogType.Info, "OnNewGame");
        }

        private void OnContinueGame()
        {
            GehennaLogger.Log(this, LogType.Info, "OnContinueGame");
        }

        private void OnSetting()
        {
            GehennaLogger.Log(this, LogType.Info, "OnSetting");
            
            SettingUIModel model = new SettingUIModel()
            {
                OnCancle = OnCancle,
                RegisterInputHandler = param.Input.SetHandler
            };

            if (!param.UI.TryOpenUI<SettingUIModel>(UIKey.SettingUI, model, out var settingUI))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to open {nameof(SettingUI)}");
                return;
            }
        }

        private void OnCancle()
        {
            if (!param.UI.TryCloseUI<SettingUIModel>(UIKey.SettingUI))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to close {nameof(SettingUI)}");
                return;
            }
            
            param.Input.RevertHandler();
        }
    }
}