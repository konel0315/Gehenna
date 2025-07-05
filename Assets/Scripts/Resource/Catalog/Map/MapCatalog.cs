using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "MapCatalog", menuName = "Gehenna/Catalog/Map Catalog")]
    public class MapCatalog : BaseCatalog
    {
        [SerializeField]
        private List<MapEntry> entries = new();
        
        private Dictionary<MapType, MapBundle> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<MapType, MapBundle>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.MapBundle))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(MapBundle)}");
                }
            }
        }
        
        public bool TryGet(MapType key, out MapBundle result)
        {
            if (!lookup.TryGetValue(key, out result))
                return false;

            return true;
        }
    }
}