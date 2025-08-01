using System.Text;

namespace Gehenna
{
    public static class ExcelCodeGenerator
    {
        public static string GenerateClassCode(ExcelSchema schema)
        {
            if (schema == null || schema.columns == null)
                return string.Empty;

            var sb = new StringBuilder();

            sb.AppendLine("namespace Gehenna");
            sb.AppendLine("{");
            sb.AppendLine("    [System.Serializable]");
            sb.AppendLine($"    public class {schema.schemaName} : IGameDesignData");
            sb.AppendLine("    {");

            foreach (var col in schema.columns)
            {
                if (string.IsNullOrWhiteSpace(col.columnType) || string.IsNullOrWhiteSpace(col.columnName))
                    continue;

                sb.AppendLine($"        public {col.columnType} {col.columnName};");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string GenerateSOCode(ExcelSchema schema)
        {
            if (schema == null || string.IsNullOrWhiteSpace(schema.schemaName))
                return string.Empty;

            var className = schema.schemaName;
            var useGrouping = schema.useGrouping;
            var soClassName = className + ("SO");

            var sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Sirenix.OdinInspector;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine();

            sb.AppendLine("namespace Gehenna");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {soClassName} : BaseTableSO");
            sb.AppendLine("    {");

            if (useGrouping)
            {
                sb.AppendLine($"        public Dictionary<string, List<{className}>> GroupedTables;");
                sb.AppendLine();
                sb.AppendLine($"        public Dictionary<string, List<{className}>> GetGroupedTables() => GroupedTables;");
            }
            else
            {
                sb.AppendLine($"        public List<{className}> Tables;");
                sb.AppendLine();
                sb.AppendLine($"        public IReadOnlyList<{className}> GetTables() => Tables;");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            return sb.ToString();
        }
    }
}