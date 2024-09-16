using UnityEngine;
public class StageUI: UIBase
{
    #region Flieds
    StageDataProvider _stageDP;
    #endregion
    #region Unity

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Load()
    {
        base.Load();
        _stageDP=GContext.Container.ResloveSingleton<StageDataProvider>();
    }

    public override void Show()
    {
        base.Show();
    }
    #endregion

    public void Refresh()
    {
                
    }
}
