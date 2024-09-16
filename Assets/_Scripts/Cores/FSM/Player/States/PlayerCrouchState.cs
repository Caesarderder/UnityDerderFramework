using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    public class PlayerCrouchState : State
    {
        private PlayerEntity player;
        private PlayerMovement movement;

        private PlayerInputHandler inputHandler;
        private PlayerDataSO data;


        //State
        private CapsuleCollider2D capsuleCrouch;
        private string _animationName;

        public PlayerCrouchState(Entity Entity, PlayerEntity player, string anmationName):base(Entity)
        {
            this.player = player;
            movement = player.Movement;
            inputHandler = player.InputHandler;
            data = player.Data;
            _animationName = anmationName;
            capsuleCrouch=player.GetComponents<CapsuleCollider2D>()[1];
        }

        public override void Enter()
        {
            base.Enter();
            movement.CanMove = true;
            player.Animator.SetBool(_animationName,true);
            Debug.Log("Enter");
            HorizontalMove();
            capsuleCrouch.enabled = false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            HorizontalMove();
        }

        private void CheckIfTransition()
        {
            if (inputHandler.Crouch == false||inputHandler.Jump)
            {
                player.StateMachine.ChangeState(player.NormalState);
            }

        }

        private void HorizontalMove()
        {
            var move = inputHandler.MoveNormalized;
            movement.SetMoveSpeed(move * data.CrouchMoveSpeed);
            movement.HorizontalMove();
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit");
            player.Animator.SetBool(_animationName,false);

            capsuleCrouch.enabled = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CheckIfTransition();
        }

    }
}
