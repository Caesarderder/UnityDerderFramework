using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public float Velcity = 8f;
    private Rigidbody2D _rb;
    private int state = 0;
    private Transform _parent;
    [SerializeField]
    private float Damage=10f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }
    public void OnCharge(Transform parent)
    {
        _parent = parent;
        state = 1;
        transform.position = _parent.position;
        Damage += Time.deltaTime * 10f;
        transform.localScale += Time.deltaTime * Vector3.one * 0.2f;
    }

    public void OnShoot()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        transform.parent = null;
        state = 2;
    }
    private void FixedUpdate()
    {
        if (state == 1)
        {
            transform.position = _parent.position;
        }
        else if (state == 2)
        {
            _rb.linearVelocity = transform.right * Velcity;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Combat>(out var combat))
        {
            print("!!!!!!!");
            combat.ReceiveDamage(-Damage);
        
        }
        Destroy(gameObject);
    }
}
