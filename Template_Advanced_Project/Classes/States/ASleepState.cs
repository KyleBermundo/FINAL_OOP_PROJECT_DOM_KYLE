using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes.States
{
    public class ASleepState : IState
    {
        public static ASleepState instance = new ASleepState();
        private ASleepState() { }

        public static ASleepState GetInstance()
        {
            return instance;
        }

        public void ExtraDamage(Pokemon pokemon)
        {
            // doesnt do damage because asleep state
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
