using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _26_Radix_base_converter
{
    class Converter
    {
        int Number { get; set; }
        int InitialBase { get; set; }
        int FinalBase { get; set; }
        List<float> Reminders { get; set; }

        public Converter()
        {
            Number = GetInput("Number to convert");
            InitialBase = GetInput("Initial base");
            FinalBase = GetInput("Final base");
            Reminders = new List<float>();
        }

        int GetInput(string display)
        {
            int storeInput;
            Console.WriteLine(display);
            while (!int.TryParse(Console.ReadLine(), out storeInput))
                Console.WriteLine("Wrong input");
            return storeInput;
        }

        public void Convert()
        {
            ConvertToDecimal();
            ConvertToTheFinalBase();
            DisplayResults();
        }

        void ConvertToDecimal()
        {
            string numberString = Number.ToString();
            Number = 0;
            for (int i = 0; i < numberString.Length; i++)
                Number += (int)(int.Parse(numberString[i].ToString()) * Math.Pow(InitialBase, numberString.Length - 1 - i));
        }

        void ConvertToTheFinalBase()
        {
            float temp = Number;
            while ((int)temp != 0)
            {
                temp = temp / FinalBase;
                Reminders.Add((temp - (int)temp) * FinalBase);
                temp = (int)temp;
            }
        }

        void DisplayResults()
        {
            Console.WriteLine("Result");
            for (int i = Reminders.Count - 1; i >= 0; i--)
                Console.Write(Math.Round(Reminders[i]));
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Converter converter = new Converter();

            converter.Convert();

            Console.ReadLine();
        }
    }
}
