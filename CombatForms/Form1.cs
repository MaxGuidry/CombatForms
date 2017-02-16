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
        public void TestEnemyCombat()
        {
            EndTurn_Click(new object(), new EventArgs());

        }
        public void UpdateCombatUI()
        {
            Combat.Instance.a = this;
            richTextBox1.Text = "Current Player: " + Combat.Instance.currentPlayer.Name + "\nCurrent State: " + Combat.Instance.currentPlayer.CurrentState().ToString() + "\n\n\nTarget: ";
            if (Combat.Instance.target != null)
                richTextBox1.Text += Combat.Instance.target.Name;

            if (Combat.Instance.currentPlayer.ToString() == typeof(Enemy).ToString())
            {
                MessageBox.Show("Press \"End Turn\" to have the enemy take their turn");
                Entity e = Combat.Instance.currentPlayer;
                Combat.Instance.currentPlayer.PlayerButton.Enabled = false;
                Combat.Instance.NextPlayer();
                while (Combat.Instance.currentPlayer != e)
                {
                    Combat.Instance.currentPlayer.PlayerButton.Enabled = false;
                    Combat.Instance.NextPlayer();

                }
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                Entity e = Combat.Instance.currentPlayer;
                Combat.Instance.currentPlayer.PlayerButton.Enabled = true;
                Combat.Instance.NextPlayer();
                while (Combat.Instance.currentPlayer != e)
                {
                    Combat.Instance.currentPlayer.PlayerButton.Enabled = true;
                    Combat.Instance.NextPlayer();
                }
            }
            Combat.Instance.UpdateUI();

        }


        public Form1()
        {
            InitializeComponent();
            foreach (Control c in Combat.Instance.CreateControls())
            {
                this.Controls.Add(c);
            }
            Combat.Instance.Start();
            pictureBox1.Location = new Point(Combat.Instance.currentPlayer.PlayerButton.Location.X + 150, Combat.Instance.currentPlayer.PlayerButton.Location.Y - 75);
            pictureBox2.Visible = false;
            PlayerHealth.Value = (int)((Combat.Instance.currentPlayer.Health / Combat.Instance.currentPlayer.MaxHealth) * 100f);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateCombatUI();
        }

        private void PlayerHealth_Click(object sender, EventArgs e)
        {

        }


        private void EnemyHealth_Click(object sender, EventArgs e)
        {

        }
        private void Attack_Click(object sender, EventArgs e)
        {
            pictureBox2.SendToBack();
            pictureBox2.Visible = true;
            if (Combat.Instance.target != null)
                pictureBox2.Location = new Point(Combat.Instance.target.PlayerButton.Location.X - 183, Combat.Instance.target.PlayerButton.Location.Y - 75);
            Combat.Instance.currentPlayer.ChangePlayerState("ATTACK");
            UpdateCombatUI();
        }
        private void Defend_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentPlayer.ChangePlayerState("DEFEND");
            UpdateCombatUI();
        }

        private void EndTurn_Click(object sender, EventArgs e)
        {

            Combat.Instance.Update();
            pictureBox2.Visible = false;
            pictureBox1.Location = new Point(Combat.Instance.currentPlayer.PlayerButton.Location.X + 150, Combat.Instance.currentPlayer.PlayerButton.Location.Y - 75);
            PlayerHealth.Value = (int)((Combat.Instance.currentPlayer.Health / Combat.Instance.currentPlayer.MaxHealth) * 100f);
            UpdateCombatUI();


        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
