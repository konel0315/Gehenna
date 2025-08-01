using UnityEngine;
using System.Collections.Generic;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "AudioCatalog", menuName = "Gehenna/Catalog/Audio Catalog")]
    public class AudioCatalog : BaseCatalog
    {
        [SerializeField]
        private List<AudioEntry> entries = new();
    
        private Dictionary<string, AudioBundle> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<string, AudioBundle>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.audioBundle))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(AudioCatalog)}");
                }
            }
        }

        public bool TryGet(string key, out AudioBundle result)
        {
            if (!lookup.TryGetValue(key, out result))
                return false;

            return true;
        }
    }
}
