namespace Gehenna
{
    public partial class ResourceManager
    {
        public bool TryGetBattle(string key, out BattleBundle result)
        {
            result = null;
            if (!TryGetCatalog<BattleCatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(BattleCatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error,$"{nameof(BattleBundle)} not found: " + key);
                return false;
            }

            return true;
        }
    }
}