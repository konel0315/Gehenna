using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    [CreateAssetMenu(menuName = "Table/DialogueActionDataSO")]
    public class DialogueActionDataSO : ScriptableObject
    {

        [SerializeField, Required]
        private DialogueTableSO dialogueTableSO;
        
        [SerializeField]
        private List<DialogueActionData> DialogueActionData = new();

        private Dictionary<string, List<DialogueActionData>> grouped;

        public Dictionary<string, List<DialogueActionData>> GetGroupedTables()
        {
            if (grouped == null || grouped.Count == 0)
            {
                grouped = new Dictionary<string, List<DialogueActionData>>();

                foreach (var del in DialogueActionData)
                {
                    if (!grouped.TryGetValue(del.DialogueKey, out var list))
                        grouped[del.DialogueKey] = list = new List<DialogueActionData>();

                    list.Add(del);
                }
            }

            return grouped;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            grouped = null;
            UpdateDropdownLists();
        }

        private void UpdateDropdownLists()
        {
            if (dialogueTableSO == null)
                return;

            var keys = new List<string>(dialogueTableSO.GetGroupedTables().Keys);

            foreach (var data in DialogueActionData)
            {
                data.DialogueKeys = keys;

                if (!string.IsNullOrEmpty(data.DialogueKey) &&
                    dialogueTableSO.GetGroupedTables().TryGetValue(data.DialogueKey, out var list))
                {
                    data.IDs = list.ConvertAll(d => d.ID);
                }
                else
                {
                    data.IDs = new List<int>();
                }
            }
        }
        
#endif
    }
}