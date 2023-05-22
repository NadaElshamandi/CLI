using System;
using System.IO;
using System.Collections.Generic;

namespace CMDproject
{
    public static class FileOperations
    {
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public static bool ChangeDirectory(string directory)
        {
            string newDirectory = Path.Combine(GetCurrentDirectory(), directory);
            if (Directory.Exists(newDirectory))
            {
                Directory.SetCurrentDirectory(newDirectory);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> GetFileList()
        {
            string[] files = Directory.GetFiles(GetCurrentDirectory());
            return new List<string>(files);
        }

        public static string ReadFile(string filename)
        {
            string filePath = Path.Combine(GetCurrentDirectory(), filename);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                return "File not found.";
            }
        }

        public static bool DeleteFile(string filename)
        {
            string filePath = Path.Combine(GetCurrentDirectory(), filename);
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            return false;
        }

        public static bool CreateFile(string filename)
        {
            string filePath = Path.Combine(GetCurrentDirectory(), filename);
            try
            {
                File.Create(filePath).Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the file: {ex.Message}");
            }
            return false;
        }

        public static bool CopyFile(string sourceFilename, string destinationFilename)
        {
            string sourceFile = Path.Combine(GetCurrentDirectory(), sourceFilename);
            string destinationFile = Path.Combine(GetCurrentDirectory(), destinationFilename);
            if (File.Exists(sourceFile))
            {
                try
                {
                    File.Copy(sourceFile, destinationFile);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while copying the file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Source file not found.");
            }
            return false;
        }

        public static bool CreateDirectory(string directoryName)
        {
            string newDirectory = Path.Combine(GetCurrentDirectory(), directoryName);
            try
            {
                Directory.CreateDirectory(newDirectory);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the directory: {ex.Message}");
            }
            return false;
        }
    }
}
