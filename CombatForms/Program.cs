using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatForms.Classes;
using System.Diagnostics;

namespace CombatForms
{
    enum GameState
    {
        init,running,paused,exit,
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Player Knight = new Player("Knight",100f, 1, 50f, 7f);
            Player Viking = new Player("Viking",150f, 1, 75f, 5f);
            Player Samuri = new Player("Samuri",90f, 1, 40f, 12f);
           
            Enemy e1 = new Enemy("e1",70f, 1, 25f, 9f);
            Enemy e2 = new Enemy("e2",100f, 1, 35f, 5f);
            Enemy e3 = new Enemy("e3",65f, 1, 20f, 12f);
            Combat.Instance.AddPlayer(Knight);
            Combat.Instance.AddPlayer(Viking);
            Combat.Instance.AddPlayer(Samuri);
            Combat.Instance.AddPlayer(e1);
            Combat.Instance.AddPlayer(e2);
            Combat.Instance.AddPlayer(e3);
            FSM<GameState> fsm = new FSM<GameState>();
            fsm.AddTransition(GameState.init, GameState.running);
            
            fsm.GetState(GameState.init).AddEnterFunction((Action)delegate { Debug.WriteLine("init state"); } );
            fsm.GetState(GameState.running).AddEnterFunction((Action)delegate { Debug.WriteLine("running state"); });
            fsm.Start(GameState.init);
            fsm.ChangeState(GameState.running);
            
            Combat.Instance.SortEntities();
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
