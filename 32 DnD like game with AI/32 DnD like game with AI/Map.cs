using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Field
    {
        bool Enemy { get; set; }
        bool Boss { get; set; }
        bool NPC { get; set; }
        bool Empty { get; set; }
        public bool Exists { get; set; }

        public bool Visited { get; set; }
        string Visual { get; set; }

        public Field(int type)
        {
            Exists = true;
            switch (type)
            {
                case 0:
                    Enemy = true;
                    Visual = "!";
                    break;
                case 1:
                    Boss = true;
                    Visual = "&";
                    break;
                case 2:
                    NPC = true;
                    Visual = "?";
                    break;
                case 3:
                    Empty = true;
                    Visual = "@";
                    break;
                case 4:
                    Exists = false;
                    Visual = " ";
                    break;
            }
        }

        public void DisplayField()
        {
            if (Enemy)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (Boss)
                Console.ForegroundColor = ConsoleColor.Cyan;
            if (NPC)
                Console.ForegroundColor = ConsoleColor.Green;
            if (Empty)
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Visual);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    class Map
    {
        Field[,] MapFields { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int[] PlayerPosition { get; set; }

        public Map()
        {
            Console.WriteLine("Width of the map");
            Width = Globals.GetNumericInput(1, Globals.WindowWidth);
            Console.WriteLine("Height of the map");
            Height = Globals.GetNumericInput(1, Globals.WindowHeight);
            MapFields = new Field[Width, Height];
            PlayerPosition = new int[] { 0, 0 };
            CreateMap();
        }

        public void CreateMap()
        {
            Random random = new Random();
            int randomValue;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    randomValue = random.Next(0, 100);
                    if (x == 0 && y == 0)
                    {
                        MapFields[x, y] = new Field(3);
                        MapFields[x, y].Visited = true;
                    }
                    else if (randomValue < 10)
                        MapFields[x, y] = new Field(0);
                    else if (randomValue < 11)
                        MapFields[x, y] = new Field(1);
                    else if (randomValue < 13)
                        MapFields[x, y] = new Field(2);
                    else if (randomValue < 75)
                        MapFields[x, y] = new Field(3);
                    else if (randomValue < 100)
                        MapFields[x, y] = new Field(4);
                }
            ShowMap();
        }

        public void ShowMap()
        {
            Globals.DrawSeparators("-");
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == PlayerPosition[0] && y == PlayerPosition[1])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        MapFields[x, y].DisplayField();
                }
                if (Width != Globals.WindowWidth)
                    Console.WriteLine();
            }
            Globals.DrawSeparators("-");
        }

        public void Move(int[] direction)
        {
            if (CheckIfFieldExists(direction))
            {
                PlayerPosition[0] += direction[0];
                PlayerPosition[1] += direction[1];
                ShowMap();
            }
            else Console.WriteLine("I don't feel like going there <lazy>");
        }

        bool CheckIfFieldExists(int[] direction)
        {
            try
            {
                if (MapFields[PlayerPosition[0] + direction[0], PlayerPosition[1] + direction[1]].Exists)
                    return true;
            }
            catch { return false; }
            return false;
        }
    }
}
