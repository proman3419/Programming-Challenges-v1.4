using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _04_Encryption_decryption_algorithm
{
    class Program
    {
        static string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "04 encrypted message.txt");

        static void Main(string[] args)
        {
            int input, translation;
            Random rand = new Random();

            Console.WriteLine("Input 1 to encrypt a message, input 2 to decrypt a message");
            while (!Int32.TryParse(Console.ReadLine(), out input))
                Console.WriteLine("Invalid input");
            translation = rand.Next(5, 60);
            switch (input)
            {
                case 1:
                    Encrypt(translation);
                    break;
                case 2:
                    Decrypt();
                    break;
            }
            Console.ReadLine();
        }

        static void Encrypt(int translation)
        {
            Console.WriteLine("Input a message which you want to encrypt");
            string inputMessage = Console.ReadLine();
            File.WriteAllText(savePath, translation.ToString() + Environment.NewLine);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char element in inputMessage)
                stringBuilder.Append(Convert.ToChar(element + translation).ToString());
            TextWriter textWriter = new StreamWriter(savePath, true);
            textWriter.Write(stringBuilder.ToString());
            textWriter.Close();
            Console.WriteLine("Your message has been encrypted and saved to this file:" + Environment.NewLine + savePath);
        }

        static void Decrypt()
        {
            Console.WriteLine("Pass the full path of file with the encrypted message");
            string input;
            do
            {
                input = Console.ReadLine();
                if (!File.Exists(input))
                    Console.WriteLine("The passed file doesn't exist");
            } while (!File.Exists(input));
            TextReader textReader = new StreamReader(input);
            int translation = Int32.Parse(textReader.ReadLine());
            string messageFromFile = textReader.ReadToEnd().ToString();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char element in messageFromFile)
                stringBuilder.Append(Convert.ToChar(element - translation).ToString());
            Console.Write("Decrypted message: " + stringBuilder.ToString());
        }
    }
}
