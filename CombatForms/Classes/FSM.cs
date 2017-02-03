using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CombatForms.Iterfaces;
namespace CombatForms.Classes
{
    
    public class FSM<T>
    {
        public FSM()
        {
            states = new Dictionary<string, State>();
            transitions = new Dictionary<string, List<State>>();
            var vals = Enum.GetValues(typeof(T));
            foreach (var v in vals)
            {
                State s = new State(v as Enum);
                states.Add(s.Name, s);
            }
        }
        public void AddState(Enum e)
        {
            State s = new State(e);
            states.Add(s.Name, s);
        }
        public void AddTransition<V>(V from, V to)
        {
            State From;
            State To;
            if (typeof(V) == typeof(State))
            {
                From = from as State;
                To = from as State;

            }
            else if (typeof(V) == typeof(T))
            {
                From = new State(from as Enum);
                To = new State(to as Enum);

            }
            else
            {
                throw new Exception("Connot convert type:" + typeof(V) + " to type State or Enum");
            }
            List<State> tmp = new List<State>();
            tmp.Add(From);
            tmp.Add(To);
            transitions.Add(From.Name + "->" + To.Name, tmp);
        }
        public void Start<V>(V state)
        {
            if (typeof(V) == typeof(State))
            {
                if (states.ContainsKey((state as State).Name))
                    currentState = state as State;
            }
            else if (typeof(V) == typeof(T))
            {
                State s = new State(state as Enum);
                currentState = s;
            }
        }
        public bool ChangeState<V>(V s)
        {
            if (typeof(V) == typeof(State))
            {
                if (transitions.ContainsKey(currentState.Name + "->" + (s as State).Name))
                {
                    if (currentState.onEnd != null)
                    {
                        currentState.onEnd.Invoke();
                    }

                    currentState = s as State;
                    if (currentState.onEnd != null)
                    {
                        currentState.onStart.Invoke();
                    }
                    return true;
                }

            }
            if (typeof(V) == typeof(T))
            {
                State tmp = new State(s as Enum);
                if (transitions.ContainsKey(currentState.Name + "->" + tmp.Name))
                {
                    if (currentState.onEnd != null)
                    {
                        currentState.onEnd.Invoke();
                    }

                    currentState = states[(s as Enum).ToString()];
                    if (currentState.onEnd != null)
                    {
                        currentState.onStart.Invoke();
                    }
                    return true;
                }
            }
            if(typeof(V)==typeof(object))
            {
                State tmp = new State(s as Enum);
                if (transitions.ContainsKey(currentState.Name + "->" + tmp.Name))
                {
                    if (currentState.onEnd != null)
                    {
                        currentState.onEnd.Invoke();
                    }

                    currentState = states[(s as Enum).ToString()];
                    if (currentState.onEnd != null)
                    {
                        currentState.onStart.Invoke();
                    }
                    return true;
                }
            }
            return false;
        }
        public void Start()
        {
            if (states.ContainsKey("INIT"))
                currentState = states["INIT"];
            else
            {

                currentState = states.ElementAt(0).Value;

            }
        }
        /// <summary>
        /// Returns the state given if it exists.
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public State GetState<V>(V s)
        {
            if (typeof(V) == typeof(T))
            {
                if (states.ContainsKey((s as Enum).ToString()))
                    return states[(s as Enum).ToString()];
            }
            if (typeof(V) == typeof(State))
            {
                if (states.ContainsKey((s as State).Name))
                    return states[(s as State).Name];
            }
            throw new Exception("Couldn't get the state because it either does not exist, or argument passed in was not correct.");
        }
        /// <summary>
        /// Returns the current state if not given a state to get.
        /// </summary>
        /// <returns></returns>
        public State GetState()
        {
            return currentState;
        }
        public void Update()
        {

        }

      

       
        private Dictionary<string, List<State>> transitions;
     
        private State currentState;
        
        private Dictionary<string, State> states;
    }
}
