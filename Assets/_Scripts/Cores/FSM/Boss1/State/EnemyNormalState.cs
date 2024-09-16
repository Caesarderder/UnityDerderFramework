using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FSM
{
    public class EnemyNormalState: State
    {
        //State
        
        private string _animationName;
        private Movement movement;
        private Boss1Entity boss;
        private EnemySense sense;
        private Boss1EntityData data;

        public EnemyNormalState(Entity Entity, Boss1Entity boss, Boss1EntityData data, string anmationName) : base(Entity)
        {
            this.data = data;
            _animationName = anmationName;
            movement = boss.Movement;
            this.boss = boss;
            sense = boss.Sense;
        }



        public override void Enter()
        {
            base.Enter();
            movement.CanMove = true;
            boss.Animator.SetTrigger(_animationName);
        }

        public override void PhysicsUpdate()
        {
            Debug.Log("AAA");
            base.PhysicsUpdate();
            Move();
        }


        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CheckIfTransition();
        }

        public void CheckIfTransition()
        {
            if(boss.AttackState.CanMeleeAttack())
            {
                boss.AttackState.Enter(typeof(Boss1MeleeAttack).ToString());
                
            }
            else if(boss.AttackState.CanRangedAttack())
            {
                boss.AttackState.Enter(typeof(Boss1RangedAttack).ToString());
            }
        }

        public void Move()
        {
            var speed = 0f;
            if (sense.IsPlayerInMaxAggroRange())
            {
                //MoveToPlayer
                var dir = sense.Player.transform.position - boss.transform.position;
                if (dir.x > 0.2f)
                {
                    speed = data.MoveSpeed;
                }
                if (dir.x < -0.2f)
                {
                    speed =- data.MoveSpeed;
                }

                movement.SetMoveSpeed(speed);
                movement.HorizontalMove();
            }
            else
            {
                //Ä¬ÈÏ×´Ì¬
            }
            boss.Animator.SetFloat("Speed", speed);
        }
    }
}
