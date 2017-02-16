using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    public class Entity : IDamagable, IDamager
    {
        public Entity()
        {
            controller = new FSM<EntityState>();
            controller.AddTransition(EntityState.INIT, EntityState.WAIT);
            controller.AddTransition(EntityState.WAIT, EntityState.ATTACK);
            controller.AddTransition(EntityState.WAIT, EntityState.DEFEND);
            controller.AddTransition(EntityState.ATTACK, EntityState.DEFEND);
            controller.AddTransition(EntityState.DEFEND, EntityState.ATTACK);
            controller.Start(EntityState.WAIT);
            m_Armor = 10f;
           
        }
        public void Start()
        {

        }
        public delegate void Handler();
        public Handler onDeath;
        public FSM<EntityState> controller;
        public enum EntityState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }
        public State CurrentState()
        {
            return controller.GetState();
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
        
        protected float m_Health;
        protected float m_MaxHealth;
        protected float m_Speed;
        protected float m_Damage;
        protected int m_NumberOfTurns;
        protected int m_TurnsTaken;
        protected string m_Name;
        protected bool m_Alive;
        protected float m_Armor;
        protected System.Windows.Forms.Control m_Button;
        protected System.Windows.Forms.ProgressBar m_HealthBar;
        protected System.Windows.Forms.RichTextBox m_Info;
        public float Damage { get { return m_Damage; } set { m_Damage = value; } }
        public float Health { get { return m_Health; } set { m_Health = value; } }

        public float Speed { get { return m_Speed; } set { m_Speed = value; } }
        public int NumberOfTurns { get { return m_NumberOfTurns; } set { m_NumberOfTurns = value; } }
        public int TurnsTaken { get { return m_TurnsTaken; } set { m_TurnsTaken = value; } }
        public string Name { get { return m_Name; } set { m_Name = value; } }
        public System.Windows.Forms.Control PlayerButton { get { return m_Button; } set { m_Button = value; } }
        public System.Windows.Forms.ProgressBar HealthBar { get { return m_HealthBar; } set { m_HealthBar = value; } }
        public bool Alive { get { return m_Alive; } set { m_Alive = value; } }
        public float MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }
        public float Armor { get { return m_Armor; }set { m_Armor = value; } }
        public System.Windows.Forms.RichTextBox Info { get { return m_Info; }set { m_Info = value; } }
    }
}
