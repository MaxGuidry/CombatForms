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
            Player Knight = new Player(50f, 1, 30, 10);
            Player Viking = new Player(75, 1, 40, 5);
            Player Samuri = new Player(35, 1, 25, 15);
            Enemy e1 = new Enemy(20, 1, 15, 9);
            Enemy e2 = new Enemy(40, 1, 10, 2);
            Enemy e3 = new Enemy(20, 1, 10, 25);
            Combat.Instance.AddPlayer(Knight);
            Combat.Instance.AddPlayer(Viking);
            Combat.Instance.AddPlayer(Samuri);
            Combat.Instance.AddPlayer(e1);
            Combat.Instance.AddPlayer(e2);
            Combat.Instance.AddPlayer(e3);
            Combat.Instance.SortEntities();
            Combat.Instance.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
