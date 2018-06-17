using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = 1, l = 1;
            for (int i = 1; i < 101; i++)
            {
                if (i % (3 * k) == 0 && i % (5 * l) == 0)
                {
                    Console.WriteLine("FizzBuzz");
                    k++;
                    l++;
                }
                else if (i % (3 * k) == 0)
                {
                    Console.WriteLine("Fizz");
                    k++;
                }
                else if (i % (5 * l) == 0)
                {
                    Console.WriteLine("Buzz");
                    l++;
                }
                else
                    Console.WriteLine(i);
            }
            Console.ReadLine();
        }
    }
}
