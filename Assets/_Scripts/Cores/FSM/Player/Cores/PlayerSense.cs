using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    public class PlayerSense : Sense
    {
        //FSM
        
        private PlayerEntity Player;
        private PlayerInputHandler inputHandler;
        protected override void Awake()
        {
            base.Awake();
            Player=GetComponentInParent<PlayerEntity>();
            inputHandler=GetComponentInParent<PlayerInputHandler>();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
