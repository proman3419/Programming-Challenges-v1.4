using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _12_Internet_time
{
    class Program
    {
        static DateTime GetTime()
        {
            DateTime dateTime = DateTime.MinValue;
            using (WebClient webClient = new WebClient())
            {
                byte[] temp = webClient.DownloadData("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
                StringBuilder stringBuilder = new StringBuilder();
                string stringFromWebpage;

                for (int i = 0; i < temp.Length; i++)
                {
                    stringBuilder.Append(Convert.ToChar(temp[i]));
                }
                stringFromWebpage = stringBuilder.ToString();
                string time = Regex.Match(stringFromWebpage, @"(?<=\btime="")[^""]*").Value;
                double milliseconds = Convert.ToInt64(time) / 1000.0;
                dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            }
            return dateTime;
        }

        static void Main(string[] args)
        {
            DateTime dateTime = GetTime();
            Console.WriteLine("Current time: " + dateTime.TimeOfDay);
            Console.ReadLine();
        } 
    }
}
