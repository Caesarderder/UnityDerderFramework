using Unity.VisualScripting;
using UnityEngine;

namespace FSM
{
    public class CoreComponent : MonoBehaviour
    {
        [HideInInspector]
        
        public Core core;
        protected Entity Entity;

        protected virtual void Awake()
        {
            Entity=GetComponentInParent<Entity>();
            core = transform.parent.GetComponent<Core>();

            if (core == null) { Debug.LogError("There is no Core on the parent"); }
            core.AddComponent(this);
        }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }

    }
}