using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatForms.Classes;
namespace CombatForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Player me=new Player(50f,1,30,10);
            Enemy you = new Enemy(40, 1, 20, 5);
           
            Combat.Instance.AddPlayer(me);
            Combat.Instance.AddPlayer(you);
            Combat.Instance.currentPlayer = me;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
