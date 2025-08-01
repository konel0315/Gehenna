namespace Gehenna
{
    public interface ISequence
    {
        void Enter(SequenceData data);
        void Exit();
        void ManualUpdate(float deltaTime);
        void ManualFixedUpdate();
    }
}