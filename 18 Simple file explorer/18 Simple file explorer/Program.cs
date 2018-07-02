using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _18_Simple_file_explorer
{
    class FileExplorer
    {
        public string[] Drives { get; set; }

        public FileExplorer()
        {
            Drives = Directory.GetLogicalDrives();
        }

        #region Main functions
        public void Start()
        {
            Console.WriteLine("If you need help type help \n");
            GetInput();
        }

        public void GetInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                // Commands
                if (input.StartsWith("help") && input.Length == 4)
                {
                    DisplayHelp();
                }
                else if (input.StartsWith("exit") && input.Length == 4)
                {
                    Environment.Exit(0);
                }
                else if (input.StartsWith("sd .") && input.Length == 4)
                {
                    if (ValidateInput(input, 1))
                        ShowDirectory("", true);
                }
                else if (input.StartsWith("sd "))
                {
                    if (ValidateInput(input, 1))
                        ShowDirectory(GetPath(input, 3), false);
                }
                else if (input.StartsWith("crtd "))
                {
                    if (ValidateInput(input, 1))
                        Create(GetPath(input, 5), true);
                }
                else if (input.StartsWith("crtf "))
                {
                    if (ValidateInput(input, 1))
                        Create(GetPath(input, 5), false);
                }
                else if (input.StartsWith("deld "))
                {
                    if (ValidateInput(input, 1))
                        Delete(GetPath(input, 5), true);
                }
                else if (input.StartsWith("delf "))
                {
                    if (ValidateInput(input, 1))
                        Delete(GetPath(input, 5), false);
                }
                else if (input.StartsWith("rnd "))
                {
                    if (ValidateInput(input, 2))
                        Rename(GetPath(input, 4), true);
                }
                else if (input.StartsWith("rnf "))
                {
                    if (ValidateInput(input, 2))
                        Rename(GetPath(input, 4), false);
                }
                else
                    Console.WriteLine("Command not recognized");
                Console.WriteLine();
            }
        }

        void DisplayHelp()
        {
            Console.WriteLine("List of all avaliable commands:");
            Console.WriteLine("-> exit");
            Console.WriteLine("Closes the program");
            Console.WriteLine("-> sd pathToTheDirectory");
            Console.WriteLine("Lists all sub-directories and files within a directory, if the argument = '.' lists all disks with their content \n");
            Console.WriteLine("-> crtd pathToTheDirectory");
            Console.WriteLine("Creates a directory \n");
            Console.WriteLine("-> crtf pathToTheFile");
            Console.WriteLine("Creates a file \n");
            Console.WriteLine("-> deld pathToTheDirectory");
            Console.WriteLine("Deletes a directory \n");
            Console.WriteLine("-> delf PathToTheFile");
            Console.WriteLine("Deletes a file \n");
            Console.WriteLine("-> rnd pathToTheDirectory newName");
            Console.WriteLine("Renames a directory \n");
            Console.WriteLine("-> rnf pathToTheFile newName");
            Console.WriteLine("Renames a file \n");
        }
        #endregion

        #region Compositional functions
        bool ValidateInput(string input, int arguments)
        {
            int actualArguments = 0;
            foreach (var character in input)
                if (character == ' ')
                    actualArguments++;
            if (actualArguments == arguments)
                return true;
            else if (actualArguments < arguments)
            {
                Console.WriteLine("You're missing an argument(s)", arguments - actualArguments);
                return false;
            }
            else
            {
                Console.WriteLine("You've entered an additional argument(s)", arguments - actualArguments);
                return false;
            }
        }

        string GetPath(string input, int commandLength)
        {
            return input.Substring(commandLength, input.Length - commandLength);
        }

        bool Exists(string path, bool isDirectory)
        {
            if (isDirectory)
            {
                if (Directory.Exists(path))
                    return true;
                else
                {
                    Console.WriteLine("The directory doesn't exist");
                    return false;
                }
            }

            if (File.Exists(path))
                return true;
            else
            {
                Console.WriteLine("The file doesn't exist");
                return false;
            }
        }

        bool YesNoInput()
        {
            while (true)
            {
                char temp = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (temp == 'y')
                    return true;
                else if (temp == 'n')
                {
                    Console.WriteLine("Action aborted");
                    return false;
                }
                Console.WriteLine("Wrong input");
            }
        }
        #endregion

        #region Commands
        void ShowDirectory(string path, bool showHome)
        {
            if (showHome)
            {
                Console.WriteLine("Current directory: Home");
                foreach (var drive in Drives)
                {
                    try { ShowContent(drive, showHome); } catch { }
                }
            }
            else
            {
                ShowContent(path, showHome);
            }
        }

        void ShowContent(string path, bool showHome)
        {
            if(!showHome)
                if (!Exists(path, true))
                    return;
            if(!showHome)
                Console.WriteLine("Current directory: {0}", path);
            string[] directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
                    Console.WriteLine("- {0}", directory);
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            foreach (var file in files)
                Console.WriteLine("--- {0}", file);
        }

        void Create(string path, bool isDirectory)
        {
            // Directory
            if (isDirectory)
            {
                try
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("The directory has been created");
                }
                catch { Console.WriteLine("Invalid path"); }
                return;
            }

            // File
            if (File.Exists(path))
            {
                Console.WriteLine("The file already exists. Do you want to overwrite it? (y/n)");
                if (!YesNoInput())
                    return;
            }
            try
            {
                using (File.Create(path)) { }
                Console.WriteLine("The file has been created");
            }
            catch { Console.WriteLine("Invalid path"); }            
        }

        void Delete(string path, bool isDirectory)
        {
            // Directory
            if (isDirectory)
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("This operation will delete the directory, {0} sub-directories and {1} files inside it. " +
                        "Do you want to continue? (y/n)", new DirectoryInfo(path).GetDirectories().Length, new DirectoryInfo(path).GetFiles().Length);
                    if (YesNoInput())
                    {
                        Directory.Delete(path, true);
                        Console.WriteLine("The directory has been deleted");
                    }
                }
                else
                    Console.WriteLine("The directory doesn't exist");
                return;
            }

            // File
            if (File.Exists(path))
            {
                Console.WriteLine("This operation will delete the file. Do you want to continue? (y/n)");
                if (YesNoInput())
                {
                    FileStream fileStream = null;
                    try { fileStream = new FileInfo(path).Open(FileMode.Open, FileAccess.Read, FileShare.None); }
                    finally { if (fileStream != null) fileStream.Close(); }
                    File.Delete(path);
                    Console.WriteLine("The file has been deleted");
                }
            }
            else
                Console.WriteLine("The file doesn't exist");
        }

        void Rename(string path, bool isDirectory)
        {
            string newName = String.Empty;
            try
            {
                string[] temp = path.Split(' ');
                path = temp[0];
                newName = temp[1];
            }
            catch { }

            // Directory
            if (isDirectory)
            {
                if (Directory.Exists(Path.Combine(Directory.GetParent(path).ToString(), newName)))
                {
                    Console.WriteLine("The original name of the directory and the new one are the same");
                    return;
                }
                try
                {
                    Directory.Move(path, Path.Combine(Directory.GetParent(path).ToString(), newName));
                    Console.WriteLine("Renaming has been successfully finished");
                }
                catch
                {
                    if(Exists(path, true))
                        Console.WriteLine("Renaming failed");
                }
                return;
            }

            // File
            if (File.Exists(Path.Combine(Path.GetDirectoryName(path), newName)))
            {
                Console.WriteLine("The original name of the file and the new one are the same");
                return;
            }
            try
            {
                File.Move(path, Path.Combine(Path.GetDirectoryName(path), newName));
                Console.WriteLine("Renaming has been successfully finished");
            }
            catch
            {
                if(Exists(path, false))
                Console.WriteLine("Renaming failed");
            }
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            FileExplorer fileExplorer = new FileExplorer();
            fileExplorer.Start();
        }
    }
}
