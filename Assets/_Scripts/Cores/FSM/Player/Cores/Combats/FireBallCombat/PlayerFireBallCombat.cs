using FSM;
using System;
using UnityEngine;

public class PlayerFireBallCombat:MonoBehaviour,ICombatStrategy 
{
    public Combat Combat { get; set; }
    public string CombatID { get=>typeof(PlayerFireBallCombat).ToString(); }
    public Action ChannelEndCombat { get; set; }

    private PlayerMovement movement;
    private Animator animator;

    [SerializeField]
    private GameObject _fireBallPrefab;
    private PlayerFireBall _fireBall;
    
    public void Init()
    {
        movement = Combat.core.GetCoreComponent<PlayerMovement>();
        animator=GetComponentInParent<Animator>();  
    }

    public void StartCombat()
    {
        Init();
        movement.CanMove = false;
        movement.SetHorizontalSpeedZero();
        _fireBall= Instantiate(_fireBallPrefab,transform).GetComponent<PlayerFireBall>();
        animator.SetInteger("Judge1", 0);
        animator.SetInteger("Judge2", 0);

        Charging();
        Debug.Log("Start");
    }
    public void Charging()
    {
        _fireBall.OnCharge(transform);
        Debug.Log("Charging");
    }
    public void ApplyCombat()
    {
        _fireBall.OnShoot();
        animator.SetInteger("Judge2", 1);

        _fireBall = null;
        Debug.Log("Apply");
        EndCombat();
    }
    public void BreakCombat()
    {
        Debug.Log("Break");
    }
    public void EndCombat()
    {
        movement.CanMove = true;

        ChannelEndCombat?.Invoke();
        Combat.CombatStrategies.Remove(this);
        Destroy(gameObject);
    }

}

