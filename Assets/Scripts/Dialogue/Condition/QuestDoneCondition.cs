namespace Gehenna
{
    public class QuestDoneCondition : IConditionEvaluator
    {
        private readonly string questName;

        public QuestDoneCondition(string questName)
        {
            this.questName = questName;
        }

        public bool Evaluate()
        {
            return true;
        }
    }
}