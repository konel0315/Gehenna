using System;

namespace Gehenna
{
    public class BattleUIModel : BaseUIModel
    {
        public BattleCache BattleCache;
        
        public Action<IInputHandler> RegisterInputHandler;
    }
}