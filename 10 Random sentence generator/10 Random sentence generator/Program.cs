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
        int NameHighestId { get; set; }
    }

    class WordBase
    {
        public string[] PastSimple { get; set; }
        public string[] PresentSimple { get; set; }
        public string[] PresentSimpleS { get; set; }
        public string[] Continous { get; set; }
        public string[] Words { get; set; }

        public int PastSimpleHighestId { get; set; }
        public int PresentSimpleHighestId { get; set; }
        public int PresentSimpleSHighestId { get; set; }
        public int ContinousHighestId { get; set; }
        public int WordsHighestId{ get; set; }

        public WordBase()
        {
            InitializePastSimple();
            InitializePresentSimple();
            InitializePresentSimpleS();
            InitializeContinous();
            InitializeWords();
        }

        #region Initializers
        public void InitializePastSimple()
        {
            PastSimple = new string[] { "killed", "ate", "edited", "walked by", "walked down", "missed", "added", "created", "drinked", "burnt", "fried", "looted", "drived",
            "climbed", "destroyed", "described", "fought with", "passed"};
            PastSimpleHighestId = PastSimple.GetUpperBound(0);
        }

        public void InitializePresentSimple()
        {
            PresentSimple = new string[] { "kill", "eat", "edit", "walk by", "walk down", "miss", "add", "create", "drink", "burn",
                "fry", "loot", "drive", "climb", "destroy", "describe", "fight with", "pass" };
            PresentSimpleHighestId = PresentSimple.GetUpperBound(0);
        }

        public void InitializePresentSimpleS()
        {
            PresentSimpleS = new string[] { "kills", "eats", "edits", "walks by", "walks down", "misses", "adds", "creates",
                "drinks", "burns", "fries", "loots", "drives", "climbs", "destroys", "describes", "fights with", "passes" };
        }

        public void InitializeContinous()
        {
            Continous = new string[] { "killing", "eating", "editing", "walking by", "walking down", "missing", "adding", "creating",
                "drinking", "burning", "looting", "driving", "climbing", "destroying", "describing", "fighting with", "passing" };
            ContinousHighestId = Continous.GetUpperBound(0);
        }

        public void InitializeWords()
        {
            Words = new string[] {"a homeless cat", "a damaged car", "a kangaroo from Australia", "an important text", "a dinner", "an apple",
                "a house with a green roof", "water", "the nearby woods", "the longest river in the country", "fries", "a machine oil", "the bag of a passerby",
                "a machine gun", "a massive rock", "an angry bear", "a bloodthirsty wolf", "a sleepy panda", "an annoying kid", "a fancy dress",
                "a delicious dish", "a dried out river", "a deep canyon", "a steep hill"};
            WordsHighestId = Words.GetUpperBound(0);
        }
        #endregion
    }

    #region Persons
    class I : IPerson
    {
        public string[] Name { get; set; }
        public string[] Be { get; set; }
        public int NameHighestId { get; set; }        

        public I()
        {
            Name = new string[] { "I" };
            Be = new string[] { "was", "am" };
            NameHighestId = Name.GetUpperBound(0);
        }
    }

    class HeSheIt : IPerson
    {
        public string[] Name { get; set; }
        public string[] Be { get; set; }
        public int NameHighestId { get; set; }

        public HeSheIt()
        {
            Name = new string[] { "He", "She", "It" };
            Be = new string[] { "was", "is"};
            NameHighestId = Name.GetUpperBound(0);
        }
    }

    class WeTheyYou : IPerson
    {
        public string[] Name { get; set; }
        public string[] Be { get; set; }
        public int NameHighestId { get; set; }

        public WeTheyYou()
        {
            Name = new string[] { "We", "They", "You" };
            Be = new string[] { "were", "are" };
            NameHighestId = Name.GetUpperBound(0);
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
            int choose = random.Next(0, 3);
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
            WordBase wordBase = new WordBase();
            int tense = random.Next(0, 4);
            int word = random.Next(0, wordBase.WordsHighestId);
            int verb, name;
            string temp;
            tense = 1;
            switch (tense)
            {
                case 0: //Past Simple
                    verb = random.Next(0, wordBase.PastSimpleHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + wordBase.PastSimple[verb] + " " + wordBase.Words[word];
                    break;
                case 1: //Present Simple
                    verb = random.Next(0, wordBase.PresentSimpleHighestId);
                    name = random.Next(0, person.NameHighestId);
                    if(person.GetType().Name == "HeSheIt")
                        temp = person.Name[name] + " " + wordBase.PresentSimpleS[verb] + " " + wordBase.Words[word];
                    else
                        temp = person.Name[name] + " " + wordBase.PresentSimple[verb] + " " + wordBase.Words[word];
                    break;
                case 2: //Past Continous
                    verb = random.Next(0, wordBase.ContinousHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + person.Be[0] + " " + wordBase.Continous[verb] + " " + wordBase.Words[word];
                    break;
                case 3: //Present Continous
                    verb = random.Next(0, wordBase.ContinousHighestId);
                    name = random.Next(0, person.NameHighestId);
                    temp = person.Name[name] + " " + person.Be[1] + " " + wordBase.Continous[verb] + " " + wordBase.Words[word];
                    break;
                default:
                    temp = "The program is broken, contact the support";
                    break;
            }
            Console.WriteLine(temp);
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
