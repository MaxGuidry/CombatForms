using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Classes
{
    class State
    {
        public State(Enum e)
        {
            m_Name =e.ToString();
        }
        private string m_Name;
        public string Name { get { return m_Name; } }
    }
}
