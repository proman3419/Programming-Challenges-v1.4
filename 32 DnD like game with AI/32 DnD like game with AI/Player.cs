using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Player
    {
        public static int Level { get; set; }
        public static int Exp { get; set; }
        static int NextLevelExp { get; set; }
        public static int MaxHealth { get; set; }
        public static int CurrentHealth { get; set; }
        public static bool IsAlive { get; set; }
        public static int MinAttack { get; set; }
        public static int MaxAttack { get; set; }
        public static int Gold { get; set; }
        public static int[] Position { get; set; }

        public static void Initialize()
        {
            IsAlive = true;
            Gold = 100;
            LevelUp(true);
            Position = new int[] { 0, 0 };
        }

        public static void LevelUp(bool initialize)
        {
            while (Exp > NextLevelExp || initialize)
            {
                Level++;
                Exp -= NextLevelExp;
                NextLevelExp = 100 * Level;
                MaxHealth = 100 * Level;
                CurrentHealth = MaxHealth;
                MinAttack = 5 * Level;
                MaxAttack = 15 * Level;
                if(!initialize)
                    DungeonMaster.Say("You leveled up!");
                initialize = false;
            }
        }

        public static void DisplayStatus()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("<{0}lvl {1}/{2}exp {3}/{4}hp {5}-{6}dmg>", Level, Exp, NextLevelExp, CurrentHealth, MaxHealth,
                MinAttack, MaxAttack, Gold);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
