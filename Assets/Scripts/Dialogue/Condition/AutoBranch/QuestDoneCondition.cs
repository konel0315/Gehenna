namespace Gehenna
{
    public class QuestDoneCondition : ABEvaluator
    {
        private readonly string questName;

        public QuestDoneCondition(string questName)
        {
            this.questName = questName;
        }

        public bool Evaluate(DialogueParam param)
        {
            return true;
        }
    }
}