using System;
using UnityEngine;

namespace Gehenna
{
    public interface IPoolableEntry
    {
        Enum Key { get; }
        GameObject Prefab { get; }
        int Capacity { get; }
        bool IsPooling { get; }
    }
}