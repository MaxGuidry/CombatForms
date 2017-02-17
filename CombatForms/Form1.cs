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
        //public StatBuff sb;
        public Form1()
        {
            InitializeComponent();
            foreach (Control c in this.CreateControls())
            {
                this.Controls.Add(c);
            }
            Combat.Instance.Start();
            Combat.Instance.OnDeath += RemoveDeadInfo;
            Combat.Instance.OnEnemyGeneration += GenerateEnemyControls;
            Combat.Instance.getTarget += GetTarget;
            foreach (var e in Combat.Instance.Entities)
            {
                if (e.Type == Entity.EntityType.PLAYER)
                {
                    (e as Player).OnLevelUp += delegate { this.Enabled = false; UpdateUI(); };
                    (e as Player).OnLevelUp += delegate { Combat.Instance.ChangeCombatState("LEVELING"); };
                }
            }
            pictureBox1.Location = new Point(Combat.Instance.CurrentPlayer.PlayerButton.Location.X + 150, Combat.Instance.CurrentPlayer.PlayerButton.Location.Y - 75);
            pictureBox2.Visible = false;
            PlayerHealth.Value = (int)((Combat.Instance.CurrentPlayer.Health / Combat.Instance.CurrentPlayer.MaxHealth) * 100f);
        }

        public void GenerateEnemyControls(Enemy e)
        {
            Combat.Instance.Target.Info = e.Info;
            Combat.Instance.Target.PlayerButton = e.PlayerButton;
            Combat.Instance.Target.PlayerButton.Text = Combat.Instance.Target.Name;
            Combat.Instance.Target.HealthBar = e.HealthBar;

            Combat.Instance.Target.HealthBar = new ProgressBar();
            Combat.Instance.Target.HealthBar.Location = new System.Drawing.Point(715, e.PlayerButton.Location.Y + e.PlayerButton.Size.Height);
            Combat.Instance.Target.HealthBar.Value = (int)((Combat.Instance.Target.Health / Combat.Instance.Target.MaxHealth) * 100f);
            Combat.Instance.Target.Info = new RichTextBox();
            Combat.Instance.Target.Info.Location = new System.Drawing.Point(700, Combat.Instance.Target.HealthBar.Location.Y + Combat.Instance.Target.HealthBar.Size.Height);
            Combat.Instance.Target.Info.Text = "Health: " + Combat.Instance.Target.Health + "\nDamage: " + Combat.Instance.Target.Damage +
                        "\nSpeed: " + Combat.Instance.Target.Speed + "\nArmor: " + Combat.Instance.Target.Armor;
            UpdateAllUI();
        }

        public void UpdateAllUI()
        {

            UpdateCombatUI();
            UpdateUI();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateCombatUI();
        }

        public void RemoveDeadInfo()
        {
            Controls.Remove(Combat.Instance.Target.Info);
            Controls.Remove(Combat.Instance.Target.HealthBar);
        }

        public void TestEnemyCombat()
        {

            EndTurn_Click(new object(), new EventArgs());

        }
        public List<Control> CreateControls()
        {
            int i = 1;
            int j = 1;
            List<Control> tmp = new List<Control>();
            foreach (Entity e in Combat.Instance.Entities)
            {
                e.StateBox = new RichTextBox();
                e.Info = new RichTextBox();
                e.HealthBar = new ProgressBar();
                e.HealthBar.Size = new System.Drawing.Size(100, 25);
                e.PlayerButton = new Button();
                e.PlayerButton.Size = new Size(150, 50);
                if (typeof(Player).ToString() == e.ToString())
                {
                    e.PlayerButton.Location = new System.Drawing.Point(110, 150 * i);
                    e.PlayerButton.Text = e.Name;

                    e.HealthBar.Location = new System.Drawing.Point(135, e.PlayerButton.Location.Y + e.PlayerButton.Size.Height);

                    (e.HealthBar as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);

                    e.Info.Location = new System.Drawing.Point(135, e.HealthBar.Location.Y + e.HealthBar.Size.Height);
                    (e.Info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    e.StateBox.Location = new System.Drawing.Point(265, e.PlayerButton.Location.Y);
                    e.StateBox.Size = new System.Drawing.Size(150, 30);
                    (e.StateBox as RichTextBox).Text = "Current State: " + e.CurrentState.ToString();
                    i++;
                }
                if (typeof(Enemy).ToString() == e.ToString())
                {
                    e.PlayerButton.Location = new System.Drawing.Point(690, 150 * j);
                    e.HealthBar.Location = new System.Drawing.Point(715, e.PlayerButton.Location.Y + e.PlayerButton.Size.Height);
                    (e.HealthBar as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);

                    e.PlayerButton.Text = e.Name;


                    e.Info.Location = new System.Drawing.Point(715, e.HealthBar.Location.Y + e.HealthBar.Size.Height);
                    (e.Info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                         "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    e.StateBox.Location = new System.Drawing.Point(845, e.PlayerButton.Location.Y);
                    e.StateBox.Size = new System.Drawing.Size(150, 30);
                    (e.StateBox as RichTextBox).Text = "Current State: " + e.CurrentState.ToString();
                    j++;
                }


                e.PlayerButton.Click += GetTarget;
                e.PlayerButton.Click += SetTarget;

                e.Info.Size = new System.Drawing.Size(100, 75);

                tmp.Add(e.PlayerButton);
                tmp.Add(e.StateBox);
                tmp.Add(e.Info);
                tmp.Add(e.HealthBar);

            }

            return tmp;
        }
        public void GetTarget(object sender, EventArgs eventargs)
        {

            Combat.Instance.Target = Combat.Instance.CurrentPlayer;
            if (typeof(Player).ToString() == Combat.Instance.CurrentPlayer.ToString())
            {
                foreach (Entity e in Combat.Instance.Entities)
                {
                    if (e.Name == (sender as Control).Text && !(Combat.Instance.CurrentPlayer.ToString() == e.ToString()))
                    {
                        Combat.Instance.Target = e;

                        return;
                    }
                }
            }
            else if (typeof(Enemy).ToString() == Combat.Instance.CurrentPlayer.ToString())
            {
                Random r = new Random();
                string target1 = Combat.Instance.Target.ToString();
                string target2 = typeof(Player).ToString();
                while (Combat.Instance.Target.ToString() != typeof(Player).ToString())
                {
                    Combat.Instance.Target = Combat.Instance.Entities[r.Next(0, Combat.Instance.Entities.Count)];
                }
            }
        }
        public void SetTarget(object o, EventArgs ea)
        {
            Entity tar = Combat.Instance.Entities.Find(x => x.Name == (o as Button).Text);
            Combat.Instance.Target = tar;

            UpdateAllUI();
        }

        public void UpdateUI()
        {


            foreach (Entity e in Combat.Instance.Entities)
            {

                if (typeof(Player).ToString() == e.ToString())
                {
                    if (e.Info == null)
                    {
                        e.Info = new RichTextBox();

                        e.Info.Location = new System.Drawing.Point(135, e.HealthBar.Location.Y + e.HealthBar.Size.Height);
                    }
                    (e.Info).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    e.StateBox.Text = "Current State: " + e.CurrentState.ToString();

                }

                if (typeof(Enemy).ToString() == e.ToString())
                {

                    if (this.Controls.Contains(e.Info) == false)
                    {
                        e.StateBox = new RichTextBox();
                        e.StateBox.Location = new System.Drawing.Point(845, Combat.Instance.CurrentPlayer.PlayerButton.Location.Y);
                        e.StateBox.Size = new System.Drawing.Size(150, 30);
                        e.Info.Location = new System.Drawing.Point(715, e.HealthBar.Location.Y + e.HealthBar.Size.Height);

                        this.Controls.Add(e.Info);
                        this.Controls.Add(e.StateBox);
                        this.Controls.Add(e.HealthBar);
                    }

                    (e.Info).Text = (e.Info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    e.StateBox.Text = "Current State: " + e.CurrentState.ToString();
                }
            }
        }
        public void UpdateCombatUI()
        {//just get the name of the button at runtime


            StatBuff.form1 = this;

            Combat.Instance.CombatLog = string.Format("Current Player: {0} \nCurrentState: {1} \nTarget: ", Combat.Instance.CurrentPlayer.Name, Combat.Instance.CurrentPlayer.CurrentState, Combat.Instance.Target);
            richTextBox1.Text = Combat.Instance.CombatLog;

            if (Combat.Instance.Target != null)
                richTextBox1.Text += Combat.Instance.Target.Name;

            if (Combat.Instance.CurrentPlayer.ToString() == typeof(Enemy).ToString())
            {

                Entity e = Combat.Instance.CurrentPlayer;
                Combat.Instance.CurrentPlayer.PlayerButton.Enabled = false;
                Combat.Instance.NextPlayer();
                while (Combat.Instance.CurrentPlayer != e)
                {
                    Combat.Instance.CurrentPlayer.PlayerButton.Enabled = false;
                    Combat.Instance.NextPlayer();

                }
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                Entity e = Combat.Instance.CurrentPlayer;
                Combat.Instance.CurrentPlayer.PlayerButton.Enabled = true;
                Combat.Instance.NextPlayer();
                while (Combat.Instance.CurrentPlayer != e)
                {
                    Combat.Instance.CurrentPlayer.PlayerButton.Enabled = true;
                    Combat.Instance.NextPlayer();
                }
            }
            pictureBox1.SendToBack();
            pictureBox1.Location = new Point(Combat.Instance.CurrentPlayer.PlayerButton.Location.X + 150, Combat.Instance.CurrentPlayer.PlayerButton.Location.Y - 75);

            //foreach(Control c in Controls)
            //{
            //    Button b = c as Button;
            //    if(b != null)
            //    {
            //        if(b.Text == Combat.Instance.CurrentPlayer.Name)
            //        {
            //            b.Enabled = false;
            //        }
            //    }
            //}


        }
        private void PlayerHealth_Click(object sender, EventArgs e)
        {

        }
        private void EnemyHealth_Click(object sender, EventArgs e)
        {

        }
        private void Attack_Click(object sender, EventArgs e)
        {
            if (Combat.Instance.Target != null)
            {
                pictureBox2.Location = new Point(Combat.Instance.Target.PlayerButton.Location.X - 183, Combat.Instance.Target.PlayerButton.Location.Y - 75);
                pictureBox2.SendToBack();
                pictureBox2.Visible = true;
            }
            Combat.Instance.CurrentPlayer.ChangePlayerState("ATTACK");
            UpdateAllUI();
        }
        private void Defend_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.ChangePlayerState("DEFEND");
            UpdateAllUI();
        }
        private void EndTurn_Click(object sender, EventArgs e)
        {

            Combat.Instance.UpdateCombat();
            pictureBox2.Visible = false;
            PlayerHealth.Value = (int)((Combat.Instance.CurrentPlayer.Health / Combat.Instance.CurrentPlayer.MaxHealth) * 100f);
            UpdateAllUI();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
