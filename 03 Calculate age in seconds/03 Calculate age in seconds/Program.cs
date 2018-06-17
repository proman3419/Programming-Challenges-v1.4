using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Calculate_age_in_seconds
{
    class Program
    {
        static void Main(string[] args)
        {
            int age, birthYear, leapYears = 0, days, seconds;

            Console.WriteLine("What's your age? (years)");
            while (!Int32.TryParse(Console.ReadLine(), out age))
                Console.WriteLine("Invalid input");
            Console.WriteLine("When were you born? (year)");
            while (!Int32.TryParse(Console.ReadLine(), out birthYear))
                Console.WriteLine("Invalid input");
            for (int i = 0; i < age; i++)
            {
                if ((birthYear + i) % 4 == 0)
                    leapYears++;
            }
            days = age * 365 + leapYears;
            seconds = days * 24 * 60 * 60;
            Console.WriteLine("You've been living for {0} seconds", seconds);
            Console.ReadLine();
        }
    }
}
