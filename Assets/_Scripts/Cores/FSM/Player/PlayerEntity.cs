using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class PlayerEntity : Entity
    {
        //State
        public PlayerNormalState NormalState;
        public PlayerCrouchState CrouchState;
        public PlayerCombatState CombatState;
        //Cores
        public PlayerMovement Movement =>movement?movement:Core.GetCoreComponent<PlayerMovement>(ref movement);
        private PlayerMovement movement;
        public PlayerSense Sense=> sense? sense: Core.GetCoreComponent<PlayerSense>(ref sense);
        private PlayerSense sense;
        
        public Combat Combat=> combat? combat : Core.GetCoreComponent<Combat>(ref combat);
        private Combat combat;
        
        //Data
        public PlayerDataSO Data=> EntityData as PlayerDataSO;

        //Player
        [HideInInspector]
        public Animator Animator;
        [HideInInspector]
        public PlayerInputHandler InputHandler;

        private void Start()
        {
            Animator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            NormalState=new PlayerNormalState(this,this,"s_Normal");
            CrouchState = new PlayerCrouchState(this,this, "s_Crouch");
            CombatState = new PlayerCombatState(this, NormalState,"s_Combat");
            StateMachine =new StateMachine();
            StateMachine.Init(NormalState);
        }

        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
        }
        protected override void OnAnimationFishedTrigger()
        {
            StateMachine.CurrentState.AnimationFinishedTrigger();
        }
    }

}
