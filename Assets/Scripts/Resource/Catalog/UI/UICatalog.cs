using UnityEngine;
using System.Collections.Generic;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "UICatalog", menuName = "Gehenna/Catalog/UI Catalog")]
    public class UICatalog : BaseCatalog , IPoolableCatalog
    {
        [SerializeField] private List<UIEntry> entries;
        
        private Dictionary<UIType, GameObject> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<UIType, GameObject>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.Prefab))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(UICatalog)}");
                }
            }
        }
        
        public bool TryGet(UIType key, out GameObject result)
        {
            if (!lookup.TryGetValue(key, out result))
                return false;

            return true;
        }
        
        public IEnumerable<IPoolableEntry> GetPoolableEntries()
        {
            foreach (var entry in entries)
                yield return entry;
        }
    }
}