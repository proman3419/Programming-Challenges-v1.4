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
        int Value { get { return Value; } set { Value = value; } }
        public int value;
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

    class SudokuGeneratorSolver
    {
        Field[,] Fields { get; set; }
        Thread ResetIfStuckForever { get; set; }
        bool StuckForever { get; set; }

        public SudokuGeneratorSolver()
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

        void Display()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write(Fields[i, j].value + " ");
                Console.WriteLine("\n");
            }
        }

        bool BruteForce(bool solve)
        {
            Random random = new Random();
            int value, stuck = 0, timeLimit;
            Stopwatch timer = new Stopwatch();

            if (solve)
                timeLimit = 100000;
            else
                timeLimit = 100;

            timer.Start();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Fields[i, j].value == 0)
                    {
                        while (true)
                        {
                            value = GetRandomDigit(random);
                            if (!SubGridConflict(i, j, value) && !RowConflict(i, value) && !ColumnConflict(j, value))
                            {
                                Fields[i, j].value = value;
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
            }
            return true;
        }

        #region Generator
        public bool Generate()
        {
            if (BruteForce(false))
            {
                Display();
                return true;
            }
            else
                return false;
        }

        int GetRandomDigit(Random random)
        {
            return random.Next(0, 10);
        }

        void Undo(ref int row, ref int column)
        {
            Fields[row, column].value = 0;
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
                    if (field.value == value)
                        return true;
            }
            return false;
        }

        bool RowConflict(int row, int value)
        {
            for (int j = 0; j < 9; j++)
                if (Fields[row, j].value == value)
                    return true;
            return false;
        }

        bool ColumnConflict(int column, int value)
        {
            for (int i = 0; i < 9; i++)
                if (Fields[i, column].value == value)
                    return true;
            return false;
        }
        #endregion
        #endregion

        #region Solver
        public void Solve()
        {
            if (GetInput() >= 17)
            {
                if (BruteForce(true))
                {
                    Console.WriteLine("\nSolution");
                    Display();
                    return;
                }
            }
            Console.WriteLine("\nI can't solve this");
        }

        int GetInput()
        {
            Console.WriteLine("Pass all fields' values, if the value is unknown input 0");
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    Console.WriteLine("\nRow: {0} Column {1}", i, j);
                    while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out Fields[i, j].value) ||
                        !(0 <= Fields[i, j].value) || !(Fields[i, j].value <= 9))
                        Console.WriteLine("\nWrong input");
                }
            int passedValues = 0;
            foreach (Field field in Fields)
                if (field.value > 0)
                    passedValues++;
            return passedValues;
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            SudokuGeneratorSolver sudokuGeneratorSolver = new SudokuGeneratorSolver();

            Console.WriteLine("1 Generate");
            Console.WriteLine("2 Solve");
            switch (GetInput())
            {
                case 1:
                    if (!sudokuGeneratorSolver.Generate())
                        do
                        {
                            sudokuGeneratorSolver.Reset();
                        } while (!sudokuGeneratorSolver.Generate());
                    break;
                case 2:
                    sudokuGeneratorSolver.Solve();
                    break;
            }
            Console.ReadLine();
        }

        static int GetInput()
        {
            int temp;
            while(!int.TryParse(Console.ReadKey().KeyChar.ToString(), out temp) || temp < 1 || temp > 2)
                Console.WriteLine("\nWrong input");
            Console.WriteLine();
            return temp;
        }
    }
}
