using System;

namespace Gehenna
{
    public class SettingUIModel : BaseUIModel
    {
        public Action OnCancle;
        public Action<IInputHandler> RegisterInputHandler;
    }
}