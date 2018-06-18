using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Project_Euler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The difference between the sum of the squares of the first one " +
                "hundred natural numbers and the square of the sum is equal to {0}", SquareOfSum(100) - SumOfSquares(100));
            Console.ReadLine();
        }

        static int SumOfSquares(int highestNumber)
        {
            int result = 0;
            for (int i = 1; i <= highestNumber; i++)
                result += i * i;
            return result;
        }

        static int SquareOfSum(int highestNumber)
        {
            int result = 0, sum = 0;
            for (int i = 1; i <= highestNumber; i++)
                sum += i;
            result = sum * sum;
            return result;
        }
    }
}
