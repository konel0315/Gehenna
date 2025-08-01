using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class UIManager : ISubManager
    {
        private UIParam param;
        private UIConfig config;
        
        private readonly Dictionary<UILayerType, Canvas> layerCanvasMap = new Dictionary<UILayerType, Canvas>();
        private readonly Dictionary<string, GameObject> nonPooledUIMap = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, LinkedList<GameObject>> pooledUIMap = new Dictionary<string, LinkedList<GameObject>>();

        public void Initialize(ManagerParam param)
        {
            if (param is not UIParam uiParam)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.param = uiParam;
            
            if (!uiParam.GameConfig.TryGetConfig<UIConfig>(out config))
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid config");
                return;
            }

            foreach ((UILayerType layerType, GameObject prefab) in config.GetCanvasPrefabs())
            {
                GameObject go = UnityEngine.Object.Instantiate(prefab);
                go.name = $"CV_{layerType}";
                
                if (!go.TryGetComponent<Canvas>(out var canvas))
                {
                    GehennaLogger.Log(this, LogType.Error, $"Canvas component is not found in {layerType}");
                    continue;
                }
                
                layerCanvasMap.Add(layerType, canvas);
            }
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }
        
        public void CleanUp()
        {
            param = null;
            config = null;

            foreach (var each in layerCanvasMap.Values)
                UnityEngine.Object.Destroy(each.gameObject);
            
            foreach (var each in nonPooledUIMap.Values)
                UnityEngine.Object.Destroy(each.gameObject);
            
            layerCanvasMap.Clear();
            nonPooledUIMap.Clear();
        }

        public void ManualUpdate(float deltaTime) { }

        public void ManualLateUpdate() { }

        public void ManualFixedUpdate() { }

        public bool TryOpenUI<TModel>(string key, BaseUIModel model, out BaseUI<TModel> result) where TModel : BaseUIModel
        {
            result = null;
            
            if (!param.Resource.TryGetUI<TModel>(key, out var bundle))
            {
                GehennaLogger.Log(this, LogType.Error, $"{typeof(TModel)} is not found");
                return false;
            }
            
            GameObject instance = CreateUIInstance(key, bundle, out var isNew);
            if (!instance.TryGetComponent<BaseUI<TModel>>(out result))
            {
                GehennaLogger.Log(this, LogType.Error, $"BaseUI Component is not found on instance '{instance.name}' for UI key '{key}'");
                return false;
            }
            
            if (!TryGetCanvas(result.LayerType, out var canvas))
            {
                GehennaLogger.Log(this, LogType.Error, $"Canvas for {result.LayerType} not found");
                return false;
            }
            
            instance.transform.SetParent(canvas.transform, false);

            if (bundle.IsPoolable)
            {
                if (!pooledUIMap.TryGetValue(key, out var list))
                {
                    list = new LinkedList<GameObject>();
                    pooledUIMap[key] = list;
                }
                
                pooledUIMap[key].AddLast(instance);
            }
            else if (isNew)
            {
                nonPooledUIMap[key] = instance;
            }
            
            result.Open(model);
            return true;
        }
        
        public bool TryCloseUI<TModel>(string key, GameObject instance = null) where TModel : BaseUIModel
        {
            if (!param.Resource.TryGetUI<TModel>(key, out var bundle))
            {
                GehennaLogger.Log(this, LogType.Error, $"{typeof(TModel)} is not found");
                return false;
            }

            if (bundle.IsPoolable)
            {
                return TryClosePooledUI<TModel>(key, instance);
            }
            else
            {
                return TryCloseNonPoolableUI<TModel>(key);
            }
        }
        
        public bool TryGetUI<TModel>(string key, out BaseUI<TModel> result) where TModel : BaseUIModel
        {
            result = null;
            
            if (!param.Resource.TryGetUI<TModel>(key, out var bundle))
            {
                GehennaLogger.Log(this, LogType.Error, $"{typeof(TModel)} is not found");
                return false;
            }

            if (bundle.IsPoolable)
            {
                GehennaLogger.Log(this, LogType.Error, $"Cannot get pooled UI instances via {nameof(TryGetUI)}. Use TryGetUIs instead.");
                return false;
            }
            
            if (!nonPooledUIMap.TryGetValue(key, out var baseUI))
            {                
                GehennaLogger.Log(this, LogType.Warning, $"{typeof(TModel)} UI is not opened");
                return false;
            }

            if (!baseUI.TryGetComponent<BaseUI<TModel>>(out result))
            {
                GehennaLogger.Log(this, LogType.Error, "");
                return false;
            }

            return true;
        }
        
        private bool TryCloseNonPoolableUI<TModel>(string key) where TModel : BaseUIModel
        {
            if (!TryGetUI<TModel>(key, out var result))
            {
                GehennaLogger.Log(this, LogType.Warning, "UI not found: " + typeof(TModel));
                return false;
            }
            
            result.Close();
            return true;
        }

        private bool TryClosePooledUI<TModel>(string key, GameObject instance) where TModel : BaseUIModel
        {
            if (!pooledUIMap.TryGetValue(key, out var list))
            {
                GehennaLogger.Log(this, LogType.Warning, $"Pooled UI list not found for type {key} ({typeof(TModel)})");
                return false;
            }
            
            if (!list.Contains(instance))
            {
                GehennaLogger.Log(this, LogType.Warning, $"The provided UI instance is not managed in the pool for type {key} ({typeof(TModel)})");
                return false;
            }
            
            if (!instance.TryGetComponent<BaseUI<TModel>>(out var baseUI))
            {
                GehennaLogger.Log(this, LogType.Error, $"BaseUI<{typeof(TModel)}> component not found on the UI instance of type {key}");
                return false;
            }
            
            baseUI.Close();
            param.Pooling.ReleaseMono(key, instance);
            pooledUIMap[key].Remove(instance);
            return true;
        }
        
        private bool TryGetCanvas(UILayerType layerType, out Canvas result)
        {
            return layerCanvasMap.TryGetValue(layerType, out result);
        }
        
        private GameObject CreateUIInstance(string key, UIBundle bundle, out bool isNew)
        {
            isNew = false;

            if (bundle.IsPoolable)
                return param.Pooling.GetMono(key);

            if (!nonPooledUIMap.TryGetValue(key, out var instance))
            {
                instance = UnityEngine.Object.Instantiate(bundle.Prefab);
                isNew = true;
            }

            return instance;
        }
    }
}