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
        private BaseCatalog[] catalogs;

        [FoldoutGroup("Input")] 
        public InputActionAsset InputActionAsset;
        
        [FoldoutGroup("Audio")]
        public AudioConfig AudioConfig;

        [FoldoutGroup("Dialogue")]//후에 list로 받아서 확장성 패치 하기
        public DialogueTableSO DialogueTableSO;
        
        private Dictionary<Type, BaseCatalog> lookup = new Dictionary<Type, BaseCatalog>();
        
        public void Initialize()
        {
            foreach (var each in catalogs)
            {
                Type type = each.GetType();
                if (lookup.ContainsKey(type))
                {
                    GehennaLogger.Log(this, LogType.Warning, $"Duplicate catalog of type {type.Name} found in GameConfig");
                    continue;
                }

                each.Initialize();
                lookup[type] = each;
            }
        }
        
        public bool TryGetCatalog<T>(out T result) where T : BaseCatalog
        {
            result = null;
            if (lookup == null)
            {
                GehennaLogger.Log(this, LogType.Error, "GameConfig is not initialized");
                return false;
            }

            if (!lookup.TryGetValue(typeof(T), out BaseCatalog catalog))
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
        
        public BaseCatalog[] GetAllCatalogs()
        {
            return catalogs;
        }
    }
}