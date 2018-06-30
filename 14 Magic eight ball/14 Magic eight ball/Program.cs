using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Magic_eight_ball
{
    class Ball
    {
        public string[] PositiveAnswers { get; set; }
        public string[] NonCommitalAnswers { get; set; }
        public string[] NegativeAnswers { get; set; }

        public Ball()
        {
            PositiveAnswers = new string[] { "It is certain", "It is decidedly so", "Without a doubt", "Yes - definitely",
                "You may rely on it", "As I see it, yes", "Most likely", "Outlook good", "Yes", "Signs point to yes"};
            NonCommitalAnswers = new string[] { "Reply hazy, try again", "Ask again later", "Better not tell you now",
                "Cannot predict now", "Concentrate and ask again"};
            NegativeAnswers = new string[] { "Don't count on it", "My reply is no", "My sources say no", "Outlook not so good",
                "Very doubtful"};
        }

        public void TakeAPosition()
        {
            Random random = new Random();
            int temp = random.Next(0, 3);
            switch (temp)
            {
                case 1:
                    Answer(PositiveAnswers, random);
                    break;
                case 2:
                    Answer(NonCommitalAnswers, random);
                    break;
                default:
                    Answer(NegativeAnswers, random);
                    break;
            }
        }

        void Answer(string[] possibleAnswers, Random random)
        {
            Console.WriteLine("The Ball says: " + @"'" + possibleAnswers[random.Next(0, possibleAnswers.GetUpperBound(0))] + @"'");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string temp;
            Ball ball = new Ball();

            Console.WriteLine("Ask a question");
            while (true)
            {
                temp = Console.ReadLine();
                if (!String.IsNullOrEmpty(temp))
                    break;
                Console.WriteLine("You haven't asked about anything yet");
            }
            ball.TakeAPosition();
            Console.ReadLine();
        }
    }
}
