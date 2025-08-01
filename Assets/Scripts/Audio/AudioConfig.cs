using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Gehenna/Config/AudioConfig")]
    public class AudioConfig : BaseConfig
    {
        public int maxDynamicAudioHandlers = 32;
        
        public override void Initialize()
        {
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }
    }
}