using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatForms.Classes;
using CombatForms.Iterfaces;
namespace CombatForms
{
    public partial class Form1 : Form
    {
        enum PlayerState
        {
            INIT,
            WAIT,
            ATTACK,
            DEFEND,
        }

        enum CombatState
        {
            PLAYER1,
            PLAYER2,
        }
        FSM<CombatState> combatFSM = new FSM<CombatState>();

        public Form1()
        {
            InitializeComponent();
            combatFSM.AddTransition(new FSM<Player>, CombatState.PLAYER2);
            combatFSM.AddTransition(CombatState.PLAYER2, CombatState.PLAYER1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            combatFSM.Start(CombatState.PLAYER1);
           
        }

        private void PlayerHealth_Click(object sender, EventArgs e)
        {

        }



        private void EnemyHealth_Click(object sender, EventArgs e)
        {

        }
        private void Attack_Click(object sender, EventArgs e)
        {

        }
        private void Defend_Click(object sender, EventArgs e)
        {

        }

        private void EndTurn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
