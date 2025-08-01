using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "AtlasCatalog", menuName = "Gehenna/Catalog/Atlas Catalog")]
    public class AtlasCatalog : BaseCatalog
    {
        [SerializeField] private List<AtlasEntry> entries;
        
        private Dictionary<string, AtlasBundle> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<string, AtlasBundle>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.Bundle))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(AtlasBundle)}");
                }
            }
        }
        
        public bool TryGet(string key, out AtlasBundle result)
        {
            if (!lookup.TryGetValue(key, out result))
                return false;

            return true;
        }
        
#if UNITY_EDITOR
        [Button]
        private void GenerateKey()
        {
            GenerateKeyInternal("AtlasKey", "AtlasKey.cs", "*.spriteatlasv2");
        }
#endif
    }
}