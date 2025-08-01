using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class CameraParamFactory : IParamFactory
    {
        public Type ManagerType => typeof(CameraManager);
        
        public ManagerParam Create(GameConfig config, ManagerContainer container)
        {
            return new CameraParam(config);
        }
    }
}