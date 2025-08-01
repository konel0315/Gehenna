using System;
using UnityEngine;

namespace Gehenna
{
    public interface IPoolableEntry
    {
        string Key { get; }
        PoolableBundle Bundle { get; }
    }
}