namespace Gehenna
{
    public interface IEventContent
    {
        bool IsCompleted { get; }
        void Execute(EventObject eventObject);
    }
}