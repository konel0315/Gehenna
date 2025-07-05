using UnityEngine;

namespace Gehenna
{
    public class AudioHandleFactory
    {
        public static DynamicAudioHandler CreateDynamicAudioHandler(Transform parent = null)
        {
            GameObject go = new GameObject($"{nameof(DynamicAudioHandler)}");
            go.hideFlags = HideFlags.HideInHierarchy; 
            
            if (parent != null)
                go.transform.SetParent(parent);
            
            AudioSource audioSource = go.AddComponent<AudioSource>();
            return new DynamicAudioHandler(audioSource);
        }

        public static PreloadedAudioHandler CreatePreloadedAudioHandler(AudioClip assignedClip, Transform parent = null)
        {
            GameObject go = new GameObject($"{nameof(PreloadedAudioHandler)}");
            go.hideFlags = HideFlags.HideInHierarchy; 
            
            if (parent != null)
                go.transform.SetParent(parent);
            
            AudioSource audioSource = go.AddComponent<AudioSource>();
            return new PreloadedAudioHandler(audioSource, assignedClip);
        }
    }
}