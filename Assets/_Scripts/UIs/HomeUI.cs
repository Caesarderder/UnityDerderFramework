using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class HomeUI : UIBase
{
    [SerializeField]
    Button 
        btn_enterGame,
        btn_bag,
        btn_producer
        ;

#region Unity

    private void Start()
    {
        btn_enterGame.OnClickAsObservable().Subscribe(_=>OnEnterGame()).AddTo(_disposables);
        btn_bag.OnClickAsObservable().Subscribe(_=>OnOpenBag()).AddTo(_disposables);
        btn_producer.OnClickAsObservable().Subscribe(_=>OnOpenProducer()).AddTo(_disposables);
    }

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
    }

    public override void Show()
    {
        base.Show();
    }
    #endregion
    
    private async void OnEnterGame()
    {
        await GContext.Container.ResloveSingleton<ActManager>().LoadAct("StageAct");
//        GContext.Container.ResloveSingleton<UIManager>().DestroyUI<HomeUI>();
    }
    private void OnOpenBag()
    {

    }
    private void OnOpenProducer()
    {

    }
}
