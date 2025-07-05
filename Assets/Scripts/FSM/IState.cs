namespace Gehenna
{
    public interface IState<T>
    {
        void Initialize(T owner, StateMachine<T> stateMachine);
        void Enter();
        void Exit();
        void Update();
        void LateUpdate();
        void FixedUpdate();
    }
}
