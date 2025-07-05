namespace Gehenna
{
    public class ItemCondition : IEventCondition
    {
        public bool IsMet(EventObject context)
        {
            return true;
        }
    }
}