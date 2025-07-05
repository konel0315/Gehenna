using UnityEngine;

namespace Gehenna
{
    public class SwitchCondition : IEventCondition
    {
        public bool IsMet(EventObject context)
        {
            return true;
        }
    }
}