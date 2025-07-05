using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gehenna
{
    [Serializable]
    public class EventGraphic
    {
        [PreviewField] public Sprite MainSprite;
        public DirectinalAnimation Idle;
        public DirectinalAnimation Walk;
    }
}