using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Globals
    {
        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }

        public static void Initialize()
        {
            WindowWidth = Console.WindowWidth;
            WindowHeight = Console.WindowHeight;
        }

        public static dynamic GetNumericInput(double minValue, double maxValue)
        {
            int value;
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (input.Any(char.IsLetter))
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                // If the value is too big to be stored in an int variable assign a high value to it
                // so the next if statement will trigger
                try 
                {
                    value = int.Parse(input);
                    if (value < minValue)
                    {
                        Console.WriteLine("The value is too low, min value = {0}", minValue);
                        continue;
                    }
                }
                catch { value = 9999; };
                if (value > maxValue)
                {
                    Console.WriteLine("The value is too high, max value = {0}", maxValue);
                    continue;
                }
                return value;
            }
        }

        public static void DrawSeparators(string segment)
        {
            for (int i = 0; i < WindowWidth; i++)
                Console.Write(segment);
        }
    }
}
