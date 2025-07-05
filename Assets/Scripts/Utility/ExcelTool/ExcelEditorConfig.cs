using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.Serialization;

namespace Gehenna
{
    [GlobalConfig("Assets/Settings/ExcelTool")]
    public class ExcelEditorConfig : GlobalConfig<ExcelEditorConfig>
    {
        [FolderPath] public string SchemaFolder;
        [FolderPath] public string SoFolder;
        [FolderPath] public string GeneratedCodeFolder;
        
        public List<string> AvailableTypes = new()
        {
            "int",
            "float",
            "string",
            "bool"
        };
    }
}