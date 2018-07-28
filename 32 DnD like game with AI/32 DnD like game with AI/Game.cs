using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Game
    {
        Player _Player { get; set; }
        Map _Map { get; set; }

        public Game()
        {
            Globals.Initialize();
            _Player = new Player();
            _Map = new Map();
        }

        public void Play()
        {
            string input;
            while (_Player.IsAlive)
            {
                input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "up":
                        _Map.Move(new int[2] { 0, -1 });
                        break;
                    case "right":
                        _Map.Move(new int[2] { 1, 0 });
                        break;
                    case "down":
                        _Map.Move(new int[2] { 0, 1 });
                        break;
                    case "left":
                        _Map.Move(new int[2] { -1, 0 });
                        break;
                    default:
                        Console.WriteLine("Unknown command used");
                        break;
                }
            }
        }
    }
}
