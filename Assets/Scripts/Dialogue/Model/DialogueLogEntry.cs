using System.Text.RegularExpressions;

namespace Gehenna
{
    public class DialogueLogEntry
    {
        public string Speaker { get; private set; }
        public string Text { get; private set; }
        public string Portrait { get; private set; }

        public DialogueLogEntry(string speaker, string text, string portrait)
        {
            Speaker = speaker;
            Text = RemoveTags(text);
            Portrait = portrait;
        }

        private string RemoveTags(string input)
        {
            var tagRegex = new Regex(@"<.*?>");
            return tagRegex.Replace(input, "");
        }
    }
}