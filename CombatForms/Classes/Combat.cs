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
                    return;
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
        }

        private void OnPlayerDeath()
        {
            if (typeof(Player).ToString() == currentPlayer.ToString())
            {
                currentPlayer.onDeath.Invoke();
                return;
            }
            int tmplv = (currentPlayer as Enemy).Level + 1;
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
            if (currentPlayer.CurrentState().ToString() == "ATTACK")
                currentPlayer.DealDamage(target, currentPlayer.Damage);
            NextPlayer();
            currentPlayer.TurnsTaken++;
        }
        public List<Control> CreateButtons()
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
            return tmp;
        }
        private void GetTarget(object sender, EventArgs e)
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
        public Entity target;
        public List<Entity> entities;
        public Entity currentPlayer;
        private int currentIndex;

    }
}