using Unity.VisualScripting;

namespace Gehenna
{
    public class WorldParam
    {
        public World World;
        public WorldConfig Config;

        public void SetService(WorldService owner)
        {
            Config.Service = owner;
        }
    }
}