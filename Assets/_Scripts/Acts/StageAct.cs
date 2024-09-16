using UnityEngine;
using System.Threading.Tasks;
public class StageAct : ActBase
{
    UIManager _uiManager;
    StageUI _stageUI;

    public override async Task OnLoad()
    {
        _=base.OnLoad();
        _uiManager=GContext.Container.ResloveSingleton<UIManager>();
        _stageUI=await _uiManager.ShowUI<StageUI>();        
    }

    public override void OnLoaded()
    {
        base.OnLoaded();
    }

    public override void OnUnload()
    {
        base.OnUnload();
        _uiManager.DestroyUI<StageUI>();
    }

    public override void OnUnloaded()
    {
        base.OnUnloaded();
    }
}
