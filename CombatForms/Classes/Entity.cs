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
            m_Armor = 10f;

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

        //would just use the AutoProperties instead of making these backing fields for readability
        protected float m_Health;
        protected float m_MaxHealth;
        protected int m_Level;
        protected float m_Speed;
        protected float m_Damage;
        protected int m_NumberOfTurns;
        protected int m_TurnsTaken;
        protected string m_Name;
        protected bool m_Alive;
        protected float m_Armor;

        public float Damage { get { return m_Damage; } set { m_Damage = value; } }
        public float Health { get { return m_Health; } set { m_Health = value; } }

        public float Speed { get { return m_Speed; } set { m_Speed = value; } }
        public int NumberOfTurns { get { return (int)(m_Speed/5f); } set { m_NumberOfTurns = value; } }
        public int TurnsTaken { get { return m_TurnsTaken; } set { m_TurnsTaken = value; } }
        public string Name { get { return m_Name; } set { m_Name = value; } }

        public bool Alive { get { return m_Alive; } set { m_Alive = value; } }
        public float MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }
        public int Level { get { return m_Level; } set { m_Level = value; } }
        public float Armor { get { return m_Armor; } set { m_Armor = value; } }
        public bool TurnTaken { get; set; }
    }
}
