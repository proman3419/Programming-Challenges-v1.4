using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Temperature_converter
{
    class Program
    {
        static void Main(string[] args)
        {
            float celsius;

            Console.WriteLine("Input temperature in Celsius scale");
            while (!float.TryParse(Console.ReadLine(), out celsius))
                Console.WriteLine("Wrong input");
            Kelvin(celsius);
            Fahrenheit(celsius);
            Rankine(celsius);
            Delisle(celsius);
            Newton(celsius);
            Reamur(celsius);
            Romer(celsius);
            Console.ReadLine();
        }

        static void Kelvin(float celsius)
        {            
            Console.WriteLine("Kelvin: " + (celsius + 273.15f));
        }

        static void Fahrenheit(float celsius)
        {
            Console.WriteLine("Fahrenheit: " + (9 / 5 * celsius + 32));
        }

        static void Rankine(float celsius)
        {
            Console.WriteLine("Rankine: " + (9 / 5 * (celsius + 273.15f)));
        }

        static void Delisle(float celsius)
        {
            Console.WriteLine("Delisle: " + ((100 - celsius) * 3 / 2));
        }

        static void Newton(float celsius)
        {
            Console.WriteLine("Newton: " + (celsius * 33 / 100));
        }

        static void Reamur(float celsius)
        {
            Console.WriteLine("Reamur: " + (celsius * 4 / 5));
        }

        static void Romer(float celsius)
        {
            Console.WriteLine("Romer: " + (celsius * 21 / 40 + 7.5f));
        }
    }
}
