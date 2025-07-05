namespace Gehenna
{
    public static class ConditionFactory
    {
        public static IConditionEvaluator CreateCondition(string conditionStr)
        {
            if (string.IsNullOrEmpty(conditionStr))
                return null;

            if (conditionStr.StartsWith("QuestDone="))
            {
                string questName = conditionStr.Substring("QuestDone=".Length);
                return new QuestDoneCondition(questName);
            }
            
            return null;
        }
    }
}