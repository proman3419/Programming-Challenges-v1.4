using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _42_ASCII_digital_clock
{
    class Clock
    {
        string Time { get; set; }
        string ToDisplay { get; set; }

        public Clock()
        {
            Console.CursorVisible = false;
            while (true)
            {
                if (Time != DateTime.Now.ToString("HHmmsstt"))
                {
                    Reset();
                    Time = DateTime.Now.ToString("HHmmss");
                    foreach (char element in Time)
                        ToASCII(int.Parse(element.ToString()));
                    Display();
                }
            }
        }

        void Reset()
        {
            Console.Clear();
            ToDisplay = String.Empty;
        }

        void ToASCII(int digit)
        {
            switch (digit)
            {
                case 0:
                    ToDisplay += "####x#  #x#  #x#  #x####";
                    break;
                case 1:
                    ToDisplay += "  #x ##x  #x  #x  #"; // 1 width less for better look 
                    break;
                case 2:
                    ToDisplay += " ## x#  #x  # x #  x####";
                    break;
                case 3:
                    ToDisplay += " ## x#  #x  # x#  #x ## ";
                    break;
                case 4:
                    ToDisplay += "  # x #  x####x  # x  # ";
                    break;
                case 5:
                    ToDisplay += "####x#   x### x   #x### ";
                    break;
                case 6:
                    ToDisplay += " ## x#   x### x#  #x ## ";
                    break;
                case 7:
                    ToDisplay += "####x   #x  # x #  x #  ";
                    break;
                case 8:
                    ToDisplay += " ## x#  #x ## x#  #x ## ";
                    break;
                case 9:
                    ToDisplay += " ## x#  #x ###x   #x ## ";
                    break;
            }
            ToDisplay += "x";
        }

        void Display()
        {
            string[] parts = ToDisplay.Split('x');
            string[,] parts2D = new string[Time.Length, 5];
            int currId = 0;
            for (int i = 0; i <= parts2D.GetUpperBound(0); i++)
                for (int j = 0; j < 5; j++)
                {
                    parts2D[i, j] = parts[currId] + " ";
                    if (i % 2 != 0)
                        parts2D[i, j] += "  ";
                    currId++;
                }

            for (int i = 0; i <= parts2D.GetUpperBound(1); i++)
            {
                for (int j = 0; j <= parts2D.GetUpperBound(0); j++)
                    Console.Write(parts2D[j, i]);
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();

            Console.ReadLine();
        }
    }
}
