using System;
using UnityEngine;

namespace Gehenna
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected BaseUIModel model;

        public void Initialized(BaseUIModel model)
        {
           this.model = model;
           RectTransform rect = GetComponent<RectTransform>();
           if(model != null){
               rect.sizeDelta = model.SizeDelta ?? new Vector2();
               rect.anchoredPosition  = model.AnchoredPosition ?? new Vector2();
               
           }
           
           OnOpen();
        }

        public virtual void OnOpen()
        {
            
        }

        public virtual void OnClose()
        {
        }

        public virtual void SetModel(BaseUIModel model)
        {
        }
        
    }
}