namespace Gehenna
{
    public class World
    {
        private WorldConfig config;
        
        public void Enter(WorldConfig config)
        {
            this.config = config;
            if (this.config == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"Invalid {nameof(WorldConfig)}");
                return;
            }
        }

        public void Exit()
        {
            
        }

        public void ManualUpdate(float deltaTime)
        {
            
        }

        public void ManualFixedUpdate()
        {
            
        }
    }
}