using System;

namespace Gehenna
{
    public class GameFlowContextFactory : IContextFactory
    {
        public Type ManagerType => typeof(GameFlowManager);
        
        public ManagerContext Create(GameConfig config, ManagerContainer container)
        {
            return new GameFlowContext(config);
        }
    }
}