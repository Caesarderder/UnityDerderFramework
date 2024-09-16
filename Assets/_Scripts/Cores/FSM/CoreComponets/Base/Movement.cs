using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace FSM
{
    public class Movement:CoreComponent 
    {
        protected Rigidbody2D Rb;
       

        public Vector2 TargetPos;
        public Vector2 MoveSpeed;
        public bool CanMove;

        public bool IsMoving;
        public bool IsMovingToTargetPos;
        private int _isFacingRight;

        public UnityAction ChannelArrivedTarget;

        protected override void Awake()
        {
            base.Awake();
            Rb=GetComponentInParent<Rigidbody2D>();
            _isFacingRight = 1;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            MoveToTargetPos();

        }

        //��Ҫ��State���ã�ִ���ƶ�----�����ƶ������Զ�ת��
        public virtual void HorizontalMove()
        {
            if (!CanMove)
                return;
            if (MoveSpeed.sqrMagnitude > 0)
            {
                IsMoving = true;
                if(_isFacingRight*MoveSpeed.x<0)
                {
                    Flip();
                }
                Rb.linearVelocity = new Vector2(MoveSpeed.x,Rb.linearVelocity.y);
            }
            else
            {

                IsMoving = false;
            }

        }
        public void Flip()
        {
            _isFacingRight*=-1;
            Entity.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
        public virtual void SetMoveSpeed(float horitalSpeed)
        {
            MoveSpeed = new Vector2(horitalSpeed,Rb.linearVelocity.y);
        }
        public virtual void SetMoveSpeed(Vector2 velocity)
        {
            MoveSpeed = velocity;
        }
        public virtual void SetTargetPos(Vector2 targetPos)
        {
            IsMovingToTargetPos = true;
            TargetPos = targetPos;
        }

        //IsMovingToTargetPos==true&&����TargetPos
        protected virtual void MoveToTargetPos()
        {
            if(IsMovingToTargetPos&&CanMove)
            {
                if (IsArrivedTargetPos())
                {
                    ChannelArrivedTarget?.Invoke();
                    IsMovingToTargetPos =false;
                    return;
                }
                    
                IsMoving = true;
                Rb.linearVelocity = (TargetPos -(Vector2) Entity.transform.position).normalized*MoveSpeed.magnitude; 
            }
        }
        
        protected virtual bool IsArrivedTargetPos()
        {
            if((TargetPos - (Vector2)Entity.transform.position).magnitude<0.02f)
            {
                return true;
            }
            return false;
        }
        public void JumpByForce(float force)
        {
            Rb.AddForce(Vector2.up*force);
        }
    }
}