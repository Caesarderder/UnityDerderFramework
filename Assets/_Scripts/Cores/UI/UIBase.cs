using System;
using UniRx;
using UnityEngine;

public class UIBase : MonoBehaviour
{
	#region Fileds 亻尔 女子
	protected CompositeDisposable _disposables=new();
	protected  string _name=>this.GetType().Name;
	#endregion

	#region Methods
	public virtual void Load() { EventAggregator.Publish(new SUILoad { Type = _name});  }
	public virtual void Show() 
	{
		EventAggregator.Publish(new SUILoaded { Type = _name});
        Debug.Log($"<color=green>{_name.ToString()}   Loaded</color>");
    }
	public virtual void Hide() { EventAggregator.Publish(new SUIUnload { Type = _name}); }
	public virtual void Destroy() 
	{
		_disposables.Dispose();
		EventAggregator.Publish(new SUIUnloaded{ Type = _name});
        Debug.Log($"<color=yellow>{_name.ToString()}   UnLoaded</color>");
        Destroy(gameObject);
	}

	#endregion
}
