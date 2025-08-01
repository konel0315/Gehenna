using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gehenna
{
    public abstract class BaseCatalog : SerializedScriptableObject
    {
        [FolderPath(RequireExistingPath = true), SerializeField]
        protected string outputFolder;
        
        [FolderPath(RequireExistingPath = false), SerializeField]
        protected string sourceFolder;
        
        public abstract void Initialize();

#if UNITY_EDITOR
        protected void GenerateKeyInternal(string className, string outputFileName, string searchPattern)
        {
            string outputPath = Path.Combine(outputFolder, outputFileName);

            if (!Directory.Exists(sourceFolder))
            {
                Debug.LogError($"Source folder not found: {sourceFolder}");
                return;
            }

            var sourceNames = Directory.GetFiles(sourceFolder, searchPattern, SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileNameWithoutExtension)
                .OrderBy(name => name)
                .ToList();

            if (sourceNames.Count == 0)
            {
                Debug.LogWarning("No resources found in folder.");
                return;
            }

            var sanitizedNames = sourceNames.Select(SanitizeName).ToList();

            var duplicateGroups = sanitizedNames.GroupBy(n => n).Where(g => g.Count() > 1).ToList();
            if (duplicateGroups.Count > 0)
            {
                Debug.LogError("Duplicate enum names after sanitization: " +
                               string.Join(", ", duplicateGroups.Select(g => g.Key)));
                return;
            }

            string classContent = GenerateStringConstClass(className, sanitizedNames);
            string outputDir = Path.GetDirectoryName(outputPath);
            if (string.IsNullOrEmpty(outputDir))
            {
                Debug.LogError($"Invalid output path: {outputPath}");
                return;
            }

            Directory.CreateDirectory(outputDir);
            File.WriteAllText(outputPath, classContent);

            UnityEditor.EditorUtility.DisplayDialog(
                title: "키 생성 성공",
                message: $"{sanitizedNames.Count}개의 키가 다음 위치에 생성되었습니다:\n{outputPath}",
                ok: "확인"
            );

            UnityEditor.AssetDatabase.Refresh();
        }
        
        protected string GenerateStringConstClass(string className, IEnumerable<string> values)
        {
            const string indent = "        ";         // 8 spaces (1 level)
            const string doubleIndent = "            "; // 12 spaces (2 levels)

            var members = values
                .Select(v => $"{indent}public const string {v} = \"{v}\";");

            var arrayItems = values
                .Select(v => $"{doubleIndent}{v}");

            var array = $@"{indent}#if UNITY_EDITOR
{indent}public static readonly string[] AllKeys = new[]
{indent}{{
{string.Join(",\n", arrayItems)}
{indent}}};
{indent}#endif";

            return $@"// 자동 생성 파일 - 수정 금지
namespace Gehenna
{{
    public static class {className}
    {{
{string.Join("\n", members)}

{array}
    }}
}}";
        }

        
        protected string SanitizeName(string name)
        {
            char[] validChars = name.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray();

            string sanitized = new string(validChars);
            if (sanitized.Length > 0 && char.IsDigit(sanitized[0]))
                sanitized = "_" + sanitized;

            return sanitized;
        }
#endif
    }
}