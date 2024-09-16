using NUnit.Framework;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AddressablesTool : EditorWindow
{
    private string targetFolder = "Assets/Addressables"; // 默认地址

    [MenuItem("Tools/Addressables/Auto Set Addressables")]
    public static void ShowWindow()
    {
        GetWindow<AddressablesTool>("Addressables Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("批量设置为 Addressable", EditorStyles.boldLabel);
        targetFolder = EditorGUILayout.TextField("Target Folder:", targetFolder);

        if (GUILayout.Button("Set Addressables"))
        {
            SetAllFilesAsAddressable(targetFolder);
        }
    }

    private void SetAllFilesAsAddressable(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError($"Folder path does not exist: {folderPath}");
            return;
        }

        // 获取所有文件（可以根据需求过滤特定类型的文件）
        string[] allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

        // 获取Addressable settings
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
        if (settings == null)
        {
            Debug.LogError("AddressableAssetSettings is missing.");
            return;
        }

        int count = 0;

        foreach (var file in allFiles)
        {
            // 过滤meta文件
            if (file.EndsWith(".meta")) continue;

            string assetPath = file.Replace('\\', '/'); // 修正Windows路径分隔符

            // 将文件名（不带扩展名）作为Addressable名称
            string fileName = Path.GetFileNameWithoutExtension(assetPath);

            // 确保路径是相对路径，符合Unity规范（"Assets/..."）
            if (!assetPath.StartsWith("Assets"))
            {
                Debug.LogWarning($"Skipping non-asset file: {assetPath}");
                continue;
            }

            // 查找该路径是否已经是Addressable
            AddressableAssetEntry entry = settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(assetPath));
            if (entry == null)
            {
                // 如果没有，创建一个新的 Addressable entry
                entry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(assetPath), settings.DefaultGroup);
                entry.address = fileName; // 设置文件名为Addressable名称
                count++;
            }
        }

        // 保存并刷新Addressable配置
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"{count} assets have been set as Addressables.");
    }

    public static void SetFileAsAddressable(string assetPath)
    {
        if (!File.Exists(assetPath))
        {
            Debug.LogError($"Folder path does not exist: {assetPath}");
            return;
        }

        // 获取Addressable settings
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
        if (settings == null)
        {
            Debug.LogError("AddressableAssetSettings is missing.");
            return;
        }

        int count = 0;

        // 过滤meta文件
        if (assetPath.EndsWith(".meta")) return;

        // 将文件名（不带扩展名）作为Addressable名称
        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        // 确保路径是相对路径，符合Unity规范（"Assets/..."）
        if (!assetPath.StartsWith("Assets"))
        {
            Debug.LogWarning($"Skipping non-asset file: {assetPath}");
            return;
        }

        // 查找该路径是否已经是Addressable
        AddressableAssetEntry entry = settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(assetPath));
        if (entry == null)
        {
            // 如果没有，创建一个新的 Addressable entry
            entry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(assetPath), settings.DefaultGroup);
            entry.address = fileName; // 设置文件名为Addressable名称
            count++;
        }

        // 保存并刷新Addressable配置
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"{count} assets have been set as Addressables.");
    }
}

