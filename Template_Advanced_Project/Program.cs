using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_Advanced_Project.Classes;

namespace Template_Advanced_Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("E: Explore | S: Store | P: Pokemon nusery | Q: Quit");
                Console.WriteLine("Choice: ");

                char choice = char.ToUpper(Console.ReadKey(true).KeyChar);

                if (choice == 'E')
                {
                    GameManager.Explore();
                }
                else if (choice == 'S')
                {
                    GameManager.Store();
                }
                else if (choice == 'P')
                {
                    Player.Pokemon_Nursery();
                    Console.WriteLine("\nAll pokemons healed!");
                    Console.WriteLine("Press any key...");
                    Console.ReadKey(true);
                }
                else if (choice == 'Q')
                {
                    Console.WriteLine("\nGoodbye!");
                    break;
                }
                else if (choice == 'G')
                {
                    Player.trainer.GP += 100000;
                    Console.WriteLine("Cheat activated: +100,000GP");
                    Console.ReadKey(true);
                }
            }
        }   
    }
}
