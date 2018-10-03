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

namespace _45_Tic_tac_toe
{
    class Field : TextBox
    {
        public int OccupiedBy { get; set; } // 0 - noone, 1 - circle, 2 - cross

        public Field()
        {
            OccupiedBy = 0;
        }

        public bool Occupy(bool turn)
        {
            if (OccupiedBy == 0)
            {
                if (!turn)
                {
                    OccupiedBy = 1;
                    this.Text = "O";
                }
                else
                {
                    OccupiedBy = 2;
                    this.Text = "X";
                }
                return true;
            }
            return false;
        }
    }

    class Column : StackPanel
    {
        public StackPanel ColumnControl { get; set; }
        public int ColumnId { get; set; }
        int FieldsInAColumn { get; set; }
        public List<Field> Fields { get; set; }

        public Column(StackPanel columnControl, int columnId, int fieldsInAColumn)
        {
            ColumnControl = columnControl;
            ColumnId = columnId;
            FieldsInAColumn = fieldsInAColumn - 1;
            Fields = new List<Field>(FieldsInAColumn);
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
            RowsAmount = 3;
            Columns = new List<Column>();
            foreach (StackPanel columnControl in Board.Children)
            {
                Columns.Add(new Column(columnControl, Columns.Capacity, RowsAmount));
            }
            ColumnsAmount = Columns.Capacity - 1;
            FieldSize = (float)Board.Height / RowsAmount; 
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
                for (int j = 0; j < RowsAmount; j++)
                {
                    Columns[i].Fields.Add(new Field());
                    SetFieldsProperties(Columns[i].Fields[j], j);
                    Columns[i].Fields[j].PreviewMouseLeftButtonUp += new MouseButtonEventHandler(FieldOccupy_Click);
                    Columns[i].ColumnControl.Children.Add(Columns[i].Fields[j]);
                }
            }
        }

        void SetFieldsProperties(Field field, int j)
        {
            field.Height = FieldSize;
            field.Width = FieldSize;
            field.IsReadOnly = true;
            field.Cursor = Cursors.Hand;
            field.Focusable = false;
            field.TextAlignment = TextAlignment.Center;
            field.FontSize = 65;
            field.Tag = j;
        }

        void FieldOccupy_Click(object sender, MouseEventArgs e)
        {
            int clickedColumnId = int.Parse(((TextBox)sender).Tag.ToString());
            if (!RoundHasEnded)
            {
                if (((Field)sender).Occupy(Turn))
                {
                    LastMove[0] = clickedColumnId;
                    LastMove[1] = int.Parse(((Field)sender).Tag.ToString());
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
            if (!CheckIfWin())
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

        bool CheckIfWin()
        {
            List<bool> results = new List<bool>();
            int mark = -1; // Not to get 0==0 (the algorithm doesn't care about empty fields)
            if (!Turn)
                mark = 1;
            else if (Turn)
                mark = 2;

            if (Vertically(mark) || Horizontally(mark) || Diagonally(mark))
                return true;

            return false;
        }

        #region Win conditions
        bool Vertically(int mark)
        {
            for (int i = 0; i < 3; i++)
                if (Columns[i].Fields[0].OccupiedBy == Columns[i].Fields[1].OccupiedBy)
                    if (Columns[i].Fields[1].OccupiedBy == Columns[i].Fields[2].OccupiedBy)
                        if(Columns[i].Fields[2].OccupiedBy == mark)
                            return true;
            return false;
        }

        bool Horizontally(int mark)
        {
            for (int i = 0; i < 3; i++)
                if (Columns[0].Fields[i].OccupiedBy == Columns[1].Fields[i].OccupiedBy)
                    if (Columns[1].Fields[i].OccupiedBy == Columns[2].Fields[i].OccupiedBy)
                        if(Columns[2].Fields[i].OccupiedBy == mark)
                            return true;
            return false;
        }

        bool Diagonally(int mark)
        {
            if (Columns[0].Fields[0].OccupiedBy == Columns[1].Fields[1].OccupiedBy)
                if (Columns[1].Fields[1].OccupiedBy == Columns[2].Fields[2].OccupiedBy)
                    if (Columns[2].Fields[2].OccupiedBy == mark)
                        return true;

            if (Columns[2].Fields[0].OccupiedBy == Columns[1].Fields[1].OccupiedBy)
                if (Columns[1].Fields[1].OccupiedBy == Columns[0].Fields[2].OccupiedBy)
                    if (Columns[0].Fields[2].OccupiedBy == mark)
                        return true;

            return false;
        }
        #endregion
        #endregion
    }

    public partial class MainWindow : Window
    {
        Game game;

        public MainWindow()
        {
            InitializeComponent();

            Board.Width = 300;
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
