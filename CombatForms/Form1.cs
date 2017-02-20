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
        //Custom Form1 Functions
        #region Custom Form1 Fucntions

        public void GenerateNewEnemyControl(Enemy e)
        {

            RichTextBox state = new RichTextBox();
            RichTextBox info = new RichTextBox();
            ProgressBar health = new ProgressBar();
            Button targetButton = new Button();

            state.Name = e.Name + "state";
            info.Name = e.Name + "info";
            health.Name = e.Name + "health";
            targetButton.Name = e.Name + "targetButton";

            health = Controls[e.Name + "health"] as ProgressBar;
            targetButton.Text = e.Name;

            health.Location = new System.Drawing.Point(715, Controls[e.Name + "targetButton"].Location.Y + Controls[e.Name + "targetButton"].Size.Height);
            health.Value = (int)((Combat.Instance.Target.Health / Combat.Instance.Target.MaxHealth) * 100f);


            info = Controls[e.Name + "info"] as RichTextBox;
            info.Location = new System.Drawing.Point(715, Controls[e.Name + "health"].Location.Y + Controls[e.Name + "health"].Size.Height);

            info.Text = "Health: " + Combat.Instance.Target.Health + "\nDamage: " + Combat.Instance.Target.Damage +
                        "\nSpeed: " + Combat.Instance.Target.Speed + "\nArmor: " + Combat.Instance.Target.Armor;

            state = Controls[e.Name + "state"] as RichTextBox;
            state.Text = "Current State: " + Combat.Instance.Target.CurrentState.ToString();
            targetButton = Controls[e.Name + "targetButton"] as Button;

            Controls.RemoveByKey(Combat.Instance.Target.Name + "state");
            Controls.RemoveByKey(Combat.Instance.Target.Name + "info");
            Controls.RemoveByKey(Combat.Instance.Target.Name + "health");
            Controls.RemoveByKey(Combat.Instance.Target.Name + "targetButton");

            Controls.Add(state);
            Controls.Add(health);
            Controls.Add(info);
            Controls.Add(targetButton);



        }
        public void OnLoad()
        {
            foreach (Entity e in Combat.Instance.Entities)
            {

                if (e.Type == Entity.EntityType.PLAYER)
                {
                    (e as Player).OnLevelUp += delegate { this.Enabled = false; UpdateUI(); };
                    (e as Player).OnLevelUp += delegate { Combat.Instance.ChangeCombatState("LEVELING"); };
                }
            }
            Combat.Instance.OnEnemyGeneration += GenerateNewEnemyControl;
            Combat.Instance.getTarget += GetTarget;
        }
        public void UpdateAllUI()
        {

            UpdateCombatUI();
            UpdateUI();
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

                //e.StateBox = new RichTextBox();
                //e.Info = new RichTextBox();
                //e.HealthBar = new ProgressBar();
                // e.PlayerButton = new Button();

                RichTextBox state = new RichTextBox();
                RichTextBox info = new RichTextBox();
                ProgressBar health = new ProgressBar();
                Button targetButton = new Button();

                state.Name = e.Name + "state";
                info.Name = e.Name + "info";
                health.Name = e.Name + "health";
                targetButton.Name = e.Name + "targetButton";

                health.Size = new System.Drawing.Size(100, 25);

                targetButton.Size = new Size(150, 50);
                if (typeof(Player).ToString() == e.ToString())
                {
                    targetButton.Location = new System.Drawing.Point(110, 150 * i);
                    targetButton.Text = e.Name;

                    health.Location = new System.Drawing.Point(135, targetButton.Location.Y + targetButton.Size.Height);

                    (health as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);

                    info.Location = new System.Drawing.Point(135, health.Location.Y + health.Size.Height);
                    (info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    state.Location = new System.Drawing.Point(265, targetButton.Location.Y);
                    state.Size = new System.Drawing.Size(150, 30);
                    (state as RichTextBox).Text = "Current State: " + e.CurrentState.ToString();
                    i++;
                }
                if (typeof(Enemy).ToString() == e.ToString())
                {
                    targetButton.Location = new System.Drawing.Point(690, 150 * j);
                    health.Location = new System.Drawing.Point(715, targetButton.Location.Y + targetButton.Size.Height);
                    (health as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);

                    targetButton.Text = e.Name;


                    info.Location = new System.Drawing.Point(715, health.Location.Y + health.Size.Height);
                    (info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                         "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    state.Location = new System.Drawing.Point(845, targetButton.Location.Y);
                    state.Size = new System.Drawing.Size(150, 30);
                    (state as RichTextBox).Text = "Current State: " + e.CurrentState.ToString();
                    j++;
                }


                targetButton.Click += GetTarget;
                targetButton.Click += SetTarget;

                info.Size = new System.Drawing.Size(100, 75);

                tmp.Add(targetButton);
                tmp.Add(state);
                tmp.Add(info);
                tmp.Add(health);

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
            if (Combat.Instance.Target != null)
            {
                pictureBox2.Location = new Point(Controls[Combat.Instance.Target.Name + "targetButton"].Location.X - 183, Controls[Combat.Instance.Target.Name + "targetButton"].Location.Y - 75);
                pictureBox2.SendToBack();
                pictureBox2.Visible = true;
            }
            UpdateAllUI();
        }
        public void UpdateUI()
        {
            foreach (Entity e in Combat.Instance.Entities)
            {

                if (typeof(Player).ToString() == e.ToString())
                {


                    Controls[e.Name + "info"].Location = new System.Drawing.Point(135, Controls[e.Name + "health"].Location.Y + Controls[e.Name + "health"].Size.Height);

                    Controls[e.Name + "info"].Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    Controls[e.Name + "state"].Text = "Current State: " + e.CurrentState.ToString();
                    (Controls[e.Name + "health"] as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);
                }


                if (typeof(Enemy).ToString() == e.ToString())
                {

                    if (!this.Controls.ContainsKey(e.Name + "info"))
                    {
                        RichTextBox state = new RichTextBox();
                        RichTextBox info = new RichTextBox();
                        ProgressBar health = new ProgressBar();
                        Button targetButton = new Button();

                        state.Name = e.Name + "state";
                        info.Name = e.Name + "info";
                        health.Name = e.Name + "health";
                        targetButton.Name = e.Name + "targetButton";



                        state.Location = new System.Drawing.Point(845, Controls[e.Name + "targetButton"].Location.Y);
                        state.Size = new System.Drawing.Size(150, 30);
                        info.Location = new System.Drawing.Point(715, Controls[e.Name + "health"].Location.Y + Controls[e.Name + "health"].Size.Height);

                        this.Controls.Add(info);
                        this.Controls.Add(state);
                        this.Controls.Add(health);
                    }

                    Controls[e.Name + "info"].Text = (Controls[e.Name + "info"]).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    Controls[e.Name + "state"].Text = "Current State: " + e.CurrentState.ToString();
                }
            }
        }
        public void UpdateCombatUI()
        {


            StatBuff.form1 = this;

            Combat.Instance.CombatLog = string.Format("Current Player: {0} \nCurrentState: {1} \nTarget: ", Combat.Instance.CurrentPlayer.Name, Combat.Instance.CurrentPlayer.CurrentState, Combat.Instance.Target);
            richTextBox1.Text = Combat.Instance.CombatLog;

            if (Combat.Instance.Target != null)
                richTextBox1.Text += Combat.Instance.Target.Name;

            if (Combat.Instance.CurrentPlayer.ToString() == typeof(Enemy).ToString())
            {


                foreach (Entity en in Combat.Instance.Entities)
                {
                    Controls[en.Name + "targetButton"].Enabled = false;

                }
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;



                foreach (Entity en in Combat.Instance.Entities)
                {
                    Controls[en.Name + "targetButton"].Enabled = true;

                }
            }
            pictureBox1.SendToBack();
            pictureBox1.Location = new Point(Controls[Combat.Instance.CurrentPlayer.Name + "targetButton"].Location.X + 150, Controls[Combat.Instance.CurrentPlayer.Name + "targetButton"].Location.Y - 75);

        }
        public void RedArrow()
        {

        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            foreach (Control c in this.CreateControls())
            {
                this.Controls.Add(c);
            }
            Combat.Instance.Start();

            Combat.Instance.OnEnemyGeneration += GenerateNewEnemyControl;
            Combat.Instance.getTarget += GetTarget;
            foreach (var e in Combat.Instance.Entities)
            {
                if (e.Type == Entity.EntityType.PLAYER)
                {
                    (e as Player).OnLevelUp += delegate { this.Enabled = false; UpdateUI(); };
                    (e as Player).OnLevelUp += delegate { Combat.Instance.ChangeCombatState("LEVELING"); };
                }
            }
            pictureBox1.Location = new Point(Controls[Combat.Instance.CurrentPlayer.Name + "targetButton"].Location.X + 150, Controls[Combat.Instance.CurrentPlayer.Name + "targetButton"].Location.Y - 75);
            pictureBox2.Visible = false;
            PlayerHealth.Value = (int)((Combat.Instance.CurrentPlayer.Health / Combat.Instance.CurrentPlayer.MaxHealth) * 100f);
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
        private void button5_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentIndex = DataSerializer<int>.Deserialize("Current Index");
            Combat.Instance.Entities = DataSerializer<List<Entity>>.Deserialize("All PLayers");
            CreateControls();
            OnLoad();
            Combat.Instance.OnLoad();
            UpdateAllUI();
        }
        private void Save_Click(object sender, EventArgs e)
        {
            DataSerializer<int>.Serialize("Current Index", Combat.Instance.Entities.IndexOf(Combat.Instance.CurrentPlayer));
            DataSerializer<List<Entity>>.Serialize("All Players", Combat.Instance.Entities);

        }
    }
}
