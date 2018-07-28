using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Player
    {
        int Level { get; set; }
        int Exp { get; set; }
        int CurrentHealth { get; set; }
        int MaxHealth { get; set; }
        public bool IsAlive { get; set; }
        int MinAttack { get; set; }
        int MaxAttack { get; set; }

        public Player()
        {
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            IsAlive = true;
            MinAttack = 10;
            MaxAttack = 30;
        }
    }
}
