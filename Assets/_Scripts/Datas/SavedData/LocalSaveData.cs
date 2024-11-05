using UnityEditorInternal;
using UnityEngine;

public class LocalSaveData
{
    public void Save()
    {
        DataFabUtil.LocalSave(this.GetType().Name,this);
    }
}
