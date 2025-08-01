using System;

namespace Gehenna
{
    public class ManagerParam
    {
        public GameConfig GameConfig { get; }

        public ManagerParam(GameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }
    }
}