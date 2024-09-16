using UnityEngine;
namespace FSM
{
    public class PlayerMovement : Movement
    {
        //FSM
        private PlayerEntity Player;
        private PlayerInputHandler inputHandler;
        private PlayerSense sense;

        private bool _inAir;

        protected Animator Animator;
        protected override void Awake()
        {
            base.Awake();
            Animator=GetComponentInParent<Animator>();
            Player = GetComponentInParent<PlayerEntity>();
            inputHandler = GetComponentInParent<PlayerInputHandler>();
        }
        private void Start()
        {
            
            sense = Player.Sense;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void HorizontalMove()
        {
            var lastMoveState = IsMoving;
            base.HorizontalMove();
            Player.Animator.SetFloat("Move",Rb.linearVelocity.x);
        }
        public void Jump(float jumpSpeed)
        {
            Player.Animator.SetTrigger("Jump");
            
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, jumpSpeed);
            _inAir = true;
        }
        private void CheckAirState()
        {
            if(_inAir&&sense.IsGrounded&&Rb.linearVelocity.y<0f)
            {
                _inAir=false;
                Player.Animator.SetTrigger("Grounded");
            }
            if (!sense.IsGrounded&&!_inAir)
            {
                Player.Animator.SetTrigger("Fall");
                _inAir=true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            CheckAirState();
        }

        public override void SetMoveSpeed(Vector2 velocity)
        {
            base.SetMoveSpeed(velocity);
        }

        public override void SetTargetPos(Vector2 targetPos)
        {
            base.SetTargetPos(targetPos);
        }


        protected override bool IsArrivedTargetPos()
        {
            return base.IsArrivedTargetPos();
        }

        protected override void MoveToTargetPos()
        {
            base.MoveToTargetPos();
        }

        public void SetHorizontalSpeedZero()
        {
            Rb.linearVelocity=new Vector2(0f,Rb.linearVelocity.y);
        }
    }
}

