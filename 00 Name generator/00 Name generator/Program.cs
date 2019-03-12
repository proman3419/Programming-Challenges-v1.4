using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Name_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            int nameLength;
            string letter;
            string name = String.Empty;
            Random rand = new Random();

            Console.WriteLine("How long name do you want to be?");
            nameLength = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < nameLength; i++)
            {
                letter = Convert.ToChar(rand.Next(65, 90)).ToString();
                if (i != 0)
                    letter = letter.ToLower();
                name += letter;
            }
            Console.WriteLine("This is a brand new generated name:");
            Console.WriteLine(name);
            Console.Read();
        }
    }
}
