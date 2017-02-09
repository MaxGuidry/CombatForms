﻿using System;
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
    public class Player : Entity,IDamagable, IDamager
    {

        public Player()
        {
            controller = new FSM<PlayerState>();
            m_ExpToNextLevel = 100f;
            controller.AddTransition(PlayerState.INIT, PlayerState.WAIT);
            controller.AddTransition(PlayerState.WAIT, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.WAIT, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.ATTACK, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.DEFEND, PlayerState.ATTACK);
            controller.Start(PlayerState.WAIT);
            m_Alive = true;
            m_Level = 1;
            m_Health = 50f;
            m_Damage = 25f;
            m_Speed = 5f;
        }

        public Player(float health, int level, float baseDamage, float speed)
        {
            controller = new FSM<PlayerState>();
            m_ExpToNextLevel = 100f;
            controller.AddTransition(PlayerState.INIT, PlayerState.WAIT);
            controller.AddTransition(PlayerState.WAIT, PlayerState.ATTACK);
            controller.AddTransition(PlayerState.WAIT, PlayerState.DEFEND);
            controller.AddTransition(PlayerState.ATTACK, PlayerState.DEFEND);
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
      


        private void LevelUp()
        {
            m_Level++;
            m_ExpToNextLevel = (5f * (float)Math.Pow((double)m_Level, 2d)) + 95f;
            StatBuff t = new StatBuff();
            t.Visible = true;
            t.Activate();

        }
        public void GainEXP(IDamagable target)
        {
            m_Exp += (float)(8f * (float)(Math.Pow((double)(target as Enemy).Level, 1.5d)) + 50f);
            if (m_Exp > m_ExpToNextLevel)
            {
                m_Exp -= m_ExpToNextLevel;
                LevelUp();

            }
        }
        


        //FIELDS AND PROPERTIES
        #region FIELDS AND PROPERTIES
        
      
        private float m_Exp;
        private float m_ExpToNextLevel;
        private int m_Level;
        private bool m_Alive;
        private float m_MaxHealth;
       

      
        public float EXP { get { return m_Exp; } set { m_Exp = value; } }
        public int Level { get { return m_Level; } set { m_Level = value; } }
        public bool Alive { get { return m_Alive; } set { m_Alive = value; } }
      
        public float MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }
      
       
        #endregion 
    }
}
