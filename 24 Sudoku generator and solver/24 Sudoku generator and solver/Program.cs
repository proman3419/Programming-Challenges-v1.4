using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _24_Sudoku_generator_and_solver
{
    class Field
    {
        public int Value { get; set; }
        public int SubGrid { get; set; }

        public Field(int row, int column)
        {
            DetermineSubGrid(row, column);
        }

        void DetermineSubGrid(int row, int column)
        {
            if (row <= 2)
            {
                if (column <= 2)
                    SubGrid = 0;
                else if (column <= 5)
                    SubGrid = 1;
                else
                    SubGrid = 2;
            }
            else if (row <= 5)
            {
                if (column <= 2)
                    SubGrid = 3;
                else if (column <= 5)
                    SubGrid = 4;
                else
                    SubGrid = 5;
            }
            else
            {
                if (column <= 2)
                    SubGrid = 6;
                else if (column <= 5)
                    SubGrid = 7;
                else
                    SubGrid = 8;
            }
        }
    }

    class SudokuGenerator
    {
        Field[,] Fields { get; set; }
        Thread ResetIfStuckForever { get; set; }
        bool StuckForever { get; set; }

        public SudokuGenerator()
        {
            Fields = new Field[9, 9];
            StuckForever = false;
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    Fields[i, j] = new Field(i, j);
            StuckForever = false;
            ResetIfStuckForever = new Thread(() => { StuckForever = true; });
        }

        public bool Generate()
        {
            Random random = new Random();
            int value, stuck = 0, timeLimit = 100;
            Stopwatch timer = new Stopwatch();

            timer.Start();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    while (true)
                    {
                        value = GetRandomDigit(random);
                        if (!SubGridConflict(i, j, value) && !RowConflict(i, value) && !ColumnConflict(j, value))
                        {
                            Fields[i, j].Value = value;
                            stuck = 0;
                            break;
                        }
                        else
                        {
                            stuck++;
                            if (stuck >= 50)
                            {
                                Undo(ref i, ref j);
                                stuck = 0;
                            }
                        }

                        if (timer.ElapsedMilliseconds >= timeLimit)
                        {
                            if (ResetIfStuckForever != null && ResetIfStuckForever.ThreadState
                                == System.Threading.ThreadState.Unstarted)
                            {
                                ResetIfStuckForever.Start();
                                ResetIfStuckForever = null;
                            }
                        }

                        if (StuckForever)
                            return false;
                    }
                }
            }
            foreach (Field field in Fields)
                if (field.Value == 0)
                    return false;

            Display();
            return true;
        }

        int GetRandomDigit(Random random)
        {
            return random.Next(0, 10);
        }

        void Undo(ref int row, ref int column)
        {
            Fields[row, column].Value = 0;
            if (column == 0)
            {
                row--;
                column = 8;
            }
            else { column--; }
        }

        #region Conflicts
        bool SubGridConflict(int row, int column, int value)
        {
            foreach (Field field in Fields)
            {
                if (field.SubGrid == Fields[row, column].SubGrid)
                    if (field.Value == value)
                        return true;
            }
            return false;
        }

        bool RowConflict(int row, int value)
        {
            for (int j = 0; j < 9; j++)
                if (Fields[row, j].Value == value)
                    return true;
            return false;
        }

        bool ColumnConflict(int column, int value)
        {
            for (int i = 0; i < 9; i++)
                if (Fields[i, column].Value == value)
                    return true;
            return false;
        }
        #endregion

        void Display()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write(Fields[i, j].Value + " ");
                Console.WriteLine("\n");
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SudokuGenerator sudokuGenerator = new SudokuGenerator();
            if (!sudokuGenerator.Generate())
                do
                {
                    sudokuGenerator.Reset();
                } while (!sudokuGenerator.Generate());

            Console.ReadKey();
        }
    }
}
