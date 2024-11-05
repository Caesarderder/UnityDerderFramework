using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine;
using Newtonsoft.Json;

public class TableManager 
{
    public TableManager()
    {
    }
    public GlobalConfig GlobalConfig { get; private set; }

    #region Sheet
    public CharacterConfig_SO CharacterConfig{ get; private set; }
    #endregion

    public async Task Init()
    {
        GlobalConfig=await Addressables.LoadAssetAsync<GlobalConfig>(typeof(GlobalConfig).ToString()).Task;


        CharacterConfig = await Addressables.LoadAssetAsync<CharacterConfig_SO>(typeof(CharacterConfig).ToString()).Task;
        CharacterConfig.Init();
    }

}
