using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _36_Currency_converter___various_units
{
    class Converter
    {
        public void Start()
        {
            GetInput();
        }

        void GetInput()
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                    Environment.Exit(0);
                string argument = "";
                try { argument = input.Split(' ')[2]; }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Unknown or misstyped command");
                    continue;
                }

                #region Units
                #region Length
                // mi
                if (input.StartsWith("mi m"))
                    Convert(argument, 1609.35, "m", 4);
                else if (input.StartsWith("mi yd "))
                    Convert(argument, 1760.0065617, "yd", 4);
                else if (input.StartsWith("mi ft "))
                    Convert(argument, 5280.019685, "ft", 4);
                else if (input.StartsWith("mi in "))
                    Convert(argument, 63360.23622, "in", 4);
                else if (input.StartsWith("mi ly "))
                    Convert(argument, 1.701096963E-13, "ly", 15);
                // m
                else if (input.StartsWith("m mi "))
                    Convert(argument, 0.0006213689, "mi", 4);
                else if (input.StartsWith("m yd "))
                    Convert(argument, 1.0936132983, "yd", 4);
                else if (input.StartsWith("m ft "))
                    Convert(argument, 3.280839895, "ft", 4);
                else if (input.StartsWith("m in "))
                    Convert(argument, 39.37007874, "in", 4);
                else if (input.StartsWith("m ly "))
                    Convert(argument, 1.057008707E-16, "ly", 15);
                // yd
                else if (input.StartsWith("yd mi "))
                    Convert(argument, 0.0005681797, "mi", 4);
                else if (input.StartsWith("yd m"))
                    Convert(argument, 0.9144, "m", 4);
                else if (input.StartsWith("yd ft "))
                    Convert(argument, 3, "ft", 4);
                else if (input.StartsWith("yd in "))
                    Convert(argument, 36, "in", 4);
                else if (input.StartsWith("yd ly "))
                    Convert(argument, 9.665287622E-17, "ly", 15);
                // ft
                else if (input.StartsWith("ft mi "))
                    Convert(argument, 0.0001893932, "mi", 4);
                else if (input.StartsWith("ft m"))
                    Convert(argument, 0.3048, "m", 4);
                else if (input.StartsWith("ft yd "))
                    Convert(argument, 0.3333333333, "yd", 4);
                else if (input.StartsWith("ft in "))
                    Convert(argument, 12, "in", 4);
                else if (input.StartsWith("ft ly "))
                    Convert(argument, 3.22176254E-17, "ly", 15);
                // in
                else if (input.StartsWith("in mi "))
                    Convert(argument, 0.0000157828, "mi", 4);
                else if (input.StartsWith("in m"))
                    Convert(argument, 0.0254, "m", 4);
                else if (input.StartsWith("in yd "))
                    Convert(argument, 0.0277777778, "yd", 4);
                else if (input.StartsWith("in ft "))
                    Convert(argument, 0.0833333333, "ft", 4);
                else if (input.StartsWith("in ly "))
                    Convert(argument, 2.684802117E-18, "ly", 15);
                // ly
                else if (input.StartsWith("ly mi "))
                    Convert(argument, 5878559666946, "mi", 4);
                else if (input.StartsWith("ly m"))
                    Convert(argument, 9460660000000000, "m", 4);
                else if (input.StartsWith("ly yd "))
                    Convert(argument, 10346303587051618, "yd", 4);
                else if (input.StartsWith("ly ft "))
                    Convert(argument, 31038910761154856, "ft", 4);
                else if (input.StartsWith("ly in "))
                    Convert(argument, 372466929133858300, "in", 4);
                #endregion

                #region Weight
                // g
                else if (input.StartsWith("g lb"))
                    Convert(argument, 0.0022046244, "lb", 4);
                else if (input.StartsWith("g oz"))
                    Convert(argument, 0.0352739907, "oz", 4);
                else if (input.StartsWith("g ct"))
                    Convert(argument, 5, "ct", 4);
                else if (input.StartsWith("g u"))
                    Convert(argument, 6.022136652E+23, "u", 15);
                // lb
                else if (input.StartsWith("lb g"))
                    Convert(argument, 453.592, "g", 4);
                else if (input.StartsWith("lb oz"))
                    Convert(argument, 16, "oz", 4);
                else if (input.StartsWith("lb ct"))
                    Convert(argument, 2267.96, "ct", 4);
                else if (input.StartsWith("lb u"))
                    Convert(argument, 2.731593008E+26, "u", 15);
                // oz
                else if (input.StartsWith("oz g"))
                    Convert(argument, 28.3495, "g", 4);
                else if (input.StartsWith("oz lb"))
                    Convert(argument, 0.0625, "lb", 4);
                else if (input.StartsWith("oz ct"))
                    Convert(argument, 141.7475, "ct", 4);
                else if (input.StartsWith("oz u"))
                    Convert(argument, 1.70724563E+25, "u", 15);
                // ct
                else if (input.StartsWith("ct g"))
                    Convert(argument, 0.2, "g", 4);
                else if (input.StartsWith("ct lb"))
                    Convert(argument, 0.0004409249, "lb", 4);
                else if (input.StartsWith("ct oz"))
                    Convert(argument, 0.0070547981, "oz", 4);
                else if (input.StartsWith("ct u"))
                    Convert(argument, 1.20442733E+23, "u", 15);
                // u
                else if (input.StartsWith("u g"))
                    Convert(argument, 1.660540199E-24, "g", 4);
                else if (input.StartsWith("u lb"))
                    Convert(argument, 3.660867475E-27, "lb", 4);
                else if (input.StartsWith("u oz"))
                    Convert(argument, 5.85738796E-26, "oz", 4);
                else if (input.StartsWith("u ct"))
                    Convert(argument, 8.302700999E-24, "ct", 4);
                #endregion
                #endregion

                #region Currencies
                // usd
                else if (input.StartsWith("usd eur "))
                    Convert(argument, 0.857226, "EUR", 2);
                else if (input.StartsWith("usd gbp "))
                    Convert(argument, 0.761817, "GBP", 2);
                else if (input.StartsWith("usd inr "))
                    Convert(argument, 68.3340, "INR", 2);
                else if (input.StartsWith("usd aud "))
                    Convert(argument, 1.35007, "AUD", 2);
                else if (input.StartsWith("usd cad "))
                    Convert(argument, 1.29990, "CAD", 2);
                else if (input.StartsWith("usd pln "))
                    Convert(argument, 3.65431, "PLN", 2);
                else if (input.StartsWith("usd rub "))
                    Convert(argument, 63.2987, "RUB", 2);
                else if (input.StartsWith("usd cny "))
                    Convert(argument, 6.84648, "CNY", 2);
                // eur
                else if (input.StartsWith("eur usd "))
                    Convert(argument, 1.16640, "USD", 2);
                else if (input.StartsWith("eur gbp "))
                    Convert(argument, 0.888578, "GBP", 2);
                else if (input.StartsWith("eur inr "))
                    Convert(argument, 79.7016, "INR", 2);
                else if (input.StartsWith("eur aud "))
                    Convert(argument, 1.57479, "AUD", 2);
                else if (input.StartsWith("eur cad "))
                    Convert(argument, 1.51615, "CAD", 2);
                else if (input.StartsWith("eur pln "))
                    Convert(argument, 4.26118, "PLN", 2);
                else if (input.StartsWith("eur rub "))
                    Convert(argument, 73.4914, "RUB", 2);
                else if (input.StartsWith("eur cny "))
                    Convert(argument, 7.95295, "CNY", 2);
                // gbp
                else if (input.StartsWith("gbp usd "))
                    Convert(argument, 1.31264, "USD", 2);
                else if (input.StartsWith("gbp eur "))
                    Convert(argument, 1.12538, "EUR", 2);
                else if (input.StartsWith("gbp inr "))
                    Convert(argument, 89.6893, "INR", 2);
                else if (input.StartsWith("gbp aud "))
                    Convert(argument, 1.77228, "AUD", 2);
                else if (input.StartsWith("gbp cad "))
                    Convert(argument, 1.70614, "CAD", 2);
                else if (input.StartsWith("gbp pln "))
                    Convert(argument, 4.79485, "PLN", 2);
                else if (input.StartsWith("gbp rub "))
                    Convert(argument, 82.7435, "RUB", 2);
                else if (input.StartsWith("gbp cny "))
                    Convert(argument, 8.95719, "CNY", 2);
                // inr
                else if (input.StartsWith("inr usd "))
                    Convert(argument, 0.0146343, "USD", 2);
                else if (input.StartsWith("inr eur "))
                    Convert(argument, 0.0125473, "EUR", 2);
                else if (input.StartsWith("inr gbp "))
                    Convert(argument, 0.0111490, "GBP", 2);
                else if (input.StartsWith("inr aud "))
                    Convert(argument, 0.0197576, "AUD", 2);
                else if (input.StartsWith("inr cad "))
                    Convert(argument, 0.0190212, "CAD", 2);
                else if (input.StartsWith("inr pln "))
                    Convert(argument, 0.0534668, "PLN", 2);
                else if (input.StartsWith("inr rub "))
                    Convert(argument, 0.922271, "RUB", 2);
                else if (input.StartsWith("inr cny "))
                    Convert(argument, 0.0996944, "CNY", 2);
                // aud
                else if (input.StartsWith("aud usd "))
                    Convert(argument, 0.740523, "USD", 2);
                else if (input.StartsWith("aud eur "))
                    Convert(argument, 0.634869, "EUR", 2);
                else if (input.StartsWith("aud gbp "))
                    Convert(argument, 0.564139, "GBP", 2);
                else if (input.StartsWith("aud inr "))
                    Convert(argument, 50.6052, "INR", 2);
                else if (input.StartsWith("aud cad "))
                    Convert(argument, 0.962595, "CAD", 2);
                else if (input.StartsWith("aud pln "))
                    Convert(argument, 2.70447, "PLN", 2);
                else if (input.StartsWith("aud rub "))
                    Convert(argument, 46.5742, "RUB", 2);
                else if (input.StartsWith("aud cny "))
                    Convert(argument, 5.04032, "CNY", 2);
                // cad
                else if (input.StartsWith("cad usd "))
                    Convert(argument, 0.769240, "USD", 2);
                else if (input.StartsWith("cad eur "))
                    Convert(argument, 0.659624, "EUR", 2);
                else if (input.StartsWith("cad gbp "))
                    Convert(argument, 0.586188, "GBP", 2);
                else if (input.StartsWith("cad inr "))
                    Convert(argument, 52.5684, "INR", 2);
                else if (input.StartsWith("cad aud "))
                    Convert(argument, 1.03909, "AUD", 2);
                else if (input.StartsWith("cad pln "))
                    Convert(argument, 2.81042, "PLN", 2);
                else if (input.StartsWith("cad rub "))
                    Convert(argument, 48.5506, "RUB", 2);
                else if (input.StartsWith("cad cny "))
                    Convert(argument, 5.25559, "CNY", 2);
                // pln
                else if (input.StartsWith("pln usd "))
                    Convert(argument, 0.273691, "USD", 2);
                else if (input.StartsWith("pln eur "))
                    Convert(argument, 0.234685, "EUR", 2);
                else if (input.StartsWith("pln gbp "))
                    Convert(argument, 0.208565, "GBP", 2);
                else if (input.StartsWith("pln inr "))
                    Convert(argument, 18.7018, "INR", 2);
                else if (input.StartsWith("pln aud "))
                    Convert(argument, 0.369763, "AUD", 2);
                else if (input.StartsWith("pln cad "))
                    Convert(argument, 0.369763, "CAD", 2);
                else if (input.StartsWith("pln rub "))
                    Convert(argument, 17.1687, "RUB", 2);
                else if (input.StartsWith("pln cny "))
                    Convert(argument, 1.85827, "CNY", 2);
                // rub
                else if (input.StartsWith("rub usd "))
                    Convert(argument, 0.0158058, "USD", 2);
                else if (input.StartsWith("rub eur "))
                    Convert(argument, 0.0136097, "EUR", 2);
                else if (input.StartsWith("rub gbp "))
                    Convert(argument, 0.0120854, "GBP", 2);
                else if (input.StartsWith("rub inr "))
                    Convert(argument, 1.08513, "INR", 2);
                else if (input.StartsWith("rub aud "))
                    Convert(argument, 0.0214752, "AUD", 2);
                else if (input.StartsWith("rub cad "))
                    Convert(argument, 0.0205975, "CAD", 2);
                else if (input.StartsWith("rub pln "))
                    Convert(argument, 0.0582435, "PLN", 2);
                else if (input.StartsWith("rub cny "))
                    Convert(argument, 0.108215, "CNY", 2);
                // cny
                else if (input.StartsWith("cny usd "))
                    Convert(argument, 0.146049, "USD", 2);
                else if (input.StartsWith("cny eur "))
                    Convert(argument, 0.125771, "EUR", 2);
                else if (input.StartsWith("cny gbp "))
                    Convert(argument, 0.111674, "GBP", 2);
                else if (input.StartsWith("cny inr "))
                    Convert(argument, 10.0330, "INR", 2);
                else if (input.StartsWith("cny aud "))
                    Convert(argument, 0.198470, "AUD", 2);
                else if (input.StartsWith("cny cad "))
                    Convert(argument, 0.190329, "CAD", 2);
                else if (input.StartsWith("cny pln "))
                    Convert(argument, 0.538281, "PLN", 2);
                else if (input.StartsWith("cny rub "))
                    Convert(argument, 9.24149, "RUB", 2);

                else
                    Console.WriteLine("Unknown command");
                #endregion
            }
        }

        void Convert(string argument, double multiplier, string currencyName, int decimals)
        {
            try
            {
                double value = double.Parse(argument);
                if (value <= 0)
                {
                    Console.WriteLine("The argument's value is too low");
                    return;
                }
                Console.WriteLine(Math.Round(value * multiplier, decimals) + " " + currencyName);
            }
            catch (FormatException)
            { Console.WriteLine("Invalid argument passed"); }
        }
    }

    class Program
    {
        static void Main(string[] args)
        { 
            // Currencies rates from 02-08-2018
            Converter converter = new Converter();

            converter.Start();
        }
    }
}
