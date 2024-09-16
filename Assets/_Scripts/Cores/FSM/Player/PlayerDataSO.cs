using UnityEngine;

[CreateAssetMenu(menuName ="Data/Player/PlayerEntityData")]
public class PlayerDataSO: EntityDataSO
{
    [Header("NormalState")]
    public float MoveSpeed;
    public float JumpSpeed;
    public int JumpCount=2;

    [Header("CrouchState")]
    public float CrouchMoveSpeed = 5f;
}