using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    [Serializable]
    public class EventContentGroup
    {
        [SerializeReference] 
        public List<IEventContent> Contents;
    }
}