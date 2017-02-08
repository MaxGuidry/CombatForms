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
            entities = new List<Player>();
        }
        public void NextPlayer()
        {
            if (currentPlayerID == 0)
            {
                currentPlayer = entities[1];
                currentPlayerID = 1;
                return;
            }
            currentPlayer = entities[0];
            currentPlayerID = 0;
        }
        public void AddPlayer(Player p)
        {
            entities.Add(p);
            p.onDeath = OnPlayerDeath;

        }
        public void GenerateNewEnemy()
        {

        }
        public Player GetTarget()
        {
            if (currentPlayerID == 0)
                return entities[1];

            return entities[0];

        }
        private void OnPlayerDeath()
        {
            entities.Remove(currentPlayer);

        }
        public void Start()
        {
            currentPlayer = entities[0];
        }
        public void Update()
        {
            if (currentPlayer.CurrentState().ToString() == "ATTACK")
                currentPlayer.DealDamage(GetTarget(), currentPlayer.AD);
        }
        private int currentPlayerID;
        private List<Player> entities;
        public Player currentPlayer;
    }
}