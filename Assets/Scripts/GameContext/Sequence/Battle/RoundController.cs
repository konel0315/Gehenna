namespace Gehenna
{
    public class RoundController
    {
        /*
         * 라운드 진행 단계
         *  - 1: 전투 입장
         *  - 2: 플레이어 캐릭터의 턴 시작
         *  - 3: 행동 순서 정렬
         *  - 4: 1순위 캐릭터 행동 수행
         *  - 5: 전투 종료 조건 검사
         *  - 6-1: 5번이 참인 경우, 라운드 종료
         *  - 6-2: 5번이 거짓인 경우, 2번으로 이동
         *
         *  ※ 모든 캐릭터의 행동이 끝날 때까지 3단계를 반복한다.
         */

        public BattleSequence BattleSequence { get; private set; }
        public StateMachine<RoundController> StateMachine { get; private set; }
        
        public void Initialize(BattleSequence battleSequence)
        {
            if (battleSequence == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"Initialize failed: {nameof(Gehenna.BattleSequence)} is null");
                return;
            }

            this.BattleSequence = battleSequence;
            
            StateMachine = new StateMachine<RoundController>(this);
            StateMachine.AddState(new EnterBattleState(this));
            StateMachine.AddState(new TurnState(this));
            StateMachine.AddState(new SortState(this));
            StateMachine.AddState(new ActionState(this));
            StateMachine.AddState(new CheckEndState(this));
            StateMachine.AddState(new EndBattleState(this));
        }

        public void CleanUp()
        {
            StateMachine.CleanUp();
            StateMachine = null;
        }

        public void Run<T>() where T : IState<RoundController>
        {
            StateMachine.ChangeState<T>();   
        }
    }
}