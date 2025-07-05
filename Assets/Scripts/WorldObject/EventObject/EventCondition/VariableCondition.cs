namespace Gehenna
{
    public class VariableCondition : IEventCondition
    {
        public bool IsMet(EventObject context)
        {
            return true;
        }
    }
}