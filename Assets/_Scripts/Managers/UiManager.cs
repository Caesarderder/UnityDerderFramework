using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#region defines

public struct SUILoad
{
    public string Type;
}
public struct SUIShow
{
    public string Type;
}
public struct SUIHide
{
    public string Type;
}
public struct SUIDestory
{
    public string Type;
}
#endregion

public class UIManager
{
    #region Fields
    // 存储已加载的UI实例
    private Dictionary<Type, ViewBase> dic_Uis = new Dictionary<Type, ViewBase>();

    // 存储加载的异步操作，避免重复加载
    private Dictionary<Type, AsyncOperationHandle<GameObject>> dic_LoadingOps = new Dictionary<Type, AsyncOperationHandle<GameObject>>();

    private Transform _root;
    #endregion

    #region Methods
    public async Task Init()
    {
        if (_root != null)
            return;
        string uiRootAddress = "UIRoot"; // UIRoot的Addressable地址

        // 加载UIRoot
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(uiRootAddress);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // 实例化UIRoot并设置为场景中的一个对象
            _root=GameObject.Instantiate(handle.Result).transform ; 
            GameObject.DontDestroyOnLoad(_root);  // 设置为DontDestroyOnLoad，防止在场景切换时被销毁
            _root.gameObject.name = "UIRoot"; // 便于识别
        }
        else
        {
            Debug.LogError($"Failed to load UIRoot from address {uiRootAddress}");
        }
    }

    /// <summary>
    /// 获取UI实例，如果已经加载则直接返回，否则通过Addressables异步加载
    /// </summary>
    private async Task<T> LoadUI<T>() where T : ViewBase
    {
        var type = typeof(T);

        // 如果已经存在该UI，直接返回
        if (dic_Uis.ContainsKey(type))
        {
            return (T)dic_Uis[type];
        }
        else
        {
            // 如果没有，则加载该UI
            string address = type.ToString();

            // 检查是否有正在进行的加载操作
            if (dic_LoadingOps.ContainsKey(type))
            {
                // 如果正在加载，等待加载完成
                var loadingOp = dic_LoadingOps[type];
                await loadingOp.Task;
                if (loadingOp.Status == AsyncOperationStatus.Succeeded)
                {
                    var loadedUI = GameObject.Instantiate(loadingOp.Result,_root).GetComponent<T>();
                    dic_Uis[type] = loadedUI;
                    loadedUI.Load();
                    return loadedUI;
                }
            }
            else
            {
                // 启动新的异步加载操作
                var handle = Addressables.LoadAssetAsync<GameObject>(address);
                dic_LoadingOps[type] = handle;
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var loadedUI = GameObject.Instantiate(handle.Result,_root).GetComponent<T>();
                    dic_Uis[type] = loadedUI;
                    dic_LoadingOps.Remove(type); // 移除加载操作
                    loadedUI.Load();
                    return loadedUI;
                }
                else
                {
                    Debug.LogError($"Failed to load UI {type} from address {address}");
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 显示UI，如果没有加载则异步加载后显示
    /// </summary>
    public async Task<T> ShowUI<T>() where T : ViewBase
    {
        var ui = await LoadUI<T>();
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            ui.Show(); 
        }
        return ui;
    }

    /// <summary>
    /// 隐藏指定的UI
    /// </summary>
    public void HideUI<T>() where T : ViewBase
    {
        var type = typeof(T);
        if (dic_Uis.ContainsKey(type))
        {
            var ui = (T)dic_Uis[type];
            ui.gameObject.SetActive(false);
            ui.Hide(); 
        }
    }

    /// <summary>
    /// 销毁指定的UI并卸载资源
    /// </summary>
    public void DestroyUI<T>() where T : ViewBase
    {
        var type = typeof(T);
        if (dic_Uis.ContainsKey(type))
        {
            var ui = dic_Uis[type];
            ui.Destroy();
            dic_Uis.Remove(type);

            // 卸载Addressables加载的资源
            if (dic_LoadingOps.ContainsKey(type))
            {
                Addressables.Release(dic_LoadingOps[type]);
                dic_LoadingOps.Remove(type);
            }
        }
    }
}
#endregion