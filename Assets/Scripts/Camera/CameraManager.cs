using UnityEngine;

namespace Gehenna
{
    public class CameraManager : ISubManager
    {
        private Camera mainCamera;
        private CameraParam param;
        private CameraConfig config;
        
        public void Initialize(ManagerParam param)
        {
            if (param is not CameraParam cameraParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = cameraParam;
            
            if (!cameraParam.GameConfig.TryGetConfig<CameraConfig>(out config))
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid config");
                return;
            }

            mainCamera = Camera.main;
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            param = null;
            config = null;
        }

        public void ManualUpdate(float deltaTime) { }

        public void ManualFixedUpdate() { }
        
        public void ApplySetting(CameraContextType contextType)
        {
            switch (contextType)
            {
                case CameraContextType.World:
                {
                    config.WorldSetting.ApplyTo(mainCamera);
                    break;
                }
                
                case CameraContextType.Battle:
                {
                    config.BattleSetting.ApplyTo(mainCamera);
                    break;
                }

                default:
                {
                    GehennaLogger.Log(this, LogType.Error, $"Invalid Type: {contextType}");
                    break;
                }
            }
        }
    }
}