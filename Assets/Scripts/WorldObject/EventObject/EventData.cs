using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [Serializable]
    [CreateAssetMenu(fileName = "EventData", menuName = "Gehenna/Events/EventData", order = 1)]
    public class EventData : SerializedScriptableObject
    {
        public List<EventPage> eventPages;
        
        public bool TryGetActivePage(EventObject eventObject, out EventPage activePage)
        {
            activePage = null;
            int highestPriority = int.MinValue;

            if (eventPages == null || eventPages.Count == 0)
                return false;

            foreach (var page in eventPages)
            {
                if (page.Condition.IsMet(eventObject))
                {
                    if (page.Priority > highestPriority)
                    {
                        highestPriority = page.Priority;
                        activePage = page;
                    }
                }
            }

            return activePage != null;
        }
    }
}