using System;
using UniRx;
using UnityEngine;

public class ViewBase: MonoBehaviour
{
	#region Fileds 亻尔 女子
	protected CompositeDisposable _disposables=new();
	public string Name=>this.GetType().Name;
	protected Type _vmType => _vm.GetType();
	protected ViewModule _vm;
	#endregion

	#region Methods
	public virtual void Load() {
		EventAggregator.Publish(new SUILoad { Type = Name});
		//_vm= ViewModule.Resolve(_vmType) ;
	}
	public virtual void Show() 
	{
		EventAggregator.Publish(new SUIShow { Type = Name});
        Debug.Log($"<color=green>UI:   {Name.ToString()}   Show</color>");
    }
	public virtual void Hide() {_disposables.Dispose(); EventAggregator.Publish(new SUIHide { Type = Name}); }
		
	public virtual void Destroy() 
	{
		//ViewModule.Unregister(_vm.GetType());
		EventAggregator.Publish(new SUIDestory{ Type = Name});
        Debug.Log($"<color=yellow>UI:   {Name.ToString()}   Destoried</color>");
        Destroy(gameObject);

	}

	#endregion
}
