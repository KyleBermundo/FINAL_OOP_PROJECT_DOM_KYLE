using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Template_Advanced_Project.Classes
{
    public static class Player
    {
        public static Trainer trainer = new Trainer();

        public static void AddPokemon(Pokemon pokemon)
        { 
            trainer.pokemonsCollection.Add(pokemon);
        }

        public static void Reward_GP(int xp)
        {
            trainer.GP += xp;
        }

        public static void BuyItem(Item item)
        {
            if (trainer.GP < item.Price)
            {
                "Not enough GP to buy this item.".PrintDanger();
                return;
            }

            trainer.GP -= item.Price;

            if (item is Potion potion)
            {
                trainer.potionsCollection.Add(potion);
            }
            else if (item is Ball ball)
            {
                trainer.ballsCollection.Add(ball);
            }

        }

        public static void Pokemon_Nursery()
        {
            foreach (Pokemon p in trainer.pokemonsCollection)
            {
                p.Full_Restore();
            }
        }

    }
}
