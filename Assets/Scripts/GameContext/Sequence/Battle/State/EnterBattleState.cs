using UnityEngine;

namespace Gehenna
{
    public class EnterBattleState : BaseBattleState
    {
        /*
         * 1단계: 전투 입장
         *  - 카메라 설정
         *  - 그리드 설정 및 캐릭터 배치
         *  - 전투 UI 초기화
         */
        
        public EnterBattleState(RoundController owner) : base(owner) { }

        public override RoundState RoundState => RoundState.EnterBattleState;

        public override void Enter()
        {
            base.Enter();

            InitializeCamera();
            InitializeGrid();
            InitializeUI();
            
            StateMachine.ChangeState<TurnState>();
        }

        public override void Update() { }
        public override void FixedUpdate() { }

        private void InitializeCamera()
        {
            Service.ContextParam.Camera.ApplySetting(CameraContextType.Battle);
        }

        private void InitializeGrid()
        {
            if (!Service.ContextParam.Resource.TryGetBattle(BattleKey.Grid, out var gridBundle))
            {
                GehennaLogger.Log(this, LogType.Error, "Failed to load battle grid resource. BattleKey: Grid");
                return;
            }

            GameObject instance = Service.ContextParam.Pooling.GetMono(BattleKey.Grid);
            if (!instance.TryGetComponent<Grid>(out var grid))
            {
                GehennaLogger.Log(this, LogType.Error, "The grid object is missing the required Grid component.");
                return;
            }
            
            grid.Initialize(new GridModel()
            {

            });
        }

        private void InitializeUI()
        {
            BattleUIModel model = new BattleUIModel()
            {
                BattleCache = Cache
            };

            if (!Service.ContextParam.UI.TryOpenUI<BattleUIModel>(UIKey.BattleUI, model, out _))
            {
                GehennaLogger.Log(this, LogType.Error, $"Failed to open {nameof(BattleUI)}");
            }
        }
    }
}