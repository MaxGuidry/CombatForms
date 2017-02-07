using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    public class Player : IDamagable, IDamager
    {


        public Player()
        {
            controller = new FSM<PlayerState>();
            m_Level = 1;
            m_ExpToNextLevel = 100f;
            controller.AddTransition(PlayerState.INIT, PlayerState.WAIT);
            controller.AddTransition(PlayerState.WAIT, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.WAIT, PlayerState.DEFEND);
        }
        public void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);

        }

        public void TakeDamage(float Amount)
        {
            if (m_Health - Amount < 0)
            {
                m_Health = 0;
                return;
            }
                m_Health -= Amount;
        }
        public void ChangePlayerState<T>(T state)
        {
            controller.ChangeState(state);
        }
        private float m_Health;
        private float m_Stamina;
        private float m_Speed;
        private float m_Exp;
        private float m_ExpToNextLevel;
        private int m_Level;
        private enum PlayerState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }
        private void LevelUp()
        {
            m_Level++;
            m_ExpToNextLevel = (5f * (float)Math.Pow(m_Level, 2f)) + 95f;
            
            //need to open a new window to select stat buffs here
        }
        private FSM<PlayerState> controller;
        public delegate void Handler();
        public Handler onDeath;
        public float Health { get { return m_Health; } set { m_Health = value; } }
        public float Stamina { get { return m_Stamina; } set { m_Stamina = value; } }
        public float Speed { get { return m_Stamina; } set { m_Stamina = value; } }
        public float EXP { get { return m_Exp; } set { m_Exp = value; } }
        public int Level { get { return m_Level; } set { m_Level = value; } }
    }
}
