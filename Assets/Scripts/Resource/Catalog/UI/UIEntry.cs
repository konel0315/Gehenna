using System;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class UIEntry : IPoolableEntry
    {
        [ValueDropdown("@UIKey.AllKeys")]
        public string Key;
        public UIBundle Bundle;
        
        string IPoolableEntry.Key => Key;
        PoolableBundle IPoolableEntry.Bundle => Bundle;
    }
}