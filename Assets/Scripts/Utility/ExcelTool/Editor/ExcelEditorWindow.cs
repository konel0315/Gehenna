using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Demos.RPGEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Gehenna
{
    public class ExcelEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Excel/Editor")]
        private static void OpenWindow()
        {
            GetWindow<ExcelEditorWindow>("Excel Editor");
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            
            tree.Add("Config", ExcelEditorConfig.Instance);
            
            ExcelSchema[] allSchemas = AssetDatabase.FindAssets("t:ExcelSchema")
                .Select(guid => AssetDatabase.LoadAssetAtPath<ExcelSchema>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
            tree.Add("Schema", new ExcelSchemaTable(allSchemas));
            tree.AddAllAssetsAtPath("Schema", "Assets/Data/GameDesign/Schema", typeof(ExcelSchema), true, true);
            
            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();

            if (string.IsNullOrEmpty(ExcelEditorConfig.Instance.SchemaFolder))
            {
                this.ShowNotification(new GUIContent("Schema folder path is not set.\nCheck the config."));
            }

            if (string.IsNullOrEmpty(ExcelEditorConfig.Instance.GeneratedCodeFolder))
            {
                this.ShowNotification(new GUIContent("GeneratedCodeFolder folder path is not set.\nCheck the config."));
            }
            
            if (string.IsNullOrEmpty(ExcelEditorConfig.Instance.SoFolder))
            {
                this.ShowNotification(new GUIContent("SoFolder folder path is not set.\nCheck the config."));
            }

            float toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Schema")))
                {
                    CreateSchema();
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Generate Code")))
                {
                    GenerateCode();
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Import Data")))
                {
                    ImportData();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private void CreateSchema()
        {
            ScriptableObjectCreator.ShowDialog<ExcelSchema>(ExcelEditorConfig.Instance.SchemaFolder, obj =>
            {
                obj.schemaName = obj.name;
                base.TrySelectMenuItemWithObject(obj);
            });
        }

        private void GenerateCode()
        {
            if (this.MenuTree.Selection.FirstOrDefault()?.Value is ExcelSchema schema)
            {
                string folder = ExcelEditorConfig.Instance.GeneratedCodeFolder;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string classCode = ExcelCodeGenerator.GenerateClassCode(schema);
                if (!string.IsNullOrEmpty(classCode))
                {
                    string classFilePath = Path.Combine(folder, schema.schemaName + ".cs");
                    File.WriteAllText(classFilePath, classCode);
                }

                string soCode = ExcelCodeGenerator.GenerateSOCode(schema);
                if (!string.IsNullOrEmpty(soCode))
                {
                    string soFilePath = Path.Combine(folder, schema.schemaName + "SO.cs");
                    File.WriteAllText(soFilePath, soCode);
                }
                
                AssetDatabase.Refresh();
                
                GameConfig config = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Data/Config/GameConfig.asset");
                if (config == null)
                {
                    Debug.LogError("GameConfig.asset not found.");
                    return;
                }
                
                string assetSearchFilter = $"t:BaseTableSO {schema.schemaName}*SO";
                string[] guids = AssetDatabase.FindAssets(assetSearchFilter, new[] { "Assets/Data/GameDesign/SO" });
                if (guids.Length == 0)
                {
                    Debug.LogError($"TableSO asset for {schema.schemaName} not found.");
                    return;
                }
                
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                BaseTableSO tableSO = AssetDatabase.LoadAssetAtPath<BaseTableSO>(path);
                config.AddTableSO(tableSO);

                if (!string.IsNullOrEmpty(classCode) && !string.IsNullOrEmpty(soCode))
                {
                    EditorUtility.DisplayDialog("Notice", "Class and SO code generated successfully.", "OK");
                }
                else if (!string.IsNullOrEmpty(classCode))
                {
                    EditorUtility.DisplayDialog("Notice", "Class code generated, but SO code generation failed.", "OK");
                }
                else if (!string.IsNullOrEmpty(soCode))
                {
                    EditorUtility.DisplayDialog("Notice", "SO code generated, but class code generation failed.", "OK");
                }
                else
                {
                    EditorUtility.DisplayDialog("Notice", "Code generation failed.", "OK");
                }
            }
        }

        private void ImportData()
        {
            if (this.MenuTree.Selection.FirstOrDefault()?.Value is ExcelSchema schema)
            {
                Type rowType = Type.GetType($"Gehenna.{schema.schemaName}, Assembly-CSharp");
                if (rowType == null)
                {
                    EditorUtility.DisplayDialog("Error", $"Row type '{schema.schemaName}' not found.", "OK");
                }

                Type soType = Type.GetType($"Gehenna.{schema.schemaName}SO, Assembly-CSharp");
                if (soType == null)
                {
                    EditorUtility.DisplayDialog("Error", $"SO type '{schema.schemaName}SO' not found.", "OK");
                }

                string soFolder = ExcelEditorConfig.Instance.SoFolder;
                
                if (schema.useGrouping)
                {
                    var groupedDataRaw = ExcelDataImporter.ImportGrouped(rowType, schema);
                    
                    string groupSoPath = Path.Combine(soFolder, $"{schema.schemaName}GroupedSO.asset").Replace("\\", "/");
                    ScriptableObject groupSO = AssetDatabase.LoadAssetAtPath<ScriptableObject>(groupSoPath);
                    if (groupSO == null)
                    {
                        if (!Directory.Exists(soFolder))
                            Directory.CreateDirectory(soFolder);

                        groupSO = ScriptableObject.CreateInstance(soType);
                        AssetDatabase.CreateAsset(groupSO, groupSoPath);
                    }

                    FieldInfo listField = soType.GetField("GroupedTables");
                    if (listField == null)
                    {
                        EditorUtility.DisplayDialog("Error", $"SO class '{soType.Name}' does not have a 'GroupedTables' field.", "OK");
                        return;
                    }

                    var groupedData = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(
                        typeof(string),
                        typeof(List<>).MakeGenericType(rowType)
                    ));
                    
                    foreach (var kvp in groupedDataRaw)
                    {
                        string key = kvp.Key;
                        IList list = kvp.Value;

                        var typedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(rowType));
                        foreach (var item in list)
                        {
                            typedList.Add(item);
                        }

                        groupedData.GetType().GetMethod("Add").Invoke(groupedData, new object[] { key, typedList });
                    }

                    listField.SetValue(groupSO, groupedData);

                    EditorUtility.SetDirty(groupSO);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    EditorUtility.DisplayDialog("Notice", "Grouped data imported and saved successfully.", "OK");
                }



                else
                {
                    string soPath = Path.Combine(soFolder, schema.schemaName + "SO.asset").Replace("\\", "/");
                    ScriptableObject soInstance = AssetDatabase.LoadAssetAtPath<ScriptableObject>(soPath);
                    if (soInstance == null)
                    {
                        soInstance = ScriptableObject.CreateInstance(soType);
                        if (!Directory.Exists(soFolder))
                            Directory.CreateDirectory(soFolder);
                        AssetDatabase.CreateAsset(soInstance, soPath);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }

                    MethodInfo importMethod = typeof(ExcelDataImporter).GetMethod("Import");
                    IList importedData = importMethod.Invoke(null, new object[] { rowType, schema }) as IList;
                    if (importedData == null)
                    {
                        EditorUtility.DisplayDialog("Error", "Failed to import Excel data.", "OK");
                    }

                    FieldInfo listField = soType.GetField("Tables");
                    if (listField == null)
                    {
                        EditorUtility.DisplayDialog("Error",
                            $"SO class '{soType.Name}' does not have a 'Tables' field.", "OK");
                    }

                    listField.SetValue(soInstance, importedData);

                    EditorUtility.SetDirty(soInstance);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    EditorUtility.DisplayDialog("Notice",
                        $"Data imported and injected into {schema.schemaName}SO successfully.", "OK");
                }
            }
        }
    }
}