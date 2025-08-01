namespace Gehenna
{
    public interface ISubManager
    {
        void Initialize(ManagerParam param);
        void CleanUp();
        void ManualUpdate(float deltaTime);
        void ManualFixedUpdate();
    }
}