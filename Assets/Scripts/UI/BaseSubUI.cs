using UnityEngine;

namespace Gehenna
{
    public abstract class BaseSubUI<TModel> : MonoBehaviour where TModel : BaseSubUIModel
    {
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