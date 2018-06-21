using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_Random_sentence_generator
{
    interface IPerson
    {
        string[] Name { get; set; }
        string[] Be { get; set; }
        string[] PastSimple { get; set; }
        string[] PresentSimple { get; set; }
        string[] Continous { get; set; }
        string[] Words { get; set; }
    }

    class Word
    {
        public static int PastSimpleHighestId { get; set; }
        public static int PresentSimpleHighestId { get; set; }
        public static int ContinousHighestId { get; set; }
        public static int WordsHighestId{ get; set; }

        static public string[] InitializePastSimple()
        {
            string[] temp = new string[] { "killed", "ate", "edited", "walked by", "walked down", "missed", "added", "created", "drinked", "burnt", "fried", "looted", "drived",
            "climbed", "destroyed", "described", "fought with", "passed"};
            PastSimpleHighestId = temp.GetUpperBound(0);
            return temp;
        }

        static public string[] InitializePresentSimple()
        {
            string[] temp = new string[] { "kill", "eat", "edit", "walk by", "walk down", "miss", "add", "create", "drink", "burn", "fry", "loot", "drive", "climb", "destroy", "describe", "fight with", "pass" };
            PresentSimpleHighestId = temp.GetUpperBound(0);
            return temp;
        }

        static public string[] InitializeContinous()
        {
            string[] temp = new string[] { "killing", "eating", "editing", "walking by", "walking down", "missing", "adding", "creating", "drinking", "burning", "looting", "driving", "climbing",
            "destroying", "describing", "fighting with", "passing" };
            ContinousHighestId = temp.GetUpperBound(0);
            return temp;
        }

        static public string[] InitializeWords()
        {
            string[] temp = new string[] {"a homeless cat", "a damaged car", "a kangaroo from Australia", "an important text", "a dinner", "an apple", "a house with a green roof",
            "water", "the nearby woods", "the longest river in the country", "fries", "a machine oil", "the bag of a passerby", "a machine gun", "a massive rock", "an angry bear",
            "a bloodthirsty wolf", "a sleepy panda", "an annoying kid", "a fancy dress", "a delicious dish", "a dried out river", "a deep canyon", "a steep hill"};
            WordsHighestId = temp.GetUpperBound(0);
            return temp;
        }
    }

    class I : Word, IPerson
    {
        public string[] Name { get; set; }
        public int NameHighestId { get; set; }
        public string[] Be { get; set; }
        public string[] PastSimple { get; set; }
        public string[] PresentSimple { get; set; }
        public string[] Continous { get; set; }
        public string[] Words { get; set; }

        public I()
        {
            Name = new string[] { "I" };
            NameHighestId = Name.GetUpperBound(0);
            Be = new string[] { "was", "am" };
            PastSimple = InitializePastSimple();
            PresentSimple = InitializePresentSimple();
            Continous = InitializeContinous();
            Words = InitializeWords();
        }
    }

    class HeSheIt : Word, IPerson
    {
        public string[] Name { get; set; }
        public int NameHighestId { get; set; }
        public string[] Be { get; set; }
        public string[] PastSimple { get; set; }
        public string[] PresentSimple { get; set; }
        public string[] Continous { get; set; }
        public string[] Words { get; set; }

        public HeSheIt()
        {
            Name = new string[] { "He", "She", "It" };
            NameHighestId = Name.GetUpperBound(0);
            Be = new string[] { "was", "is"};
            PastSimple = InitializePastSimple();
            PresentSimple = InitializePresentSimple();
            Continous = InitializeContinous();
            Words = InitializeWords();
        }
    }

    class WeTheyYou : Word, IPerson
    {
        public string[] Name { get; set; }
        public int NameHighestId { get; set; }
        public string[] Be { get; set; }
        public string[] PastSimple { get; set; }
        public string[] PresentSimple { get; set; }
        public string[] Continous { get; set; }
        public string[] Words { get; set; }

        public WeTheyYou()
        {
            Name = new string[] { "We", "They", "You" };
            NameHighestId = Name.GetUpperBound(0);
            Be = new string[] { "were", "are" };
            PastSimple = InitializePastSimple();
            PresentSimple = InitializePresentSimple();
            Continous = InitializeContinous();
            Words = InitializeWords();
        }
    }

    class SentenceGenerator
    {
        public void Start()
        {
            Console.WriteLine("This is a brand new randomly generated sentence:");    
        }

        public dynamic SelectPerson()
        {
            Random random = new Random();
            int choose = random.Next(0, 2);
            switch (choose)
            {
                case 1:
                    return new I();
                case 2:
                    return new HeSheIt();
                default:
                    return new WeTheyYou();
            }
        }

        public void CreateSentence(dynamic person)
        {  
            Random random = new Random();
            int tense = random.Next(0, 4);
            int word = random.Next(0, Word.WordsHighestId);
            switch (tense)
            {
                case 0:
                    int verb = random.Next(0, Word.ContinousHighestId);
                    int name = random.Next(0, person.NameHighestId);
                    string temp = person.Name[name] + " " + person.Be[0] + " " + person.Continous[verb] + " " + person.Words[word];
                    Console.WriteLine(temp);
                    break;
                case 1:
                    int verbNd = random.Next(0, Word.ContinousHighestId);
                    int nameNd = random.Next(0, person.NameHighestId);
                    string tempNd = person.Name[nameNd] + " " + person.Be[1] + " " + person.Continous[verbNd] + " " + person.Words[word];
                    Console.WriteLine(tempNd);
                    break;
                case 2:
                    int verbRd = random.Next(0, Word.PastSimpleHighestId);
                    int nameRd = random.Next(0, person.NameHighestId);
                    string tempRd = person.Name[nameRd] + " " + person.PastSimple[verbRd] + " " + person.Words[word];
                    Console.WriteLine(tempRd);
                    break;
                case 3:
                    int verbTh = random.Next(0, Word.PresentSimpleHighestId);
                    int nameTh = random.Next(0, person.NameHighestId);
                    string tempTh = person.Name[nameTh] + " " + person.PresentSimple[verbTh] + " " + person.Words[word];
                    Console.WriteLine(tempTh);
                    break;
                default:
                    Console.WriteLine("The program is broken, contact the support");
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SentenceGenerator sentenceGenerator = new SentenceGenerator();
            sentenceGenerator.Start();
            sentenceGenerator.CreateSentence(sentenceGenerator.SelectPerson());
            Console.ReadLine();
        }
    }
}
