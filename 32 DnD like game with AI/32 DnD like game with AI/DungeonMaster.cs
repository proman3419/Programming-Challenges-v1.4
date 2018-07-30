using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class DungeonMaster
    {
        public static void Say(string phrase)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("<Dungeon Master>{0}", phrase);
        }

        public static void Fight(Enemy enemy)
        {
            Say("You feel an appearance of something behind your back. You turn back rapdily to confuse the potential oponent. " +
                "Your instinct was right, you see a rushing " + enemy.Name + " towards you. The fight begins");
            Random random = new Random();
            int hit;
            while (true)
            {
                hit = Hit(random, Player.MinAttack, Player.MaxAttack);
                enemy.CurrentHealth -= hit;
                if (hit == 0)
                    Say("You miss the enemy");
                else if (hit < 6)
                    Say("You strike swiftly, but without power dealing " + hit + " damage");
                else if (hit < 9)
                    Say("You hit forcefully, you hear sound the of crushing bones. You dealt " + hit + " damage");
                else
                    Say("You hit your oponent right in their's weak spot, the enemy screams loudly. You dealt " + hit + " damage");
                if (enemy.CurrentHealth <= 0)
                {
                    Say("You've killed the skank");
                    enemy.Loot();
                    break;
                }
                hit = Hit(random, enemy.MinAttack, enemy.MaxAttack);
                Player.CurrentHealth -= hit;
                if (hit == 0)
                    Say("The enemy gets distracted by your moves and misses their's hit");
                else if (hit < 6)
                    Say("The enemy hits you with their's left hand causing " + hit + " damage");
                else if (hit < 9)
                    Say("The oponent punches you in the face causing a blackout. The hit dealt " + hit + " damage");
                else
                    Say("The oponent turns your strike against you dealing destructive " + hit + " damage");
                if (Player.CurrentHealth <= 0)
                {
                    Say("You've been defeated by " + enemy.Name + ". Your adventure ends up here");
                    Player.IsAlive = false;
                    break;
                }
            }
            Player.LevelUp(false);
        }

        static int Hit(Random random, int minAttack, int maxAttack)
        {
            int value = random.Next(0, 10);
            return Globals.Map(value, 0, 10, minAttack, maxAttack);
        }
    }
}
