using System.Threading.Tasks;
public class HomeAct : ActBase
{

    public override async Task OnLoad()
    {
        _=base.OnLoad();
        var uiManager=GContext.Container.ResloveSingleton<UIManager>();
        await uiManager.ShowUI<HomeUI>();
    }

    public override void OnLoaded()
    {
        base.OnLoaded();
    }

    public override void OnUnload()
    {
        base.OnUnload();
        GContext.Container.ResloveSingleton<UIManager>().DestroyUI<HomeUI>();
    }

    public override void OnUnloaded()
    {
        base.OnUnloaded();
    }
}
