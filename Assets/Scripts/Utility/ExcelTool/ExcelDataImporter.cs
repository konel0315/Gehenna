using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Gehenna
{
    public static class ExcelDataImporter
    {
        public static IList Import(Type rowType, ExcelSchema schema)
        {
            if (!typeof(IGameDesignData).IsAssignableFrom(rowType))
                throw new ArgumentException($"Type '{rowType.Name}' must implement IGameDesignData.");

            string excelFile = schema.excelFile;
            if (string.IsNullOrEmpty(excelFile))
                throw new Exception($"Excel file path is not set for schema '{schema.schemaName}'.");

            IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(rowType));
            
            using FileStream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read);
            {
                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                IRow headerRow = sheet.GetRow(0);
                List<string> headers = new();
                for (int i = 0; i < headerRow.LastCellNum; i++)
                    headers.Add(headerRow.GetCell(i)?.StringCellValue?.Trim() ?? "");

                List<string> schemaColumns = schema.columns.Select(c => c.columnName.Trim()).ToList();
                if (schemaColumns.Count != headers.Count)
                    throw new Exception($"Column count mismatch: Schema({schemaColumns.Count}) vs Excel({headers.Count})");

                for (int i = 0; i < headers.Count; i++)
                {
                    if (!string.Equals(schemaColumns[i], headers[i], StringComparison.Ordinal))
                        throw new Exception($"Column mismatch at {i}: Schema = '{schemaColumns[i]}', Excel = '{headers[i]}'");
                }

                Dictionary<string, MemberInfo> members = rowType
                    .GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m is PropertyInfo or FieldInfo)
                    .ToDictionary(m => m.Name, m => m);

                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null) continue;

                    object instance = Activator.CreateInstance(rowType);
                    for (int col = 0; col < headers.Count; col++)
                    {
                        string header = headers[col];
                        if (!members.TryGetValue(header, out MemberInfo member)) continue;

                        ICell cell = row.GetCell(col);
                        if (cell == null) continue;

                        object value = GetCellValue(cell, GetMemberType(member));
                        SetMemberValue(instance, member, value);
                    }

                    list.Add(instance);
                }
            }

            return list;
        }

        private static Type GetMemberType(MemberInfo member) =>
            member switch
            {
                FieldInfo f => f.FieldType,
                PropertyInfo p => p.PropertyType,
                _ => typeof(object)
            };

        private static void SetMemberValue(object obj, MemberInfo member, object value)
        {
            if (member is FieldInfo f) f.SetValue(obj, value);
            else if (member is PropertyInfo p && p.CanWrite) p.SetValue(obj, value);
        }

        private static object GetCellValue(ICell cell, Type targetType)
        {
            return targetType switch
            {
                Type t when t == typeof(int)    => (int)cell.NumericCellValue,
                Type t when t == typeof(float)  => (float)cell.NumericCellValue,
                Type t when t == typeof(bool)   => cell.BooleanCellValue,
                Type t when t == typeof(string) => cell.ToString(),
                _ => null
            };
        }
    }
}