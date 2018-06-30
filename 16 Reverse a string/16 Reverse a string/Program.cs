using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_Reverse_a_string
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input a string to reverse");
            char[] input = Console.ReadLine().ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = input.GetUpperBound(0); i >= 0; i--)
                stringBuilder.Append(input[i]);
            Console.WriteLine("Reversed string: \n{0}", stringBuilder.ToString());
            Console.ReadLine();
        }
    }
}
