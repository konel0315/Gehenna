using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gehenna
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Gehenna/Core/GameConfig")]
    public class GameConfig : SerializedScriptableObject
    {
        [FoldoutGroup("Catalog"), SerializeField] 
        private List<BaseCatalog> catalogs;

        [FoldoutGroup("Config"), SerializeField] 
        private List<BaseConfig> configs;

        [FoldoutGroup("GameDesign"), SerializeField]
        private List<BaseTableSO> tableSOList;

        [FoldoutGroup("GameState"), SerializeField]
        private List<BaseGameStateConfig> gameStateConfigs;

        public float FixedDeltaTime;
        
        private readonly Dictionary<Type, BaseCatalog> catalogLookup = new Dictionary<Type, BaseCatalog>();
        private readonly Dictionary<Type, BaseConfig> configLookup = new Dictionary<Type, BaseConfig>();
        private readonly Dictionary<Type, BaseTableSO> tableLookup = new Dictionary<Type, BaseTableSO>();
        private readonly Dictionary<Type, BaseGameStateConfig> gameStateConfigLookup = new Dictionary<Type, BaseGameStateConfig>();
        
        public void Initialize()
        {
            InitializeCatalog();
            InitializeConfig();
            InitializeTableSO();
            InitializeGameStateConfig();
        }
        
        public bool TryGetCatalog<T>(out T result) where T : BaseCatalog
        {
            result = null;
            if (catalogLookup == null)
            {
                GehennaLogger.Log(this, LogType.Error, "GameConfig is not initialized");
                return false;
            }

            if (!catalogLookup.TryGetValue(typeof(T), out BaseCatalog catalog))
            {
                return false;
            }

            result = catalog as T;
            if (result == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"Could not find catalog of type {typeof(T).Name}");
                return false;
            }
            
            return true;
        }

        public bool TryGetConfig<T>(out T result) where T : BaseConfig
        {
            result = null;
            if (configLookup == null)
            {
                GehennaLogger.Log(this, LogType.Error, "GameConfig is not initialized");
                return false;
            }

            if (!configLookup.TryGetValue(typeof(T), out BaseConfig config))
            {
                return false;
            }

            result = config as T;
            if (result == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"Could not find config of type {typeof(T).Name}");
                return false;
            }
            
            return true;
        }
        
        public bool TryGetTableSO<T>(out T result) where T : BaseTableSO
        {
            result = null;
            if (tableLookup == null)
            {
                GehennaLogger.Log(this, LogType.Error, "GameConfig is not initialized");
                return false;
            }

            if (!tableLookup.TryGetValue(typeof(T), out BaseTableSO tableSO))
            {
                return false;
            }

            result = tableSO as T;
            if (result == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"Could not find TableSO of type {typeof(T).Name}");
                return false;
            }
            
            return true;
        }
        
        public bool TryGetGameStateConfig<T>(out T result) where T : BaseGameStateConfig
        {
            result = null;
            if (gameStateConfigLookup == null)
            {
                GehennaLogger.Log(this, LogType.Error, "GameConfig is not initialized");
                return false;
            }

            if (!gameStateConfigLookup.TryGetValue(typeof(T), out BaseGameStateConfig gameStateConfig))
            {
                return false;
            }

            result = gameStateConfig as T;
            if (result == null)
            {
                GehennaLogger.Log(this, LogType.Error, $"Could not find GameStateConfig of type {typeof(T).Name}");
                return false;
            }
            
            return true;
        }
        
        public List<BaseCatalog> GetAllCatalogs()
        {
            return catalogs;
        }
        
        public List<BaseConfig> GetAllConfigs()
        {
            return configs;
        }
        
        public List<BaseTableSO> GetAllTableSOs()
        {
            return tableSOList;
        }

        public List<BaseGameStateConfig> GetAllGameStateConfigs()
        {
            return gameStateConfigs;
        }

        private void InitializeCatalog()
        {
            foreach (var each in catalogs)
            {
                Type type = each.GetType();
                if (catalogLookup.ContainsKey(type))
                {
                    GehennaLogger.Log(this, LogType.Warning, $"Duplicate item of type {type.Name} found in GameConfig");
                    continue;
                }

                each.Initialize();
                catalogLookup[type] = each;
            }
        }
        
        private void InitializeConfig()
        {
            foreach (var each in configs)
            {
                Type type = each.GetType();
                if (configLookup.ContainsKey(type))
                {
                    GehennaLogger.Log(this, LogType.Warning, $"Duplicate item of type {type.Name} found in GameConfig");
                    continue;
                }

                each.Initialize();
                configLookup[type] = each;
            }
        }

        private void InitializeTableSO()
        {
            foreach (var each in tableSOList)
            {
                Type type = each.GetType();
                if (tableLookup.ContainsKey(type))
                {
                    GehennaLogger.Log(this, LogType.Warning, $"Duplicate item of type {type.Name} found in GameConfig");
                    continue;
                }

                tableLookup[type] = each;
            }
        }

        private void InitializeGameStateConfig()
        {
            foreach (var each in gameStateConfigs)
            {
                Type type = each.GetType();
                if (gameStateConfigLookup.ContainsKey(type))
                {
                    GehennaLogger.Log(this, LogType.Warning, $"Duplicate item of type {type.Name} found in GameConfig");
                    continue;
                }

                gameStateConfigLookup[type] = each;
            }
        }
        
#if UNITY_EDITOR
        public void AddTableSO(BaseTableSO tableSO)
        {
            if (tableSOList.Contains(tableSO))
                return;
            
            tableSOList.Add(tableSO);
        }
#endif
    }
}