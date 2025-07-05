using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public class VolumeMixer
    {
        private float masterVolume;
        private readonly Dictionary<BusType, float> busVolumeMap = new Dictionary<BusType, float>();
        
        public void Initialize()
        {
            masterVolume = 1f;
        }

        public void CleanUp()
        {
            busVolumeMap.Clear();
        }

        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
        }

        public void SetBusVolume(BusType busType, float volume)
        {
            busVolumeMap[busType] = Mathf.Clamp01(volume);
        }

        public float GetMasterVolume()
        {
            return masterVolume;
        }
        
        public float GetBusVolume(BusType busType)
        {
            return busVolumeMap.TryGetValue(busType, out var v) ? v : 1f;
        }

        public float GetFinalVolume(BusType busType, float baseVolume)
        {
            float busVolume = busVolumeMap.TryGetValue(busType, out var v) ? v : 1f;
            return baseVolume * busVolume * masterVolume;
        }
    }
}