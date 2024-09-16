using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class Boss1Entity: Entity
    {
        //State
        public EnemyNormalState NormalState;
        public EnemyChaseState ChaseState;
        public EnemyAttackState AttackState;

        //Cores
        public Stats Stats=> stats? stats: Core.GetCoreComponent<Stats>(ref stats);
        private Stats stats;
        public Movement Movement => movement ? movement : Core.GetCoreComponent<Movement>(ref movement);
        private Movement movement;

        public Combat Combat => combat ? combat : Core.GetCoreComponent<Combat>(ref combat);
        private Combat combat;

        public EnemySense Sense => sense ? sense : Core.GetCoreComponent<EnemySense>(ref sense);
        private EnemySense sense;

        //Data
        public Boss1EntityData Data => EntityData as Boss1EntityData;


        //Player
        [HideInInspector]
        public Animator Animator;

        private void Start()
        {
            NormalState=new EnemyNormalState(this,this,Data,"Normal");
            ChaseState = new EnemyChaseState(this, this,Data, "Walk");
            AttackState = new EnemyAttackState(this, NormalState,Data,"Attack");

            Animator = GetComponent<Animator>();
            StateMachine.Init(NormalState);
        }

        public override void Die()
        {
            base.Die();
            Debug.Log(typeof(Boss1Entity).ToString() + "Die");
            Destroy(gameObject);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            print(StateMachine.CurrentState.ToString());
            StateMachine.CurrentState.PhysicsUpdate();
            base.Update();
            
        }
        protected override void OnAnimationFishedTrigger()
        {
            StateMachine.CurrentState.AnimationFinishedTrigger();
        }
         }

}
