using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _35_Benford_s_law
{
    class BenfordsLaw
    {
        double[] Probabilities { get; set; }
        List<double> Digits { get; set; }
        List<double> InputProbabilities { get; set; }

        public BenfordsLaw()
        {
            Probabilities = new double[9];
            for (int i = 1; i < 10; i++)
                Probabilities[i - 1] = Math.Round(Math.Log10(1 + (double)1 / i), 4) * 100;
            Digits = new List<double>();
            InputProbabilities = new List<double>();
            ReadFromFile();
            CalculateProbabilities();
        }

        void ReadFromFile()
        {
            Console.WriteLine("Pass the full path of a file with data");
            string input;
            do
            {
                input = Console.ReadLine();
                if (!File.Exists(input))
                    Console.WriteLine("The passed file doesn't exist");
            } while (!File.Exists(input));

            TextReader textReader = new StreamReader(input);
            string[] dataFromFile = textReader.ReadToEnd().ToString().Split(' ');
            foreach (string data in dataFromFile)
            {
                try { Digits.Add(Double.Parse(data[0].ToString())); }
                catch (FormatException)
                {
                    Console.WriteLine("Part of the data wasn't a number. The program will now exit");
                    Environment.Exit(-1);
                }
            }
        }

        void CalculateProbabilities()
        {
            var groups = Digits.GroupBy(i => i);
            foreach (var group in groups)
                InputProbabilities.Add(Math.Round((double)group.Count() / Digits.Count(), 4) * 100);
        }

        void DisplaySpacing(int titleLength, int infoLength)
        {
            for (int i = 0; i < titleLength - infoLength; i++)
                Console.Write(" ");
        }

        public void Display()
        {
            Console.Write("Leading digit | ");
            Console.Write("Benford's law | ");
            Console.Write("Inputted set of values | ");
            Console.Write("Difference");
            Console.WriteLine();
            for (int i = 1; i < 10; i++)
            {
                Console.Write(i);
                DisplaySpacing("Leading digit | ".Length, 1);
                Console.Write(Probabilities[i - 1] + "%");
                DisplaySpacing("Benford's law | ".Length, (Probabilities[i - 1] + "%").Length);
                double difference;
                try
                {
                    Console.Write(InputProbabilities[i - 1] + "%");
                    DisplaySpacing("Inputted set of values | ".Length, (InputProbabilities[i - 1] + "%").Length);
                    difference = Probabilities[i - 1] - InputProbabilities[i - 1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.Write("0%");
                    DisplaySpacing("Inputted set of values | ".Length, "0%".Length);
                    difference = Probabilities[i - 1];
                }
                difference = Math.Round(difference, 4);
                if (difference >= 0)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(difference + "%");
                    Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenfordsLaw benfordsLaw = new BenfordsLaw();

            benfordsLaw.Display();

            Console.ReadLine();
        }
    }
}
