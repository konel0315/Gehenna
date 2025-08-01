using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Gehenna
{
    public partial class ResourceManager
    {
        public bool TryGetText(string tableKey, string textKey, out string result)
        {
            result = string.Empty;

            StringTable table = LocalizationSettings.StringDatabase.GetTable(tableKey);
            if (table == null)
            {
                GehennaLogger.Log(this, LogType.Warning, $"Table not found: {tableKey}");
                return false;
            }

            StringTableEntry entry = table.GetEntry(textKey);
            if (entry == null)
            {
                GehennaLogger.Log(this, LogType.Warning, $"Entry not found: {textKey} in table {tableKey}");
                return false;
            }

            result = entry.LocalizedValue;
            return true;
        }
    }
}