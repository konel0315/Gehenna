using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public class SequenceData : SerializedScriptableObject
    {
        [HideInInspector]
        public SequenceService Service;
    }
}