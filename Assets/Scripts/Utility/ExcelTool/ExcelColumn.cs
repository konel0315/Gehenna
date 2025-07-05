using System;
using Sirenix.OdinInspector;

namespace Gehenna
{
    [Serializable]
    public class ExcelColumn
    {
        [ValueDropdown("@Gehenna.ExcelEditorConfig.Instance.AvailableTypes")]
        public string columnType;
        public string columnName;
    }
}