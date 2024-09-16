using UnityEngine;


[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Scriptable Objects/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    #region Stage
    [Header("=====Stage=====")]
    public int MaxStageSteps=3;
    public int StageMapSlelectCount=3;
    #endregion

}
