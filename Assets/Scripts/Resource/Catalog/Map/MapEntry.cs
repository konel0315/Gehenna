using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class MapEntry
    {
        [ValueDropdown("@MapKey.AllKeys")]
        public string Key;
        public MapBundle Bundle;
    }
}