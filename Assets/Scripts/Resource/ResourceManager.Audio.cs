namespace Gehenna
{
    public partial class ResourceManager
    {
        public bool TryGetAudio(string key, out AudioBundle result)
        {
            result = null;
            if (!TryGetCatalog<AudioCatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(AudioCatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(AudioBundle)} not found: " + key);
                return false;
            }

            return true;
        }
    }
}