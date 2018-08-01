using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _33_Generate_ASCII_tree
{
    class Globals
    {
        public static char[,] Cells { get; set; }

        public static void Initialize(int x, int y)
        {
            Cells = new char[x, y];
        }
    }

    class Branch
    {
        bool Side { get; set; }
        char[] Content { get; set; }
        int CurrentX { get; set; }
        int CurrentY { get; set; }

        // false = left, true = right
        public Branch(bool side, char[] content, Random random)
        {
            Side = side;
            Content = content;
            CurrentY = random.Next(0, Globals.Cells.GetUpperBound(1));
            if (!side)
            {
                CurrentX = (Globals.Cells.GetUpperBound(0) + 1) / 2 - 1;
                CreateBranch(random, -1);
            }
            else
            {
                CurrentX = (Globals.Cells.GetUpperBound(0) + 1) / 2 + 1;
                CreateBranch(random, 1);
            }                
        }

        void CreateBranch(Random random, int side)
        {
            int lastCellVertical = 0;
            int lastCellHorizontal = 0;
            bool lastSpecial = false;
            for (int i = 0; i < random.Next(5, Globals.Cells.GetUpperBound(1)); i++)
            {
                int option = random.Next(0, 3);
                if (i == 0 || lastSpecial)
                    option = 0;
                if (option == 0)
                {
                    try
                    {
                        if (Globals.Cells[CurrentX + i * side, CurrentY - i] == '\0')
                        {
                            Globals.Cells[CurrentX + i * side, CurrentY - i] = Content[0];
                            lastCellVertical = 0;
                            lastCellHorizontal = 0;
                            lastSpecial = false;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { return; }
                }
                else if (option == 1)
                {
                    try
                    {
                        if (Globals.Cells[CurrentX + (i - lastCellVertical) * side, CurrentY - i] == '\0')
                        {
                            Globals.Cells[CurrentX + (i - lastCellVertical) * side, CurrentY - i] = Content[1];
                            lastCellVertical = 1;
                            lastSpecial = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { return; }
                }
                else if (option == 2)
                {
                    try
                    {
                        if (Globals.Cells[CurrentX + i * side, CurrentY - i + lastCellHorizontal] == '\0')
                        {
                            Globals.Cells[CurrentX + i * side, CurrentY - i + lastCellHorizontal] = Content[2];
                            lastCellHorizontal = 1;
                            lastSpecial = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    { return; }
                }
            }
        }
    }

    class Tree
    {
        int Width { get; set; }
        int Height { get; set; }

        public Tree()
        {
            Random random = new Random();
            Width = random.Next(10, Console.WindowWidth);
            Height = random.Next(10, Console.WindowHeight);
            Globals.Initialize(Width, Height);
            CreateTrunk();
            CreateBranches(random);
        }

        void CreateTrunk()
        {
            for (int y = 0; y < Height; y++)
                Globals.Cells[Width / 2, y] = '|';
        }

        void CreateBranches(Random random)
        {
            for (int i = 0; i < random.Next(Height, Height * 3 / 2); i++)
            {
                int side = random.Next(0, 2);
                if (side == 0)
                    new Branch(false, new char[] { '\\', '|', '-' }, random);
                else
                    new Branch(true, new char[] { '/', '|', '-' }, random);
            }
        }

        public void DisplayTree()
        {
            Console.WriteLine("\n");
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write(Globals.Cells[x, y]);
                Console.WriteLine();
            }
            for (int i = 0; i < Width; i++)
                Console.Write("^");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();

            tree.DisplayTree();

            Console.ReadLine();
        }
    }
}
