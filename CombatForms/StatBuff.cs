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
            Combat.Instance.currentPlayer.Damage += Combat.Instance.currentPlayer.Damage * .20f;
        }

        private void Armor_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentPlayer.Armor += Combat.Instance.currentPlayer.Armor * .20f;
        }

        private void Speed_Click(object sender, EventArgs e)
        {
            Combat.Instance.currentPlayer.Speed += Combat.Instance.currentPlayer.Speed * .20f;

        }
    }
}
