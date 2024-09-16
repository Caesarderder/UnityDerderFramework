using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class TableManager 
{
    public GlobalConfig GlobalConfig { get; private set; }

    #region Sheet
    public Stage_SO Stage{ get; private set; }
    public Map_SO Map{ get; private set; }
    #endregion

    public async Task Init()
    {
        GlobalConfig=await Addressables.LoadAssetAsync<GlobalConfig>(typeof(GlobalConfig).ToString()).Task;

        Stage=await Addressables.LoadAssetAsync<Stage_SO>(typeof(Stage).ToString()).Task;
        Stage.Init();

        Map=await Addressables.LoadAssetAsync<Map_SO>(typeof(Map).ToString()).Task;
        Map.Init();
    }

}
