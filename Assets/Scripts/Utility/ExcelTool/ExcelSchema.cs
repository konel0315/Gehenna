using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public class ExcelSchema : SerializedScriptableObject
    {
        public string schemaName;
        [TextArea(2, 4)] public string description;
        [FilePath(AbsolutePath = false)] public string excelFile;
        [TableList] public List<ExcelColumn> columns;
    }
}