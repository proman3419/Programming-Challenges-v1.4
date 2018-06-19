using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _08_Hangman
{
    class Game
    {
        string[] words = {"APPLE JUICE", "BANANA", "DIVERSITY", "ASTRONAUT", "LYNX", "INSECTIVORE", "MICROWAVE", "JACKPOT", "KILOBYTE",
        "STRONGHOLD", "VODKA", "UNKNOWN", "PEEKABOO", "NOWADAYS", "MNEMONIC", "MEGAHERTZ", "FUCHSIA", "COBWEB", "BLIZZARD", "AWKWARD"};
        string Word { get; set; }
        bool Win { get; set; }
        bool Lose { get; set; }
        string ActualWord { get; set; }
        int Mistakes { get; set; }
        bool FirstRun { get; set; }
        List<char> Letters { get; set; }
        const int maxMistakes = 8;

        public Game()
        {
            Mistakes = 0;
            FirstRun = true;
            Win = false;
            Lose = false;
            Letters = new List<char>();
        }

        public void ChooseWord()
        {
            Random random = new Random();
            foreach(var element in words[random.Next(0, words.GetUpperBound(0))])
            {
                if (Char.IsLetter(element))
                    Word += element + " ";
                else
                    Word += "   ";
            }
        }

        public void Start()
        {
            do
            {
                if(FirstRun)
                    FirstDisplay();
                CheckGuess(GetUserGuess());
            } while (!Win && !Lose);

        }

        public void FirstDisplay()
        {
            ActualWord = new Regex("\\S").Replace(Word, "_");
            Console.WriteLine(ActualWord);
            FirstRun = false;
        }

        public string GetUserGuess()
        {
            char input;
            Console.WriteLine("Make your guess");
            while (true)
            {
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (Letters.Contains(input))
                {
                    Console.WriteLine("You've already tried this letter, input different one");
                    continue;
                }
                else if (Char.IsLetter(input))
                {
                    Letters.Add(input);
                    break;
                }
                Console.WriteLine("Only letters are allowed");
            }
            return input.ToString().ToUpper();
        }

        public void CheckGuess(string guess)
        {
            if (Word.Contains(guess))
            {
                UpdateActualWord(guess);
                if (ActualWord.Equals(Word))
                {
                    Win = true;
                    Console.WriteLine("Congratulations, you won!");
                }
            }
            else
                Mistake();
        }

        public void UpdateActualWord(string guess)
        {
            char[] temp = new char[Word.Length];
            for (int i = 0; i < Word.Length; i++)
            {
                temp[i] = ActualWord[i];
                if (guess.Equals(Word[i].ToString()))
                {
                    temp[i] = Word[i];
                } 
            }
            ActualWord = new string(temp);
            Console.WriteLine(ActualWord);
        }

        public void Mistake()
        {
            switch (Mistakes)
            {
                case 0:
                    Console.WriteLine(@"
+----------------+
|                |
|                |
|   O            |
|                |
|                |
|                |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 1:
                    Console.WriteLine(@"
+----------------+
|                |
|                |
|   O            |
|   |            |
|   |            |
|                |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 2:
                    Console.WriteLine(@"
+----------------+
|                |
|                |
|   O            |
|  \|            |
|   |            |
|                |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 3:
                    Console.WriteLine(@"
+----------------+
|                |
|                |
|   O            |
|  \|/           |
|   |            |
|                |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 4:
                    Console.WriteLine(@"
+----------------+
|                |
|                |
|   O            |
|  \|/           |
|   |            |
|  /             |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 5:
                    Console.WriteLine(@"
+----------------+
|                |
|                |
|   O            |
|  \|/           |
|   |            |
|  / \           |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 6:
                    Console.WriteLine(@"
+----------------+
|   _________    |
|   |            |
|   O            |
|  \|/           |
|   |            |
|  / \           |
|                |
|                |
|		 |
+----------------+");
                    break;
                case 7:
                    Console.WriteLine(@"
+----------------+
|   _________    |
|   |     \ |    |
|   O      \|    |
|  \|/      |    |
|   |       |    |
|  / \      |    |
|           |    |
|          / \   |
|		 |
+----------------+");
                    Lose = true;
                    Console.WriteLine("You lost");
                    break;
            }
            Mistakes++;
            if(Mistakes < 8)
            Console.WriteLine("{0} attempts left", maxMistakes - Mistakes);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Classic Hangman game");
            Console.WriteLine();
            do
            {
                Game game = new Game();
                game.ChooseWord();
                game.Start();
            } while (Continue());
        }

        static bool Continue()
        {
            Console.WriteLine("Do you want to play again? (y/n)");
            while (true)
            {
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (input == 'y')
                    return true;
                else if (input == 'n')
                    return false;
                else
                    Console.WriteLine("Wrong input, answer with either y or n");
            }
        }
    }
}
