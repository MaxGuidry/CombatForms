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
    public partial class Form1 : Form
    {
        public void Red()
        {

        }
        enum Player
        {
            INIT,
            IDLE,
            WALK,
            RUN,
            SPRINT,
            EXIT,
        }

        FSM<Player> playerFSM = new FSM<Player>();

        public Form1()
        {
            InitializeComponent();
            //playerFSM.AddState(Player.INIT);
            //playerFSM.AddState(Player.IDLE);
            //playerFSM.AddState(Player.WALK);
            //playerFSM.AddState(Player.RUN);
            //playerFSM.AddState(Player.EXIT);
            playerFSM.AddTransition(Player.INIT, Player.IDLE);
            playerFSM.AddTransition(Player.IDLE, Player.WALK);
            playerFSM.AddTransition(Player.WALK, Player.RUN);
            playerFSM.AddTransition(Player.RUN, Player.EXIT);
            playerFSM.AddTransition(Player.RUN, Player.SPRINT);
            
            playerFSM.AddTransition(Player.WALK, Player.IDLE);
            playerFSM.AddTransition(Player.RUN, Player.WALK);
            
            playerFSM.AddTransition(Player.SPRINT, Player.RUN);
            playerFSM.Start();
            richTextBox1.Text = "Current State: " + playerFSM.GetState().Name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            playerFSM.ChangeState(Player.IDLE);
            richTextBox1.Text = "Current State: " + playerFSM.GetState().Name;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (playerFSM.GetState() == (new State(Player.IDLE)))
                {
                    playerFSM.ChangeState(Player.WALK);
                    break;
                }
                if (playerFSM.GetState() == (new State(Player.WALK)))
                {
                    playerFSM.ChangeState(Player.RUN);
                    break;
                }
                if (playerFSM.GetState() == (new State(Player.RUN)))
                {
                    playerFSM.ChangeState(Player.SPRINT);
                    break;
                }
                else
                    break;
            }
            richTextBox1.Text = "Current State: " + playerFSM.GetState().Name;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (playerFSM.GetState() == (new State(Player.WALK)))
                {
                    playerFSM.ChangeState(Player.IDLE);
                    break;
                }
                if (playerFSM.GetState() == (new State(Player.RUN)))
                {
                    playerFSM.ChangeState(Player.WALK);
                    break;
                }

                if (playerFSM.GetState() == (new State(Player.SPRINT)))
                {
                    playerFSM.ChangeState(Player.RUN); 
                        break;
                }
                else
                    break;
            }
            richTextBox1.Text = "Current State: " + playerFSM.GetState().Name;
        }
    }
}
