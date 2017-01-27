using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Classes
{
    class FSM<T>
    {
        public FSM()
        {

        }
        public void AddState(Enum e)
        {
            State s = new State(e);
            states.Add(s.Name, s);
        }
        public void Start(T state)
        {
            if (typeof(T) == typeof(State))
            {
                if (states.ContainsKey((state as State).Name))
                    currentState = state as State;
            }
            else if (typeof(T) == typeof(Enum))
            {
                State s = new State(state as Enum);
                currentState = s;
            }
        }
        private State currentState;
        private Dictionary<string, State> states;
    }
}
