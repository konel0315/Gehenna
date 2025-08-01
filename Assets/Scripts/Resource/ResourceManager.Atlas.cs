using UnityEngine;

namespace Gehenna
{
    public partial class ResourceManager
    {
        public bool TryGetAtlas(string key, out AtlasBundle result)
        {
            result = null;
            if (!TryGetCatalog<AtlasCatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(AtlasCatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error,$"{nameof(AtlasCatalog)} not found: " + key);
                return false;
            }

            return true;
        }

        public bool TryGetSprite(string atlasKey, string spriteKey, out Sprite result)
        {
            result = null;
            if (!TryGetAtlas(atlasKey, out var bundle))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(AtlasCatalog)} not found");
                return false;
            }
            
            result = bundle.Atlas.GetSprite(spriteKey);
            if (result == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(Sprite)} not found: " + spriteKey);
                return false;
            }

            return true;
        }
    }
}