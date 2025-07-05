namespace Gehenna
{
    public interface IEventCondition
    {
        bool IsMet(EventObject context);
    }
}