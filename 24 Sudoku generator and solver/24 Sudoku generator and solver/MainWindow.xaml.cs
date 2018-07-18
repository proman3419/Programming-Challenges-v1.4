using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _24_Sudoku_generator_and_solver
{
    class Field : TextBlock
    {
        public int SubGrid { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
        public List<int> ForbiddenValues { get; set; }

        public Field(int subGrid, int row, int column)
        {
            SubGrid = subGrid;
            Row = row;
            Column = column;
            ForbiddenValues = new List<int>();
        }
    }

    class ProgramManager
    {
        Grid Board { get; set; }
        double FieldSize { get; set; }
        Field[] Fields { get; set; }
        List<int>[] SubGridsValues { get; set; }
        List<int>[] RowsValues { get; set; }
        List<int>[] ColumnsValues { get; set; }
        List<Field> History { get; set; }

        public ProgramManager(Grid board)
        {
            Board = board;
            FieldSize = Board.Width / 9 - 2;
            Fields = new Field[81];
            SubGridsValues = new List<int>[9];
            InitializeAnArrayOfLists(SubGridsValues);
            RowsValues = new List<int>[9];
            InitializeAnArrayOfLists(RowsValues);
            ColumnsValues = new List<int>[9];
            InitializeAnArrayOfLists(ColumnsValues);
            History = new List<Field>();
            CreateBoard();
        }

        void InitializeAnArrayOfLists(List<int>[] arrayOfLists)
        {
            for (int i = 0; i < arrayOfLists.Length; i++)
                arrayOfLists[i] = new List<int>();
        }

        #region Create board
        public void CreateBoard()
        {
            int currentId = 0;
            for (int i = 0; i < 9; i++)
            {
                foreach (WrapPanel child in Board.Children)
                {
                    if (int.Parse(child.Tag.ToString()) == i)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            int row = GetRow(i, j);
                            int column = GetColumn(currentId, i);
                            Fields[currentId] = new Field(i, row, column);
                            Fields[currentId].Width = FieldSize;
                            Fields[currentId].Height = FieldSize;
                            Fields[currentId].FontSize = 37;
                            Fields[currentId].TextAlignment = TextAlignment.Center;
                            Fields[currentId].Background = GetColor(i);
                            Fields[currentId].Margin = new Thickness(1);
                            
                            child.Children.Add(Fields[currentId]);
                            currentId++;
                        }
                    }
                }
            }
        }

        Brush GetColor(int subGrid)
        {
            if (subGrid == 0)
                return Brushes.LightYellow;
            if (subGrid == 1)
                return Brushes.LightPink;
            if (subGrid == 2)
                return Brushes.LightSalmon;
            if (subGrid == 3)
                return Brushes.LightCyan;
            if (subGrid == 4)
                return Brushes.LightGreen;
            if (subGrid == 5)
                return Brushes.LightSeaGreen;
            if (subGrid == 6)
                return Brushes.LightBlue;
            if (subGrid == 7)
                return Brushes.LightSkyBlue;
            if (subGrid == 8)
                return Brushes.LightSteelBlue;
            return Brushes.White;
        }

        int GetRow(int subGrid, int fieldInSubGrid)
        {
            if (0 <= subGrid && subGrid < 3)
            {
                if (fieldInSubGrid < 3)
                    return 0;
                if (fieldInSubGrid < 6)
                    return 1;
                if (fieldInSubGrid < 9)
                    return 2;
            }
            if (3 <= subGrid && subGrid < 6)
            {
                if (fieldInSubGrid < 3)
                    return 3;
                if (fieldInSubGrid < 6)
                    return 4;
                if (fieldInSubGrid < 9)
                    return 5;
            }
            if (6 <= subGrid && subGrid < 9)
            {
                if (fieldInSubGrid < 3)
                    return 6;
                if (fieldInSubGrid < 6)
                    return 7;
                if (fieldInSubGrid < 9)
                    return 8;
            }
            return 0;
        }

        int GetColumn(int currentId, int subGrid)
        {
            if (subGrid % 3 == 0)
            {
                if (currentId % 3 == 0)
                    return 0;
                if (currentId % 3 == 1)
                    return 1;
                if (currentId % 3 == 2)
                    return 2;
            }
            if (subGrid % 3 == 1)
            {
                if (currentId % 3 == 0)
                    return 3;
                if (currentId % 3 == 1)
                    return 4;
                if (currentId % 3 == 2)
                    return 5;
            }
            if (subGrid % 3 == 2)
            {
                if (currentId % 3 == 0)
                    return 6;
                if (currentId % 3 == 1)
                    return 7;
                if (currentId % 3 == 2)
                    return 8;
            }
            return 0;
        }
        #endregion

        #region Create sudoku
        public void CreateSudoku()
        {
            int currentId = 0, value = 0;
            while (currentId < 20)
            {
                GetRandomValue(currentId, ref value);
                if (!CheckConflicts(currentId, value))
                    currentId++;
            }
        }

        void GetRandomValue(int currentId, ref int value)
        {
            Random random = new Random();
            int stuck = 0;
            while (true)
            {
                value = random.Next(1, 10);
                if (!Fields[currentId].ForbiddenValues.Contains(value))
                    break;
                //Debug.WriteLine(currentId+"-----------------------------------------------");
                //foreach (var element in SubGridsValues[Fields[currentId].SubGrid])
                //    Debug.WriteLine("grid: "+element);
                //foreach (var element in RowsValues[Fields[currentId].Row])
                //    Debug.WriteLine("row: "+element);
                //foreach (var element in ColumnsValues[Fields[currentId].Column])
                //    Debug.WriteLine("column: "+element);
                stuck++;
                if (stuck > 50)
                {
                    //Undo(currentId, value);
                    //currentId--;
                    stuck = 0;
                }
            }
        }

        bool CheckConflicts(int currentId, int value)
        {
            if (!SubGridConflict(currentId, value) && !RowConflict(currentId, value) && !ColumnConflict(currentId, value))
            {
                SubGridsValues[Fields[currentId].SubGrid].Add(value);
                RowsValues[Fields[currentId].Row].Add(value);
                ColumnsValues[Fields[currentId].Column].Add(value);

                History.Add(new Field(Fields[currentId].SubGrid, Fields[currentId].Row, Fields[currentId].Column));
                
                Fields[currentId].Value = value;
                UpdateBoard(currentId);
                return false;
            }
            if (!Fields[currentId].ForbiddenValues.Contains(value))
                Fields[currentId].ForbiddenValues.Add(value);
            return true;
        }

        void Undo(int currentId, int value)
        {
            // Add the wrong value to the ForbiddenValues list of the field
            if (!Fields[currentId].ForbiddenValues.Contains(value))
                Fields[currentId].ForbiddenValues.Add(value);
            // Remove the value from the field
            Fields[currentId].Value = 0;
            // Remove the previously added values (to SubGridsValues, RowsValues, ColumnsValues)
            SubGridsValues[History[History.Count - 1].SubGrid].RemoveAt(SubGridsValues[History[History.Count - 1].SubGrid].Count - 1);
            RowsValues[History[History.Count - 1].Row].RemoveAt(RowsValues[History[History.Count - 1].Row].Count - 1);
            ColumnsValues[History[History.Count - 1].Column].RemoveAt(ColumnsValues[History[History.Count - 1].Column].Count - 1);
            // Remove the last record in the history
            History.RemoveAt(History.Count - 1);
        }

        #region Check conflicts
        bool SubGridConflict(int currentId, int value)
        {
            if (SubGridsValues[Fields[currentId].SubGrid].Contains(value))
                return true;
            return false;
        }

        bool RowConflict(int currentId, int value)
        {
            if (RowsValues[Fields[currentId].Row].Contains(value))
                return true;
            return false;
        }

        bool ColumnConflict(int currentId, int value)
        {
            if (ColumnsValues[Fields[currentId].Column].Contains(value))
                return true;
            return false;
        }
        #endregion

        void UpdateBoard(int fieldId)
        {
            Fields[fieldId].Text = Fields[fieldId].Value.ToString();
        }
        #endregion
    }

    public partial class MainWindow : Window
    {
        ProgramManager programManager;

        public MainWindow()
        {
            InitializeComponent();

            programManager = new ProgramManager(Board);
        }

        #region Buttons
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            programManager.CreateSudoku();
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
