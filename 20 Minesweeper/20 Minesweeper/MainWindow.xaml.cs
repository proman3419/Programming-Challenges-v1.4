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

namespace _20_Minesweeper
{
    class Field : Button
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

        public void OnClick(Button clickedField)
        {
            if (!IsRevealed)
            {
                IsRevealed = true;
                clickedField.Content = HiddenContent;
                clickedField.Background = Brushes.LightGreen;
            }
        }
    }

    class Game
    {
        WrapPanel Board { get; set; }
        Field[] Fields { get; set; }
        int Mines { get; set; }
        int Difficulty { get; set; }
        int FieldSize { get; set; }
        int FieldsAmount { get; set; }
        int FieldsInARow { get; set; }

        public Game(WrapPanel board)
        {
            Difficulty = 0;
            Board = board;
            CalculateVariables();
            CreateBoard();
        }

        void CalculateVariables()
        {
            switch (Difficulty)
            {
                case 0:
                    FieldsAmount = 36;
                    Mines = 10;
                    break;
                case 1:
                    FieldsAmount = 81;
                    Mines = 20;
                    break;
                case 2:
                    FieldsAmount = 144;
                    Mines = 30;
                    break;
            }
            FieldsInARow = (int)Math.Sqrt(FieldsAmount);
            FieldSize = (int)(Board.Height / FieldsInARow);
        }

        public void CreateBoard()
        {
            Fields = new Field[FieldsAmount];
            int[] minesFieldsIds = PlaceMines();

            for (int i = 0; i < FieldsAmount; i++)
            {
                Fields[i] = new Field();
                Fields[i].Height = FieldSize;
                Fields[i].Width = FieldSize;
                Fields[i].Tag = i;
                Fields[i].Click += new RoutedEventHandler(Field_Click);
                foreach (var id in minesFieldsIds)
                    if (i == id)
                    {
                        Fields[i].Content = "M";
                        Fields[i].IsMine = true;
                    }
                Board.Children.Add(Fields[i]);
            }

            PlaceHints();
        }

        void Field_Click(object sender, RoutedEventArgs e)
        {
            Fields[int.Parse(((Button)sender).Tag.ToString())].OnClick((Button)sender);
        }

        int[] PlaceMines()
        {
            Random random = new Random();
            int[] temp = new int[Mines];
            for (int i = 0; i < Mines; i++)
                temp[i] = random.Next(0, FieldsAmount);
            return temp;
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
            CalculateVariables();
            Board.Children.Clear();
            CreateBoard();
        }
    }

    public partial class MainWindow : Window
    {
        Game game;

        public MainWindow()
        {
            InitializeComponent();
            Board.Width = 350;
            Board.Height = Board.Width;

            game = new Game(Board);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Difficulty_Click(object sender, RoutedEventArgs e)
        {
            game.ChangeDifficulty((Button)sender);
            game.ResetGame();
        }
    }
}
