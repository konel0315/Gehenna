using System.Collections.Generic;

namespace Gehenna
{
    public class DialogueLine
    {
        public int ID;
        public string Speaker;
        public string Text;
        public string Portrait;
        
        public IDialogueCommand Commands;
        
        public DialogueBranch Branch;
        
        
    }
}