using System;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class AudioEntry
    {
        public string Key;
        public AudioBundle audioBundle;
    }
}