using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CombatForms.Iterfaces;
using System.Xml.Serialization;

namespace CombatForms.Classes
{
    [Serializable]
    public class Player : Entity
    {

        public Player()
        {
            new Entity();
            
            m_ExpToNextLevel = 100f;
            Alive = true;
           
            Level = 1;
            m_Type = EntityType.PLAYER;

        }

        public Player(string name,float health, int level, float baseDamage, float speed)
        {

            m_ExpToNextLevel = 100f;
            Name = name;
            Alive = true;
            Level = level;
            Health = health;
            MaxHealth = health;
            Damage = baseDamage;
            Speed = speed;
            m_Type = EntityType.PLAYER;

        }
        public override void DealDamage(IDamagable target, float Amount)
        {
            target.TakeDamage(Amount);
            if (!(target as Entity).Alive)
                this.GainEXP(target);
            ChangePlayerState("WAIT");
        }

        public override void TakeDamage(float Amount)
        {
            if (CurrentState.ToString() == "DEFEND")
                Amount -= Armor;
            if (Health - Amount < 0)
            {
                Health = 0;

                this.DeathUpdate();
                this.onDeath.Invoke();
                
                return;
            }
            Health -= Amount;
            
        }
        [XmlIgnore]
        public System.Action OnLevelUp;
        [XmlIgnore]
        public System.Action DeathUpdate;
        private void LevelUp()
        {
            Level++;
            m_ExpToNextLevel = (5f * (float)Math.Pow((double)Level, 2d)) + 95f;
            StatBuff t = new StatBuff();
            t.Visible = true;
            t.Activate();
            OnLevelUp.Invoke();
            Health =MaxHealth;
        }
          
        public void GainEXP(IDamagable target)
        {
            EXP += (float)(8f * (float)(Math.Pow((double)(target as Enemy).Level, 1.5d)) + 50f);
            if (EXP > m_ExpToNextLevel)
            {
                EXP -= m_ExpToNextLevel;
                LevelUp();

            }
        }
        
        private float m_ExpToNextLevel;
        public float EXP { get;  set ;  }
       
       


    }
}
