using UnityEngine;

namespace FSM
{
    public class EnemySense:Sense
    {
        public float MinAggroDis;
        public float MaxAggroDis;
        private PlayerEntity _player;
        public PlayerEntity Player
        {
            get
            {
                if(_player == null)
                    _player = FindObjectOfType<PlayerEntity>();
                return _player;
            }
        }

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
        public override void GroundCheck()
        {
            base.GroundCheck();

        }
        public bool IsPlayerInMaxAggroRange()
        {
            if(Vector2.Distance( Player.transform.position,transform.position)<MaxAggroDis)
                return true;
            return false;
        }
        public bool IsPlayerInMinAggroRange()
        {
            if (Vector2.Distance(Player.transform.position, transform.position) < MinAggroDis)
                return true;
            return false;
        }
        public bool CanSeePlayer()
        {
            if(Vector2.Dot(transform.right , Player.transform.position - transform.position) >=0)
            {
                return true;
            }
            return false;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawLine(transform.position,transform.position+transform.right*MinAggroDis);
            Gizmos.color= Color.green;
            Gizmos.DrawLine(transform.position, transform.position+Vector3.up + transform.right* MaxAggroDis);

        }

    }
}