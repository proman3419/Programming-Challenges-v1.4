using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _29_Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            long result = 1;
            Console.WriteLine("Input a number of which you want to get a factorial");
            while (!int.TryParse(Console.ReadLine(), out n))
                Console.WriteLine("Wrong input");
            for (int i = 2; i <= n; i++)
                result *= i;
            Console.WriteLine("Result");
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
