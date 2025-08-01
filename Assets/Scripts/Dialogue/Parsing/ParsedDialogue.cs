using System.Collections.Generic;
using UnityEditor;

namespace Gehenna
{
    public class ParsedDialogue
    {
        public List<TextSegment> Segment = new();

        public void AddSegment(string text,float speed,string animation=null)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Segment.Add(new TextSegment(text,speed,animation));
            }
        }
    }
}