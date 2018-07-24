using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _28_Fibonacci_sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int elements, previousNumber = 0, beforePreviousNumber = 0, currentNumber = 0;
            Console.WriteLine("How many elemenets of the Fibonacci sequence do you want to get?");
            while (!int.TryParse(Console.ReadLine(), out elements))
                Console.WriteLine("Wrong input");
            for (int i = 0; i < elements; i++)
            {
                if (i == 0)
                    currentNumber = 1;
                else if (i == 1)
                    previousNumber = 1;
                else if (i == 2)
                    beforePreviousNumber = 1;
                if(i >= 2)
                {
                    currentNumber = previousNumber + beforePreviousNumber;
                    beforePreviousNumber = previousNumber;
                    previousNumber = currentNumber;
                }
                Console.Write(currentNumber + " ");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
