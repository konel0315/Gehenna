using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public partial class ResourceManager : ISubManager
    {
        private ResourceParam param;
        private readonly Dictionary<Type, BaseCatalog> catalogMap = new Dictionary<Type, BaseCatalog>();

        public void Initialize(ManagerParam param)
        {
            if (param is not ResourceParam resourceParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = resourceParam;

            foreach (var each in resourceParam.GameConfig.GetAllCatalogs())
            {
                if (!catalogMap.TryAdd(each.GetType(), each))
                    GehennaLogger.Log(this, LogType.Error, "Duplicate catalog");
            }
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void CleanUp()
        {
            param = null;
            catalogMap.Clear();
        }
        
        public void ManualUpdate(float deltaTime) { }
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
    }
}