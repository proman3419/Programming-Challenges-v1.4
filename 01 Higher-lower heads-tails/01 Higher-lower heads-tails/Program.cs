using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Higher_lower_heads_tails
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;
            do
            {
                Console.WriteLine("Input 1 to play Higher-Lower, input 2 to play Heads-Tails");
                input = Int32.Parse(Console.ReadLine());
            } while (input != 1 && input != 2);
            if (input == 1)
                HigherLower();
            if (input == 2)
                HeadsTails();
            Console.ReadLine();
        }

        static void HigherLower()
        {
            Random rand = new Random();
            int number, input;

            number = rand.Next(0, 101);
            Console.WriteLine("Guess the number from range 0-100");
            do
            {
                input = Convert.ToInt32(Console.ReadLine());
                if (input > number)
                    Console.WriteLine("The number is smaller");
                else if (input < number)
                    Console.WriteLine("The number is greater");
            } while (input != number);
            Console.WriteLine("You've guessed the number");
        }

        static void HeadsTails()
        {
            Random rand = new Random();
            int number, input;
            string inputString;

            number = rand.Next(0, 2);
            Console.WriteLine("Guess which side of the coin is faced up. Input 0 to vote for head, input 1 to vote for tail");
            do
            {
                inputString = Console.ReadLine().ToLower();
                switch (inputString)
                {
                    case "head":
                        input = 0;
                        break;
                    case "tail":
                        input = 1;
                        break;
                    default:
                        input = 2;
                        Console.WriteLine("Wrong input");
                        break;
                }
            } while (input == 2);
            if (input != number)
            {
                Console.WriteLine("Your tip isn't correct");
                return;
            }
            
            if (number == 0)
                Console.WriteLine("You've guessed. It is the head side");
            else if (number == 1)
                Console.WriteLine("You've guessed. It is the tail side");
        }
    }
}
