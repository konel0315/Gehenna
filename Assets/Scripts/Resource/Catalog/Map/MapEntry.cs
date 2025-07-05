using System;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class MapEntry
    {
        public MapType Key;
        public MapBundle MapBundle;
    }
}