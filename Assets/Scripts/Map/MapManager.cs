using System;
using Cysharp.Threading.Tasks;

namespace Gehenna
{
    public class MapManager : ISubManager
    {
        private MapContext context;
        private MapRoot currentMapRoot;
        private bool isTransitioning;
        
        public void Initialize(ManagerContext context)
        {
            if (context is not MapContext mapContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = mapContext;
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            context = null;
        }

        public void ManualUpdate()
        {
            
        }

        public void ManualLateUpdate()
        {
            
        }

        public void ManualFixedUpdate()
        {
            
        }
        
        public async UniTask ChangeMap(MapType mapType)
        {
            if (isTransitioning)
                return;

            isTransitioning = true;
            
            try
            {
                // TODO: Fade-Out

                UnloadMap();
                LoadMap(mapType);
                
                // TODO: Fade-In
            }
            finally
            {
                isTransitioning = false;
            }
        }

        private void LoadMap(MapType mapType)
        {
            if (!context.ResourceManager.TryGetMap(mapType, out var mapBundle))
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