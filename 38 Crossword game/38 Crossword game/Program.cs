using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _38_Crossword_game
{
    class Word
    {
        public string Content { get; set; }
        public string Hint { get; set; }
        public int SolutionId { get; set; }
        public bool Guessed { get; set; }

        public Word(string content, string hint, int solutionId)
        {
            Content = content;
            Hint = hint;
            SolutionId = solutionId;
            Guessed = false;
        }
    }

    class Crossword
    {
        char[,] Fields { get; set; }
        List<Word> Words { get; set; }
        int SolutionCol { get; set; }
        List<string> History { get; set; }

        #region Initialization
        public Crossword()
        {
            Words = new List<Word>();
            InitializeWords();
            InitializeFields();
            History = new List<string>();
        }

        void InitializeWords()
        {
            Words.Add(new Word("SILVER", "192,192,192", 0));
            Words.Add(new Word("ORANGE", "255,165,0", 2));
            Words.Add(new Word("MAGENTA", "255,0,255", 0));
            Words.Add(new Word("PURPLE", "128,0,128", 3));
            Words.Add(new Word("YELLOW", "255,255,0", 2));
            Words.Add(new Word("GREEN", "0,128,0", 3));
        }

        void InitializeFields()
        {
            int width = 0, mostLeft = 0, mostRight = 0;
            for (int i = 0; i < Words.Count; i++)
            {
                if (mostLeft < Words[i].SolutionId)
                    mostLeft = Words[i].SolutionId;
                if (mostRight < Words[i].Content.Length - (Words[i].SolutionId))
                    mostRight = Words[i].Content.Length - Words[i].SolutionId;
            }
            width = mostLeft + mostRight;
            SolutionCol = mostLeft;

            Fields = new char[width, Words.Count];

            for (int y = 0; y < Words.Count; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x >= SolutionCol - Words[y].SolutionId && x < SolutionCol - Words[y].SolutionId + Words[y].Content.Length)
                    {
                        Fields[x, y] = '*';
                        if (x == SolutionCol)
                            Fields[x, y] = '?';
                        if (Fields[x, y] == ' ')
                            Fields[x, y] = '_';
                    }
                }
            }
        }
        #endregion

        public void Start()
        {
            do
            {
                DisplayCrossword();
                DisplayHints();
                DisplayHistory();
                GetInput();
                Console.Clear();
            } while (!CheckIfSolved());
            DisplayCrossword();
            Console.WriteLine("Congratulations, you win");
        }

        void DisplayCrossword()
        {
            for (int y = 0; y <= Fields.GetUpperBound(1); y++)
            {
                Console.Write(y + " ");
                if (Words[y].Guessed)
                {
                    int currId = 0;
                    for (int x = 0; x <= Fields.GetUpperBound(0); x++)
                    {
                        if (x >= SolutionCol - Words[y].SolutionId && x < SolutionCol - Words[y].SolutionId + Words[y].Content.Length)
                        {
                            Fields[x, y] = Words[y].Content[currId];
                            currId++;
                            if (x == ' ')
                                Fields[x, y] = '_';
                        }
                    }
                }

                for (int x = 0; x < Fields.GetUpperBound(0); x++)
                    Console.Write(Fields[x, y]);

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        void DisplayHints()
        {
            for (int i = 0; i < Words.Count; i++)
                Console.WriteLine(i + " " + Words[i].Hint);
            Console.WriteLine();
        }

        void DisplayHistory()
        {
            foreach (var element in History)
                Console.WriteLine(element);
        }

        void GetInput()
        {
            string[] input = Console.ReadLine().ToUpper().Split(' ');
            foreach (var word in input)
                word.Replace('_', ' ');

            if (input.Length == 2)
            {
                if (int.Parse(input[0]) >= 0 && int.Parse(input[0]) < Words.Count)
                {
                    if (input[1] == Words[int.Parse(input[0])].Content)
                    {
                        History.Add("You're right");
                        Words[int.Parse(input[0])].Guessed = true;
                    }
                    else
                        History.Add("You're wrong");
                }
                else
                    History.Add("Index out of range");
            }
            else
                History.Add("Unknown command");
        }

        bool CheckIfSolved()
        {
            foreach (Word word in Words)
                if (word.Guessed == false)
                    return false;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Crossword crossword = new Crossword();

            crossword.Start();

            Console.ReadLine();
        }
    }
}
