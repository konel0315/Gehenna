using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public class ResourceManager : ISubManager
    {
        private ResourceContext context;
        private readonly Dictionary<Type, BaseCatalog> catalogMap = new Dictionary<Type, BaseCatalog>();

        public void Initialize(ManagerContext context)
        {
            if (context is not ResourceContext resourceContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = resourceContext;

            foreach (var each in resourceContext.GameConfig.GetAllCatalogs())
            {
                if (!catalogMap.TryAdd(each.GetType(), each))
                    GehennaLogger.Log(this, LogType.Error, "Duplicate catalog");
            }
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            context = null;
            catalogMap.Clear();
        }
        
        public void ManualUpdate() { }
        public void ManualLateUpdate() { }
        public void ManualFixedUpdate() { }

        public bool TryGetCatalog<T>(out T result) where T : BaseCatalog
        {
            if (catalogMap.TryGetValue(typeof(T), out var catalog))
            {
                result = catalog as T;
                return true;
            }

            GehennaLogger.Log(this, LogType.Error, "Catalog not found: " + typeof(T).Name);
            result = null;
            return false;
        }
        
        public bool TryGetUI(UIType key, out GameObject result)
        {
            result = null;
            if (!TryGetCatalog<UICatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(UICatalog)} not found");
                return false;
            }
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error, "UI not found: " + key);
                return false;
            }

            return true;
        }


        public bool TryGetAudio(AudioType key, out AudioTrack result)
        {
            result = null;
            if (!TryGetCatalog<AudioCatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(AudioCatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error, "Audio not found: " + key);
                return false;
            }

            return true;
        }

        public bool TryGetMap(MapType key, out MapBundle result)
        {
            result = null;
            if (!TryGetCatalog<MapCatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(MapCatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out result))
            {
                GehennaLogger.Log(this, LogType.Error, "Map not found: " + key);
                return false;
            }

            return true;
        }
    }
}