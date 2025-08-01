using System;

namespace Gehenna
{
    public class TitleUIModel : BaseUIModel
    {
        public Action OnNewGame;
        public Action OnContinueGame;
        public Action OnSetting;
        public Action<IInputHandler> RegisterInputHandler;
    }
}