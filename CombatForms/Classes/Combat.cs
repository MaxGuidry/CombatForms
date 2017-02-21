﻿using System;
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

        public void OnLoad()
        {
            foreach (Entity e in Entities)
            {
                e.onDeath += OnPlayerDeath;
            }
        }
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
            CurrentPlayer.TurnsTaken++;
            if (CurrentPlayer.NumberOfTurns <= CurrentPlayer.TurnsTaken)
            {
                CurrentPlayer.TurnsTaken = 0;
                CurrentPlayer.TurnTaken = true;
                while (CurrentPlayer.TurnTaken == true && currentIndex < Entities.Count)
                    currentIndex++;
                if (currentIndex+1 > Entities.Count - 1)
                {

                    currentIndex = 0;
                    foreach (Entity e in Entities)
                        e.TurnTaken = false;
                }
            }
        }

        public void AddPlayer(Entity e)
        {
            if (e.onDeath == null)
            {
                e.onDeath = OnPlayerDeath;
                Entities.Add(e);
                SortEntities();
                if (Entities.IndexOf(e) < Entities.IndexOf(CurrentPlayer))
                    currentIndex++;
                return;
            }
            e.onDeath += OnPlayerDeath;
            Entities.Add(e);

            SortEntities();
            if (Entities.IndexOf(e) < Entities.IndexOf(CurrentPlayer))
                currentIndex++;
        }

        public string CurrentInfoLog = "";
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
            AddPlayer(ne);
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
                {
                    Combat.Instance.CurrentPlayer.DealDamage(Combat.Instance.Target, Combat.Instance.CurrentPlayer.Damage);
                    if (Target.CurrentState.ToString() == "DEFEND")
                    {
                        CombatLog = Target.Name + " defended for " + Target.Armor + " damage \n";
                        CombatLog = CurrentPlayer.Name + " did " + CurrentPlayer.Damage + " damage to " + Target.Name;
                    }

                    CombatLog = CurrentPlayer.Name + " did " + CurrentPlayer.Damage + " damage to " + Target.Name + "\n";
                }
            }
            else if (typeof(Enemy).ToString() == Combat.Instance.CurrentPlayer.ToString())
            {
                getTarget(new object(), new EventArgs());
                Combat.Instance.CurrentPlayer.DealDamage(Combat.Instance.Target, Combat.Instance.CurrentPlayer.Damage);

                if (Target.CurrentState.ToString() == "DEFEND")
                {
                    CombatLog = Target.Name + " defended for " + Target.Armor + " damage \n";
                    CombatLog += CurrentPlayer.Name + " did " + CurrentPlayer.Damage + " damage to " + Target.Name;
                }

                else
                    CombatLog = CurrentPlayer.Name + " did " + CurrentPlayer.Damage + " damage to " + Target.Name + "\n";
            }

            if (controller.GetState().ToString() == "FIGHTING")
                Combat.Instance.NextPlayer();


            Combat.Instance.Target = null;

        }


        private void OnPlayerDeath()
        {
            if (Entities.IndexOf(Target) < Entities.IndexOf(CurrentPlayer))
                currentIndex--;
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


        }

        public void SortEntities()
        {
            Entities.Sort((x, y) => -1 * x.Speed.CompareTo(y.Speed));

        }

        private enum CombatState
        {
            FIGHTING,
            LEVELING,
        }


        private FSM<CombatState> controller;
        public Entity Target;
        public List<Entity> Entities;
        public Entity CurrentPlayer
        {
            get
            {
                return Entities[currentIndex];
            }
        }
        public int currentIndex;

    }
}