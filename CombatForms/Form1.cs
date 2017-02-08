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

        Player me;
        Player you;
        public Combat test;
        public Form1()
        {
            InitializeComponent();
            test = new Combat();
            me = new Player(598f,1,30f);
            you = new Player(2f,1,20f);
            test.AddPlayer(me);
            test.AddPlayer(you);
            test.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PlayerHealth_Click(object sender, EventArgs e)
        {

        }


        private void EnemyHealth_Click(object sender, EventArgs e)
        {

        }
        private void Attack_Click(object sender, EventArgs e)
        {
            test.currentPlayer.ChangePlayerState("ATTACK");
           
        }
        private void Defend_Click(object sender, EventArgs e)
        {
            test.currentPlayer.ChangePlayerState("DEFEND");

        }

        private void EndTurn_Click(object sender, EventArgs e)
        {
            test.Update();
            test.currentPlayer.ChangePlayerState("WAIT");
            test.NextPlayer();
        }
    }
}
