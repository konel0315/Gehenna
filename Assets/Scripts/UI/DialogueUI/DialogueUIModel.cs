using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public class DialogueUIModel : BaseUIModel
    {
        public string Speaker { get; set; }
        
        public string Text { get; set; }
        public List<TextSegment> Segments { get; set; }
        public string Portrait { get; set; }
        public float TypingSpeed { get; set; } = 0;
        
        public Action OnAutoNext { get; set;}
        
        public Action OnLog { get; set; }

        public Action OnNext { get; set; }
    }
}