using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "BattleCatalog", menuName = "Gehenna/Catalog/Battle Catalog")]
    public class BattleCatalog : BaseCatalog, IPoolableCatalog
    {
        [SerializeField] private List<BattleEntry> entries;

        private Dictionary<string, BattleBundle> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<string, BattleBundle>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.Bundle))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(UICatalog)}");
                }
            }
        }
        
        public bool TryGet(string key, out BattleBundle result)
        {
            if (!lookup.TryGetValue(key, out result))
                return false;

            return true;
        }
        
        public IEnumerable<IPoolableEntry> GetPoolableEntries()
        {
            return entries;
        }

#if UNITY_EDITOR
        [Button]
        private void GenerateKey()
        {
            GenerateKeyInternal("BattleKey", "BattleKey.cs", "*.prefab");
        }
#endif
    }
}