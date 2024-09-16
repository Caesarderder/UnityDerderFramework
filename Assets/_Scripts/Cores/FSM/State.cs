using UnityEditor;

namespace FSM
{
    public class State
    {

        protected Entity Entity;
        public bool isExitingState;
        public State(Entity entity)
        {
            Entity = entity;
        }

        public virtual void Enter()
        {
            
        }
        
        public virtual void Exit()

        {
            
        }

        public virtual void LogicUpdate()
        {

        }
        public virtual void PhysicsUpdate()
        {

        }
        public virtual void AnimationFinishedTrigger()
        {

        }
    }
}