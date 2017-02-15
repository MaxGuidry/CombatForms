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



        public Form1()
        {
            InitializeComponent();

            foreach(Control c in Combat.Instance.CreateControls())
            {
                this.Controls.Add(c);
            }
            Combat.Instance.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1 = Combat.Instance.UpdateCombatUI(richTextBox1);
        }

        private void PlayerHealth_Click(object sender, EventArgs e)
        {

        }


        private void EnemyHealth_Click(object sender, EventArgs e)
        {
           
        }
        private void Attack_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentPlayer.ChangePlayerState("ATTACK");
            richTextBox1 = Combat.Instance.UpdateCombatUI(richTextBox1);
        }
        private void Defend_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentPlayer.ChangePlayerState("DEFEND");
            richTextBox1 = Combat.Instance.UpdateCombatUI(richTextBox1);
        }

        private void EndTurn_Click(object sender, EventArgs e)
        {
         
            Combat.Instance.Update();

            richTextBox1 = Combat.Instance.UpdateCombatUI(richTextBox1);

        }

       
    }
}
