using System.Collections.Generic;

namespace Gehenna
{
    public class DialogueConverter
    {
        public static Dictionary<string, List<DialogueTable>> GroupByEventName(List<DialogueTable> tables)
        {
            var dic = new Dictionary<string, List<DialogueTable>>();
            string currentEventName = null;
            
            foreach (var table in tables)
            {
                if (!string.IsNullOrEmpty(table.EventName))
                {
                    if (table.EventName == "End")
                    {
                        currentEventName = null;
                        continue;
                    }
                    currentEventName = table.EventName;
                    if (!dic.ContainsKey(currentEventName))
                    {
                        dic[currentEventName] = new List<DialogueTable>();
                    }
                }

                if (currentEventName != null)
                {
                    dic[currentEventName].Add(table);
                }
            }
            return dic;
        }

        public static List<DialogueLine> ConvertTablesToLines(List<DialogueTable> tables)
        {
            var list = new List<DialogueLine>();
            foreach (var table in tables)
            {
                IDialogueCommand command = null;

                if (!string.IsNullOrEmpty(table.CommandType))
                {
                    switch (table.CommandType.Trim().ToLower())
                    {
                        case "showui":
                            command = new ShowUICommand(table.CommandData);
                            break;
                    }
                }

                var branch = new DialogueBranch()
                {
                    DefaultNextID = table.DefaultNextID,
                    ConditionalNextIDs = new Dictionary<IConditionEvaluator, int>()
                };
                if (!string.IsNullOrEmpty(table.Condition1))
                {
                    var cond1 = ConditionFactory.CreateCondition(table.Condition1);
                    if (cond1 != null)
                        branch.ConditionalNextIDs.Add(cond1, table.NextID1);
                }

                if (!string.IsNullOrEmpty(table.Condition2))
                {
                    var cond2 = ConditionFactory.CreateCondition(table.Condition2);
                    if (cond2 != null)
                        branch.ConditionalNextIDs.Add(cond2, table.NextID2);
                }

                var line = new DialogueLine()
                {
                    ID = table.ID,
                    Speaker = table.Speaker,
                    Text = table.Text,
                    Portrait = table.Portrait,
                    Commands = command,
                    Branch = branch,
                };
                list.Add(line);
            }



            return list;
        }
    }
}
