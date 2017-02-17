using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CombatForms.Classes
{
    public class Combat
    {
        public Form1 form1;

        private static Combat instance;

        private Combat()
        {
            Entities = new List<Entity>();


        }

        public static Combat Instance
        {
            get
            {
                if (instance == null)
                    instance = new Combat();
                return instance;
            }

        }


        public void NextPlayer()
        {
            if (CurrentPlayer.NumberOfTurns <= CurrentPlayer.TurnsTaken)
            {
                currentIndex++;
                if (currentIndex >= Entities.Count)
                {

                    currentIndex = 0;

                }
                CurrentPlayer = Entities[currentIndex];
                CurrentPlayer.ChangePlayerState("WAIT");
            }

        }

        public void AddPlayer(Entity e)
        {
            if (e.onDeath == null)
            {
                e.onDeath = OnPlayerDeath;
                Entities.Add(e);
                SortEntities();
                return;
            }
            e.onDeath += OnPlayerDeath;
            Entities.Add(e);
            SortEntities();
        }
        public string CombatLog = "";
        
        
        
        
        public void GenerateNewEnemy(Enemy dead)
        {
            Random r = new Random();
            Enemy e = new Enemy();
            Enemy ne = new Enemy(Target.Name, (float)Math.Pow((double)e.Health, r.NextDouble() * (1.3d - 1d) + 1d),
                   dead.Level + 1,
                  (float)Math.Pow((double)e.Damage,
                  r.NextDouble() * (1.3d - 1d) + 1d),
                  (float)Math.Pow(e.Speed, r.NextDouble() * (1.3d - 1d) + 1d));
            ne.Info = dead.Info;
            ne.PlayerButton = dead.PlayerButton;
            ne.PlayerButton.Text = ne.Name;
            ne.HealthBar = dead.HealthBar;
            ne.onDeath = OnPlayerDeath;
            ne.HealthBar = new ProgressBar();
            ne.HealthBar.Location = new System.Drawing.Point(715, dead.PlayerButton.Location.Y + dead.PlayerButton.Size.Height);
            ne.HealthBar.Value = (int)((ne.Health / ne.MaxHealth) * 100f);
            ne.Info = new RichTextBox();
            ne.Info.Location = new System.Drawing.Point(700, ne.HealthBar.Location.Y + ne.HealthBar.Size.Height);
            ne.Info.Text = "Health: " + ne.Health + "\nDamage: " + ne.Damage +
                        "\nSpeed: " + ne.Speed + "\nArmor: " + ne.Armor;
            Entities.Add(ne);
            SortEntities();
        }

        private void OnPlayerDeath()
        {
            if (typeof(Player).ToString() == Target.ToString())
            {
                Entities.Remove(Target);
                return;
            }

            GenerateNewEnemy(Target as Enemy);
            form1.Controls.Remove(Target.Info);
            form1.Controls.Remove(Target.HealthBar);
            Entities.Remove(Target);
            SortEntities();
        }

        public void Start()
        {
            currentIndex = 0;
            CurrentPlayer = Entities[currentIndex];

        }

        public void SortEntities()
        {
            Entities.Sort((x, y) => -1 * x.Speed.CompareTo(y.Speed));
            currentIndex = Entities.IndexOf(CurrentPlayer);
        }




        public Entity Target;
        public List<Entity> Entities;
        public Entity CurrentPlayer;
        private int currentIndex;

    }
}