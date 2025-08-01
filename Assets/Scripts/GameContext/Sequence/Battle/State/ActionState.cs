namespace Gehenna
{
    public class ActionState : BaseBattleState
    {
        /*
         * 4단계: 1순위 캐릭터 행동 수행
         *  - 전달 받은 캐릭터의 행동을 수행한다.
         *    적인 경우 판단 및 명령을 먼저 수행한다.
         */
        
        public ActionState(RoundController owner) : base(owner) { }

        public override RoundState RoundState => RoundState.ActionState;

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}