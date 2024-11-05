using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#region define

public enum EActLoadType
{
    Only,
    Additive,
}

public enum EAct
{
    None = -1,
    HomeAct,
    MonoBehaviorAct,
    BurstAndJobsAct,
    EcsAct
}

public struct SActLoadEvent
{
    public EAct ActName;
}

public struct SActLoadedEvent
{
    public EAct ActName;
}

public struct SActUnloadEvent
{
    public EAct ActName;
}

public struct SActUnloadedEvent
{
    public EAct ActName;
}

#endregion

public class ActManager
{
    #region Fields 亻尔 女子
    private Dictionary<string, ActBase> dic_acts = new Dictionary<string, ActBase>();
    private Dictionary<string, AsyncOperationHandle<GameObject>> dic_loadingOps = new Dictionary<string, AsyncOperationHandle<GameObject>>();
    #endregion

    #region Methods
    /// <summary>
    /// 使用Addressable加载Act（GameObject），并根据加载类型进行相应操作
    /// </summary>
    public async Task<ActBase> LoadAct(EAct actName, EActLoadType type=EActLoadType.Only)
    {
        // 如果加载类型为 Only，先卸载所有已加载的 Act
        if (type == EActLoadType.Only)
        {
            UnloadAllActs();
        }

        AsyncOperationHandle<GameObject> handle;
        var address = actName.ToString();
        // 防止重复加载
        if (!dic_loadingOps.ContainsKey(address))
        {
            // 使用Addressables异步加载GameObject
            handle = Addressables.LoadAssetAsync<GameObject>(address);
            dic_loadingOps.Add(address, handle);
        }
        else
            handle = dic_loadingOps[address];

        var go=await handle.Task;


        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // 实例化GameObject
            GameObject loadedGameObject = GameObject.Instantiate(go);
            ActBase actBase = loadedGameObject.GetComponent<ActBase>();

            if (actBase != null)
            {
                await actBase.OnLoad();  // 加载事件
                dic_acts.Add(address, actBase);
                actBase.OnLoaded();  // 加载完成事件
                return actBase;
            }
        }
        else
        {
            Debug.LogError($"Failed to load {address} via Addressables.");
            dic_loadingOps.Remove(address);
        }

        Debug.LogError($"none ActBase script adds to load {address}.");
        return null;
    }

    /// <summary>
    /// 卸载指定的Act（GameObject）;scope:0:发送卸载事件，1：不发送
    /// </summary>
    public void UnloadAct(string address)
    {
        if (dic_acts.ContainsKey(address))
        {
            ActBase actBase = dic_acts[address];

            if (actBase != null)
            {
                actBase.OnUnload();  
                actBase.OnUnloaded();  
                GameObject.Destroy(actBase.gameObject);
            }

            dic_acts.Remove(address);
        }

        //暂时不卸载资源加载的内存
//        if (dic_loadingOps.ContainsKey(address))
//        {
//            // 卸载Addressables资源
//            Addressables.Release(dic_loadingOps[address]);
//            dic_loadingOps.Remove(address);
//        }

    }

    /// <summary>
    /// 卸载当前加载的所有Act
    /// </summary>
    private void UnloadAllActs()
    {
        // 遍历所有已加载的 Act，并依次卸载
        var actList = dic_acts.Keys.ToList();
        foreach (var address in actList)
        {
            UnloadAct(address);
        }
    }
    #endregion
}

