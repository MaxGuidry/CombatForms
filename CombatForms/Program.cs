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
            Player Knight = new Player("Knight",100f, 1, 460f, 10f);
            Player Viking = new Player("Viking",150f, 1, 655f, 5f);
            Player Samuri = new Player("Samuri",90f, 1, 635f, 30f);
           
            Enemy e1 = new Enemy("e1",70f, 1, 15f, 9f);
            Enemy e2 = new Enemy("e2",100f, 1, 10f, 2f);
            Enemy e3 = new Enemy("e3",65f, 1, 10f, 25f);
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
