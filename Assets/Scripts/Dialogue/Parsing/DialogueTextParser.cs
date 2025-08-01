using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Gehenna
{
    public static class DialogueTextParser
    {
        public static ParsedDialogue Parse(string text, float defaultSpeed = 0.05f)
        {
            var result = new ParsedDialogue();
            var currentSpeed = defaultSpeed;
            string currentAnim = null;

            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '<')
                {
                    if (sb.Length > 0)
                    {
                        result.AddSegment(sb.ToString(), currentSpeed, currentAnim);
                        sb.Clear();
                    }
                    int end = text.IndexOf('>', i);
                    if (end == -1) break;

                    string tag = text.Substring(i + 1, end - i - 1);
                    
                    if (tag.StartsWith("speed="))
                    {
                        float.TryParse(tag.Substring(6), out currentSpeed);
                    }
                    else if (tag == "/speed")
                    {
                        currentSpeed = defaultSpeed;
                    }
                    else if (tag.StartsWith("anim="))
                    {
                        currentAnim = tag.Substring(5);
                    }
                    else if (tag == "/anim")
                    {
                        currentAnim = null;
                    }
                    else
                    {
                        sb.Append('<').Append(tag).Append('>');
                        result.AddSegment(sb.ToString(), 0f, null);
                        sb.Clear();
                    }
                    i = end;
                }
                
                else
                {
                    sb.Append(text[i]);
                }
            }

            if (sb.Length > 0)
            {
                result.AddSegment(sb.ToString(), currentSpeed, currentAnim);
            }

            return result;
        }
    }
}
