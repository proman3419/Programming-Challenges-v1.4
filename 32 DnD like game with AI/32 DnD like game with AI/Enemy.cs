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

        public Enemy(Player player, bool boss)
        {
            float multiplier = 0.5f;
            if (boss)
            {
                multiplier = 0.7f;
                Boss = true;
            }
            Name = GetName();
            MaxHealth = (int)(multiplier * player.MaxHealth);
            CurrentHealth = MaxHealth;
            MinAttack = (int)(multiplier * player.MinAttack);
            MaxAttack = (int)(multiplier * player.MaxAttack);
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

        public int GiveExp(Player player)
        {
            Random random = new Random();
            int value = random.Next(0, 10);
            int multiplier = 25;
            if (Boss)
                multiplier = 75;
            return Globals.Map(value, 0, 10, multiplier * player.Level, 2 * multiplier * player.Level);
        }
    }
}
