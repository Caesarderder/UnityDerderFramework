using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Init : MonoBehaviour
{
    #region Methods
    public async void Awake()
    {
        await ContainerInit();
        LoadMainAct();
        Destroy(gameObject);
    }

    private async Task ContainerInit()
    {
        #region Register      
        var tableManager = new TableManager();
        await tableManager.Init();
        Debug.Log("Table Init time:"+Time.time);

        var uiManager=new UIManager();
        await uiManager.Init();

        GContext.RegisterSingleton<TableManager>(tableManager);
        GContext.RegisterSingleton<UIManager>(uiManager);
        GContext.RegisterSingleton<ActManager>();

        Debug.Log("GContext register time:"+Time.time);

        #endregion
    }

    private void LoadMainAct()
    {
        _=Manager<ActManager>.Inst.LoadAct(EAct.HomeAct);
    }

    #endregion
}
