using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    class Player : IDamagable, IDamager
    {
      

       
        public void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);

        }

        public void TakeDamage(float Amount)
        {
            m_Health -= Amount;
        }

        private float m_Health;
        private float m_Stamina;
        //FSM<> controller-------not sure what T should be yet
        public float Health { get { return m_Health; }set { m_Health = value; } }
        public float Stamina { get { return m_Stamina; } set { m_Stamina = value; } }
    }
}
