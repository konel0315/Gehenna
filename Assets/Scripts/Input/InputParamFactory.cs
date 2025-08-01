using System;

namespace Gehenna
{
    public class InputParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(InputManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new InputParam(gameConfig: config);
        }
    }
}