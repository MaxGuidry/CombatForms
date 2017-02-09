using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    public class Enemy : Entity,IDamagable, IDamager
    {

        public Enemy()
        {
            controller = new FSM<PlayerState>();

            controller.AddTransition(PlayerState.INIT, PlayerState.WAIT);
            controller.AddTransition(PlayerState.WAIT, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.WAIT, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.DEFEND, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.ATTACK, PlayerState.WAIT);
            controller.AddTransition(PlayerState.DEFEND, PlayerState.WAIT);
            controller.Start(PlayerState.WAIT);
            m_Alive = true;
            m_Level = 1;
            m_Health = 50f;
            m_Damage = 25f;
            m_Speed = 5f;
        }

        public Enemy(float health, int level, float baseDamage, float speed)
        {
            controller = new FSM<PlayerState>();

            controller.AddTransition(PlayerState.INIT, PlayerState.WAIT);
            controller.AddTransition(PlayerState.WAIT, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.WAIT, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.DEFEND, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.ATTACK, PlayerState.WAIT);
            controller.AddTransition(PlayerState.DEFEND, PlayerState.WAIT);
            controller.Start(PlayerState.WAIT);
            m_Alive = true;
            m_Level = level;
            m_Health = health;
            m_Damage = baseDamage;
            m_Speed = speed;
        }
        public new void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);
            if (!(target as Player).Alive)
                (target as Player).GainEXP(target);
        }

        public new void TakeDamage(float Amount)
        {
            if (m_Health - Amount < 0)
            {
                m_Health = 0;
                this.onDeath.Invoke();
                return;
            }
            m_Health -= Amount;
        }
        public void ChangePlayerState(string state)
        {
            foreach (PlayerState ps in Enum.GetValues(typeof(PlayerState)))
            {
                if (state == ps.ToString())
                {
                    controller.ChangeState(ps);
                    break;
                }
            }
        }


        public void ChooseAction()
        {
            Random r = new Random();
            int chance = r.Next(1, 5);
            if (chance < 4)
                Combat.Instance.enemy.ChangePlayerState("ATTACK");
            else if (chance == 4)
                Combat.Instance.enemy.ChangePlayerState("DEFEND");
        }

       

        //FIELDS AND PROPERTIES
        #region FIELDS AND PROPERTIES
       
        private float m_Health;
        
        private float m_Speed;

        private int m_Level;
        private bool m_Alive;
        private float m_MaxHealth;
        
     
   
        public int Level { get { return m_Level; } set { m_Level = value; } }
        public bool Alive { get { return m_Alive; } set { m_Alive = value; } }
   
        public float MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }
        
        private enum PlayerState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }
        #endregion 
    }


}
