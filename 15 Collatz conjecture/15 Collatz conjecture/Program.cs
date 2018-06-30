using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_Collatz_conjecture
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> results = new List<long>();
            long tempResult = 0;
            
            Console.WriteLine("From which number should I start calculating?");
            while (!long.TryParse(Console.ReadLine(), out tempResult))
                Console.WriteLine("Invalid input");
            Console.WriteLine("Calculation in process");
            results.Add(tempResult);
            while (tempResult != 1)
            {
                if (tempResult % 2 == 0)
                    tempResult /= 2;
                else
                    tempResult = 3 * tempResult + 1;
                results.Add(tempResult);
            }
            Console.Clear();
            Console.WriteLine("RESULTS");
            Console.WriteLine("Operations done: {0}", results.Count - 1);
            Console.WriteLine("Path of calculating: ");
            for (int i = 0; i < results.Count; i++)
            {
                if (i == results.Count - 1)
                    Console.Write(results[i]);
                else
                    Console.Write(results[i] + " -> ");
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
