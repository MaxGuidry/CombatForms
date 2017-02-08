using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatForms.Iterfaces;

namespace CombatForms.Classes
{
    public class Player : IDamagable, IDamager
    {


        public Player(float health,int level,float baseDamage)
        {
            controller = new FSM<PlayerState>();
            m_Level = 1;
            m_ExpToNextLevel = 100f;
            controller.AddTransition(PlayerState.INIT, PlayerState.WAIT);
            controller.AddTransition(PlayerState.WAIT, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.WAIT, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.ATTACK, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.DEFEND, PlayerState.ATTACK);
            controller.Start(PlayerState.WAIT);
            m_Alive = true;
            m_Level = level;
            m_Health = health;
            m_Damage = baseDamage;
        }
        public void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);
            if (!(target as Player).Alive)
                GainEXP(target as Player);
        }

        public void TakeDamage(float Amount)
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


        private void LevelUp()
        {
            m_Level++;
            m_ExpToNextLevel = (5f * (float)Math.Pow(m_Level, 2f)) + 95f;
            StatBuff t = new StatBuff();
            t.Visible = true;
            t.Activate();
           
        }
        private void GainEXP(Player target)
        {
            EXP += (float)(8f * (float)(Math.Pow((double)target.Level, 1.5d)));
        }
        public State CurrentState()
        {
            return controller.GetState();
        }

        private float m_Damage;
        private float m_Health;
        private float m_Stamina;
        private float m_Speed;
        private float m_Exp;
        private float m_ExpToNextLevel;
        private int m_Level;
        private bool m_Alive;
        private FSM<PlayerState> controller;
        public delegate void Handler();
        public Handler onDeath;
        
        public float Health { get { return m_Health; } set { m_Health = value; } }
        public float Stamina { get { return m_Stamina; } set { m_Stamina = value; } }
        public float Speed { get { return m_Stamina; } set { m_Stamina = value; } }
        public float EXP { get { return m_Exp; } set { m_Exp = value; } }
        public int Level { get { return m_Level; } set { m_Level = value; } }
        public bool Alive { get { return m_Alive; } set { m_Alive = value; } }
        public float AD { get { return m_Damage; } set { m_Damage = value; } }
        private enum PlayerState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }
    }
}
