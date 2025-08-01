namespace Gehenna
{
    public partial class ResourceManager
    {
        public bool TryGetMap(string key, out MapBundle result)
        {
            result = null;
            if (!TryGetCatalog<MapCatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(MapCatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(MapBundle)} not found: " + key);
                return false;
            }

            return true;
        }
    }
}