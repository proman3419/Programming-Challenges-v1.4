using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _44_Text_to_Morse_code_translator
{
    class TextToMorse
    {
        Dictionary<char, string> Alphabet { get; set; }
        string Input { get; set; }
        string Encoded { get; set; }

        public TextToMorse()
        {
            #region Alphabet Initialization
            Alphabet = new Dictionary<char, string>();
            Alphabet.Add('a', ".—");
            Alphabet.Add('b', "-...");
            Alphabet.Add('c', "-.-.");
            Alphabet.Add('d', "-..");
            Alphabet.Add('e', ".");
            Alphabet.Add('f', "..-.");
            Alphabet.Add('g', "--.");
            Alphabet.Add('h', "....");
            Alphabet.Add('i', "..");
            Alphabet.Add('j', ".---");
            Alphabet.Add('k', "-.-");
            Alphabet.Add('l', ".-..");
            Alphabet.Add('m', "--");
            Alphabet.Add('n', "-.");
            Alphabet.Add('o', "---");
            Alphabet.Add('p', ".--.");
            Alphabet.Add('q', "--.-");
            Alphabet.Add('r', ".-.");
            Alphabet.Add('s', "...");
            Alphabet.Add('t', "-");
            Alphabet.Add('u', "..-");
            Alphabet.Add('v', "...-");
            Alphabet.Add('w', ".--");
            Alphabet.Add('x', "-..-");
            Alphabet.Add('y', "-.--");
            Alphabet.Add('z', "--..");
            Alphabet.Add('0', "-----");
            Alphabet.Add('1', ".----");
            Alphabet.Add('2', "..---");
            Alphabet.Add('3', "...--");
            Alphabet.Add('4', "....-");
            Alphabet.Add('5', ".....");
            Alphabet.Add('6', "-....");
            Alphabet.Add('7', "--...");
            Alphabet.Add('8', "---..");
            Alphabet.Add('9', "----.");
            Alphabet.Add('.', ".-.-.-");
            Alphabet.Add(',', "--..--");
            Alphabet.Add('?', "..--..");
            Alphabet.Add('!', "-.-.--");
            Alphabet.Add('_', "..--.-");
            #endregion

            Translate();
            Display();
        }

        void Translate()
        {
            do
            {
                Input = Console.ReadLine().ToLower().Replace(' ', '_');
            } while (String.IsNullOrEmpty(Input));        
            for (int i = 0; i < Input.Length; i++)
            {
                if (Alphabet.TryGetValue(Input[i], out string temp))
                    Encoded += temp + " "; 
            }
            Console.WriteLine(Encoded);
        }

        void Display()
        {
            for (int i = 0; i < Encoded.Length; i++)
            {
                if (Encoded[i] == '.')
                    Console.Beep(10000, 500);
                else if (Encoded[i] == '-')
                    Console.Beep(10000, 1500);
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextToMorse textToMorse = new TextToMorse();

            Console.ReadLine();
        }
    }
}
