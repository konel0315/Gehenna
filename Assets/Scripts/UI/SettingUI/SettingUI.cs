using UnityEngine;

namespace Gehenna
{
    public class SettingUI : BaseUI<SettingUIModel>, IInputHandler
    {
        public override UILayerType LayerType => UILayerType.Overlay;

        protected override void OnOpen()
        {
            base.OnOpen();

            model.RegisterInputHandler(this);
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        public void Move(Vector2 direction)
        {
            
        }

        public void Submit()
        {
            
        }

        public void Cancel()
        {
            model.OnCancle.Invoke();
        }
    }
}