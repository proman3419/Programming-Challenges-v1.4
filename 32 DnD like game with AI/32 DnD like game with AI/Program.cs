using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.Play();

            Console.ReadLine();
        }
    }
}
