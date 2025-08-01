using System;
using Sirenix.OdinInspector;

namespace Gehenna
{
    [Serializable]
    public class BattleEntry : IPoolableEntry
    {
        [ValueDropdown("@BattleKey.AllKeys")]
        public string Key;
        public BattleBundle Bundle;

        string IPoolableEntry.Key => Key;
        PoolableBundle IPoolableEntry.Bundle => Bundle;
    }
}