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
            m_Health -= Amount;
        }
       public void ChangePlayerState<T>(T state)
        {
            controller.ChangeState(state);
        }
        private float m_Health;
        private float m_Stamina;
        private enum PlayerState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }
        private FSM<PlayerState> controller;

        public float Health { get { return m_Health; } set { m_Health = value; } }
        public float Stamina { get { return m_Stamina; } set { m_Stamina = value; } }
    }
}
