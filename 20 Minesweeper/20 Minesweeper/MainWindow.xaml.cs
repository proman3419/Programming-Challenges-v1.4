using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20_Minesweeper
{
    class Field : TextBox
    {
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsMarked { get; set; }
        public int MinesNearby { get; set; }
        public string HiddenContent { get; set; }


        public Field()
        {
            IsMine = false;
            IsRevealed = false;
            IsMarked = false;
            MinesNearby = 0;
            HiddenContent = String.Empty;
        }

        public bool OnClick(TextBox clickedField, bool recursiveClick)
        {
            if (!IsMine || !recursiveClick)
            {
                if (MinesNearby == 0 || !recursiveClick)
                {
                    if (!IsRevealed)
                    {
                        RevealField(this);
                        return true;
                    }
                }
            }
            return false;
        }

        public void FirstMove(Field[] fields, int fieldsAmount, int fieldsInARow)
        {
            if (!IsMine)
            {
                int i = int.Parse(Tag.ToString());
                try // Up
                {
                    if (!(i < fieldsInARow - 1)) // Without the first row
                        if (!fields[i - fieldsInARow].IsMine)
                            RevealField(fields[i - fieldsInARow]);
                }
                catch { }
                try // Left
                {
                    if (!(i % fieldsInARow == 0)) // Without the first column
                        if (!fields[i - 1].IsMine)
                            RevealField(fields[i - 1]);
                }
                catch { }
                try // Right
                {
                    if (!(i % fieldsInARow == fieldsInARow - 1)) // Without the last colum
                        if (!fields[i + 1].IsMine)
                            RevealField(fields[i + 1]);
                }
                catch { }
                try // Down
                {
                    if (!((fieldsAmount - fieldsInARow) < i && i < (fieldsAmount - 1))) // Without the last row
                        if (!fields[i + fieldsInARow].IsMine)
                            RevealField(fields[i + fieldsInARow]);
                }
                catch { }
            }
        }

        public void MarkField()
        {
            if (!IsRevealed)
            {
                IsMarked = true;
                this.Text = "☺";
            }
        }

        public void RevealField(Field field)
        {
            field.IsRevealed = true;
            field.Text = field.HiddenContent;
            if (field.IsMine)
                field.Background = Brushes.Red;
            else
                field.Background = Brushes.LightGreen;
        }
    }

    class Game
    {
        WrapPanel Board { get; set; }
        TextBlock GameResult { get; set; }
        TextBlock MinesStatus { get; set; }
        Field[] Fields { get; set; }
        int[] MinesPositions { get; set; }
        int MinesAmount { get; set; }
        int MarkedFields { get; set; }
        int Difficulty { get; set; }
        int FieldSize { get; set; }
        int FieldsAmount { get; set; }
        int FieldsInARow { get; set; }
        bool FirstMove { get; set; }
        int FontSize { get; set; }

        public Game(WrapPanel board, TextBlock gameResult, TextBlock minesStatus)
        {
            Difficulty = 0;
            Board = board;
            GameResult = gameResult;
            MinesStatus = minesStatus;
            GameResult.VerticalAlignment = VerticalAlignment.Center;
            GameResult.HorizontalAlignment = HorizontalAlignment.Center;
            GameResult.Padding = new Thickness(10, 10, 10, 10);
            FirstMove = true;
            CalculateVariables();
            CreateBoard();
        }

        #region Compositional functions
        public void ChangeDifficulty(Button difficultyButton)
        {
            if (Difficulty == 2)
            {
                Difficulty = 0;
                difficultyButton.Content = "Easy";
                return;
            }

            Difficulty++;
            if (Difficulty == 1)
                difficultyButton.Content = "Medium";
            if (Difficulty == 2)
                difficultyButton.Content = "Hard";
        }

        public void ResetGame()
        {
            FirstMove = true;
            GameResult.Text = "";
            GameResult.Background = Brushes.Azure;
            CalculateVariables();
            Board.Children.Clear();
            CreateBoard();
            MarkedFields = 0;
            DisplayMinesStatus();
        }

        void GameOver()
        {
            foreach (var field in Fields)
                field.RevealField(field);
            GameResult.Text = "You lose";
            GameResult.Background = Brushes.Red;
        }

        void Victory()
        {
            foreach (var mine in MinesPositions)
                if (!Fields[mine].IsMarked)
                    return;
            GameResult.Text = "You win";
            GameResult.Background = Brushes.LightGreen;
        }

        void DisplayMinesStatus()
        {
            MinesStatus.Text = MarkedFields + "/" + MinesAmount;
        }
        #endregion

        #region Create board
        void CalculateVariables()
        {
            switch (Difficulty)
            {
                case 0:
                    FieldsAmount = 36;
                    MinesAmount = 10;
                    FontSize = 34;
                    break;
                case 1:
                    FieldsAmount = 100;
                    MinesAmount = 20;
                    FontSize = 22;
                    break;
                case 2:
                    FieldsAmount = 196;
                    MinesAmount = 30;
                    FontSize = 12;
                    break;
            }
            FieldsInARow = (int)Math.Sqrt(FieldsAmount);
            FieldSize = (int)(Board.Height / FieldsInARow);
            DisplayMinesStatus();
        }

        public void CreateBoard()
        {
            Fields = new Field[FieldsAmount];
            PlaceMines();

            for (int i = 0; i < FieldsAmount; i++)
            {
                Fields[i] = new Field();
                Fields[i].Height = FieldSize;
                Fields[i].Width = FieldSize;
                Fields[i].Tag = i;
                Fields[i].TextAlignment = TextAlignment.Center;
                Fields[i].FontSize = FontSize;
                if(Difficulty == 2)
                    Fields[i].FontWeight = FontWeights.Bold;
                Fields[i].IsReadOnly = true;
                Fields[i].Cursor = Cursors.Arrow;
                Fields[i].SelectionBrush = Brushes.Transparent;
                Fields[i].Focusable = false;
                Fields[i].PreviewMouseLeftButtonUp += new MouseButtonEventHandler(FieldReveal_Click);
                Fields[i].PreviewMouseRightButtonUp += new MouseButtonEventHandler(FieldMark_Click);
                foreach (var id in MinesPositions)
                    if (i == id)
                    {
                        Fields[i].HiddenContent = "☻";
                        Fields[i].IsMine = true;
                    }
                Board.Children.Add(Fields[i]);
            }

            PlaceHints();
        }

        void PlaceMines()
        {
            Random random = new Random();
            MinesPositions = new int[MinesAmount];
            for (int i = 0; i < MinesAmount; i++)
                MinesPositions[i] = random.Next(0, FieldsAmount);
        }

        void PlaceHints()
        {
            for (int i = 0; i < FieldsAmount; i++)
            {
                if (Fields[i].IsMine)
                {
                    if (!(i < FieldsInARow - 1)) // Without the first row
                    {
                        if (i != 0 && i != FieldsAmount - FieldsInARow) // Without the upper-left corner and the bottom-down corner
                            try { Fields[i - FieldsInARow - 1].MinesNearby++; } catch { }
                        try { Fields[i - FieldsInARow].MinesNearby++; } catch { }
                        if (i != FieldsInARow - 1 && i % FieldsInARow != FieldsInARow - 1) // Without the upper-right corner and the last column
                            try { Fields[i - FieldsInARow + 1].MinesNearby++; } catch { }
                    }

                    if (!(i % FieldsInARow == 0)) // Without the first column
                        try { Fields[i - 1].MinesNearby++; } catch { }
                    if (!(i % FieldsInARow == FieldsInARow - 1)) // Without the last colum
                        try { Fields[i + 1].MinesNearby++; } catch { }

                    if (!((FieldsAmount - FieldsInARow) < i && i < (FieldsAmount - 1))) // Without the last row
                    {
                        if (i != 0 && i != FieldsAmount - FieldsInARow) // Without the upper-left corner and the bottom-down corner
                            try { Fields[i + FieldsInARow - 1].MinesNearby++; } catch { }
                        try { Fields[i + FieldsInARow].MinesNearby++; } catch { }
                        if (i != FieldsAmount - 1 && i % FieldsInARow != FieldsInARow - 1) // Without the bottom-left corner and the last column
                            try { Fields[i + FieldsInARow + 1].MinesNearby++; } catch { }
                    }
                }
            }

            foreach (var field in Fields)
            {
                if (field.MinesNearby != 0 && !field.IsMine)
                {
                    field.HiddenContent = field.MinesNearby.ToString();
                }
            }
        }
        #endregion

        #region Field clicks 
        void FieldReveal_Click(object sender, MouseEventArgs e)
        {
            int clickedFieldId = int.Parse(((TextBox)sender).Tag.ToString());
            if (Fields[clickedFieldId].IsMine)
            {
                Fields[clickedFieldId].RevealField(Fields[clickedFieldId]);
                GameOver();
                return;
            }

            List<int> revealedFields = new List<int>();


            if (Fields[clickedFieldId].OnClick((TextBox)sender, false))
                revealedFields.Add(CheckNeighbours(clickedFieldId, revealedFields));
            if (FirstMove)
            {
                foreach (var id in revealedFields)
                    Fields[id].FirstMove(Fields, FieldsAmount, FieldsInARow);
                FirstMove = false;
            }

            Victory();
        }

        int CheckNeighbours(int i, List<int> revealedFields)
        {
            try // Up
            {
                if (!(i < FieldsInARow - 1)) // Without the first row
                    if (Fields[i - FieldsInARow].OnClick(Fields[i - FieldsInARow], true))
                        revealedFields.Add(CheckNeighbours(i - FieldsInARow, revealedFields));
            }
            catch { }
            try // Left
            {
                if (!(i % FieldsInARow == 0)) // Without the first column
                    if (Fields[i - 1].OnClick(Fields[i - 1], true))
                        revealedFields.Add(CheckNeighbours(i - 1, revealedFields));
            }
            catch { }
            try // Right
            {
                if (!(i % FieldsInARow == FieldsInARow - 1)) // Without the last colum
                    if (Fields[i + 1].OnClick(Fields[i + 1], true))
                        revealedFields.Add(CheckNeighbours(i + 1, revealedFields));
            }
            catch { }
            try // Down
            {
                if (!((FieldsAmount - FieldsInARow) < i && i < (FieldsAmount - 1))) // Without the last row
                    if (Fields[i + FieldsInARow].OnClick(Fields[i + FieldsInARow], true))
                        revealedFields.Add(CheckNeighbours(i + FieldsInARow, revealedFields));
            }
            catch { }
            return i;
        }

        void FieldMark_Click(object sender, MouseEventArgs e)
        {
            if (!((Field)sender).IsMarked)
            {
                ((Field)sender).MarkField();
                MarkedFields++;
                DisplayMinesStatus();
            }
        }
        #endregion
    }

    public partial class MainWindow : Window
    {
        Game game;

        public MainWindow()
        {
            InitializeComponent();
            Board.Width = 350;
            Board.Height = Board.Width;

            game = new Game(Board, GameResult, MinesStatus);
        }

        #region Buttons
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            game.ResetGame();
        }

        private void Difficulty_Click(object sender, RoutedEventArgs e)
        {
            game.ChangeDifficulty((Button)sender);
            game.ResetGame();
        }
        #endregion
    }
}
