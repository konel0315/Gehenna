using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public abstract class PoolableBundle
    {
        public GameObject Prefab;
        public bool IsPoolable;
        [ShowIf(nameof(IsPoolable))] public int Capacity;
    }
}