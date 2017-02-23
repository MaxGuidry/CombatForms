using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
using System.Xml.Serialization;
namespace CombatForms.Classes
{
    [Serializable]
    [XmlInclude(typeof(Player))]
    [XmlInclude(typeof(Enemy))]
    public class Entity : IDamagable, IDamager
    {
        public enum EntityType
        {
            PLAYER,
            ENEMY,
        }
        public Entity()
        {
            controller = new FSM<EntityState>();
            controller.AddTransition(EntityState.INIT, EntityState.WAIT);
            controller.AddTransition(EntityState.WAIT, EntityState.ATTACK);
            controller.AddTransition(EntityState.WAIT, EntityState.DEFEND);
            controller.AddTransition(EntityState.ATTACK, EntityState.DEFEND);
            controller.AddTransition(EntityState.DEFEND, EntityState.ATTACK);
            controller.AddTransition(EntityState.ATTACK, EntityState.WAIT);
            controller.AddTransition(EntityState.DEFEND, EntityState.WAIT);
            controller.Start(EntityState.WAIT);
            State s = controller.GetState();
            Armor = 10f;

        }

        protected EntityType m_Type;
        public EntityType Type
        {
            get { return m_Type; }
        }
        public delegate void Handler();
        [XmlIgnore]
        public Handler onDeath;
        [XmlIgnore]
        private FSM<EntityState> controller;
        public enum EntityState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }
        public State CurrentState
        {
            get { return controller.GetState(); }

        }

        public virtual void TakeDamage(float Amount)
        {

        }

        public virtual void DealDamage(IDamagable target, float Amount)
        {

        }
        public void ChangePlayerState(string state)
        {
            foreach (EntityState ps in Enum.GetValues(typeof(EntityState)))
            {
                if (state == ps.ToString())
                {
                    controller.ChangeState(ps);
                    break;
                }
            }
        }


    

        public int NumberOfTurns { get { return (int)(Speed / 5f); } set { } }


        public float Damage { get; set; }
        public float Health { get; set; }

        public float Speed { get; set; }

        public int TurnsTaken { get; set; }
        public string Name { get; set; }

        public bool Alive { get; set; }
        public float MaxHealth { get; set; }
        public int Level { get; set; }
        public float Armor { get; set; }

        public bool TurnTaken { get; set; }
    }
}
