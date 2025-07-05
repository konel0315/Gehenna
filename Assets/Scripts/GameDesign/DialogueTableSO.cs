using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Gehenna
{
    public class DialogueTableSO : SerializedScriptableObject
    {
        public List<DialogueTable> Tables;

        public List<DialogueTable> GetTables() => Tables;
    }
}
