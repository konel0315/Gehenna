using System.IO;
using System.Text;
using UnityEditor;

namespace Gehenna
{
    public static class EnumCodeGenerator
    {
        public static void GenerateEnum(string outputPath, string enumName, string[] enumValues)
        {
            var sb = new StringBuilder();

            sb.AppendLine("// 자동 생성 파일 - 수정 금지");
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine("namespace Gehenna");
            sb.AppendLine("{");
            sb.AppendLine($"    public enum {enumName}");
            sb.AppendLine("    {");

            for (int i = 0; i < enumValues.Length; i++)
            {
                string member = enumValues[i].Trim();
                if (string.IsNullOrEmpty(member))
                    continue;

                sb.AppendLine("        " + member + (i < enumValues.Length - 1 ? "," : ""));
            }
            
            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            var directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);

            AssetDatabase.Refresh();
        }
    }
}