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

    class Initializer
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

    #region Persons
    class I : Initializer, IPerson
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

    class HeSheIt : Initializer, IPerson
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

    class WeTheyYou : Initializer, IPerson
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
    #endregion

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
            int word = random.Next(0, Initializer.WordsHighestId);
            int verb, name;
            string temp;
            switch (tense)
            {
                case 0:
                    verb = random.Next(0, Initializer.ContinousHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + person.Be[0] + " " + person.Continous[verb] + " " + person.Words[word];
                    Console.WriteLine(temp);
                    break;
                case 1:
                    verb = random.Next(0, Initializer.ContinousHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + person.Be[1] + " " + person.Continous[verb] + " " + person.Words[word];
                    Console.WriteLine(temp);
                    break;
                case 2:
                    verb = random.Next(0, Initializer.PastSimpleHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + person.PastSimple[verb] + " " + person.Words[word];
                    Console.WriteLine(temp);
                    break;
                case 3:
                    verb = random.Next(0, Initializer.PresentSimpleHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + person.PresentSimple[verb] + " " + person.Words[word];
                    Console.WriteLine(temp);
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
