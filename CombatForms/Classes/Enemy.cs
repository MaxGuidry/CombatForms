using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    public class Enemy : Entity
    {

        public Enemy()
        {



            m_Alive = true;
            m_Level = 1;
            m_Health = 50f;

            m_Speed = 5f;
        }

        public Enemy(float health, int level, float baseDamage, float speed)
        {


            m_Alive = true;
            m_Level = level;
            m_Health = health;

            m_Speed = speed;
        }
        public override void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);
            if (!(target as Player).Alive)
                (target as Player).GainEXP(target);
        }

        public override void TakeDamage(float Amount)
        {
            if (m_Health - Amount < 0)
            {
                m_Health = 0;
                this.onDeath.Invoke();
                return;
            }
            m_Health -= Amount;
        }

        public void ChooseAction()
        {
            Random r = new Random();
            int chance = r.Next(1, 5);
            if (chance < 4)
                Combat.Instance.currentPlayer.ChangePlayerState("ATTACK");
            else if (chance == 4)
                Combat.Instance.currentPlayer.ChangePlayerState("DEFEND");
        }



        //FIELDS AND PROPERTIES
        #region FIELDS AND PROPERTIES
        private int m_Level;
        private bool m_Alive;
        private float m_MaxHealth;
        
        public int Level { get { return m_Level; } set { m_Level = value; } }
        public bool Alive { get { return m_Alive; } set { m_Alive = value; } }

        public float MaxHealth { get { return m_MaxHealth; } set { m_MaxHealth = value; } }


        #endregion
    }


}
