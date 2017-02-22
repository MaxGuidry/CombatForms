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

            health.Location = new System.Drawing.Point(685, Controls[e.Name + "targetButton"].Location.Y + Controls[e.Name + "targetButton"].Size.Height);
            health.Value = (int)((Combat.Instance.Target.Health / Combat.Instance.Target.MaxHealth) * 100f);


            info = Controls[e.Name + "info"] as RichTextBox;
            info.Location = new System.Drawing.Point(685, Controls[e.Name + "health"].Location.Y + Controls[e.Name + "health"].Size.Height);

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
                    (e as Player).DeathUpdate += UpdateAllUI;
                    (e as Player).OnLevelUp += delegate { this.Enabled = false; UpdateUI(); };
                    (e as Player).OnLevelUp += delegate { Combat.Instance.ChangeCombatState("LEVELING"); };
                }
            }
            Combat.Instance.OnEnemyGeneration += GenerateNewEnemyControl;
            Combat.Instance.getTarget += GetTarget;
            Combat.Instance.OnLoad();
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


                RichTextBox state = new RichTextBox();
                RichTextBox info = new RichTextBox();
                ProgressBar health = new ProgressBar();
                Button targetButton = new Button();

                state.Name = e.Name + "state";
                info.Name = e.Name + "info";
                health.Name = e.Name + "health";
                targetButton.Name = e.Name + "targetButton";
                info.ReadOnly = true;
                health.Size = new System.Drawing.Size(100, 25);

                targetButton.Size = new Size(150, 50);
                if (typeof(Player).ToString() == e.ToString())
                {
                    targetButton.Location = new System.Drawing.Point(80, 150 * i);
                    targetButton.Text = e.Name;

                    health.Location = new System.Drawing.Point(105, targetButton.Location.Y + targetButton.Size.Height);

                    (health as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);

                    info.Location = new System.Drawing.Point(105, health.Location.Y + health.Size.Height);
                    (info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    state.Location = new System.Drawing.Point(235, targetButton.Location.Y);
                    state.Size = new System.Drawing.Size(140, 30);
                    (state as RichTextBox).Text = "Current State: " + e.CurrentState.ToString();
                    i++;
                }
                if (typeof(Enemy).ToString() == e.ToString())
                {
                    targetButton.Location = new System.Drawing.Point(660, 150 * j);
                    health.Location = new System.Drawing.Point(685, targetButton.Location.Y + targetButton.Size.Height);
                    (health as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);

                    targetButton.Text = e.Name;


                    info.Location = new System.Drawing.Point(685, health.Location.Y + health.Size.Height);
                    (info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                         "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    state.Location = new System.Drawing.Point(820, targetButton.Location.Y);
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


            Combat.Instance.SetTarget((sender as Control).Text); 
        }
        public void SetTarget(object o, EventArgs ea)
        {
            
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


                    Controls[e.Name + "info"].Location = new System.Drawing.Point(105, Controls[e.Name + "health"].Location.Y + Controls[e.Name + "health"].Size.Height);

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



                        state.Location = new System.Drawing.Point(835, Controls[e.Name + "targetButton"].Location.Y);
                        state.Size = new System.Drawing.Size(150, 30);
                        info.Location = new System.Drawing.Point(705, Controls[e.Name + "health"].Location.Y + Controls[e.Name + "health"].Size.Height);

                        this.Controls.Add(info);
                        this.Controls.Add(state);
                        this.Controls.Add(health);
                    }

                    Controls[e.Name + "info"].Text = (Controls[e.Name + "info"]).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    Controls[e.Name + "state"].Text = "Current State: " + e.CurrentState.ToString();
                    (Controls[e.Name + "health"] as ProgressBar).Value = (int)((e.Health / e.MaxHealth) * 100f);
                }
            }
        }
        public void UpdateCombatUI()
        {


            StatBuff.form1 = this;
            if (richTextBox2.Text.Length > 150)
                richTextBox2.Text = "";
            richTextBox2.Text += Combat.Instance.CombatLog;
            Combat.Instance.CombatLog = "";

            Combat.Instance.CurrentInfoLog = string.Format("Current Player: {0} \nCurrentState: {1} \nTurns Left: {2} \nTarget: ", Combat.Instance.CurrentEntity.Name, Combat.Instance.CurrentEntity.CurrentState, Combat.Instance.CurrentEntity.NumberOfTurns - Combat.Instance.CurrentEntity.TurnsTaken, Combat.Instance.Target);
            richTextBox1.Text = Combat.Instance.CurrentInfoLog;

            if (Combat.Instance.Target != null)
                richTextBox1.Text += Combat.Instance.Target.Name;

            if (Combat.Instance.CurrentEntity.ToString() == typeof(Enemy).ToString())
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
            pictureBox1.Location = new Point(Controls[Combat.Instance.CurrentEntity.Name + "targetButton"].Location.X + 150, Controls[Combat.Instance.CurrentEntity.Name + "targetButton"].Location.Y - 75);
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            numericUpDown1.Value = Combat.Instance.KillCount;
        }
        public void Setup()
        {
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
                    (e as Player).DeathUpdate += UpdateAllUI;
                    (e as Player).OnLevelUp += delegate { this.Enabled = false; UpdateUI(); };
                    (e as Player).OnLevelUp += delegate { Combat.Instance.ChangeCombatState("LEVELING"); };
                }
            }
            pictureBox1.Location = new Point(Controls[Combat.Instance.CurrentEntity.Name + "targetButton"].Location.X + 150, Controls[Combat.Instance.CurrentEntity.Name + "targetButton"].Location.Y - 75);
            pictureBox2.Visible = false;
            PlayerHealth.Value = (int)((Combat.Instance.CurrentEntity.Health / Combat.Instance.CurrentEntity.MaxHealth) * 100f);
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            Setup();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateAllUI();
        }
        private void Attack_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentEntity.ChangePlayerState("ATTACK");
            UpdateAllUI();
        }
        private void Defend_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentEntity.ChangePlayerState("DEFEND");
            UpdateAllUI();
        }
        private void EndTurn_Click(object sender, EventArgs e)
        {

            Combat.Instance.UpdateCombat();
            pictureBox2.Visible = false;
            PlayerHealth.Value = (int)((Combat.Instance.CurrentEntity.Health / Combat.Instance.CurrentEntity.MaxHealth) * 100f);
            UpdateAllUI();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentIndex = DataSerializer<int>.Deserialize("Current Index");
            Combat.Instance.Entities = DataSerializer<List<Entity>>.Deserialize("All PLayers");
            Combat.Instance.KillCount = DataSerializer<int>.Deserialize("Kill Count");
            CreateControls();
            OnLoad();
            richTextBox2.Text = "Game Loaded.\n";
            UpdateAllUI();
        }
        private void Save_Click(object sender, EventArgs e)
        {
            DataSerializer<int>.Serialize("Current Index", Combat.Instance.Entities.IndexOf(Combat.Instance.CurrentEntity));
            DataSerializer<List<Entity>>.Serialize("All Players", Combat.Instance.Entities);
            DataSerializer<int>.Serialize("Kill Count", Combat.Instance.KillCount);
            richTextBox2.Text += "Game Saved.\n";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Entities on the left side are on your team.\n\nAll Entities on the right side are on the opposing team" +
                "\n\nTo attack an enemy: Click the button of the enemy you would like to attack\nPress the attack button\nThen end your turn\n\nWhen it is the enemies turn just end the turn and the enemy will act\n\n"+
                "If you are low on health and want to defend just press the defend button then end your turn.\n\n"+
                "The only way to heal is to level up.\n\n Good Luck.");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = Combat.Instance.KillCount;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
