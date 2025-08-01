using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "UICatalog", menuName = "Gehenna/Catalog/UI Catalog")]
    public class UICatalog : BaseCatalog, IPoolableCatalog
    {
        [SerializeField] private List<UIEntry> entries;
        
        private Dictionary<string, UIBundle> lookup;
        
        public override void Initialize()
        {
            if (lookup != null) 
                return;
        
            lookup = new Dictionary<string, UIBundle>();
            foreach (var entry in entries)
            {
                if (!lookup.TryAdd(entry.Key, entry.Bundle))
                {
                    Debug.LogWarning($"Duplicate Item: {entry.Key} in {nameof(UICatalog)}");
                }
            }
        }
        
        public bool TryGet(string key, out UIBundle result)
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
            GenerateKeyInternal("UIKey", "UIKey.cs", "*.prefab");
        }
#endif
    }
}