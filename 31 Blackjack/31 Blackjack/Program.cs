using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _31_Blackjack
{
    static class GameElements
    {
        public static List<Card> Deck { get; set; }
        public static List<Card> DiscardedDeck { get; set; }
        public static int CurrentBalance { get; set; }
        public static int Bet { get; set; }
        public static int Insurance { get; set; }
        static int WindowWidth { get; set; }

        public static void Initialize()
        {
            CurrentBalance = 500;
            WindowWidth = Console.WindowWidth;
        }

        public static void Reset()
        {
            Deck = new List<Card>();
            DiscardedDeck = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j <= 10; j++)
                    Deck.Add(new Card(j.ToString(), j));
                Deck.Add(new Card("Jack", 10));
                Deck.Add(new Card("Queen", 10));
                Deck.Add(new Card("King", 10));
                Deck.Add(new Card("Ace", 11));
            }
            Shuffle(Deck);
        }

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            Random random = new Random();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void MoveToTheDiscardDeck()
        {
            DiscardedDeck.Add(Deck[Deck.Count - 1]);
            Deck.RemoveAt(Deck.Count - 1);
        }

        public static void DrawSeparators(string segment)
        {
            for (int i = 0; i < WindowWidth; i++)
                Console.Write(segment);
        }

        public static List<Hand> ResetPlayerAndCroupier()
        {
            List<Hand> hands = new List<Hand>();
            hands.Add(new Hand());
            return hands;
        }

        public static void ReplaceDecks()
        {
            Deck = DiscardedDeck;
            DiscardedDeck.Clear();
            Shuffle(Deck);
        }
    }

    class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Card(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    class Hand
    {
        public List<Card> Cards { get; set; }
        public bool Inactive { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }
    }

    class Croupier
    {
        public List<Hand> Hands { get; set; }

        public Croupier()
        {
            Hands = GameElements.ResetPlayerAndCroupier();
        }

        public bool Insurance()
        {
            if (Hands[0].Cards[0].Name == "Ace")
            {
                if (GameElements.CurrentBalance - GameElements.Bet > 0)
                {
                    GameElements.CurrentBalance -= GameElements.Bet;
                    GameElements.Insurance = GameElements.Bet;
                    GameElements.DrawSeparators("-");
                    Console.WriteLine("Current insurance: {0}", GameElements.Insurance);
                    GameElements.DrawSeparators("-");
                    return true;
                }
                Console.WriteLine("You don't have this much money");
                return false;
            }
            Console.WriteLine("Croupier's first card isn't an ace");
            return false;
        }

        public bool CroupiersMove()
        {
            int points = 0;
            foreach (Card card in Hands[0].Cards)
                points += card.Value;
            if (points <= 16)
            {
                Console.WriteLine("Croupier: Draw");
                GameElements.DrawSeparators("-");
                return true;
            }
            else if (points == 21)
            {
                Console.WriteLine("Croupier has blackjack");
                GameElements.DrawSeparators("-");
                GameElements.CurrentBalance += GameElements.Insurance;
                return false;
            }
            else
            {
                Console.WriteLine("Croupier: Stand");
                GameElements.DrawSeparators("-");
                return false;
            }
        }
    }

    class Player
    {
        public List<Hand> Hands { get; set; }

        public Player()
        {
            Hands = GameElements.ResetPlayerAndCroupier();
        }

        public bool DoubleTheBet()
        {
            if (GameElements.CurrentBalance - GameElements.Bet > 0)
            {
                GameElements.CurrentBalance -= GameElements.Bet;
                GameElements.Bet *= 2;
                GameElements.DrawSeparators("-");
                Console.WriteLine("Current bet: {0}", GameElements.Bet);
                return true;
            }
            Console.WriteLine("You don't have this much money");
            return false;
        }

        public bool Split(int i)
        {
            try
            {
                if (String.Compare(Hands[i].Cards[0].Name, Hands[i].Cards[1].Name) == 0)
                {
                    if (Hands[i].Cards.Count == 2)
                    {
                        if (DoubleTheBet())
                        {
                            Hands.Add(new Hand());
                            Hands[i + 1].Cards.Add(Hands[i].Cards[1]);
                            Hands[i].Cards.RemoveAt(1);
                            Console.WriteLine("You've splitted your hand. Now you have {0} hands", Hands.Count);
                            GameElements.DrawSeparators("-");
                            return true;
                        }
                        return false;
                    }
                    Console.WriteLine("It's not your first move");
                    return false;
                }
                Console.WriteLine("Your first two cards don't have the same value");
                return false;
            }
            catch
            {
                Console.WriteLine("You don't even have two cards in this hand");
                return false;
            }
        }
    }

    class Game
    {
        Croupier Croupier { get; set; }
        Player Player { get; set; }
        bool RoundIsOver { get; set; }
        int RoundNo { get; set; }

        public Game()
        {
            GameElements.Initialize();
            GameElements.Reset();
            Reset();
            RoundNo = 1;
        }

        void Reset()
        {
            Croupier = new Croupier();
            Player = new Player();
            RoundIsOver = false;
        }

        public void Play()
        {
            while (GameElements.CurrentBalance > 0)
            {
                if(RoundNo != 1)
                    Console.WriteLine();
                GameElements.DrawSeparators("+");
                Console.WriteLine("Round {0}", RoundNo);
                GameElements.DrawSeparators("+");
                Round();
                GameElements.Reset();
                Reset();
                RoundNo++;
            }
            Console.WriteLine("/nYou're out of money. Maybe next time Fortune will be in your favour");
        }

        void Round()
        {
            PlaceABet();
            FirstDraw();
            do
            {
                for (int i = 0; i < Player.Hands.Count; i++)
                {
                    if (!Player.Hands[i].Inactive)
                    {
                        if (Player.Hands.Count > 1)
                        {
                            Console.WriteLine("Hand no. {0}|", i);
                            Console.WriteLine("+++++++++++");
                        }
                        PresentOptions();
                        GetInput:
                        switch (GetInput())
                        {
                            case 1:
                                GameElements.DrawSeparators("-");
                                GetACard(Player, true, i);
                                CheckIfBusted(Player.Hands[i].Cards, i, true);
                                break;
                            case 2:
                                GameElements.DrawSeparators("-");
                                Console.WriteLine("Stand");
                                GameElements.DrawSeparators("-");
                                Player.Hands[i].Inactive = true;
                                break;
                            case 3:
                                if (!Player.DoubleTheBet())
                                    goto GetInput;
                                GetACard(Player, true, i);
                                CheckIfBusted(Player.Hands[i].Cards, i, true);
                                Player.Hands[i].Inactive = true;
                                break;
                            case 4:
                                if (!Player.Split(i))
                                    goto GetInput;
                                break;
                            case 5:
                                if (!Croupier.Insurance())
                                    goto GetInput;
                                break;
                        }
                    }
                    if (CheckIfAllInactive())
                    {
                        if (Croupier.CroupiersMove())
                        {
                            GetACard(Croupier, true, 0);
                            CheckIfBusted(Croupier.Hands[0].Cards, 0, false);
                        }
                        else
                            Win(i);
                    }
                }
            } while (!RoundIsOver);
            ShowAllCards(Croupier, 0);
            DisplayCurrentBlance();
        }

        #region Game phases
        void PlaceABet()
        {
            Console.WriteLine("Place a bet");
            int temp;
            string input;
            do
            {
                temp = 1;
                input = Console.ReadLine();
                try { temp = int.Parse(input); }
                catch { Console.WriteLine("We don't accept this currency"); }
                if (temp <= 0)
                    Console.WriteLine("The bet is too low");
                if (GameElements.CurrentBalance - temp < 0)
                    Console.WriteLine("You don't have this much money");
            } while (!int.TryParse(input, out temp) || temp <= 0 || GameElements.CurrentBalance - temp < 0);
            GameElements.Bet = temp;
            GameElements.CurrentBalance -= temp;
            GameElements.DrawSeparators("-");
        }

        void FirstDraw()
        {
            GetACard(Player, false, 0);
            GetACard(Player, true, 0);
            GetACard(Croupier, true, 0);
            GetACard(Croupier, false, 0);
        }

        void GetACard<T>(T receiver, bool show, int i)
        {
            try
            {
                if (receiver is Croupier)
                    ((Croupier)(object)receiver).Hands[i].Cards.Add(GameElements.Deck[GameElements.Deck.Count - 1]);
                if (receiver is Player)
                    ((Player)(object)receiver).Hands[i].Cards.Add(GameElements.Deck[GameElements.Deck.Count - 1]);
            }
            catch (IndexOutOfRangeException e)
            {
                GameElements.ReplaceDecks();
            }
            GameElements.MoveToTheDiscardDeck();
            if (show)
                ShowAllCards(receiver, i);
        }

        void ShowAllCards<T>(T source, int i)
        {
            dynamic temp = null;
            if (source is Croupier)
            {
                temp = ((Croupier)(object)source);
                Console.WriteLine("Croupier's hand: ");
            }
            if (source is Player)
            {
                temp = ((Player)(object)source);
                if(temp.Hands.Count == 1)
                    Console.WriteLine("Cards in hand: ");
                else
                    Console.WriteLine("Cards in  hand no. {0}:", i);
            }
            for (int j = 0; j < temp.Hands[i].Cards.Count; j++)
            {
                Console.Write(temp.Hands[i].Cards[j].Name);
                if (j != temp.Hands[i].Cards.Count - 1)
                    Console.Write(", ");
            }
            Console.WriteLine();
            GameElements.DrawSeparators("-");
        }

        void PresentOptions()
        {
            Console.WriteLine("1 Hit");
            Console.WriteLine("2 Stand");
            Console.WriteLine("3 Double down");
            Console.WriteLine("4 Split");
            Console.WriteLine("5 Insurance");
        }

        int GetInput()
        {
            int temp;
            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out temp) || temp < 1 || temp > 5)
                Console.WriteLine("\nThere's no such an option");
            Console.WriteLine();
            return temp;
        }

        void CheckIfBusted(List<Card> Hand, int i, bool player)
        {
            int points = 0;
            foreach (Card card in Hand)
                points += card.Value;
            if (points > 21)
            {
                for (int j = 0; j < Hand.Count; j++)
                {
                    if (Hand[j].Name == "Ace" && Hand[j].Value == 11)
                    {
                        Hand[j].Value = 1;
                        points -= 10;
                        if (points <= 21)
                            return;
                    }
                }
                if (player)
                    PlayerBusted(i);
                else
                {
                    GameElements.DrawSeparators("!");
                    Console.WriteLine("Croupier busted");
                    GameElements.DrawSeparators("!");
                    GameElements.CurrentBalance += 2 * GameElements.Bet;
                    RoundIsOver = true;
                }
            }
        }

        void PlayerBusted(int i)
        {
            if (Player.Hands.Count > 1)
            {
                GameElements.DrawSeparators("!");
                Console.WriteLine("Hand no. {0} got busted", i);
                GameElements.DrawSeparators("!");
                Player.Hands[i].Inactive = true;
                if (CheckIfAllInactive())
                    RoundIsOver = true;
            }
            else
            {
                GameElements.DrawSeparators("!");
                Console.WriteLine("You busted");
                GameElements.DrawSeparators("!");
                RoundIsOver = true;
            }
        }

        void Win(int i)
        {
            int playerPoints = 0;
            int croupierPoints = 0;
            foreach (Card card in Croupier.Hands[0].Cards)
                croupierPoints += card.Value;
            foreach (Card card in Player.Hands[i].Cards)
                playerPoints += card.Value;
            GameElements.DrawSeparators("!");
            if (playerPoints > croupierPoints)
            {
                if (Player.Hands.Count == 1)
                    Console.WriteLine("You win");
                else
                    Console.WriteLine("Hand no. {i} wins", i);
                GameElements.CurrentBalance += 2 * GameElements.Bet / Player.Hands.Count;
            }
            else
            {
                if (Player.Hands.Count == 1)
                    Console.WriteLine("You lose");
                else
                    Console.WriteLine("Hand no. {0} loses", i);
            }
            GameElements.DrawSeparators("!");
            RoundIsOver = true;
        }

        bool CheckIfAllInactive()
        {
            foreach (Hand hand in Player.Hands)
                if (!hand.Inactive)
                    return false;
            return true;
        }

        void DisplayCurrentBlance()
        {
            string temp = "Current balance: " + GameElements.CurrentBalance + "$#";
            Console.WriteLine(temp);
            for (int i = 0; i < temp.Length; i++)
                Console.Write("#");
            Console.WriteLine();
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.WriteLine("Welcome to the Casino! Check if Fortune smiles upon you today");
            Console.WriteLine("You have {0}$\n", GameElements.CurrentBalance);
            game.Play();

            Console.ReadLine();
        }
    }
}
