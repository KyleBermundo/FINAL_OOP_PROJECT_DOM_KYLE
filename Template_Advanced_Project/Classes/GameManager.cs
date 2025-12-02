using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_Advanced_Project.Classes
{
    public static class GameManager
    {
        //Q7 Explore
        public static void Explore()
        {
            Console.Clear();
            "Exploring the world...".PrintBlue();

            Pokemon wild = GameFactory.Get_Random_Pokemon();
            Player.trainer.target = wild;

            Console.WriteLine($"\nA wild {wild.Name} appeard!");
            Console.WriteLine($"HP: {wild.Hp} PP: {wild.MyMove.PP} Status: {wild}");

            if (Player.trainer.currentFighter == null && Player.trainer.pokemonsCollection.Count > 0)
            {
                Player.trainer.currentFighter = Player.trainer.pokemonsCollection[0];
            }

            while (true)
            {
                //menu for explore
                Console.WriteLine("\nExplore Options:");
                Console.WriteLine("B: Battle | C: Catch | R: Run Away");
                Console.WriteLine("Choice:");
                char choice = Char.ToUpper(Console.ReadKey(true).KeyChar);

                if (choice == 'B')
                {
                    Battle();

                    if (wild.Is_Fainted())
                    {
                        Console.WriteLine($"\nThe wild {wild.Name} is already fainted. The battle is over.");
                        Player.trainer.target = null;
                        return;
                    }

                    if (Player.trainer.currentFighter == null ||
                        Player.trainer.target == null ||
                            Player.trainer.target.Is_Fainted())
                    {
                        Console.WriteLine("\nThe encounter is over");
                        break;
                    }
                }
                else if (choice == 'C')
                {
                    bool caught = CatchMenu(wild);
                    if (caught)
                    {
                        Player.AddPokemon(wild);
                        Console.WriteLine($"\nYou caught {wild.Name}!");
                        Player.trainer.target = null;
                    }
                    else
                    {
                       Console.WriteLine($"\n{wild.Name} broke free!");
                    }
                    break;
                }
                else if (choice == 'R')
                {
                    Console.WriteLine("\nYou ran away safely");
                    break;
                }
            }
            Console.WriteLine("\nPress any key to return to Main Menu...");
            Console.ReadKey(true);
        }

        //Q7 Battle
        public static void Battle()
        {
            Console.Clear();

            Pokemon wild = Player.trainer.target;
            Pokemon fighter = Player.trainer.currentFighter;

            if (wild == null)
            {
                "no wild pokemon to battle".PrintWarning();
                return;
            }
            if (fighter == null)
            {
                "You have no pokemon to fight with!".PrintWarning();
                return;
            }
            if (wild.Is_Fainted() ) 
            {
                Console.WriteLine($"\nThe wild {wild.Name} is already fainted. The battle is over,");
                Player.trainer.target = null;
                return;
            }
            Console.WriteLine($"Battle Started! {fighter.Name} VS {wild.Name}");

            while (true)
            {
                //display for battle
                Console.WriteLine("\n------------------------------");
                Console.WriteLine($"Your Pokemon: {fighter.Name} Hp: {fighter.Hp} PP: {fighter.MyMove.PP} Status: {fighter}");
                Console.WriteLine($"Wild Pokemon: {wild.Name} Hp: {wild.Hp} PP: {wild.MyMove.PP} Status: {wild}");
                Console.WriteLine("\n------------------------------");

                //Battle Menu
                Console.WriteLine("\nBattle Menu: A: Attack | B: Bag | R: Run Away | S: Switch Pokemon");
                Console.WriteLine("Choice: ");
                char choice = char.ToUpper(Console.ReadKey(true).KeyChar);

                if (choice == 'A')
                {
                    AttackType atkType = ChooseAttackType();
                    fighter.Attack(wild, atkType);

                    if (wild.Is_Fainted())
                    {
                        Console.WriteLine($"\n{wild.Name} fainted!");
                        Player.Reward_GP(wild.Base_Exp);
                        Console.WriteLine($"You gained {wild.Base_Exp} GP!");
                        Player.trainer.target = null;
                        break;
                    }

                    AttackType wildAtk = (AttackType)RNG.GetInstance().Next(0, 3);
                    wild.Attack(fighter, wildAtk);

                    if (fighter.Is_Fainted())
                    {
                        Console.WriteLine($"\nYour {fighter.Name} fainted!");
                        break;
                    }

                }
                else if (choice == 'B')
                {
                    BagMenu();
                    fighter = Player.trainer.currentFighter;

                    if (Player.trainer.target == null)
                    {
                        Console.WriteLine("\nBattle ended - the wild pokemon was caught");
                        break;
                    }
                }
                else if (choice == 'S')
                {
                    SwitchPokemonMenu();
                    fighter = Player.trainer.currentFighter;
                    if(fighter == null)
                    {
                        "You have no pokemon left to fight".PrintDanger();
                        break;
                    }
                }
                else if (choice == 'R')
                {
                    Console.WriteLine("\nYou ran away form the battle!");
                    break;
                }
            }
        }
        //Attack type choices
        private static AttackType ChooseAttackType()
        {
            Console.WriteLine("\nChoose Atttack Type:");
            Console.WriteLine("1: PHYSICAL |  2: SPECIAL | 3: STATUS");
            Console.WriteLine("Choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": return AttackType.PHYSICAL;
                case "2": return AttackType.SPECIAL;
                case "3": return AttackType.STATUS;
                default: return AttackType.PHYSICAL;
            }
        }

        //Catch Menu
        private static bool CatchMenu(Pokemon wild)
        {
            var balls = Player.trainer.ballsCollection;

            if (balls.Count == 0)
            {
                "You have no balls!".PrintDanger();
                return false;
            }

            Console.WriteLine("\nCatch Menu - Choose a ball:");
            for (int i = 0; i < balls.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {balls[i].Name}");
            }

            Console.WriteLine("Ball #:");
            if(!int.TryParse(Console.ReadLine(), out int pick) ||
                pick < 1 || pick > balls.Count)
            {
                "invalid ball choice.".PrintWarning();
                return false;
            }

            Ball chosen = balls[pick - 1];
            balls.RemoveAt(pick - 1);

            return chosen.Catch(wild);
        }

        //Bag Menu
        private static void BagMenu()
        {
            while (true)
            {
                Console.WriteLine("\nBag menu:");
                Console.WriteLine("1: Use potion");
                Console.WriteLine("2: Use ball (Catch)");
                Console.WriteLine("Q: Back");
                Console.Write("Choice: ");

                char c = Char.ToUpper(Console.ReadKey(true).KeyChar);

                if (c == '1')
                {
                    UsePotionMenu();
                }
                else if (c == '2')
                {
                    bool caught = CatchMenu(Player.trainer.target);
                    if (caught)
                    {
                        Player.AddPokemon(Player.trainer.target);
                        Console.WriteLine($"\nYou caught {Player.trainer.target.Name}!");
                        Player.trainer.target = null;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nIt escaped!");
                    }
                }
                else if (c == 'Q')
                {
                    break;
                }
            }
        }
        //potion choosing menu
        private static void UsePotionMenu()
        {
            var potions = Player.trainer.potionsCollection;
            var fighter = Player.trainer.currentFighter;

            if (potions.Count == 0)
            {
                "No potions avalailable.".PrintWarning();
                return;
            }
            if (fighter == null)
            {
                "No current fighter selected.".PrintWarning();
                return;
            }

            Console.WriteLine("\nChoose a potion:");
            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i + 1}). { potions[i].Name} ({potions[i].Type})"); //debug later
            }

            Console.WriteLine("Potion #: ");
            if (!int.TryParse(Console.ReadLine(), out int pick) || pick < 1 || pick > potions.Count)
            {
                "Invalid potion choice.".PrintWarning();
                return;
            }

            Potion chosen = potions[pick - 1];
            potions.RemoveAt(pick - 1);

            fighter.ApplyPotion(chosen.Type);
            Console.WriteLine($"{fighter.Name} used {chosen.Name}!");
        }

        //switch pokemon menu
        private static void SwitchPokemonMenu()
        {
            var list = Player.trainer.pokemonsCollection;

            if (list.Count == 0)
            {
                "You have no pokemons.".PrintDanger();
                Player.trainer.pokemonsCollection = null;
                return;
            }

            Console.WriteLine("\nPokemon Menu - Choose fighter:");
            for (int i = 0;i < list.Count;i++)
            {
                Console.WriteLine($"{i + 1}, {list[i].Name} HP {list[i].Hp}");
            }

            Console.WriteLine("Pokemon #: ");
            if (!int.TryParse (Console.ReadLine(), out int pick) || pick < 1 || pick > list.Count)
            {
                "Invalid pokemon choice.".PrintWarning();
                return;
            }

            Player.trainer.currentFighter = list[pick - 1];
            Console.WriteLine($"You sent out {Player.trainer.currentFighter.Name}!");
        }
        //store for potions and pokeballs
        public static void Store()
        {

            while (true)
            { 
                //store menu
                Console.Clear();

                Console.WriteLine("==== Pokemon Store ====");
                Console.WriteLine($"Your GP: {Player.trainer.GP}");

                Console.WriteLine("\n1: Buy Potions");
                Console.WriteLine("2: Buy Balls");
                Console.WriteLine("Q: Exit Store");
                Console.WriteLine("Choice: ");

                char choice = Char.ToUpper(Console.ReadKey(true).KeyChar);

                if (choice == '1')
                {
                    BuyPotionMenu();
                }
                else if (choice == '2')
                {
                    BuyBallMenu();
                }
                else if (choice == 'Q')
                {
                    break;
                }
            }
        }
        //potions menu
        public static void BuyPotionMenu()
        {
            Console.Clear();
            Console.WriteLine("==== BUY POTIONS====");
            Console.WriteLine($"Your GP: {Player.trainer.GP}\n");

            var potions = GameFactory.PotionsInventory;

            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i+ 1}. {potions[i].Name} - {potions[i].Price} GP");
            }

            Console.WriteLine("Q: Back");
            Console.WriteLine("\nChoice: ");

            string input = Console.ReadLine();
            if (input.ToUpper() == "Q") return;

            if (!int.TryParse(input, out int pick) || pick <1 || pick > potions.Count)
            {
                "Invalid choice".PrintWarning();
                Console.ReadKey();
                return;
            }

            Potion item = potions[pick - 1].Clone();
            Player.BuyItem(item);

            Console.WriteLine($"Purchased {item.Name}");
            Console.ReadKey();

        }
        //buyball menu
        private static void BuyBallMenu()
        {
            Console.Clear();
            Console.WriteLine("==== Buy Balls ====");
            Console.WriteLine($"Your GP: {Player.trainer.GP}\n");

            var balls = GameFactory.BallsInventory;

            for (int i = 0; i < balls.Count;i++)
            {
                Console.WriteLine($"{i + 1}. {balls[i].Name} - {balls[i].Price} GP");
            }

            Console.WriteLine("Q: Back");
            Console.WriteLine("\nChoice: ");

            string input = Console.ReadLine();
            if (input.ToUpper() == "Q") return;

            if (!int.TryParse (input, out int pick) || pick < 1 || pick > balls.Count)
            {
                "Invalid choice!".PrintWarning();
                Console.ReadKey();
                return;
            }
            
            Ball item = balls[pick - 1];
            Player.BuyItem(item);

            Console.WriteLine($"Purchased {item.Name}!");
            Console.ReadKey();
        }

    }
}
