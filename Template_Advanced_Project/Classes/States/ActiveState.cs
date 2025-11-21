    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class ActiveState : IState
    {
        public void ExtraDamage(Pokemon pokemon)
        {
            
        }

        public int GetThreshold()
        {
            return 0; 
        }

        //TODO ...

        public override string ToString()
        {
            return "ACTIVE";
        }
    }
}
