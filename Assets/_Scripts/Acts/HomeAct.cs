using System.Threading.Tasks;
public class HomeAct : ActBase
{
    TestDataModule _testDataModule;
    public override async Task OnLoad()
    {
        _=base.OnLoad();
        _testDataModule = DataModule.Resolve<TestDataModule>();

        var uiManager = Manager<UIManager>.Inst;
        var num = _testDataModule.GetTestSavedData();
        var panel = await uiManager.ShowUI<HomePanel>();
        panel.Init(num);
    }

    public override async void OnLoaded()
    {
        base.OnLoaded();
    }

    public override void OnUnload()
    {
        base.OnUnload();
        Manager<UIManager>.Inst.DestroyUI<HomePanel>();
        //Manager<UIManager>.Inst.ShowUI<ProfilerPanel>();
    }

    public override void OnUnloaded()
    {
        base.OnUnloaded();
    }
}
