using UnityEditor;

namespace FSM
{
    public class PlayerCombatState: AbilityState 
    {
        public ICombatStrategy combatStrategy;
        private PlayerInputHandler _input;
        private PlayerEntity _player;
        private string _animationName;
        public PlayerCombatState(Entity entity, State NormalState,string animationName) : base(entity,NormalState)
        {
            _player = entity as PlayerEntity;
            _input = _player.InputHandler;
            _animationName = animationName;
        }

        public override void Enter()
        {
            base.Enter();
            _player.Animator.SetBool(_animationName, true);
            combatStrategy.StartCombat();
            combatStrategy.ChannelEndCombat += OnComatEnd;
        }

        public override void Exit()
        {
            base.Exit();
            combatStrategy.ChannelEndCombat -= OnComatEnd;
            _player.Animator.SetBool(_animationName, false);

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            combatStrategy.Charging();
            if(!_input.LeftButtonDown)
            {
                combatStrategy.ApplyCombat();
            }
        }
        public override void PhysicsUpdate()
        {

        }
        public override void AnimationFinishedTrigger()
        {

        }
        private void OnComatEnd()
        {
            isAbilityDone = true;
            Entity.StateMachine.ChangeState(NormalState);
        }
    }
}