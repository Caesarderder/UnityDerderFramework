using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class Combat:CoreComponent 
    {
        private Stats Stats;
        [Header("¡ý¡ý¡ýCombat¡ý¡ý¡ý"),SerializeField]
        private List<GameObject> combatPrefab;
        public List<ICombatStrategy> CombatStrategies =new();

        protected  void Start()
        {
            Stats=core.GetCoreComponent<Stats>();
        }
        public virtual void Init()
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public ICombatStrategy GetCurCombatStrategy(string name)
        {
            foreach (var prefab in combatPrefab)
            {
                if (prefab.GetComponent<ICombatStrategy>().CombatID == name)
                {
                    Instantiate(prefab, transform);
                    break;
                }
            }
            return null;
        } 
        public ICombatStrategy ActivateCombatStrategy(string name)
        {
            foreach (var prefab in combatPrefab)
            {
                print(prefab.GetComponent<ICombatStrategy>().CombatID);
                if (prefab.GetComponent<ICombatStrategy>().CombatID==name)
                {
                    var go=Instantiate(prefab,transform);
                    var combat=go.GetComponent<ICombatStrategy>();
                    CombatStrategies.Add(combat);
                    combat.Combat = this;
                    return combat;
                }
            }
            Debug.LogError("Can not Find Combat:" + name);
            return null;
        }
        public void ReceiveDamage(float value)
        {
            print("Health:" + value);

            Stats.HealthChange(value);
        }
            
    }
}