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
            Alive = true;
            Level = 1;
            Damage = 20f;
            Health = 50f;
            MaxHealth = Health;
            Speed = 7f;
            m_Type = EntityType.ENEMY;
        }

        public Enemy(string name, float health, int level, float baseDamage, float speed)
        {
            Damage = baseDamage;

            Alive = true;
            Level = level;
            Health = health;
            MaxHealth = health;
           Name = name;
            Speed = speed;
            m_Type = EntityType.ENEMY;
        }
        public override void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);

        }

        public override void TakeDamage(float Amount)
        {
            if (CurrentState.ToString() == "DEFEND")
                Amount -= Armor;
            if (Health - Amount <= 0)
            {
                Health = 0;
                Alive = false;


                this.onDeath.Invoke();
                return;
            }
            Health -= Amount;

        }


    }


}
