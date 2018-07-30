using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Enemy
    {
        bool Boss { get; set; }
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }

        public Enemy(bool boss)
        {
            float multiplier = 0.7f;
            if (boss)
            {
                multiplier = 1f;
                Boss = true;
            }
            Name = GetName();
            MaxHealth = (int)(multiplier * Player.MaxHealth);
            CurrentHealth = MaxHealth;
            MinAttack = (int)(multiplier * Player.MinAttack);
            MaxAttack = (int)(multiplier * Player.MaxAttack);
        }

        string GetName()
        {
            string[] names;
            if (!Boss)
                names = new string[] { "kobold", "dragon", "human", "gnome", "rat", "goblin", "zombie", "skeleton", "spider", "ghoul",
                    "homunculus", "vampire" };
            else
                names = new string[] { "Adellon", "Bachtreia", "Cuhbello", "Delphen", "Ettyr", "Foliter", "Gordabo", "Halienton",
                "Illios", "Juleho", "Konix" };
            Random random = new Random();
            return names[random.Next(0, names.Length - 1)];
        }

        public void Loot()
        {
            Random random = new Random();
            int value = random.Next(0, 10);
            int multiplier = 25;
            if (Boss)
                multiplier = 75;
            int exp = Globals.Map(value, 0, 10, multiplier * Player.Level, 4 * multiplier * Player.Level);
            int gold = Globals.Map(value, 0, 10, multiplier * Player.Level, (int)(2.5 * multiplier * Player.Level));
            Player.Exp += exp;
            Player.Gold += gold;
            DungeonMaster.Say("You gained " + exp + "exp and " + gold + "gold");
        }
    }
}
