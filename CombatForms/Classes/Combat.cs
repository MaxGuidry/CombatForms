using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CombatForms.Classes
{
    [Serializable]
    public class Combat
    {


        private static Combat instance;

        private Combat()
        {
            Entities = new List<Entity>();
            controller = new FSM<CombatState>();
            controller.AddTransition(CombatState.FIGHTING, CombatState.LEVELING);
            controller.AddTransition(CombatState.LEVELING, CombatState.FIGHTING);
            controller.Start("FIGHTING");
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



        public delegate void GenerateEnemy(Enemy e);
        [XmlIgnore]
        public GenerateEnemy OnEnemyGeneration { get; set; }

        public void GenerateNewEnemy(Enemy dead)
        {
            Entities.Remove(Target);
            Random r = new Random();
            Enemy e = new Enemy();
            Enemy ne = new Enemy(Target.Name, (float)Math.Pow((double)e.Health, r.NextDouble() * (1.3d - 1d) + 1d),
                   dead.Level + 1,
                  (float)Math.Pow((double)e.Damage,
                  r.NextDouble() * (1.3d - 1d) + 1d),
                  (float)Math.Pow(e.Speed, r.NextDouble() * (1.3d - 1d) + 1d));
            Target = ne;
            Combat.Instance.Target.onDeath = Combat.Instance.OnPlayerDeath;
            OnEnemyGeneration(dead);
            Entities.Add(ne);
            SortEntities();
        }
        public delegate void Targeting(Object obj, EventArgs evt);
        [XmlIgnore]
        public Targeting getTarget;

        public void UpdateCombat()
        {
            if (typeof(Player).ToString() == Combat.Instance.CurrentPlayer.ToString())
            {
                if (Combat.Instance.CurrentPlayer.CurrentState.ToString() == "WAIT")
                    return;
                if (Combat.Instance.CurrentPlayer.CurrentState.ToString() == "ATTACK" && Combat.Instance.Target != null)
                    Combat.Instance.CurrentPlayer.DealDamage(Combat.Instance.Target, Combat.Instance.CurrentPlayer.Damage);

            }
            else if (typeof(Enemy).ToString() == Combat.Instance.CurrentPlayer.ToString())
            {
                getTarget(new object(), new EventArgs());
                Combat.Instance.CurrentPlayer.DealDamage(Combat.Instance.Target, Combat.Instance.CurrentPlayer.Damage);

            }

            if(controller.GetState().ToString()=="FIGHTING")
                Combat.Instance.NextPlayer();


            Combat.Instance.Target = null;

        }
        [XmlIgnore]
        public Action OnDeath { get; set; }
        private void OnPlayerDeath()
        {
            if (typeof(Player).ToString() == Target.ToString())
            {
                Entities.Remove(Target);
                return;
            }
            
            GenerateNewEnemy(Target as Enemy);
            

            SortEntities();
        }
        public void ChangeCombatState(string state)
        {
            foreach (CombatState s in Enum.GetValues(typeof(CombatState)))
            {
                if (state == s.ToString())
                {
                    controller.ChangeState(s);
                    break;
                }
            }
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

        private enum CombatState
        {
            FIGHTING,
            LEVELING,
        }


        private FSM<CombatState> controller;
        public Entity Target;
        public List<Entity> Entities;
        public Entity CurrentPlayer;
        private int currentIndex;

    }
}