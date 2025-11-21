using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class ParalyzedState : IState
    {
        public void ExtraDamage(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public int GetThreshold()
        {
            throw new NotImplementedException();
        }

        //TODO ...

        public override string ToString()
        {
            return "PARALYZED";
        }
    }
}
