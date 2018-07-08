using System;
using System.Collections.Generic;
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

namespace _21_Connect_four
{
    class Field : TextBox
    {
        public int OccupiedBy { get; set; } // 0 - noone, 1 - yellow, 2 - red

        public Field()
        {
            OccupiedBy = 0;
        }

        public void Occupy(bool turn)
        {
            if (OccupiedBy == 0)
            {
                if (!turn)
                {
                    OccupiedBy = 1;
                    this.Background = Brushes.Yellow;
                }
                else
                {
                    OccupiedBy = 2;
                    this.Background = Brushes.Red;
                }
            }
        }
    }

    class Column : StackPanel
    {
        public StackPanel ColumnControl { get; set; }
        public int ColumnId { get; set; }
        int FieldsInAColumn { get; set; }
        public List<Field> Fields { get; set; }
        public int HighestNotOccupiedField { get; set; }

        public Column(StackPanel columnControl, int columnId, int fieldsInAColumn)
        {
            ColumnControl = columnControl;
            ColumnId = columnId;
            FieldsInAColumn = fieldsInAColumn - 1;
            Fields = new List<Field>(FieldsInAColumn);
            HighestNotOccupiedField = FieldsInAColumn;
        }

        public bool OccupyField(bool turn)
        {
            if (HighestNotOccupiedField >= 0)
            {
                Fields[HighestNotOccupiedField].Occupy(turn);
                HighestNotOccupiedField--;
                return true;
            }
            return false;
        }
    }

    class Game
    {
        Grid Board { get; set; }
        List<Column> Columns { get; set; } 
        TextBlock RoundResult { get; set; }
        TextBlock Score1 { get; set; }
        TextBlock Score2 { get; set; }
        float FieldSize { get; set; }
        int RowsAmount { get; set; }
        int ColumnsAmount { get; set; }
        bool Turn { get; set; }
        int[] Scores { get; set; }
        int[] LastMove { get; set; }
        bool RoundHasEnded { get; set; }

        public Game(Grid board, TextBlock roundResult, TextBlock score1, TextBlock score2)
        {
            Board = board;
            RoundResult = roundResult;
            Turn = false;
            RowsAmount = 6;
            Columns = new List<Column>();
            foreach (StackPanel columnControl in Board.Children)
            {
                Columns.Add(new Column(columnControl, Columns.Capacity, RowsAmount));
            }
            ColumnsAmount = Columns.Capacity - 1;
            FieldSize = (float)Board.Height / (RowsAmount + 1); // The additional row is the upper one with arrows
            Scores = new int[2];
            Score1 = score1;
            Score2 = score2;
            LastMove = new int[2];
            RoundHasEnded = false;

            CreateBoard();
        }

        void CreateBoard()
        {
            for (int i = 0; i < Columns.Capacity - 1; i++)
            {
                Field arrowField = new Field();
                SetFieldsProperties(arrowField);
                arrowField.FontSize = 35;
                arrowField.TextAlignment = TextAlignment.Center;
                arrowField.Text = "↓";
                arrowField.Tag = i;
                arrowField.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(FieldOccupy_Click);
                Columns[i].ColumnControl.Children.Add(arrowField);
                for (int j = 0; j < RowsAmount; j++) 
                {
                    Columns[i].Fields.Add(new Field());
                    SetFieldsProperties(Columns[i].Fields[j]);

                    Columns[i].ColumnControl.Children.Add(Columns[i].Fields[j]);
                }
            }
        }

        void SetFieldsProperties(Field field)
        {
            field.Height = FieldSize;
            field.Width = FieldSize;
            field.IsReadOnly = true;
            field.Cursor = Cursors.Arrow;
            field.Focusable = false;
        }

        void FieldOccupy_Click(object sender, MouseEventArgs e)
        {
            if (!RoundHasEnded)
            {
                int clickedColumnId = int.Parse(((TextBox)sender).Tag.ToString());
                if (Columns[clickedColumnId].OccupyField(Turn))
                {
                    LastMove[0] = clickedColumnId;
                    LastMove[1] = Columns[clickedColumnId].HighestNotOccupiedField + 1; // +1 because HighestNotOccupiedField is an empty field
                    Win();
                    Turn = !Turn;
                }
            }
        }

        #region Reset
        public void Reset(bool newGame)
        {            
            ResetBoard();
            CreateBoard();
            if (newGame)
            {
                Scores = new int[2];
                Score1.Text = "Player1: 0";
                Score2.Text = "Player2: 0";
            }
            Turn = false;
            RoundResult.Text = String.Empty;
            RoundHasEnded = false;
        }

        void ResetBoard()
        {
            foreach (var column in Columns)
                column.ColumnControl.Children.Clear();

            Columns.Clear();
            foreach (StackPanel columnControl in Board.Children)
                Columns.Add(new Column(columnControl, Columns.Capacity, RowsAmount));
        }
        #endregion

        #region Win
        void Win()
        {
            if (!CheckIfWin(LastMove))
                return;

            if (!Turn)
            {
                RoundResult.Text = "Player1 wins the round";
                Scores[0]++;
                Score1.Text = "Player1: " + Scores[0];
            }
            else
            {
                RoundResult.Text = "Player2 wins the round";
                Scores[1]++;
                Score2.Text = "Player2: " + Scores[1];
            }

            RoundHasEnded = true;
        }

        bool CheckIfWin(int[] lastMove)
        {
            List<bool> results = new List<bool>();
            int color = -1; // Not to get 0==0 (the algorithm doesn't care about empty fields)
            if (!Turn)
                color = 1; // color = yellow
            else if (Turn)
                color = 2; // color = red               
            
            if (UpToDownDiagonally(results, color, false))
                return true;
            results.Clear();
            if (UpToDownDiagonally(results, color, true))
                return true;
            results.Clear();
            if (DownToUpDiagonally(results, color, false))
                return true;
            results.Clear();
            if (DownToUpDiagonally(results, color, true))
                return true;
            results.Clear();
            if (Column(results, color, false))
                return true;
            results.Clear();
            if (Column(results, color, true))
                return true;
            results.Clear();
            if (Row(results, color, false))
                return true;
            results.Clear();
            if (Row(results, color, true))
                return true;
            results.Clear();

            return false;
        }

        #region Win conditions
        bool UpToDownDiagonally(List<bool> results, int color, bool checkBackwards)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    CheckBackwards(checkBackwards, ref i);
                    if (Columns[LastMove[0] + i].Fields[LastMove[1] + i].OccupiedBy == color) // 1st possibility: up to down diagonally
                        results.Add(true);
                    CheckBackwards(checkBackwards, ref i);
                }
                if (CheckResultsArray(results))
                    return true;
            }
            catch { }
            return false;
        }

        bool DownToUpDiagonally(List<bool> results, int color, bool checkBackwards)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    CheckBackwards(checkBackwards, ref i);
                    if (Columns[LastMove[0] + i].Fields[LastMove[1] - i].OccupiedBy == color) // 2nd possibility: down to up diagonally
                        results.Add(true);
                    CheckBackwards(checkBackwards, ref i);
                }
                if (CheckResultsArray(results))
                    return true;
            }
            catch { }
            return false;
        }

        bool Column(List<bool> results, int color, bool checkBackwards)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    CheckBackwards(checkBackwards, ref i);
                    if (Columns[LastMove[0]].Fields[LastMove[1] + i].OccupiedBy == color) // 3rd possibility: column
                        results.Add(true);
                    CheckBackwards(checkBackwards, ref i);
                }
                if (CheckResultsArray(results))
                    return true;
            }
            catch { }
            return false;
        }

        bool Row(List<bool> results, int color, bool checkBackwards)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    CheckBackwards(checkBackwards, ref i);
                    if (Columns[LastMove[0] + i].Fields[LastMove[1]].OccupiedBy == color) // 4th possibility: row
                        results.Add(true);
                    CheckBackwards(checkBackwards, ref i);
                }
                if (CheckResultsArray(results))
                    return true;
            }
            catch { }
            return false;
        }

        void CheckBackwards(bool checkBackwards, ref int i)
        {
            if (checkBackwards)
                i = -i;
        }
        #endregion

        bool CheckResultsArray(List<bool> temp)
        {
            if (temp.Count == 4)
                return true;
            return false;
        }
        #endregion
    }

    public partial class MainWindow : Window
    {
        Game game;

        public MainWindow()
        {
            InitializeComponent();

            Board.Width = 400;
            Board.Height = Board.Width;

            game = new Game(Board, RoundResult, Score1, Score2);
        }

        #region Buttons
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            game.Reset(true);
        }

        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            game.Reset(false);
        }
        #endregion
    }
}
