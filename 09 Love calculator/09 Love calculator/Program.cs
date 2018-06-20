using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _09_Love_calculator
{
    class LoveCalculator
    {
        string FirstName { get; set; }
        string SecondName { get; set; }
        string Phrase { get; set; }
        List<char> CountedLetters { get; set; }

        public LoveCalculator()
        {
            CountedLetters = new List<char>();
        }

        public void GetNames()
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            Console.WriteLine("Give me the first name");
            while (true)
            {
                FirstName = Console.ReadLine().ToLower();
                if (regex.IsMatch(FirstName))
                    break;
                Console.WriteLine("It's not a name");
            }
            Console.WriteLine("Give me the second name");
            while (true)
            {
                SecondName = Console.ReadLine().ToLower();
                if (regex.IsMatch(SecondName))
                    break;
                Console.WriteLine("It's not a name");
            }
            Phrase = FirstName + "loves" + SecondName;
        }

        public void CountLetters()
        {
            string result = String.Empty;
            int temp = 0;
            foreach (var element in Phrase)
            {
                try
                {
                    if (CountedLetters.Contains(element))
                        continue;
                }
                catch (NullReferenceException) { }

                temp = 0;
                foreach (var elementSecond in Phrase)
                {
                    if (element == elementSecond)
                    {
                        temp++;
                        CountedLetters.Add(element);
                    }
                }
                result += temp;
            }
            Phrase = result;
        }

        public void ShortenNumber()
        {
            string temp = String.Empty;
            while (true)
            {
                while (Phrase.Length > 1)
                {
                    temp = temp + (Int32.Parse(Phrase[0].ToString()) + Int32.Parse(Phrase[Phrase.Length - 1].ToString())).ToString();
                    Phrase = Phrase.Substring(1, Phrase.Length - 2);
                    if(Phrase.Length == 1)
                        temp += Phrase;
                }
                Phrase = temp;
                temp = String.Empty;
                if (Phrase.Length <= 2)
                    break;
            }
        }

        public void DisplayPhrase()
        {
            Console.WriteLine(FirstName[0].ToString().ToUpper() + FirstName.Substring(1) + " loves "
                + SecondName[0].ToString().ToUpper() + SecondName.Substring(1) + " in " + Phrase + "%");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LoveCalculator loveCalculator = new LoveCalculator();
            loveCalculator.GetNames();
            loveCalculator.CountLetters();
            loveCalculator.ShortenNumber();
            loveCalculator.DisplayPhrase();
            Console.ReadLine();
        }
    }
}
