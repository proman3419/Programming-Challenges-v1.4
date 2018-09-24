using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _39_Scientific_calculator
{
    class Calculator
    {
        String Input { get; set; }    
        double Result { get; set; }

        public Calculator()
        {
            Start();
        }

        void Start()
        {
            while (true)
            {
                Console.Write(">");
                Input = Console.ReadLine();
                if (!BasicOperations())
                    if (!OneArgOperations())
                        if (!TwoArgOperations())
                        {
                            Console.WriteLine("Wrong input");
                            continue;
                        }
                if (double.IsInfinity(Result))
                    Console.WriteLine("Infinity");
                else
                    Console.WriteLine(Result);
            }

        }

        bool BasicOperations()
        {
            try { Result = double.Parse(new DataTable().Compute(Input, null).ToString()); return true; }
            catch { return false; }
        }

        bool OneArgOperations()
        {
            try
            {
                string[] input = Input.Split(' ');
                if (input.Length > 2)
                    return false;
                double value = double.Parse(input[1]);
                switch (input[0])
                {
                    case "sin":
                        Result = Math.Sin(value);
                        break;
                    case "cos":
                        Result = Math.Cos(value);
                        break;
                    case "tan":
                        Result = Math.Tan(value);
                        break;
                    case "ctan":
                        if (Math.Tan(value) != 0)
                            Result = 1 / Math.Tan(value);
                        break;
                    case "fact":
                        Result = 1;
                        for (int i = 1; i < value; i++)
                            Result *= value;
                        break;
                    case "mod":
                        if (value < 0)
                            Result = -value;
                        else
                            Result = value;
                        break;
                    case "ln":
                        Result = Math.Log(value);
                        break;
                }
                return true;
            }
            catch { return false; };
        }

        bool TwoArgOperations()
        {
            try
            {
                string[] input = Input.Split(' ');
                if (input.Length > 3)
                    return false;
                double[] values = { double.Parse(input[1]), double.Parse(input[2]) };
                switch (input[0])
                {
                    case "root":
                        Result = Math.Pow(values[0], 1 / values[1]);
                        break;
                    case "pow":
                        Result = Math.Pow(values[0], values[1]);
                        break;
                    case "log":
                        Result = Math.Log(values[0], values[1]);
                        break;
                }
                return true;
            }
            catch { return false; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            Console.ReadLine();
        }
    }
}
