using System;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : ViewBase
{
    [SerializeField]
    TMP_InputField if_num;
    [SerializeField]
    Button 
        btn_monobehavior,
        btn_burstAndJobs,
        btn_Ecs
        ;

    protected Type _vmType=>typeof(HomePanelVM);
    HomePanelVM VM=>_vm as HomePanelVM;


    //UI打开时参数传递
    public void Init(int num)
    {
        if_num.text = num.ToString();
    }
    

#region Unity

    private void Start()
    {
    }

    private void Update()
    {
    }
    public override void Show()
    {
        base.Show();
        btn_monobehavior.OnClickAsObservable().Subscribe(_=>OnEnterMono()).AddTo(_disposables);
        btn_burstAndJobs.OnClickAsObservable().Subscribe(_=>OnEnterBurstAndJobs()).AddTo(_disposables);
        btn_Ecs.OnClickAsObservable().Subscribe(_=>OnEnterEcs()).AddTo(_disposables);

        if_num.onEndEdit.AddListener((x =>
        {
            DataModule.Resolve<TestDataModule>().OnTestDataChange(int.Parse(x));
        }));
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void Hide()
    {
        base.Hide();
        if_num.onEndEdit.RemoveAllListeners();
    }

    public override void Load()
    {
        base.Load();
    }

    #endregion

    
    
    private async void OnEnterMono()
    {
        await Manager<ActManager>.Inst.LoadAct(EAct.MonoBehaviorAct);
    }

    private async void OnEnterBurstAndJobs()
    {
        await Manager<ActManager>.Inst.LoadAct(EAct.BurstAndJobsAct);
    }
    private async void OnEnterEcs()
    {
        await Manager<ActManager>.Inst.LoadAct(EAct.EcsAct);
    }


}
