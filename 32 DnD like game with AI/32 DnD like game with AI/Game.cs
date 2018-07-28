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
            DungeonMaster.Say("If you feel confused ask me about help");
            DungeonMaster.Say("I created a map for today's session");
            DungeonMaster.Say("If you don't like it use resetmap command");
            _Map.Show();
            while (_Player.IsAlive)
            {
                _Player.DisplayStatus();
                input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "help":
                        break;
                    case "map":
                        _Map.Show();
                        break;
                    case "resetmap":
                        DungeonMaster.Say("This is the new map, I hope you like it");
                        _Map = new Map();
                        _Map.Show();
                        break;
                    case "north":
                        _Map.Move(new int[2] { 0, -1 }, _Player);
                        break;
                    case "east":
                        _Map.Move(new int[2] { 1, 0 }, _Player);
                        break;
                    case "south":
                        _Map.Move(new int[2] { 0, 1 }, _Player);
                        break;
                    case "west":
                        _Map.Move(new int[2] { -1, 0 }, _Player);
                        break;
                    default:
                        Console.WriteLine("Unknown command used");
                        break;
                }
            }
        }
    }
}
