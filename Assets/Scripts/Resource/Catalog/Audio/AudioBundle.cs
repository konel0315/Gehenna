using System;
using UnityEngine;

namespace Gehenna
{
    [Serializable]
    public class AudioBundle
    {
        public AudioClip AudioClip;
        [Range(0, 1)] public float BaseVolume = 1f;
        [Range(1, 9)] public int MaxVoices;
        public BusType BusType;
        public bool Preload;
        public bool Loop;
    }
}