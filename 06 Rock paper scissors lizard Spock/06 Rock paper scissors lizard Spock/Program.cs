using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Rock_paper_scissors_lizard_Spock
{
    class Program
    {
        public abstract class OptionBase : IComparable<OptionBase>
        {
            public string Name { get { return GetType().Name.ToLower(); } }
            public abstract int CompareTo(OptionBase other);
            public void DisplayResult(int result)
            {
                if (result == -1) Console.WriteLine("so you lose");
                if (result == 0) Console.WriteLine("so it's a tie");
                if (result == 1) Console.WriteLine("so you win");
            }
        }

        public class Rock : OptionBase
        {
            public override int CompareTo(OptionBase other)
            {
                if (other is Rock) return 0;
                if (other is Paper) return -1;
                if (other is Scissors) return 1;
                if (other is Lizard) return 1;
                if (other is Spock) return -1;
                throw new InvalidOperationException("Don't cheat...");
            }
        }

        public class Paper : OptionBase
        {
            public override int CompareTo(OptionBase other)
            {
                if (other is Rock) return 1;
                if (other is Paper) return 0;
                if (other is Scissors) return -1;
                if (other is Lizard) return -1;
                if (other is Spock) return 1;
                throw new InvalidOperationException("Don't cheat...");
            }
        }

        public class Scissors : OptionBase
        {
            public override int CompareTo(OptionBase other)
            {
                if (other is Rock) return -1;
                if (other is Paper) return 1;
                if (other is Scissors) return 0;
                if (other is Lizard) return 1;
                if (other is Spock) return -1;
                throw new InvalidOperationException("Don't cheat...");
            }
        }

        public class Lizard : OptionBase
        {
            public override int CompareTo(OptionBase other)
            {
                if (other is Rock) return -1;
                if (other is Paper) return 1;
                if (other is Scissors) return -1;
                if (other is Lizard) return 0;
                if (other is Spock) return 1;
                throw new InvalidOperationException("Don't cheat...");
            }
        }

        public class Spock : OptionBase
        {
            public override int CompareTo(OptionBase other)
            {
                if (other is Rock) return 1;
                if (other is Paper) return -1;
                if (other is Scissors) return 1;
                if (other is Lizard) return -1;
                if (other is Spock) return 0;
                throw new InvalidOperationException("Don't cheat...");
            }
        }

        static void Main(string[] args)
        {
            Random random = new Random();
            bool playAgain = false;
            string input;

            do
            {
                int computerChooseInt = random.Next(0, 3);
                var computerChoose = ComputerChoose(computerChooseInt);
                var userChoose = UserChoose();
                Console.Write("Computer has chosen {0}, ", computerChoose.Name);
                userChoose.DisplayResult(userChoose.CompareTo(computerChoose));
                Console.WriteLine("Do you want to play again? (y/n)");
                while (true)
                {
                    input = Console.ReadLine().ToLower();
                    if (input == "y")
                    {
                        playAgain = true;
                        break;
                    }
                    else if (input == "n")
                    {
                        playAgain = false;
                        break;
                    }
                    else
                        Console.WriteLine("Wrong input, answer with either y or n");
                }
            } while (playAgain);
        }

        static dynamic ComputerChoose(int computerChooseInt)
        {
            if (computerChooseInt == 0) return new Rock();
            if (computerChooseInt == 1) return new Paper();
            if (computerChooseInt == 2) return new Scissors();
            throw new InvalidOperationException("The AI is broken");
        }

        static dynamic UserChoose()
        {
            string input;
            Console.WriteLine("Computer has made a move, now it's your turn");
            while(true)
            {
                input = Console.ReadLine().ToLower();
                if (new[] { "rock", "paper", "scissors", "lizard", "spock" }.Contains(input))
                    break;
                Console.WriteLine("Dont cheat, you're only allowed to play rock, paper, scissors, lizard or Spock");
            }
            if (input == "rock") return new Rock();
            if (input == "paper") return new Paper();
            if (input == "scissors") return new Scissors();
            if (input == "lizard") return new Lizard();
            if (input == "spock") return new Spock();
            throw new InvalidOperationException("Don't cheat...");
        }
    }
}
