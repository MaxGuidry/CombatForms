using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    public class Combat
    {


        private static Combat instance;
        private Combat()
        {
            entities = new List<Entity>();


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
            if (currentPlayer.NumberOfTurns <= currentPlayer.TurnsTaken)
            {
                currentIndex++;
                if (currentIndex >= entities.Count)
                {

                    currentIndex = 0;
                    
                }
                currentPlayer = entities[currentIndex];
            }
            target = null;
        }
        public void AddPlayer(Entity e)
        {
            if (e.onDeath == null)
            {
                e.onDeath = OnPlayerDeath;

                entities.Add(e);
                SortEntities();
                return;
            }
            e.onDeath += OnPlayerDeath;
            entities.Add(e);
            SortEntities();
        }
        public void GenerateNewEnemy(int level)
        {
            Random r = new Random();
            Enemy e = new Enemy();
            Enemy ne = new Enemy(currentPlayer.Name + " Level:" + level, (float)Math.Pow((double)e.Health, r.NextDouble() * (1.3d - 1d) + 1d),
                   level,
                  (float)Math.Pow((double)e.Damage,
                  r.NextDouble() * (1.3d - 1d) + 1d),
                  (float)Math.Pow(e.Speed, r.NextDouble() * (1.3d - 1d) + 1d));
            entities.Add(ne);
            SortEntities();
        }

        private void OnPlayerDeath()
        {
            if (typeof(Player).ToString() == currentPlayer.ToString())
            {
                entities.Remove(currentPlayer);
                return;
            }

            int tmplv = (currentPlayer as Enemy).Level + 1;
            entities.Remove(currentPlayer);
            GenerateNewEnemy(tmplv);
        }
        public void Start()
        {
            currentIndex = 0;
            currentPlayer = entities[currentIndex];

        }
        public void SortEntities()
        {
            entities.Sort((x, y) => -1 * x.Speed.CompareTo(y.Speed));
        }

        public void Update()
        {
            if (typeof(Player).ToString() == currentPlayer.ToString())
            {
                if (currentPlayer.CurrentState().ToString() == "ATTACK")
                    currentPlayer.DealDamage(target, currentPlayer.Damage);
               
            }
            else if (typeof(Enemy).ToString() == currentPlayer.ToString())
            {
                GetTarget(new object(), new EventArgs());
            }
            NextPlayer();
            currentPlayer.TurnsTaken++;
        }

        public List<Control> CreateControls()
        {
            int i = 1;
            int j = 1;
            List<Control> tmp = new List<Control>();
            foreach (Entity e in entities)
            {

                currentPlayer.PlayerButton = new Button();
                if (typeof(Player).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.PlayerButton.Location = new System.Drawing.Point(110, 150 * i);
                    currentPlayer.PlayerButton.Text = currentPlayer.Name;

                    i++;
                }

                if (typeof(Enemy).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.PlayerButton.Location = new System.Drawing.Point(690, 150 * j);
                    currentPlayer.PlayerButton.Text = currentPlayer.Name;

                    j++;
                }
                currentPlayer.PlayerButton.Size = new System.Drawing.Size(150, 50);
                currentPlayer.PlayerButton.Click += GetTarget;

                tmp.Add(currentPlayer.PlayerButton);
                NextPlayer();
            }
            i = 1;
            j = 1;
            foreach (Entity e in entities)
            {

                currentPlayer.HealthBar = new ProgressBar();
                if (typeof(Player).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.HealthBar.Location = new System.Drawing.Point(135, 200 * i);
                    (currentPlayer.HealthBar as ProgressBar).Value = (int)((currentPlayer.Health/currentPlayer.MaxHealth)*100f);

                    i++;
                }

                if (typeof(Enemy).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.HealthBar.Location = new System.Drawing.Point(715, 200 * j);
                    (currentPlayer.HealthBar as ProgressBar).Value = (int)((currentPlayer.Health / currentPlayer.MaxHealth) * 100f);


                    j++;
                }
                currentPlayer.HealthBar.Size = new System.Drawing.Size(100, 25);
              

                tmp.Add(currentPlayer.HealthBar);
                NextPlayer();
            }
            return tmp;
        }
        private void GetTarget(object sender, EventArgs e)
        {
            target = currentPlayer;
            if (typeof(Player).ToString() == currentPlayer.ToString())
            {
                foreach (Entity E in entities)
                {
                    if (E.Name == (sender as Control).Text && !(currentPlayer.ToString() == E.ToString()))
                    {
                        target = E;

                        return;
                    }
                }
            }
            else if(typeof(Enemy).ToString() == currentPlayer.ToString())
            {
                Random r = new Random();
                while(target.ToString()!=typeof(Player).ToString())
                {
                    target = entities[r.Next(0, entities.Count)];

                }
            }
        }
        public Entity target;
        public List<Entity> entities;
        public Entity currentPlayer;
        private int currentIndex;

    }
}