using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes
{
    public class Trainer
    {
        //player profile
        //Fields of question 6
        public List<Pokemon> pokemonsCollection;
        public List<Potion> potionsCollection;
        public List<Ball> ballsCollection;

        public Pokemon target = null;
        public Pokemon currentFighter = null;

        public string name; //Player
        public int GP; //GoldPiece or currency

        //Constructor of question 6
        public Trainer(string name = "Player",  int GP = 0)
        {
            this.name = name;
            this.GP = GP;
            //creates new list
            pokemonsCollection = new List<Pokemon>();
            potionsCollection = new List<Potion>();
            ballsCollection = new List<Ball>();
           
            //creates the pokemon on start for usage in his party or collection
            Pokemon partner = GameFactory.CreatePartnerPikachu();
            pokemonsCollection.Add(partner);

            //creates new pokeballs ready to use in bag
            ballsCollection.Add(GameFactory.CreatePokeBall());
            ballsCollection.Add(GameFactory.CreatePokeBall());
            ballsCollection.Add(GameFactory.CreatePokeBall());

            ballsCollection.Add(GameFactory.CreateGreatBall());

            ballsCollection.Add(GameFactory.CreateUltraBall());

            ballsCollection.Add(GameFactory.CreateMasterBall());
        }
    }
}
