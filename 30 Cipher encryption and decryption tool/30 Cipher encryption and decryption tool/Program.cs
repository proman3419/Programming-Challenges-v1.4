using System;
using System.Collections.Generic;
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

        void GetInput(string temp1, string temp2)
        {
            Console.WriteLine(temp1);
            Key = Console.ReadLine();
            Console.WriteLine(temp2);
            Message = Console.ReadLine();
        }

        #region Encrypt
        public void Encrypt()
        {
            GetInput(@"Input the key you wanted to use, for example ""APPLE""", "Input the message you want to encrypt");
            List<string> parts = new List<string>();
            while (Message != String.Empty)
                parts.Add(GetASubstring());
            Display(EncryptDecryptParts(parts, true));
        }

        string GetASubstring()
        {
            string temp;
            if (Message.Length >= Key.Length)
            {
                Random random = new Random();
                int length = random.Next(1, Key.Length - 1);
                temp = Message.Substring(0, length);
                Message = Message.Substring(length - 1, Message.Length - length);
            }
            else
            {
                temp = Message.Substring(0, Message.Length);
                Message = String.Empty;
            }

            return temp;
        }
        #endregion

        #region Decrypt
        public void Decrypt()
        {
            GetInput("Input the key", "Input the encrypted message");
            List<string> parts = new List<string>();
            string[] temp = Message.Split(' ');
            for (int i = 0; i < temp.Length; i++)
                parts.Add(temp[i]);

            Display(EncryptDecryptParts(parts, false));
        }
        #endregion

        List<string> EncryptDecryptParts(List<string> parts, bool encrypt)
        {
            List<string> temp = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string part in parts)
            {
                for (int i = 0; i < part.Length; i++)
                {
                    if (encrypt)
                    {
                        Console.WriteLine(part[i] + "|" + Key[i]);
                        int a = part[i];
                        int b = Key[i];
                        int c = part[i] + Key[i];
                        Console.WriteLine(a + "|" + b + "|" + c);
                        stringBuilder.Append(Convert.ToChar(part[i] + Key[i]));
                    }

                    else
                    {
                        Console.WriteLine(part[i] + "|" + Key[i]);
                        int a = part[i];
                        int b = Key[i];
                        int c = part[i] - Key[i];
                        Console.WriteLine(a + "|" + b + "|" + c);
                        //stringBuilder.Append(Convert.ToChar(Convert.ToInt32(part[i] - Key[i])));
                    }


                }

                temp.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            return temp;
        }

        void Display(List<string> message)
        {
            foreach (string part in message)
            {
                foreach (char character in part)
                {
                    string temp = HexStringToString(((int)character).ToString("X4"));
                    Console.Write(temp);
                }
            }            
            Console.WriteLine();
        }

        string HexStringToString(string hexString)
        {
            if (hexString == null || (hexString.Length & 1) == 1)
            {
                throw new ArgumentException();
            }
            var sb = new StringBuilder();
            for (var i = 0; i < hexString.Length; i += 2)
            {
                var hexChar = hexString.Substring(i, 2);
                sb.Append((char)Convert.ToByte(hexChar, 16));
            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            InterruptedKey interruptedKey = new InterruptedKey();

            interruptedKey.Encrypt();
            interruptedKey.Decrypt();

            Console.ReadLine();
        }
    }
}
