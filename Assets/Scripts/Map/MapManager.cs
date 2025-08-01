using System;
using Cysharp.Threading.Tasks;

namespace Gehenna
{
    public class MapManager : ISubManager
    {
        private MapParam _param;
        private MapRoot currentMapRoot;
        private bool isTransitioning;
        
        public void Initialize(ManagerParam param)
        {
            if (param is not MapParam mapContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this._param = mapContext;
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            _param = null;
        }

        public void ManualUpdate(float deltaTime)
        {
            
        }

        public void ManualFixedUpdate()
        {
            
        }
        
        public void ChangeMap(string key)
        {
            if (isTransitioning)
                return;

            isTransitioning = true;
            
            try
            {
                // TODO: Fade-Out

                UnloadMap();
                LoadMap(key);
                
                // TODO: Fade-In
            }
            finally
            {
                isTransitioning = false;
            }
        }

        private void LoadMap(string key)
        {
            if (!_param.ResourceManager.TryGetMap(key, out var mapBundle))
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid map type");
                return;
            }

            currentMapRoot = UnityEngine.Object.Instantiate(mapBundle.mapPrefab);
        }

        private void UnloadMap()
        {
            if (currentMapRoot == null)
                return;
            
            UnityEngine.Object.Destroy(currentMapRoot.gameObject);
            currentMapRoot = null;
        }
    }
}