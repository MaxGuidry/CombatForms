﻿using System;
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
        public Form1 a;

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
                currentPlayer.ChangePlayerState("WAIT");
            }

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

        public void GenerateNewEnemy(Enemy dead)
        {
            Random r = new Random();
            Enemy e = new Enemy();
            Enemy ne = new Enemy(target.Name, (float)Math.Pow((double)e.Health, r.NextDouble() * (1.3d - 1d) + 1d),
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
            entities.Add(ne);
            SortEntities();
        }

        private void OnPlayerDeath()
        {
            if (typeof(Player).ToString() == target.ToString())
            {
                entities.Remove(target);
                return;
            }

            GenerateNewEnemy(target as Enemy);
            a.Controls.Remove(target.Info);
            a.Controls.Remove(target.HealthBar);
            entities.Remove(target);
            SortEntities();
        }

        public void Start()
        {
            currentIndex = 0;
            currentPlayer = entities[currentIndex];

        }

        public void SortEntities()
        {
            entities.Sort((x, y) => -1 * x.Speed.CompareTo(y.Speed));
            currentIndex = entities.IndexOf(currentPlayer);
        }

        public void Update()
        {
            if (typeof(Player).ToString() == currentPlayer.ToString())
            {
                if (currentPlayer.CurrentState().ToString() == "WAIT")
                    return;
                if (currentPlayer.CurrentState().ToString() == "ATTACK" && target != null)
                    currentPlayer.DealDamage(target, currentPlayer.Damage);
               
            }
            else if (typeof(Enemy).ToString() == currentPlayer.ToString())
            {
                GetTarget(new object(), new EventArgs());
                currentPlayer.DealDamage(target, currentPlayer.Damage);

            }
            if (a.Enabled == true)
                NextPlayer();
            target = null;
            UpdateUI();
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

                    currentPlayer.HealthBar.Location = new System.Drawing.Point(135, currentPlayer.PlayerButton.Location.Y + currentPlayer.PlayerButton.Size.Height);
                    (currentPlayer.HealthBar as ProgressBar).Value = (int)((currentPlayer.Health / currentPlayer.MaxHealth) * 100f);

                    i++;
                }

                if (typeof(Enemy).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.HealthBar.Location = new System.Drawing.Point(715, currentPlayer.PlayerButton.Location.Y + currentPlayer.PlayerButton.Size.Height);
                    (currentPlayer.HealthBar as ProgressBar).Value = (int)((currentPlayer.Health / currentPlayer.MaxHealth) * 100f);
                    j++;
                }
                currentPlayer.HealthBar.Size = new System.Drawing.Size(100, 25);


                tmp.Add(currentPlayer.HealthBar);
                NextPlayer();
            }
            i = 1;
            j = 1;
            foreach (Entity e in entities)
            {

                currentPlayer.Info = new RichTextBox();
                if (typeof(Player).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.Info.Location = new System.Drawing.Point(135, currentPlayer.HealthBar.Location.Y + currentPlayer.HealthBar.Size.Height);
                    (currentPlayer.Info as RichTextBox).Text = "Health: " + currentPlayer.Health + "\nDamage: " + currentPlayer.Damage +
                        "\nSpeed: " + currentPlayer.Speed + "\nArmor: " + currentPlayer.Armor + "\nLevel: " + currentPlayer.Level;

                    i++;
                }

                if (typeof(Enemy).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.Info.Location = new System.Drawing.Point(715, currentPlayer.HealthBar.Location.Y + currentPlayer.HealthBar.Size.Height);
                    (currentPlayer.Info as RichTextBox).Text = "Health: " + currentPlayer.Health + "\nDamage: " + currentPlayer.Damage +
                         "\nSpeed: " + currentPlayer.Speed + "\nArmor: " + currentPlayer.Armor+"\nLevel: " + currentPlayer.Level;


                    j++;
                }
                currentPlayer.Info.Size = new System.Drawing.Size(100, 100);


                tmp.Add(currentPlayer.Info);
                NextPlayer();
            }
            i = 1;
            j = 1;
            foreach (Entity e in entities)
            {

                currentPlayer.StateBox = new RichTextBox();
                if (typeof(Player).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.StateBox.Location = new System.Drawing.Point(265, currentPlayer.PlayerButton.Location.Y);
                    currentPlayer.StateBox.Size = new System.Drawing.Size(150, 30);
                    (currentPlayer.StateBox as RichTextBox).Text = "Current State: "+ currentPlayer.CurrentState().ToString();
                    i++;
                }

                if (typeof(Enemy).ToString() == currentPlayer.ToString())
                {

                    currentPlayer.StateBox.Location = new System.Drawing.Point(845, currentPlayer.PlayerButton.Location.Y);
                    currentPlayer.StateBox.Size = new System.Drawing.Size(150, 30);
                    (currentPlayer.StateBox as RichTextBox).Text = "Current State: " + currentPlayer.CurrentState().ToString();


                    j++;
                }
                currentPlayer.Info.Size = new System.Drawing.Size(100, 100);


                tmp.Add(currentPlayer.StateBox);
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
            else if (typeof(Enemy).ToString() == currentPlayer.ToString())
            {
                Random r = new Random();
                while (target.ToString() != typeof(Player).ToString())
                {
                    target = entities[r.Next(0, entities.Count)];

                }
            }

        }

        public void UpdateUI()
        {

            foreach (Entity e in entities)
            {

                if (typeof(Player).ToString() == e.ToString())
                {
                    if (e.Info == null)
                    {
                        e.Info = new RichTextBox();
                        
                        e.Info.Location = new System.Drawing.Point(135, e.HealthBar.Location.Y + e.HealthBar.Size.Height);
                    }
                    (e.Info).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    e.StateBox.Text = "Current State: " + e.CurrentState().ToString();

                }

                if (typeof(Enemy).ToString() == e.ToString())
                {

                    if (!a.Controls.Contains(e.Info))
                    {
                        e.StateBox = new RichTextBox();
                        e.StateBox.Location = new System.Drawing.Point(845, currentPlayer.PlayerButton.Location.Y);
                        e.StateBox.Size = new System.Drawing.Size(150, 30);
                        e.Info.Location = new System.Drawing.Point(715, e.HealthBar.Location.Y + e.HealthBar.Size.Height);

                        a.Controls.Add(e.Info);
                        a.Controls.Add(e.StateBox);
                        a.Controls.Add(e.HealthBar);
                    }

                    (e.Info).Text = (e.Info as RichTextBox).Text = "Health: " + e.Health + "\nDamage: " + e.Damage +
                        "\nSpeed: " + e.Speed + "\nArmor: " + e.Armor + "\nLevel: " + e.Level;
                    e.StateBox.Text = "Current State: " + e.CurrentState().ToString();
                }
            }
        }


        public Entity target;
        public List<Entity> entities;
        public Entity currentPlayer;
        private int currentIndex;

    }
}