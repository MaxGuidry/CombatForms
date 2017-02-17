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
namespace CombatForms
{
    public partial class StatBuff : Form
    {
        static public Form1 form1;
        public StatBuff()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void StatBuff_Load(object sender, EventArgs e)
        {

        }

        private void Damage_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.Damage += Combat.Instance.CurrentPlayer.Damage * .20f;
            this.Close();
            form1.Enabled = true;
            Combat.Instance.NextPlayer();
            form1.UpdateAllUI();
            form1.Show();
            Combat.Instance.ChangeCombatState("FIGHTING");
        }

        private void Armor_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.Armor += Combat.Instance.CurrentPlayer.Armor * .20f;
            this.Close();
            form1.Enabled = true;
            Combat.Instance.NextPlayer();
            form1.UpdateAllUI();
            form1.Show();
            Combat.Instance.ChangeCombatState("FIGHTING");
        }

        private void Speed_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.Speed += Combat.Instance.CurrentPlayer.Speed * .20f;
            this.Close();
            form1.Enabled = true;
            Combat.Instance.NextPlayer();
            form1.UpdateAllUI();
            form1.Show();
            Combat.Instance.ChangeCombatState("FIGHTING");
        }

        private void Health_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.MaxHealth += Combat.Instance.CurrentPlayer.MaxHealth * .20f;
            this.Close();
            form1.Enabled = true;
            Combat.Instance.NextPlayer();
            form1.UpdateAllUI();
            form1.Show();
            Combat.Instance.ChangeCombatState("FIGHTING");
        }
    }
}
