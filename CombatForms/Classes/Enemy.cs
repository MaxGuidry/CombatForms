using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
using System.Windows.Forms;
namespace CombatForms.Classes
{
    [Serializable]
    public class Enemy : Entity
    {

        public Enemy()
        {


            new Entity();
            m_Alive = true;
            m_Level = 1;
            m_Damage = 20f;
            m_Health = 50f;
            m_MaxHealth = m_Health;
            m_Speed = 5f;
            m_Type = EntityType.ENEMY;
        }

        public Enemy(string name,float health, int level, float baseDamage, float speed)
        {
            m_Damage = baseDamage;

            m_Alive = true;
            m_Level = level;
            m_Health = health;
            m_MaxHealth = health;
            m_Name = name;
            m_Speed = speed;
            m_Type = EntityType.ENEMY;
        }
        public override void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);
            
        }

        public override void TakeDamage(float Amount)
        {
            if (CurrentState.ToString() == "DEFEND")
                Amount -= m_Armor;
            if (m_Health - Amount <= 0)
            {
                m_Health = 0;
                m_Alive = false;
                
                
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
                Combat.Instance.CurrentPlayer.ChangePlayerState("ATTACK");
            else if (chance == 4)
                Combat.Instance.CurrentPlayer.ChangePlayerState("DEFEND");
        }
      
    }


}
