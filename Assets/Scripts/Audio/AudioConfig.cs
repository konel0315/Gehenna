using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Gehenna/Audio/AudioConfig")]
    public class AudioConfig : SerializedScriptableObject
    {
        public int maxDynamicAudioHandlers = 32;
    }
}