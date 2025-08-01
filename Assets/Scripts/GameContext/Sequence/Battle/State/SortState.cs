namespace Gehenna
{
    public class SortState : BaseBattleState
    {
        /*
         * 3단계: 행동 순서 정렬
         *  - 전투에 참여 중인 모든 캐릭터를 '속도' 기반으로 내림차순 한다.
         *  - 그 중 1순위 캐릭터를 다음 단계에 전달한다.
         */
        
        public SortState(RoundController owner) : base(owner) { }

        public override RoundState RoundState => RoundState.SortState;

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }
    }
}