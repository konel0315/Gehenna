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
        [Space(10)]
        
        [LabelText("Use Grouping (Key-Value)")]
        [Tooltip("Row 데이터를 Key를 기준으로 그룹핑하여 Dictionary<string, List<T>> 형태로 저장할지 여부")]
        public bool useGrouping;
        
        [Space(10)]
        
        [TableList] public List<ExcelColumn> columns;
    }
}