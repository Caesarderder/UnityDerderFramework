using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityQuickSheet
{

    public class SheetTool : EditorWindow
    {
        private static string _sourceFolder = "Assets/SheetDatas"; // 表格地址
        private static string _targetFolder = "Assets/Addressables/GameData"; // SO数据地址

        [MenuItem("Tools/Sheet/Generate Sheet Class And Data")]
        public static void GenerateClass()
        {
            // 获取_sourceFolder下的所有文件GUID
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { _sourceFolder });

            foreach (string guid in guids)
            {
                // 根据GUID获取文件路径
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                // 加载资源为 ScriptableObject
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                // 检查是否为ExcelMachineEditor类型的ScriptableObject
                if (so is ExcelMachine excelMachineEditor)
                {
                    // 调用Generate方法
                    Debug.Log($"Generating class for: {so.name}");
                    excelMachineEditor.Generate();
                }
            }

            // 刷新AssetDatabase
            AssetDatabase.Refresh();
            GenerateData();
        }

        [MenuItem("Tools/Sheet/Generate Sheet Data")]
        public static void GenerateData()
        {
            ImportXlsFiles();

            GenertateScriptableObjects();

            // 刷新 AssetDatabase 以确保 Unity 资源库是最新状态
            AssetDatabase.Refresh();
        }

        private static void ImportXlsFiles()
        {
            // 查找所有的 .xls 和 .xlsx 文件
            string[] xlsxFiles = Directory.GetFiles(_sourceFolder, "*.xlsx", SearchOption.AllDirectories);

            foreach (string filePath in xlsxFiles)
            {
//                Debug.Log($"Found Excel file: {filePath}");

                AssetDatabase.ImportAsset(filePath);
            }
        }

        private static void GenertateScriptableObjects()
        {
            // 查找 _sourceFolder 目录下的所有 ScriptableObject
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { _sourceFolder });

            foreach (string guid in guids)
            {
                // 获取资源路径
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                // 加载资源对象
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                // 检查类名是否以 _SO 结尾
                if (obj != null && obj.GetType().Name.EndsWith("_SO"))
                {
                    AddressablesTool.SetFileAsAddressable(assetPath);
                }
            }
        }

    }
}
