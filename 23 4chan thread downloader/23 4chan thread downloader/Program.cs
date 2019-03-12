using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _23_4chan_thread_downloader
{
    class ThreadDownloader
    {
        string Url { get; set; }
        WebClient Client { get; set; }
        string WebPageCode { get; set; }
        string NewWebPageCode { get; set; }
        string MyDocumentsFolderPath { get; set; }
        string SaveFolderPath { get; set; }
        string SaveFileName { get; set; }
        string ImagesDownloadUrl { get; set; }
        List<string> ImagesSrcs { get; set; }
        List<string> NewImageSrcs { get; set; }

        public ThreadDownloader()
        {
            Client = new WebClient();
            Client.Headers.Add("User-Agent: Other");
            MyDocumentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ImagesSrcs = new List<string>();
            NewImageSrcs = new List<string>();
        }

        public void GetInput()
        {
            Console.WriteLine("Pass the adress of the thread you want to download");
            while (true)
            {
                Url = Console.ReadLine();
                if (DownloadWebPageCode())
                    break;
                Console.WriteLine("Couldn't establish connection between you and the desired webpage, try again");
            }
            Console.WriteLine("Connection established, the webpage's code has been downloaded");
        }

        bool DownloadWebPageCode()
        {
            try
            {
                WebPageCode = Client.DownloadString(Url);
                return true;
            }
            catch { return false; }
        }

        public void DownloadImages()
        {

            int temp1 = Url.IndexOf("4chan.org") + 9;
            int temp2 = Url.IndexOf("thread/");
            ImagesDownloadUrl = "http://i.4cdn.org" + Url.Substring(temp1, temp2 - temp1);

            foreach (Match m in Regex.Matches(NewWebPageCode, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Multiline))
                ImagesSrcs.Add("http:" + m.Groups[1].Value); 
            
            int downloadedImgs = 0;
            foreach (var imgSrc in ImagesSrcs)
            {
                if (DownloadImage(imgSrc))
                    downloadedImgs++;
            }
            Console.WriteLine("Downloaded {0} images, downloading of {1} has failed", downloadedImgs, ImagesSrcs.Count - downloadedImgs);
        }

        bool DownloadImage(string src)
        {
            try
            {
                Client.DownloadFile(src, Path.Combine(SaveFolderPath, src.Substring(ImagesDownloadUrl.Length, src.Length - ImagesDownloadUrl.Length)));
                return true;
            }
            catch { return false; }
        }

        void UpdateSrcs()
        {
            foreach (var src in ImagesSrcs)
            {
                NewImageSrcs.Add(src.Substring(src.IndexOf(ImagesDownloadUrl), ImagesDownloadUrl.Length - 1));
                foreach (var newSrc in NewImageSrcs)
                {
                    NewWebPageCode.Replace(src.Substring(5, src.Length - 1), newSrc); // Remove http:
                }
            }
            Save(true);
        }

        string GetPosts()
        {
            int start = WebPageCode.IndexOf(@"<div class=""thread""");
            int end = WebPageCode.IndexOf(@"<div class=""navLinks navLinksBot desktop"">") - 4;
            return WebPageCode.Substring(start, end - start);
        }

        public void CreateANewWebPage()
        {
            string aboveThePosts = @"<!DOCTYPE html><html><head><meta charset=""utf - 8""></head><body>";
            string posts = GetPosts();
            string belowThePosts = @"</body></html>";

            NewWebPageCode = aboveThePosts + posts + belowThePosts;
        }

        public void Save(bool update)
        {
            if (!update)
            {
                SaveFolderPath = Path.Combine(MyDocumentsFolderPath, "4chan thread" + DateTime.Now.ToString("_MMddyyyy_HHmmss"));
                SaveFileName = "4chan thread" + DateTime.Now.ToString("_MMddyyyy_HHmmss") + ".html";
            }

            try
            {
                Directory.CreateDirectory(SaveFolderPath);
                Console.WriteLine("Folder with the saved thread: {0}", SaveFolderPath);
            }
            catch { Console.WriteLine("Couldn't create the save directory"); }
            try
            {
                using (StreamWriter streamWriter = File.CreateText(Path.Combine(SaveFolderPath, SaveFileName)))
                    streamWriter.WriteLine(NewWebPageCode);
            }
            catch { Console.WriteLine("Couldn't save the file"); }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ThreadDownloader threadDownloader = new ThreadDownloader();
            threadDownloader.GetInput();
            threadDownloader.CreateANewWebPage();
            threadDownloader.Save(false);
            threadDownloader.DownloadImages();
            Console.ReadLine();
        }
    }
}
