using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Game
    {
        Map _Map { get; set; }
        
        public Game()
        {
            Globals.Initialize();
            Player.Initialize();
            _Map = new Map();
        }

        public void Play()
        {
            string input;
            DungeonMaster.Say("I created a map for today's session");
            DungeonMaster.Say("If you feel confused ask me about help");
            DungeonMaster.Say("If you don't like it use the resetmap command");
            _Map.Show();
            while (Player.IsAlive)
            {
                Player.DisplayStatus();
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
                    case "gold":
                        DungeonMaster.Say("You have " + Player.Gold + " gold");
                        break;
                    case "north":
                        _Map.Move(new int[2] { 0, -1 });
                        break;
                    case "east":
                        _Map.Move(new int[2] { 1, 0 });
                        break;
                    case "south":
                        _Map.Move(new int[2] { 0, 1 });
                        break;
                    case "west":
                        _Map.Move(new int[2] { -1, 0 });
                        break;
                    default:
                        Console.WriteLine("Unknown command used");
                        break;
                }
                // Check if player reached the door to the next level
                if (_Map.MapFields[Player.Position[0], Player.Position[1]].Door)
                {
                    DungeonMaster.Say("Congratulations, you've reached The Door. Good luck on another level of this dungeon");
                    _Map = new Map();
                    DungeonMaster.Say("This is the new level, if you don't like it use the resetmap command");
                    _Map.Show();
                }
            }
        }
    }
}
