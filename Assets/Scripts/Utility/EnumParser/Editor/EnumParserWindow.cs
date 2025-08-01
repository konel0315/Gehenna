using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Gehenna
{
    public class EnumParserWindow : OdinEditorWindow
    {
        public string enumName;

        [ListDrawerSettings(ShowPaging = false, DraggableItems = true, Expanded = true)]
        public string[] enumMembers;
        
        [MenuItem("Tools/Enum Parser")]
        private static void OpenWindow()
        {
            GetWindow<EnumParserWindow>("Enum Parser");
        }
        
        [Button()]
        private void GenerateEnum()
        {
            if (string.IsNullOrWhiteSpace(enumName))
            {
                EditorUtility.DisplayDialog("안내", "enum 이름이 유효하지 않습니다.", "OK");
                return;
            }
            
            if (enumMembers == null || enumMembers.Length == 0)
            {
                EditorUtility.DisplayDialog("안내", "enum 멤버를 하나 이상 입력하세요.", "OK");
                return;
            }

            foreach (var member in enumMembers)
            {
                if (string.IsNullOrWhiteSpace(member))
                {
                    EditorUtility.DisplayDialog("안내", "유효하지 않은 멤버 이름이 있습니다.", "OK");
                    return;
                }
            }
            
            string outputPath = $"Assets/Scripts/GameDesign/GeneratedEnum/{enumName}.cs";
            EnumCodeGenerator.GenerateEnum(outputPath, enumName, enumMembers);
        }
    }
}