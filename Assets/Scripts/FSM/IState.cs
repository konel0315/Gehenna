namespace Gehenna
{
    public interface IState<T>
    {
        void Enter();
        void Exit();
        void Update();
        void FixedUpdate();
    }
}
