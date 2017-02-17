using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Classes
{
    public class State
    {
        public State(Enum e)
        {
            onEnd = null;
            onStart = null;
            onUpdate = null;
            m_Name = e.ToString();
        }
        public void AddEnterFunction(Delegate os)
        {
            onStart += os as Handler;
        }
        public void AddExitFunction(Delegate oe)
        {
            onEnd += oe as Handler;
        }
        public void AddActiveFunction(Delegate wa)
        {
            onUpdate += wa as Handler;
        }
        static public bool operator ==(State current, State other)
        {
            if(current.Name == other.Name && current.onEnd == other.onEnd && current.onStart == other.onStart && current.onUpdate == other.onUpdate)
                return true;
            return false;
        }
        static public bool operator !=(State current, State other)
        {
            if(current.Name == other.Name && current.onEnd == other.onEnd && current.onStart == other.onStart && current.onUpdate == other.onUpdate)
                return false;
            return true;

        }
        public override string ToString()
        {
            return m_Name;
        }
        public Handler onUpdate;
        public delegate void Handler();
        public Handler onStart;
        public Handler onEnd;
        private string m_Name;
        public string Name { get { return m_Name; } }
    }
}
