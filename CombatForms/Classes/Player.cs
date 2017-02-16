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
    public class Player : Entity
    {

        public Player()
        {

            m_ExpToNextLevel = 100f;
            m_Alive = true;
            m_Level = 1;

        }

        public Player(string name,float health, int level, float baseDamage, float speed)
        {

            m_ExpToNextLevel = 100f;
            m_Name = name;
            m_Alive = true;
            m_Level = level;
            m_Health = health;
            m_MaxHealth = health;
            m_Damage = baseDamage;
            m_Speed = speed;

        }
        public override void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);
            if (!(target as Entity).Alive)
                this.GainEXP(target);
        }

        public override void TakeDamage(float Amount)
        {
            if (m_Health - Amount < 0)
            {
                m_Health = 0;
                (HealthBar as ProgressBar).Value = (int)((m_Health / m_MaxHealth) * 100f);
                m_Info = null;
                this.onDeath.Invoke();
                return;
            }
            m_Health -= Amount;
            (HealthBar as ProgressBar).Value = (int)((m_Health/m_MaxHealth)*100f);
        }



        private void LevelUp()
        {
            m_Health = m_MaxHealth;
            m_Level++;
            m_ExpToNextLevel = (5f * (float)Math.Pow((double)m_Level, 2d)) + 95f;
            StatBuff t = new StatBuff();
            t.Visible = true;
            t.Activate();
            Combat.Instance.a.Visible = false;

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
     
      



        public float EXP { get { return m_Exp; } set { m_Exp = value; } }
        public int Level { get { return m_Level; } set { m_Level = value; } }
       
       


        #endregion
    }
}
