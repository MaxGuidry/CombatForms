using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatForms.Iterfaces
{
    interface IDamager
    {
        void DealDamage(IDamagable target,float Amount);
    }
}
