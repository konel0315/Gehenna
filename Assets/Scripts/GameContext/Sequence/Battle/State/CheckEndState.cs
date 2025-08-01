namespace Gehenna
{
    public class CheckEndState : BaseBattleState
    {
        /*
         * 5단계: 전투 종료 조건 검사
         *  - case true: EndBattleState로 전이
         *  - case false: TurnState로 전이 
         */
        
        public CheckEndState(RoundController owner) : base(owner) { }

        public override RoundState RoundState => RoundState.CheckEndState;

        public override void Update()
        {
            
        }
        
        public override void FixedUpdate()
        {
            
        }
    }
}