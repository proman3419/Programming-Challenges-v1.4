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

        public static List<List<Card>> ResetPlayerAndCroupier()
        {
            List<List<Card>> hands = new List<List<Card>>();
            hands.Add(new List<Card>());
            return hands;
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

    class Croupier
    {
        public List<List<Card>> Hands { get; set; }

        public Croupier()
        {
            Hands = GameElements.ResetPlayerAndCroupier();
        }

        public bool Insurance()
        {
            if (Hands[0][0].Name == "Ace")
            {
                if (GameElements.CurrentBalance - 0.5 * GameElements.Bet > 0)
                {
                    GameElements.CurrentBalance -= (int)(0.5 * GameElements.Bet);
                    GameElements.Insurance = (int)(0.5 * GameElements.Bet);
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
            foreach (Card card in Hands[0])
                points += card.Value;
            if (points <= 16)
            {
                Console.WriteLine("Croupier: Draw");
                return true;
            }
            else if (points == 21)
            {
                Console.WriteLine("Croupier has blackjack");
                GameElements.CurrentBalance += GameElements.Insurance;
                return false;
            }
            else
            {
                GameElements.DrawSeparators("-");
                Console.WriteLine("Croupier: Stand");
                GameElements.DrawSeparators("-");
                return false;
            }
        }
    }

    class Player
    {
        public List<List<Card>> Hands { get; set; }

        public Player()
        {
            Hands = GameElements.ResetPlayerAndCroupier();
        }

        public void Hit(int i)
        {
            Hands[i].Add(GameElements.Deck[GameElements.Deck.Count - 1]);
            GameElements.MoveToTheDiscardDeck();
        }

        public bool DoubleTheBet(bool split)
        {
            if (GameElements.CurrentBalance - GameElements.Bet > 0)
            {
                GameElements.CurrentBalance -= GameElements.Bet;
                GameElements.Bet *= 2;
                GameElements.DrawSeparators("-");
                Console.WriteLine("Current bet: {0}", GameElements.Bet);
                if(split)
                    Console.WriteLine("You've splitted your hand. Now you have {0} hands", Hands.Count + 1);
                GameElements.DrawSeparators("-");
                return true;
            }
            Console.WriteLine("You don't have this much money");
            return false;
        }

        public bool Split(int i)
        {
            if (String.Compare(Hands[i][0].Name, Hands[i][1].Name) == 0)
            {
                if (DoubleTheBet(true))
                {
                    Hands.Add(new List<Card>());
                    Hands[i + 1].Add(Hands[i][1]);
                    Hands[i].RemoveAt(1);

                    return true;
                }
                return false;
            }
            Console.WriteLine("Your first two cards don't have the same value");
            return false;
        }
    }

    class Game
    {
        Croupier Croupier { get; set; }
        Player Player { get; set; }
        bool RoundIsOver { get; set; }
        List<bool> Stand { get; set; }
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
            Stand = new List<bool>();
            Stand.Add(false);
            RoundIsOver = false;
        }

        public void Play()
        {
            while (GameElements.CurrentBalance > 0)
            {
                GameElements.DrawSeparators("+");
                Console.WriteLine("Round {0}", RoundNo);
                GameElements.DrawSeparators("+");
                Round();
                GameElements.Reset();
                Reset();
                RoundNo++;
            }
        }

        void Round()
        {
            PlaceABet();
            FirstDraw();
            while (!RoundIsOver)
            {
                for (int i = 0; i < Player.Hands.Count; i++)
                {
                    if (!Stand[i])
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
                                Player.Hit(i);
                                ShowAllCards(Player, i);
                                GameElements.DrawSeparators("-");
                                CheckIfBusted(Player.Hands[i], i);
                                break;
                            case 2:
                                GameElements.DrawSeparators("-");
                                Console.WriteLine("Stand");
                                GameElements.DrawSeparators("-");
                                Stand[i] = true;
                                break;
                            case 3:
                                if (!Player.DoubleTheBet(false))
                                    goto GetInput;
                                GetACard(Player, true, i);
                                CheckIfBusted(Player.Hands[i], i);
                                Stand[i] = true;
                                break;
                            case 4:
                                if (!Player.Split(i))
                                    goto GetInput;
                                Stand.Add(false);
                                break;
                            case 5:
                                if (!Croupier.Insurance())
                                    goto GetInput;
                                break;
                        }
                    }

                    if (Stand.All(a => a))
                    {
                        if (Croupier.CroupiersMove())
                        {
                            GetACard(Croupier, true, 0);
                            CheckIfBusted(Croupier.Hands[0], 0);
                        }
                        else
                            Win(i);
                        Console.WriteLine("Current balance: {0}\n", GameElements.CurrentBalance);
                    }
                }
            } 
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
            //GetACard(Player, false, 0);
            //GetACard(Player, true, 0);
            Player.Hands[0].Add(new Card("2", 2));
            Player.Hands[0].Add(new Card("2", 2));
            GetACard(Croupier, true, 0);
            GetACard(Croupier, false, 0);
            GameElements.DrawSeparators("-");
        }

        void GetACard<T>(T receiver, bool show, int i)
        {
            if(receiver is Croupier)
                ((Croupier)(object)receiver).Hands[i].Add(GameElements.Deck[GameElements.Deck.Count - 1]);
            if(receiver is Player)
                ((Player)(object)receiver).Hands[i].Add(GameElements.Deck[GameElements.Deck.Count - 1]);
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
                    Console.WriteLine("Your hand: ");
                else
                    Console.WriteLine("Your hand no. {0}:", i);
            }
            for (int j = 0; j < temp.Hands[i].Count; j++)
            {
                Console.Write(temp.Hands[i][j].Name);
                if (j != temp.Hands[i].Count - 1)
                    Console.Write(", ");
            }
            Console.WriteLine();
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

        void CheckIfBusted(List<Card> Hand, int i)
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
                if (!Stand[i])
                    PlayerLostRound(i);
                else
                {
                    GameElements.DrawSeparators("!");
                    Console.WriteLine("Croupier busted");
                    GameElements.DrawSeparators("!");
                    RoundIsOver = true;
                    GameElements.CurrentBalance += GameElements.Bet;
                }
            }
        }

        void PlayerLostRound(int i)
        {
            if (Player.Hands.Count > 1)
            {
                GameElements.DrawSeparators("!");
                Console.WriteLine("Hand no {0} got busted", i);
                GameElements.DrawSeparators("!");
                Player.Hands.RemoveAt(i);
                Stand[i] = true;
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
            foreach (Card card in Croupier.Hands[0])
                croupierPoints += card.Value;
            foreach (Card card in Player.Hands[i])
                playerPoints += card.Value;
            if (playerPoints > croupierPoints)
            {
                GameElements.DrawSeparators("!");
                if (Player.Hands.Count == 1)
                    Console.WriteLine("You win");
                else
                    Console.WriteLine("Hand no. wins");
                GameElements.CurrentBalance += GameElements.Bet;
            }
            else
            {
                GameElements.DrawSeparators("!");
                if (Player.Hands.Count == 1)
                    Console.WriteLine("You lose");
                else
                    Console.WriteLine("Hand no. loses");
            }
            GameElements.DrawSeparators("!");
            RoundIsOver = true;
        }
        #endregion
    }

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
