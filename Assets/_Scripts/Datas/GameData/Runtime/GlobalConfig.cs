using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Scriptable Objects/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    public int 
        StarMinEnergy,
        StarMaxEnergy,
        InitCount,
        FieldRowCount,
        RepulsionApplyCellCount,
        GravitationApplyCount,
        FieldColCount;

    public float
        GridSize, 
        GravitationalConstant
        ;

    public Vector3
        Center;


}

