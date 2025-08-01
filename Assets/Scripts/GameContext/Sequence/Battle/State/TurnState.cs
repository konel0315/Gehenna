namespace Gehenna
{
    public class TurnState : BaseBattleState
    {
        /*
         * 2단계: 플레이어 캐릭터의 턴 시작
         *  - 플레이어 명령 UI를 연다.
         *  - 모든 플레이어의 명령이 끝날 때까지 기다린다.
         */
        
        public TurnState(RoundController owner) : base(owner) { }

        public override RoundState RoundState => RoundState.TurnState;

        public override void Enter()
        {
            base.Enter();

            BattleUIModel model = new BattleUIModel()
            {

            };

            Service.ContextParam.UI.TryOpenUI<BattleUIModel>(UIKey.BattleUI, model, out var _);
        }

        public override void Update()
        {
            
        }
        
        public override void FixedUpdate()
        {
            
        }
    }
}