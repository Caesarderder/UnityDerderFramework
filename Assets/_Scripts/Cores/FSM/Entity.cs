using System;
using System.ComponentModel;
using UnityEngine;

namespace FSM
{
    public class Entity:MonoBehaviour
    {
        public StateMachine StateMachine;

        public EntityDataSO EntityData;
        [HideInInspector]
        public Core Core;
        public bool isDie;

        protected void Awake()
        {

            Core = GetComponentInChildren<Core>();
            StateMachine = new StateMachine();
        }

        protected virtual void Update()
        {
            if (!StateMachine.CurrentState.isExitingState)
                StateMachine.CurrentState.LogicUpdate();
            if (isDie)
                return;
            Core.LogicUpdate();

        }
        public virtual void Die()
        {
            isDie = true;
        }    

        protected virtual void FixedUpdate()
        {
            Core.PhysicsUpdate();
            StateMachine.CurrentState.PhysicsUpdate();
        }
        protected virtual void OnAnimationFishedTrigger()
        {

        }
    }
}   