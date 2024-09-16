using System.Threading.Tasks;
using UnityEngine;

public class ActBase:MonoBehaviour
{
    protected string _name=> this.GetType().Name;
    // 在 Awake 中自动赋值 Name
    public virtual async Task OnLoad() { EventAggregator.Publish(new SActLoadEvent { ActName=_name});}
    public virtual void OnLoaded() 
    { 
        EventAggregator.Publish(new SActLoadedEvent { ActName = _name }); 
        Debug.Log($"<color=green>{_name.ToString()}   Loaded</color>");
    }

    public virtual void OnUnload() { EventAggregator.Publish(new SActUnloadEvent { ActName = _name }); }
    public virtual void OnUnloaded() 
    { 
        EventAggregator.Publish(new SActUnloadedEvent { ActName = _name }); 
        Debug.Log($"<color=yellow>{_name.ToString()}   UnLoaded</color>");
    }
}
