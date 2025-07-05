namespace Gehenna
{
    public class WaitContent : IEventContent
    {
        public bool IsCompleted { get; private set; }
        public void Execute(EventObject eventObject)
        {
            
        }
    }
}