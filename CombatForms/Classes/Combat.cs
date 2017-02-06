using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Classes
{
    public class Combat
    {


        public Combat()
        {
            currentPlayer = CombatStates.PLAYER1;
            combatControl.AddTransition(CombatStates.PLAYER1, CombatStates.ENEMY);
            combatControl.AddTransition( CombatStates.ENEMY,CombatStates.PLAYER1);
        }

        public void ChangePlayers()
        {
            if (currentPlayer == CombatStates.PLAYER1)
                combatControl.ChangeState(CombatStates.ENEMY);
            else if (currentPlayer == CombatStates.ENEMY)
                combatControl.ChangeState(CombatStates.PLAYER1);

        }
        public void ChangeCombatState<T>(T state)
        {
            combatControl.ChangeState(state);
        }
        private List<Player> entities;

        private enum CombatStates
        {
            PLAYER1,
            ENEMY,
        }
        public void AddPlayer(Player p)
        {
            entities.Add(p);
        }
        CombatStates currentPlayer;
        private FSM<CombatStates> combatControl;
    }
}