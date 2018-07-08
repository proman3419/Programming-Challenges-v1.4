using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22_BMI_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            float weight, height, result;
            string category;

            Console.WriteLine("Input your weight in kilograms");
            while (!float.TryParse(Console.ReadLine(), out weight))
                Console.WriteLine("Wrong input");
            Console.WriteLine("Input your height in meteres");
            while (!float.TryParse(Console.ReadLine(), out height))
                Console.WriteLine("Wrong input");
            result = (float)Math.Round(weight / Math.Pow(height, 2), 2);
            if (result < 18.5)
                category = "underweight";
            else if (result >= 18.5 && result <= 24.9)
                category = "normal weight";
            else if (result >= 25 && result <= 29.9)
                category = "overweight";
            else if (result >= 30)
                category = "obese";
            else
                category = "uncategorized";
            Console.WriteLine("Your BMI equals {0}, it means that you fit in the {1} category", result, category);
            Console.ReadLine();
        }
    }
}
