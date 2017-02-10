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

            foreach(Control c in Combat.Instance.CreateButtons())
            {
                this.Controls.Add(c);
            }
            Combat.Instance.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //richTextBox1.Text = "Current Level: " + Combat.Instance.user.Level + '\n' + "Attack: " + Combat.Instance.user.AD + '\n' + "Speed: " + Combat.Instance.user.Speed;
            //richTextBox2.Text = "Current Level: " + Combat.Instance.enemy.Level + '\n' + "Attack: " + Combat.Instance.enemy.AD + '\n' + "Speed: " + Combat.Instance.enemy.Speed;
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
            
        }
        private void Defend_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentPlayer.ChangePlayerState("DEFEND");
        }

        private void EndTurn_Click(object sender, EventArgs e)
        {

            Combat.Instance.Update();
            // Combat.Instance.user.ChangePlayerState("WAIT");
           
            Combat.Instance.NextPlayer();
            // richTextBox1.Text = "Current Level: " + Combat.Instance.user.Level;
            // richTextBox1.Text = "Current Level: " + Combat.Instance.user.Level + '\n' + "Attack: " + Combat.Instance.user.AD + '\n' + "Speed: " + Combat.Instance.user.Speed;
            //richTextBox2.Text = "Current Level: " + Combat.Instance.enemy.Level + '\n' + "Attack: " + Combat.Instance.enemy.AD + '\n' + "Speed: " + Combat.Instance.enemy.Speed;


        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
