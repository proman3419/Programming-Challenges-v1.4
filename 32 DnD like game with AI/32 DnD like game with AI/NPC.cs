using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32_DnD_like_game_with_AI
{
    class Item
    {
        public string Name { get; set; }
        public int AttributeValue { get; set; }
        public string Attribute { get; set; }
        public int Price { get; set; }

        public Item(string name, int multiplier, string attribute)
        {
            Name = name;
            Attribute = attribute;
            if (Attribute == "hp ")
                AttributeValue = Player.Level * multiplier * 10;
            else
                AttributeValue = Player.Level * multiplier;
            Price = Player.Level * multiplier * 100;
        }

        public void DisplayItem(int maxLength, int maxAttributeValueLength, int maxPriceLength)
        {
            int spacing = maxLength - Name.Length -  AttributeValue.ToString().Length - Attribute.Length + 15;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Name);
            for (int i = 0; i < spacing; i++)
                Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("{0}{1}", AttributeValue, Attribute);
            Console.ForegroundColor = ConsoleColor.Yellow;
            spacing = maxPriceLength - Price.ToString().Length + 10;
            for (int i = 0; i < spacing; i++)
                Console.Write(" ");
            Console.WriteLine("{0}gold", Price);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Buy()
        {
            Player.Gold -= Price;
            DungeonMaster.Say("The ginger hair dwarf gives you the " + Name);
            if (Attribute == "dmg")
            {
                Player.MinAttack += AttributeValue;
                Player.MaxAttack += AttributeValue;
                DungeonMaster.Say("You feel much stronger now");
            }
            if (Attribute == "hp ")
            {
                Player.MaxHealth += AttributeValue;
                Player.CurrentHealth += AttributeValue;
                DungeonMaster.Say("You feel that your durability has improved");
            }
        }
    }

    class NPC
    {
        public NPC()
        {
            Random random = new Random();
            int NPCType = random.Next(0, 100);
            if (0 <= NPCType && NPCType < 33)
                Heal();
            if (33 <= NPCType && NPCType < 66)
                Trade();
            if (66 <= NPCType && NPCType < 100)
                Steal();
        }

        #region Activities
        void Heal()
        {
            DungeonMaster.Say("You meet an old lady cooking a fragrant soup. She offers you a bowl of the soup for " +
                Player.Level * 100 / 3 + " gold mentioning that it's prepared using healing herbs. Are you interested? (y/n)");
            bool choice = Answer();
            if (choice)
            {
                if (CheckIfEnoughGold(Player.Level * 100 / 3))
                {
                    int heal = (int)(0.4 * Player.MaxHealth);
                    Player.CurrentHealth += heal;
                    if (Player.CurrentHealth > Player.MaxHealth)
                    {
                        heal = Player.MaxHealth - (Player.CurrentHealth - heal);
                        Player.CurrentHealth = Player.MaxHealth;
                    }
                    DungeonMaster.Say("You feel warm spreading through your whole body. You feel energized and ready to continue your " +
                        "adventure. You've got healed for " + heal + " healthpoints");
                }
            }
            else
                DungeonMaster.Say("The woman starts blending the soup and seems to ignore you");
        }

        void Trade()
        {
            DungeonMaster.Say("You hear many low voices talking about forging, food and mining. As you peek out from behind a giant " +
                "you see a group of five dwarves consuming a boar and drinking beer. They seem confused for the first couple of seconds " +
                "but they quickly find you as a potential client. They present you their goods");
            List<Item> Items = new List<Item>();
            // Dmg items
            Items.Add(new Item("Dagger", 1, "dmg"));
            Items.Add(new Item("Light sword", 2, "dmg"));
            Items.Add(new Item("Rapier", 3, "dmg"));
            Items.Add(new Item("Strong sword", 4, "dmg"));
            Items.Add(new Item("Massive hammer", 13, "dmg"));
            Items.Add(new Item("Berserk's sword", 20, "dmg"));
            // HP items
            Items.Add(new Item("Leather coat", 1, "hp "));
            Items.Add(new Item("Paper armor", 2, "hp "));
            Items.Add(new Item("Chain mail", 4, "hp "));
            Items.Add(new Item("Plate armor", 6, "hp "));
            Items.Add(new Item("Ancient magic armor", 12, "hp "));
            int maxLength = 0;
            int maxAttributeValueLength = 0;
            int maxPriceLength = 0;
            foreach (Item item in Items)
            {
                if (item.Name.Length + item.AttributeValue.ToString().Length + item.Attribute.Length > maxLength)
                    maxLength = item.Name.Length;
                if (item.AttributeValue.ToString().Length > maxAttributeValueLength)
                    maxAttributeValueLength = item.AttributeValue.ToString().Length;
                if (item.Price.ToString().Length > maxPriceLength)
                    maxPriceLength = item.Price.ToString().Length;
            }
            foreach (Item item in Items)
                item.DisplayItem(maxLength, maxAttributeValueLength, maxPriceLength);
            DungeonMaster.Say("You have " + Player.Gold + " gold. Do you want to buy something? (y/n)");
            while (Answer())
            {
                DungeonMaster.Say("Go ahead, choose something");
                while (true)
                {
                    Player.DisplayStatus();
                    string input = Console.ReadLine();
                    if (input.StartsWith("buy "))
                    {
                        input = input.Remove(0, 4);
                        int id;
                        try { id = int.Parse(input); }
                        catch(StackOverflowException)
                        { DungeonMaster.Say("The dwarves don't have such an item for sell"); continue; }
                        catch(FormatException)
                        {
                            DungeonMaster.Say("Just tell me what's the number of the item on the list. Don't forget that " +
                              "dwarves count from 0.");
                            continue;
                        }
                        try
                        {
                            if (CheckIfEnoughGold(Items[id].Price))
                                Items[id].Buy();
                            break;
                        }
                        catch (IndexOutOfRangeException)
                        { DungeonMaster.Say("The dwarves don't have such an item for sell"); }
                    }
                    else
                        DungeonMaster.Say("The dwarves don't have such an item for sell");
                }
            }
            DungeonMaster.Say("You greet the dwarves and continue the adventure, they go back to feasting");
        }

        void Steal()
        {
            DungeonMaster.Say("You enter a dark cave. After a few steps you hear feel that your puch has gone. Suddenly you hear " +
                "a clink of coins somewhere on front of you. You rush rapidly and hit something, it runs away dropping coins. You " +
                "see that chase doesn't make sense since you can barely see so you focus on collecting the dropped coins");
            Random random = new Random();
            Player.Gold = (random.Next(2, 5) * Player.Gold / 10);
            DungeonMaster.Say("After some time you've collected all of the coins laying on the ground. Now you have " + Player.Gold + " gold");
        }
        #endregion

        bool Answer()
        {
            do
            {
                Player.DisplayStatus();
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (input == 'y')
                    return true;
                else if (input == 'n')
                    return false;
                else
                    DungeonMaster.Say("Answer with either 'y' or 'n'");
            } while (true);
        }

        bool CheckIfEnoughGold(int price)
        {
            if (price > Player.Gold)
            {
                DungeonMaster.Say("You don't have this much gold");                
                return false;
            }
            return true;
        }
    }
}
