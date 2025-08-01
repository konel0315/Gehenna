using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Unity.VisualScripting;
using UnityEngine;

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

                if (!TryGetFirstValidRow(sheet, out (int headerIndex, IRow headerRow) headerTuple))
                    throw new Exception($"No valid header row found in the sheet '{sheet.SheetName}' of file '{excelFile}'.");
                
                List<string> headers = new();
                for (int i = 0; i < headerTuple.headerRow.LastCellNum; i++)
                    headers.Add(headerTuple.headerRow.GetCell(i)?.StringCellValue?.Trim() ?? "");

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

                for (int rowIndex = headerTuple.headerIndex + 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null) 
                        continue;
                    
                    ICell firstCell = row.GetCell(0);
                    if (firstCell != null && firstCell.CellType == CellType.String)
                    {
                        string firstCellValue = firstCell.StringCellValue?.Trim();
                        if (!string.IsNullOrEmpty(firstCellValue) && firstCellValue.StartsWith("#"))
                        {
                            Debug.Log($"Row {rowIndex + 1} is skipped because it starts with '#'.");
                            continue;
                        }
                    }

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
        
        public static Dictionary<string, IList> ImportGrouped(Type rowType, ExcelSchema schema)
        {
            var list = Import(rowType, schema);

            var firstColumnName = schema.columns.FirstOrDefault()?.columnName;
            if (string.IsNullOrEmpty(firstColumnName))
                throw new Exception("Schema에 정의된 컬럼이 없습니다.");

            var member = rowType
                .GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m =>
                    (m is FieldInfo or PropertyInfo) &&
                    string.Equals(m.Name, firstColumnName, StringComparison.OrdinalIgnoreCase));

            if (member == null)
                throw new Exception($"'{firstColumnName}' 필드를 {rowType.Name} 클래스에서 찾을 수 없습니다.");
            
            var dict = new Dictionary<string, IList>();
            foreach (var item in list)
            {
                var key = GetMemberValue(item, member)?.ToString();
                if (string.IsNullOrEmpty(key)) continue;

                if (!dict.TryGetValue(key, out var groupList))
                {
                    groupList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(rowType));
                    dict[key] = groupList;
                }

                groupList.Add(item);
            }

            return dict;
        }
        
        private static bool TryGetFirstValidRow(ISheet sheet, out (int index, IRow row) result)
        {
            result = default;
            
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                ICell firstCell = row.GetCell(0);
                if (firstCell == null) 
                    continue;

                if (firstCell.CellType == CellType.String)
                {
                    string value = firstCell.StringCellValue?.Trim();
                    if (!string.IsNullOrEmpty(value) && value.StartsWith("#"))
                        continue;
                }

                result = (i, row);
                return true;
            }

            return false;
        }
        
        private static Type GetMemberType(MemberInfo member) =>
            member switch
            {
                FieldInfo f => f.FieldType,
                PropertyInfo p => p.PropertyType,
                _ => typeof(object)
            };
        
        private static object GetMemberValue(object obj, MemberInfo member)
        {
            return member switch
            {
                FieldInfo f => f.GetValue(obj),
                PropertyInfo p => p.GetValue(obj),
                _ => null
            };
        }

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