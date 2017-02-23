using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Classes
{
    public class State
    {
        public State()
        {
        }
        public State(Enum e)
        {
            onEnd = null;
            onStart = null;
            onUpdate = null;
            Name = e.ToString();
        }
        public void AddEnterFunction(Delegate os)
        {
            Action h = os as Action;
            onStart += h;
        }
        public void AddExitFunction(Delegate oe)
        {
            onEnd += oe as Action;
        }
        public void AddActiveFunction(Delegate wa)
        {
            onUpdate += wa as Action;
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
            return Name;
        }
        [System.Xml.Serialization.XmlIgnore]
        public Action onUpdate;
        
        
        [System.Xml.Serialization.XmlIgnore]
        public Action onStart;
        [System.Xml.Serialization.XmlIgnore]
        public Action onEnd;
        public string Name { get; private set;  }
    }
    
}
