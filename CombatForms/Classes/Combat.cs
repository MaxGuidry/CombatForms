using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
           

        }
        public void AddPlayer( Entity e)
        {
            e.onDeath += OnPlayerDeath;
            entities.Add(e);

        }
        public void GenerateNewEnemy(int level)
        {
            Random r = new Random();

            Enemy e = new Enemy();
            Enemy ne = new Enemy((float)Math.Pow((double)e.Health, r.NextDouble() * (1.3d - 1d) + 1d),
                   level,
                  (float)Math.Pow((double)e.Damage,
                  r.NextDouble() * (1.3d - 1d) + 1d),
                  (float)Math.Pow(e.Speed, r.NextDouble() * (1.3d - 1d) + 1d));
            entities.Add(ne);
        }

        private void OnPlayerDeath()
        {
           
                
            if(typeof(Player).ToString()==currentPlayer.ToString())
            {
                currentPlayer.onDeath.Invoke();
                return;
            }

            int tmplv = (currentPlayer as Enemy).Level + 1;
            GenerateNewEnemy(tmplv);
        }
        public void Start()
        {
            
            
        }
        public void Update()
        {
            if (currentPlayer.CurrentState().ToString() == "ATTACK")
                currentPlayer.DealDamage(target, currentPlayer.Damage);
        }
        public Entity target;
        public List<Entity> entities;
        public Entity currentPlayer;


    }
}