using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Classes
{
    public class Combat
    {

        //Not sure whether to do this with the fsm or just a list of players since there is just the Player against enemies that level as you kill them
        //THIS IS THE FSM WAY?????
        /*
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
           
        private enum CombatStates
        {
            PLAYER1,
            ENEMY,
        }
        CombatStates currentPlayer;
        private FSM<CombatStates> combatControl;
        */

        public Combat()
        {

        }
        public void NextPlayer()
        {
            
        }
        public void AddPlayer(Player p)
        {
            entities.Add(p);
            p.onDeath = OnPlayerDeath;
           
        }
        private void OnPlayerDeath()
        {
            entities.Remove(currentPlayer);
        }
        private List<Player> entities;
        public Player currentPlayer;
    }
}