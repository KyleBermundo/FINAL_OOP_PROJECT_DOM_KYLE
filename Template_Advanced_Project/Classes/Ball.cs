using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_Advanced_Project.Classes.States;

namespace Template_Advanced_Project.Classes
{
    public class Ball : Item
    {

        private double rate;
        private int ball_Value;
        private int max_RND;
        private bool isMaster;
            

        public double Rate { get => rate; set => rate = value; }
        public int Ball_Value { get => ball_Value; set => ball_Value = value; }
        public int Max_RND { get => max_RND; set => max_RND = value; }
        public bool IsMaster { get => isMaster; set => isMaster = value; }

        public Ball(string name, int price, double rate, int ball_val, int max_RND, bool isMaster) : base(name, price)
        {
            this.rate = rate;
            this.ball_Value = ball_val;
            this.max_RND = max_RND;
            this.isMaster = isMaster;
        }

        public bool Catch(Pokemon pokemon)
        {
            if (pokemon == null) return false;

            Random rnd = RNG.GetInstance();

            // Step 1: Master Ball
            if (IsMaster)
                return true;

            // Step 2: N between 0 and Max_RND
            int N = rnd.Next(0, Max_RND + 1);

            // Step 3: Compare with State-based threshold bonus
            int threshold = pokemon.state.GetThreshold();

            if (N < threshold)
                return true;

            // Step 4: Check catch rate
            if (N - threshold > pokemon.Catch_Rate)
                return false;

            // Step 5: Gen-1 formula
            int M = rnd.Next(0, 256);

            int hp;

            if (pokemon.Hp <= 0)
                hp = 1;
            else
                hp = pokemon.Hp;

            int F = (pokemon.Hp_MAX * 255 * 4) / (hp * ball_Value);

            if (F < 1) F = 1;
            if (F > 255) F = 255;

            return F >= M;
        }

    }
}