using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class UIEntry : IPoolableEntry
    {
        public UIType Key;
        public GameObject Prefab;
        public int Capacity;
        public bool IsPooling;
        
        
        Enum IPoolableEntry.Key => Key;
        GameObject IPoolableEntry.Prefab => Prefab;
        int IPoolableEntry.Capacity => Capacity;
        bool IPoolableEntry.IsPooling => IsPooling;
    }
}