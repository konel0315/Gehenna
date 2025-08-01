using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Gehenna/Config/CameraConfig")]
    public class CameraConfig : BaseConfig
    {
        public CameraSetting WorldSetting;
        public CameraSetting BattleSetting;
        
        public override void Initialize()
        {
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }
    }
}