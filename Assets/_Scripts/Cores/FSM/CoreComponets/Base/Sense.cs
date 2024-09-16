using UnityEngine;

namespace FSM
{
    public class Sense:CoreComponent 
    {
        [SerializeField]
        protected LayerMask _wahtIsGround;
        [SerializeField]
        protected float _groundCheckDis=0.2f;
        public bool IsGrounded;

        protected override void Awake()
        {
            base.Awake();
        }
 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            GroundCheck();
        }
        public virtual void GroundCheck()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDis, _wahtIsGround);

            if (hit)
            {
                IsGrounded = true;
            }
            else
                IsGrounded = false;

        }
        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.down * _groundCheckDis);
        }

    }
}