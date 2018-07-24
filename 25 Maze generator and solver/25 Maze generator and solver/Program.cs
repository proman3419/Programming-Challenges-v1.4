using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_Maze_generator_and_solver
{
    class Cell
    {
        int Row { get; set; }
        int Column { get; set; }
        public bool Visited { get; set; }
        public string Content { get; set; }
        public List<int[]> Neighbours { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Neighbours = new List<int[]>();
        }

        public void GetNeighbours(Cell[,] cells)
        {
            // Initialization of the Visited property is used to verify if the cell exists
            try { cells[Row - 1, Column].Visited = false; Neighbours.Add(new int[] { Row - 1, Column }); } catch { }
            try { cells[Row + 1, Column].Visited = false; Neighbours.Add(new int[] { Row + 1, Column }); } catch { }
            try { cells[Row, Column - 1].Visited = false; Neighbours.Add(new int[] { Row, Column - 1 }); } catch { }
            try { cells[Row, Column + 1].Visited = false; Neighbours.Add(new int[] { Row, Column + 1 }); } catch { }
        }

        public bool HaveAnUnvisitedNeighbour(Cell[,] cells)
        {
            for (int i = 0; i < Neighbours.Count; i++)
                for (int j = 0; j < 2; j++)
                    if (!cells[i, j].Visited)
                        return true;
            return false;
        }

        public Cell GetRandomNeighbour(Cell[,] cells)
        {
            for (int i = 0; i < Neighbours.Count; i++)
                for (int j = 0; j < 2; j++)
                    if (!cells[i, j].Visited)
                        return cells[i, j];
            return cells[0, 0];
        }
    }

    class RecursiveBacktracker
    {
        Cell[,] Cells { get; set; }
        int CellsInRow { get; set; }
        int CellsInColumn { get; set; }
        Cell CurrentCell { get; set; }
        Stack<Cell> History { get; set; }

        public RecursiveBacktracker()
        {
            CellsInRow = 3;
            CellsInColumn = 4;

            CurrentCell = new Cell(0,0);

            Cells = new Cell[CellsInColumn, CellsInRow];
            for (int i = 0; i < CellsInColumn; i++)
                for (int j = 0; j < CellsInRow; j++)
                    Cells[i, j] = new Cell(i, j);

            for (int i = 0; i < CellsInColumn; i++)
                for (int j = 0; j < CellsInRow; j++)
                    Cells[i, j].GetNeighbours(Cells);
        }



        public void Generate()
        {
            while (!AllVisited())
            {
                if (CurrentCell.HaveAnUnvisitedNeighbour(Cells))
                {
                    History.Push(CurrentCell);
                    CurrentCell = CurrentCell.GetRandomNeighbour(Cells);
                }
            }
        }

        bool AllVisited()
        {
            foreach (Cell cell in Cells)
                if (!cell.Visited)
                    return false;
            return true;
        }

        void Display()
        {
            for (int i = 0; i < CellsInColumn; i++)
            {
                for (int j = 0; j < CellsInRow; j++)
                    Console.Write(Cells[i, j].Content);
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RecursiveBacktracker recursiveBacktracker = new RecursiveBacktracker();

            Console.ReadLine();
        }
    }
}
