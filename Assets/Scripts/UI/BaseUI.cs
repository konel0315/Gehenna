using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public abstract class BaseUI<TModel> : MonoBehaviour where TModel : BaseUIModel
    {
        [ShowInInspector, ReadOnly]
        public abstract UILayerType LayerType { get; }

        protected TModel model { get; private set; }

        public void Open(BaseUIModel model)
        {
            gameObject.SetActive(true);
            
            if (model is not TModel castedModel)
            {
                GehennaLogger.Log(this, LogType.Error, $"Invalid model type. Expected: {typeof(TModel).Name}, Received: {model?.GetType().Name}");
                return;
            } 
            this.model = castedModel; 
            
            RectTransform rect = GetComponent<RectTransform>(); 
            rect.sizeDelta = model.SizeDelta ?? new Vector2(); 
            rect.anchoredPosition  = model.AnchoredPosition ?? new Vector2(); 
            
            OnOpen();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            OnClose();
        }

        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
    }
}