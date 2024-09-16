using System.Threading.Tasks;
using UnityEngine;

public class Init : MonoBehaviour
{
    #region Methods
    public async void Awake()
    {
        await CaontainerInit();
        LoadMainAct();
        Destroy(gameObject);
    }

    private async Task CaontainerInit()
    {
        #region Register      
        var tableManager = new TableManager();
        await tableManager.Init();

        var uiManager=GContext.Container.ResloveSingleton<UIManager>();
        await uiManager.Init();

        GContext.Container.RegisterSingleton<TableManager>(tableManager);
        GContext.Container.RegisterSingleton<UIManager>(uiManager);
        GContext.Container.RegisterSingleton<ActManager>();

        GContext.Container.RegisterSingleton<StageDataProvider>();
        #endregion
    }

    private void LoadMainAct()
    {
        _=GContext.Container.ResloveSingleton<ActManager>().LoadAct("MainAct");
    }

    #endregion
}
