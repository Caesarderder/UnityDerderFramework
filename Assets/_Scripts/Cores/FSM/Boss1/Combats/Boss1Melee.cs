using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Melee : MonoBehaviour
{
    [SerializeField]
    private float _damage=10f;
    private List<Combat> combats=new();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Combat>(out var combat))
        {
            print("!!!!!!#!##@");
            if(!combats.Contains(combat))
            {
                combat.ReceiveDamage(-_damage);
                combats.Add(combat);
            }

        }
    }
    public void AnimationFinshTrigger()
    {

        Destroy(transform.parent.gameObject);
    }
}
