using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
  public  class Entity :IDamagable,IDamager
    {
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

        public void TakeDamage(float Amount)
        {
            
        }

        public void DealDamage(IDamagable target, float Amount)
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
        private float m_Health;
        private float m_Speed;
        private float m_Damage;
        public float Damage { get { return m_Damage; } set { m_Damage = value; } }
        public float Health { get { return m_Health; } set { m_Health = value; } }

        public float Speed { get { return m_Speed; } set { m_Speed = value; } }
    }
}
