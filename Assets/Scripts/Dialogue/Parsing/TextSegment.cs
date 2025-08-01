namespace Gehenna
{
    public class TextSegment
    {
        public string Content;
        public float TypingSpeed;
        public string Animation;

        public TextSegment(string content, float typingSpeed, string animation=null)
        {
            Content = content;
            TypingSpeed = typingSpeed;
            Animation = animation;
        }
    }
}