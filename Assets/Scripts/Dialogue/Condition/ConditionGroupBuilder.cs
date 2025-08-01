using System.Collections.Generic;

namespace Gehenna
{
    public static class ConditionGroupBuilder
    {
        public static ConditionGroup Build(DialogueTable table)
        {
            var group = new ConditionGroup
            {
                ID = table.ID,
                Conditions = new List<ConditionBranch>()
            };

            if (!string.IsNullOrEmpty(table.Condition1))
                group.Conditions.Add(new ConditionBranch { Condition = table.Condition1, NextID = table.NextID1 });

            if (!string.IsNullOrEmpty(table.Condition2))
                group.Conditions.Add(new ConditionBranch { Condition = table.Condition2, NextID = table.NextID2 });

            if (!string.IsNullOrEmpty(table.Condition3))
                group.Conditions.Add(new ConditionBranch { Condition = table.Condition3, NextID = table.NextID3 });

            return group;
        }
    }
}