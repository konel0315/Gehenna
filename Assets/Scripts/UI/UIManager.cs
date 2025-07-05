using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class UIManager : ISubManager
    {
        private UIContext context;
        
        private Transform root;
        private readonly Dictionary<UIType, GameObject> openedUIs = new();
        
        public void Initialize(ManagerContext context)
        {
            if (context is not UIContext uiContext)
            {
                GehennaLogger.Log(this, LogType.Error, "Invalid context");
                return;
            }
            this.context = uiContext;
            var uiRoot = new GameObject("UIRoot", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            
            this.root = uiRoot.transform;
            var canvas = uiRoot.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = uiRoot.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
            
            GehennaLogger.Log(this, LogType.Success, "Initialize");
        }

        public void OpenUI(UIType type, BaseUIModel model = null)
        {
            if (openedUIs.ContainsKey(type)) return;
            
            if (!context.ResourceManager.TryGetUI(type, out var ui))
            {
                GehennaLogger.Log(this, LogType.Error, $"{type}Catalog is not found");
                return;
            }
            
            GameObject instance = Object.Instantiate(ui, root);
            if (!instance.TryGetComponent<BaseUI>(out var baseUI))
            {
                GehennaLogger.Log(this, LogType.Error, "BaseUI Component is not found");
                return;
            }

            baseUI.Initialized(model);
            
            openedUIs[type] = instance; 
        }

        public void CloseUI(UIType type)
        {
            var result = GetUI(type);
            if (result == null)
            {
                GehennaLogger.Log(this, LogType.Error, "UI not found");
                return;
            }
            Object.Destroy(result);
            openedUIs.Remove(type);

        }

        public GameObject GetUI(UIType type)
        {
            openedUIs.TryGetValue(type, out var result);
            return result;
        }

        public void CloseAllUI()
        {
            foreach (var openedUI in openedUIs.Values)
                Object.Destroy(openedUI);
            openedUIs.Clear();
        }

        public void CleanUp()
        {
            
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
    }
}