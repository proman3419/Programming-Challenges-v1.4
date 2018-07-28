using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Player
    {
        public int Level { get; set; }
        public int Exp { get; set; }
        int NextLevelExp { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsAlive { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }

        public Player()
        {
            IsAlive = true;
            Level = 1;
            LevelUp(true);
        }

        public void LevelUp(bool initialize)
        {
            if (Exp > NextLevelExp || initialize)
            {
                Exp -= NextLevelExp;
                NextLevelExp = 100 * Level;
                MaxHealth = 100 * Level;
                CurrentHealth = MaxHealth;
                MinAttack = 5 * Level;
                MaxAttack = 15 * Level;
                if(!initialize)
                    DungeonMaster.Say("You level up!");
            }
        }

        public void DisplayStatus()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("<{0}lvl {1}/{2}exp {3}/{4}hp {5}-{6}dmg>", Level, Exp, NextLevelExp, CurrentHealth, MaxHealth,
                MinAttack, MaxAttack);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
