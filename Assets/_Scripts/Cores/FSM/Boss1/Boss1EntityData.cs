using UnityEngine;

[CreateAssetMenu(menuName ="Data/Player/Boss1EntityData")]
public class Boss1EntityData: EntityDataSO
{
    [Header("NormalState")]
    public float MoveSpeed;

    [Header("AttackState")]
    public float MeleeAttackCD=4f;
    public float MeleeAttackJumpForce = 10f;
    public float RangedAttackCD=5f;
}