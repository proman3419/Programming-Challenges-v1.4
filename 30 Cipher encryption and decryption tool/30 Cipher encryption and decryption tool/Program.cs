using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30_Cipher_encryption_and_decryption_tool
{
    class InterruptedKey
    {
        string Key { get; set; }
        string Message { get; set; }
        string Result { get; set; }
        string SavePath { get; set; }
        List<string> Parts { get; set; }

        public InterruptedKey()
        {
            SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "30 encrypted message.txt");
            Parts = new List<string>();
        }

        #region Compositional functions
        void GetInput(string temp1, string temp2, bool encrypt)
        {
            Console.WriteLine(temp1);
            Key = Console.ReadLine();
            if (encrypt)
            {
                Console.WriteLine(temp2);
                Message = Console.ReadLine();
            }            
        }

        void Reset()
        {
            Parts.Clear();
            Message = String.Empty;
        }

        void EncryptDecryptParts(bool encrypt)
        {
            List<string> temp = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string part in Parts)
            {
                for (int i = 0; i < part.Length; i++)
                {
                    if (encrypt)
                        stringBuilder.Append(Convert.ToChar(part[i] + Key[i]));
                    else
                        stringBuilder.Append(Convert.ToChar(part[i] - Key[i]));
                }
                temp.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            Parts = temp;
        }
        #endregion

        #region Encrypt
        public void Encrypt()
        {
            GetInput(@"Input the key you wanted to use, for example ""APPLE""", "Input the message you want to encrypt", true);
            while (Message != String.Empty)
                Parts.Add(GetASubstring());
            EncryptDecryptParts(true);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string part in Parts)
                stringBuilder.Append(part + " ");
            Message = stringBuilder.ToString();
            Save();
            Reset();
        }

        string GetASubstring()
        {
            string temp;
            if (Message.Length >= Key.Length)
            {
                Random random = new Random();
                int length = random.Next(1, Key.Length - 1);
                temp = Message.Substring(0, length);
                Message = Message.Substring(length);
            }
            else
            {
                temp = Message.Substring(0, Message.Length);
                Message = String.Empty;
            }

            return temp;
        }

        void Save()
        {
            File.WriteAllText(SavePath, Message);
            TextWriter textWriter = new StreamWriter(SavePath, false);
            textWriter.Write(Message);
            textWriter.Close();
            Console.WriteLine("Your message has been encrypted and saved to this file:" + Environment.NewLine + SavePath);
        }
        #endregion

        #region Decrypt
        public void Decrypt()
        {
            ReadFromFile();
            GetInput("Input the key", " ", false);
            EncryptDecryptParts(false);
            Display();
            Reset();
        }

        void ReadFromFile()
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
            Message = textReader.ReadToEnd().ToString();
            string[] temp = Message.Split(' ');
            foreach (string part in temp)
                Parts.Add(part);
        }

        void Display()
        {
            Console.Write("Decrypted message: ");
            foreach (string part in Parts)
                Console.Write(part);
            Console.WriteLine();
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            InterruptedKey interruptedKey = new InterruptedKey();

            switch (GetInput())
            {
                case 1:
                    interruptedKey.Encrypt();
                    break;
                case 2:
                    interruptedKey.Decrypt();
                    break;
            }

            Console.ReadLine();
        }

        static int GetInput()
        {
            int input;
            Console.WriteLine("Input 1 to encrypt a message, input 2 to decrypt a message");
            while (!Int32.TryParse(Console.ReadKey().KeyChar.ToString(), out input))
                Console.WriteLine("\nInvalid input");
            Console.WriteLine();
            return input;
        }
    }
}
