using FSM;
using System;
using UnityEngine;

public class Boss1MeleeAttack:MonoBehaviour,ICombatStrategy 
{
    public Combat Combat { get; set; }
    public string CombatID { get=>typeof(Boss1MeleeAttack).ToString(); }
    public Action ChannelEndCombat { get; set; }


    [SerializeField]
    private GameObject _attackPrefab;
    private PlayerFireBall _fireBall;
    
    public void Init()
    {
    }

    public void StartCombat()
    {
        Init();
        Combat=GetComponentInParent<Combat>();
        _fireBall = Instantiate(_attackPrefab,transform).GetComponent<PlayerFireBall>();
        Charging();
        GetComponentInParent<Animator>().SetInteger("Judge1", 0);
        GetComponentInParent<Animator>().SetInteger("Judge2", 0);

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
        GetComponentInParent<Animator>().SetInteger("Judge2", 1);
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

        ChannelEndCombat?.Invoke();
        Combat.CombatStrategies.Remove(this);
        Destroy(gameObject);
    }

}

