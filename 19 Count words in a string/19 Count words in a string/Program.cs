using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _19_Count_words_in_a_string
{
    class StringStatsCollecter
    {
        string Input { get; set; }
        int Words { get; set; }
        int Lines { get; set; }
        int Sentences { get; set; }
        int Paragraphs { get; set; }

        public void Start()
        {
            GetInput();
            CountWords();
            CountLines();
            CountSentences();
            CountParagraphs();
            DisplayResults();
        }

        void GetInput()
        {
            Input = Console.ReadLine();
        }

        void DisplayResults()
        {
            Console.WriteLine("\nString statistics");
            Console.WriteLine("Words: {0}", Words);
            Console.WriteLine("Lines: {0}", Lines);
            Console.WriteLine("Sentences: {0}", Sentences);
            Console.WriteLine("Paragraphs: {0}", Paragraphs);
        }

        void CountWords()
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '!', '?' };
            Words = Input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        void CountLines()
        {
            Lines = (int)Math.Ceiling((double)Input.Length / Console.WindowWidth);
        }

        void CountSentences()
        {
            foreach (Match match in Regex.Matches(Input, @"(\S.+?[.!?])(?=\s+|$)"))
                Sentences++;
        }

        void CountParagraphs()
        {
            foreach (Match match in Regex.Matches(Input, @"\t\w+"))
                Paragraphs++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StringStatsCollecter stringStatsCollecter = new StringStatsCollecter();
            stringStatsCollecter.Start();

            Console.ReadLine();
        }
    }
}
