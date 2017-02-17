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
            Combat.Instance.form1.Enabled = true;
            Combat.Instance.NextPlayer();
            Combat.Instance.form1.UpdateCombatUI();
            Combat.Instance.form1.Show();
        }

        private void Armor_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.Armor += Combat.Instance.CurrentPlayer.Armor * .20f;
            this.Close();
            Combat.Instance.form1.Enabled = true;
            Combat.Instance.NextPlayer();
            Combat.Instance.form1.UpdateCombatUI();
        }

        private void Speed_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.Speed += Combat.Instance.CurrentPlayer.Speed * .20f;
            this.Close();
            Combat.Instance.form1.Enabled = true;
            Combat.Instance.NextPlayer();
            Combat.Instance.form1.UpdateCombatUI();
        }

        private void Health_Click(object sender, EventArgs e)
        {
            Combat.Instance.CurrentPlayer.Health += Combat.Instance.CurrentPlayer.Health * .20f;
            this.Close();
            Combat.Instance.form1.Enabled = true;
            Combat.Instance.NextPlayer();
            Combat.Instance.form1.UpdateCombatUI();
        }
    }
}
