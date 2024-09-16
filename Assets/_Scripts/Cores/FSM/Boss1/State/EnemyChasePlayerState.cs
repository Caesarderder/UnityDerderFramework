using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FSM
{
    public class EnemyChaseState: State
    {
        //State
        private string _animationName;
        private Movement movement;
        private Boss1Entity boss;
        private EnemySense sense;
        private Boss1EntityData data;

        public EnemyChaseState(Entity Entity, Boss1Entity boss,Boss1EntityData data,string anmationName) : base(Entity)
        {
            this.data = data;
            _animationName = anmationName;
            movement = boss.Movement;
            this.boss = boss;
            sense=boss.Sense;
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
            Debug.Log("AA");
        }
        public void Move()
        {
            Debug.Log("!");
            if(sense.IsPlayerInMaxAggroRange())
            {
                //MoveToPlayer
                Debug.Log("FindPlayer!");
                var dir = sense.Player.transform.position - boss.transform.position;
                if(dir.x>0f)
                {
                    movement.SetMoveSpeed(data.MoveSpeed);
                }
                if (dir.x < 0f)
                {
                    movement.SetMoveSpeed(-data.MoveSpeed);
                }
                movement.HorizontalMove();
            }
            else
            {
                //Ä¬ÈÏ×´Ì¬
            }
        }
    }
}
