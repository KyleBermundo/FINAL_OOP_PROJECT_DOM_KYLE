using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class ASleepState : IState
    {
        internal static IState GetInstance()
        {
            throw new NotImplementedException();
        }

        public void ExtraDamage(Pokemon pokemon)
        {
            
        }

        public int GetThreshold()
        {
            return 25; 
        }


        public override string ToString()
        {
            return "SLEEP";
        }
    }
}
