using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "MapCatalog", menuName = "Gehenna/Catalog/Map Catalog")]
    public class MapCatalog : BaseCatalog
    {
        [SerializeField]
        private List<MapEntry> entries = new();
        
        private Dictionary<string, MapBundle> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<string, MapBundle>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.Bundle))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(MapBundle)}");
                }
            }
        }
        
        public bool TryGet(string key, out MapBundle result)
        {
            if (!lookup.TryGetValue(key, out result))
                return false;

            return true;
        }

#if UNITY_EDITOR
        [Button]
        private void GenerateKey()
        {
            GenerateKeyInternal("MapKey", "MapKey.cs", "*.prefab");
        }
#endif
    }
}