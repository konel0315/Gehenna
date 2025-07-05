using System.Collections.Generic;

namespace Gehenna
{
    public class DialogueBranch
    {
        public int DefaultNextID;
        public Dictionary<IConditionEvaluator, int> ConditionalNextIDs = new();
    }
}