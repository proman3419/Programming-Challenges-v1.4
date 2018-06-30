using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Haiku_generator
{
    interface IPerson
    {
        Word[] Name { get; set; }
        Word Be { get; set; }
        Word[] PresentSimple { get; set; }

        int NameHighestId { get; set; }
    }

    class Word
    {
        public string Content { get; set; }
        public int Syllables { get; set; }

        public Word(string content, int syllables)
        {
            Content = content;
            Syllables = syllables;
        }
    }

    static class Initializer
    {
        public static Word[] Continous { get; set; }
        public static Word[] Epithets { get; set; }

        public static int PresentSimpleHighestId { get; set; }
        public static int PresentSimpleSHighestId { get; set; }
        public static int ContinousHighestId { get; set; }
        public static int EpithetsHighestId { get; set; }

        static public Word[] InitializePresentSimple()
        {
            Word[] temp = new Word[] { new Word("kill", 1), new Word("fade", 1), new Word("drink", 1), new Word("hide", 1),
                new Word("lose", 1), new Word("fall", 1), new Word("look", 1), new Word("shine", 1), new Word("mock", 1), new Word("light", 1)};
            PresentSimpleHighestId = temp.GetUpperBound(0);
            return temp;
        }

        static public Word[] InitializePresentSimpleS()
        {
            Word[] temp = new Word[] { new Word("kills", 1), new Word("fades", 1), new Word("drinks", 1), new Word("hides", 1),
                new Word("loses", 1), new Word("falls", 1), new Word("looks", 1), new Word("shines", 1), new Word("mocks", 1), new Word("lights", 1)};
            PresentSimpleSHighestId = temp.GetUpperBound(0);
            return temp;
        }

        static public void InitializeContinous()
        {
            Continous = new Word[] { new Word("killing", 2), new Word("fading", 2), new Word("drinking", 2), new Word("hiding", 2),
                new Word("losing", 2), new Word("falling", 2), new Word("looking", 2), new Word("shining", 2), new Word("mocking", 2),
                new Word("lightening", 3)};
            ContinousHighestId = Continous.GetUpperBound(0);
        }

        static public void InitializeEpithets()
        {
            Epithets = new Word[] { new Word("astounding", 3), new Word("astonishing", 4), new Word("breathtaking", 3), new Word("enchanted", 3),
                new Word("forbidden", 3), new Word("fabulous", 3), new Word("fearless", 2), new Word("epic", 2), new Word("charming", 2),
                new Word("old", 1), new Word("nasty", 2), new Word("poisonous", 3), new Word("rude", 1), new Word("wild", 1)};
            EpithetsHighestId = Epithets.GetUpperBound(0);
        }
    }

    class HeSheIt : IPerson
    {
        public Word[] Name { get; set; }
        public Word Be { get; set; }
        public Word[] PresentSimple { get; set; }

        public int NameHighestId { get; set; }

        public HeSheIt()
        {
            Name = new Word[] { new Word("The sky", 2), new Word("Ocean", 2), new Word("Black Dahlia", 3), new Word("The atmosphere", 4),
                new Word("Sunset", 2), new Word("Nature", 2), new Word("Music", 2), new Word("Man", 1), new Word("Brain", 1), new Word("Fox", 1),
                new Word("Earth", 1), new Word("Sun", 1), new Word("Moon", 1)};
            Be = new Word("is", 1);
            PresentSimple = Initializer.InitializePresentSimpleS();
            Initializer.InitializeContinous();
            Initializer.InitializeEpithets();
            NameHighestId = Name.GetUpperBound(0);
        }
    }

    class They : IPerson
    {
        public Word[] Name { get; set; }
        public Word Be { get; set; }
        public Word[] PresentSimple { get; set; }
        public Word[] Continous { get; set; }
        public Word[] Epithets { get; set; }

        public int NameHighestId { get; set; }

        public They()
        {
            Name = new Word[] { new Word("Birds", 1), new Word("People", 2), new Word("Grain fields", 2), new Word("Flowers", 2),
                new Word("Icicles", 3), new Word("Guts", 1), new Word("Bees", 1), new Word("Leaves", 1), new Word("Clouds", 1),
                new Word("Cats", 1), new Word("Hills", 1)};
            Be = new Word("are", 1);
            PresentSimple = Initializer.InitializePresentSimple();
            Initializer.InitializeContinous();
            Initializer.InitializeEpithets();
            NameHighestId = Name.GetUpperBound(0);
        }
    }

    class HaikuGenerator
    {
        public dynamic SelectPerson()
        {
            Random random = new Random();
            int choose = random.Next(0, 2);
            switch (choose)
            {
                case 1:
                    return new HeSheIt();
                default:
                    return new They();
            }
        }

        public string CreateOption0(dynamic person, int syllablesMax)
        {
            Random random = new Random();
            int[] temp = new int[3];
            while (true)
            {
                temp[0] = random.Next(0, person.NameHighestId);
                if(person.GetType().Name == "HeSheIt")
                    temp[1] = random.Next(0, Initializer.PresentSimpleSHighestId);
                else
                    temp[1] = random.Next(0, Initializer.PresentSimpleHighestId);
                temp[2] = random.Next(0, Initializer.EpithetsHighestId);
                int test = person.Name[temp[0]].Syllables + person.PresentSimple[temp[1]].Syllables + Initializer.Epithets[temp[2]].Syllables;
                if (test == syllablesMax)
                    return person.Name[temp[0]].Content + " " + person.PresentSimple[temp[1]].Content;
            }
        }

        public string CreateOption1(dynamic person, int syllablesMax)
        {
            Random random = new Random();
            int[] temp = new int[3];
            while (true)
            {
                temp[0] = random.Next(0, person.NameHighestId);
                temp[1] = random.Next(0, Initializer.ContinousHighestId);
                temp[2] = random.Next(0, Initializer.EpithetsHighestId);
                if (person.Name[temp[0]].Syllables + person.Be.Syllables + Initializer.Continous[temp[1]].Syllables + Initializer.Epithets[temp[2]].Syllables == syllablesMax)
                    return person.Name[temp[0]].Content + " " + person.Be.Content + " " + Initializer.Continous[temp[1]].Content;
            }
        }

        public string CreateOption2(dynamic person, int syllablesMax)
        {
            Random random = new Random();
            int[] temp = new int[2];
            while (true)
            {
                temp[0] = random.Next(0, person.NameHighestId);
                temp[1] = random.Next(0, Initializer.EpithetsHighestId);
                if (person.Name[temp[0]].Syllables + person.Be.Syllables + Initializer.Epithets[temp[1]].Syllables == syllablesMax)
                    return person.Name[temp[0]].Content + " " + person.Be.Content + " " + Initializer.Epithets[temp[1]].Content;
            }
        }

        public string CreateSentence(dynamic person, int syllablesMax)
        {
            Random random = new Random();
            int option = random.Next(0, 3);
            string temp;
            switch (option)
            {
                case 0:
                    temp = CreateOption0(person, syllablesMax);
                    break;
                case 1:
                    temp = CreateOption1(person, syllablesMax);
                    break;
                case 2:
                    temp = CreateOption2(person, syllablesMax);
                    break;
                default:
                    temp = "The program is broken";
                    break;
            }
            return temp;
        }

        public void CreateHaiku()
        {
            Console.WriteLine("The haiku will be ready in 30 seconds");
            string[] temp = new string[3];
            temp[0] = CreateSentence(SelectPerson(), 5);
            System.Threading.Thread.Sleep(15000);
            temp[1] = CreateSentence(SelectPerson(), 7);
            System.Threading.Thread.Sleep(15000);
            temp[2] = CreateSentence(SelectPerson(), 5);
            Console.Clear();
            foreach (string element in temp)
                Console.WriteLine(element);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HaikuGenerator haikuGenerator = new HaikuGenerator();
            haikuGenerator.CreateHaiku();
            Console.ReadLine();
        }
    }
}
