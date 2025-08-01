using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public class DialogueTableSO : BaseTableSO
    {
        public Dictionary<string, List<DialogueTable>> GroupedTables;

        public Dictionary<string, List<DialogueTable>> GetGroupedTables() => GroupedTables;
    }
}
