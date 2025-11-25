using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class ParalyzedState : IState
    {
        internal static IState GetInstance()
        {
            throw new NotImplementedException();
        }

        public void ExtraDamage(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public int GetThreshold()
        {
            return 12;
        }


        public override string ToString()
        {
            return "PARALYZED";
        }
    }
}
