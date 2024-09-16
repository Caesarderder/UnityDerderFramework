using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FSM
{
    public class EnemyAttackState:AbilityState 
    {

        //State
        private string _animationName;
        private Boss1EntityData _data;
        private Boss1Entity boss;
        private Combat combat;
        private Movement movement;

        private string combatName;
        private float lastMeleeAttackTime;
        private float lastRangedAttackTime;


        public EnemyAttackState(Entity entity, State NormalState,Boss1EntityData data,string animationName) : base(entity, NormalState)
        {
            _animationName = animationName;
            _data=data;
            this.boss = entity as Boss1Entity;
            combat = boss.Combat;
            movement=boss.Movement;
        }
        public void Enter(string combatName)
        {
            this.combatName = combatName;
            boss.StateMachine.ChangeState(this);
        }


        public override void Enter()
        {
            base.Enter();
            

            boss.Animator.SetBool(_animationName, true);
            if (combatName == typeof(Boss1MeleeAttack).ToString())
            {
                Debug.Log("EnterAttack");
                boss.Animator.SetInteger("Judge", 0);
                movement.JumpByForce(_data.MeleeAttackJumpForce);
                lastMeleeAttackTime=Time.time;
            }
            if (combatName == typeof(Boss1MeleeAttack).ToString())
            {
                Debug.Log("EnterarangedAttack");
                boss.Animator.SetInteger("Judge", 1);
                lastRangedAttackTime= Time.time;
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }


        public override void Exit()
        {
            base.Exit();
            boss.Animator.SetBool(_animationName, false);

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CheckIfTransition();
        }

        public void CheckIfTransition()
        {

        }

        public bool CanMeleeAttack()
        {
            if(Time.time>lastMeleeAttackTime+_data.MeleeAttackCD)
            {
                return true;
            }
            return false;
        }

        public bool CanRangedAttack()
        {
            if (Time.time > lastRangedAttackTime+ _data.RangedAttackCD)
            {
                return true;
            }
            return false;

        }

        public override void AnimationFinishedTrigger()
        {
            base.AnimationFinishedTrigger();
            if(combatName==typeof(Boss1MeleeAttack).ToString())
            {
                Debug.Log("Attack");
                isAbilityDone = true;

                combat.ActivateCombatStrategy(combatName);
            }
            if (combatName == typeof(Boss1RangedAttack).ToString())
            {
                Debug.Log("Attack");
                isAbilityDone = true;

                combat.ActivateCombatStrategy(combatName);
            }

        }
    }
}
