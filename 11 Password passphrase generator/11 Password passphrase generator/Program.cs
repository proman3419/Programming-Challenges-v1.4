using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_Password_passphrase_generator
{
    class PassGenerator
    {
        int Length { get; set; }
        int length;
        string Password { get; set; }
        Random Random { get; set; }

        public void Start()
        {
            Console.WriteLine("What's your desired length of the password? Minimum 10 signs is recommended");
            while (!Int32.TryParse(Console.ReadLine(), out length))
                Console.WriteLine("It's not a number, try again");
            Length = length;
            Random = new Random(Length);
        }

        public void GeneratePassword()
        {
            StringBuilder stringBuilder = new StringBuilder(Length);
            for (int i = 0; i < Length; i++)
            {
                if (i % 2 == 0)
                    stringBuilder.Append(RandomLETTER());
                else if (i % 3 == 0)
                    stringBuilder.Append(RandomNumber());
                else
                    stringBuilder.Append(RandomLetter());
            }
            Password = stringBuilder.ToString();
        }

        public void DisplayPassword()
        {
            Console.WriteLine("Your freshly generated password:");
            Console.WriteLine(Password);
        }

        #region ChooseRandom
        char RandomLETTER()
        {
            int temp = Random.Next(65, 91);           
            return Convert.ToChar(temp);
        }

        char RandomNumber()
        {
            int temp = Random.Next(48, 58);
            return Convert.ToChar(temp);
        }

        char RandomLetter()
        {
            int temp = Random.Next(97, 123);
            return Convert.ToChar(temp);
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            PassGenerator passGenerator = new PassGenerator();
            passGenerator.Start();
            passGenerator.GeneratePassword();
            passGenerator.DisplayPassword();
            Console.ReadLine();
        }
    }
}
