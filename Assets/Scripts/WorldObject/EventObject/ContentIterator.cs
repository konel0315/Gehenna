namespace Gehenna
{
    public class ContentIterator
    {
        private EventObject owner;
        private EventData eventData;
        
        private int currentIndex;
        private bool isCompleted;
        
        public void Initialize(EventObject owner, EventData eventData)
        {
            this.owner = owner;
            this.eventData = eventData;
        }

        public void CleanUp()
        {
            owner = null;
            eventData = null;
        }
        
        public void ManualUpdate()
        {
            if (isCompleted)
                return;
            
            if (!eventData.TryGetActivePage(owner, out var activePage))
                return;

            EventContentGroup contentGroup = activePage.Content;
            IEventContent current = contentGroup.Contents[currentIndex];
            current.Execute(owner);
            
            if (current.IsCompleted)
            {
                currentIndex++;
                if (currentIndex >= contentGroup.Contents.Count)
                    isCompleted = true;
            }
        }

        public void ManualLateUpdate()
        {
            
        }
        
        public void ManualFixedUpdate()
        {
            
        }
    }
}