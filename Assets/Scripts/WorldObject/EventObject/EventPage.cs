using System;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class EventPage
    {
        public int Priority;
        public EventGraphic Graphic;
        public EventConditionGroup Condition;
        public EventContentGroup Content;
    }
}