using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace _46_Snake
{
    class Fruit
    {
        int X { get; set; }
        int Y { get; set; }

        public Fruit(int x, int y)
        {
            X = x;
            Y = y;
            DrawFruit();
        }

        void DrawFruit()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("*");
        }

        public bool Eat(int x, int y, ConsoleKey direction)
        {
            if (X == x && Y == y)
                return true;
            return false;
        }
    }

    class Game
    {
        bool Alive { get; set; }
        ConsoleKey Direction { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        List<int[]> Snake { get; set; }
        Timer Timer { get; set; }
        Random Random { get; set; }
        Fruit Fruit { get; set; }
        List<int[]> History { get; set; }

        public Game()
        {
            Alive = true;
            Direction = ConsoleKey.UpArrow;
            Width = Console.WindowWidth - 1;
            Height = Console.WindowHeight - 1;
            for (int y = 0; y <= Height; y++)
                for (int x = 0; x <= Width; x++)
                    if (x == 0 || y == 0 || x == Width || y == Height)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write('#');
                    }
            Snake = new List<int[]>();
            Snake.Add(new int[] { Console.WindowWidth / 2, Console.WindowHeight / 2 });
            Timer = new Timer(200);
            Timer.Elapsed += Play;
            Random = new Random();
            History = new List<int[]>();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Start();
        }

        void Play(Object source, ElapsedEventArgs e)
        {
            Move();
            if (Fruit.Eat(Snake[0][0], Snake[0][1], Direction))
            {
                SpawnFruit();
                Snake.Add(History[History.Count - 1]);
            }
            Display();
        }

        void Start()
        {
            SpawnFruit();
            Timer.Enabled = true;
            while (Alive)
            {
                Console.SetCursorPosition(0, 0);
                Direction = Console.ReadKey().Key;
                Console.SetCursorPosition(0, 0);
                Console.Write("#");
            }
            Timer.Enabled = false;
        }

        void Display()
        {
            for (int i = 0; i < Snake.Count; i++)
            {
                Console.SetCursorPosition(Snake[i][0], Snake[i][1]);
                Console.Write('@');
                Console.SetCursorPosition(0, 0);
            }
            Console.SetCursorPosition(History[History.Count - Snake.Count][0], History[History.Count - Snake.Count][1]);
            Console.Write(' ');            
        }

        void Move()
        {
            History.Add(new int[] { Snake[0][0], Snake[0][1] });
            for (int i = 0; i < Snake.Count; i++)
            {
                try
                {
                    Snake[i + 1][0] = Snake[i][0];
                    Snake[i + 1][1] = Snake[i][1];
                }
                catch { };
            }

            switch (Direction)
            {
                case ConsoleKey.UpArrow:
                    Snake[0][1]--;
                    break;
                case ConsoleKey.DownArrow:
                    Snake[0][1]++;
                    break;
                case ConsoleKey.RightArrow:
                    Snake[0][0]++;
                    break;
                case ConsoleKey.LeftArrow:
                    Snake[0][0]--;
                    break;
            }
        }

        void SpawnFruit()
        {
            int x, y;
            do
            {
                x = Random.Next(1, Width);
                y = Random.Next(1, Height);
            } while (Snake.Contains(new int[] { x, y }));
            Fruit = new Fruit(x, y);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.ReadLine();
        }
    }
}
