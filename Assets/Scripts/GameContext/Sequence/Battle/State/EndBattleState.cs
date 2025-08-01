namespace Gehenna
{
    public class EndBattleState : BaseBattleState
    {
        /*
         * 6단계: 전투 종료
         *  - 월드로 돌아간다.
         */
        
        public EndBattleState(RoundController owner) : base(owner) { }

        public override RoundState RoundState => RoundState.EndBattleState;

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}