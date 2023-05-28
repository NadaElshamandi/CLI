namespace LineWizard.Shared;

public class ListFilesReturn
{
    public List<string> files { get; set; } = new List<string>();
    public List<string> folders { get; set; } = new List<string>();
}

public static class CommandPrompt
{
    public static ListFilesReturn ListFiles(string path)
    {
        string[] files = Directory.GetFiles(path);
        string[] directories = Directory.GetDirectories(path);

        var returnValue = new ListFilesReturn();
        foreach (string file in files)
        {
            returnValue.files.Add(file);
        }


        foreach (string directory in directories)
        {
            returnValue.folders.Add(directory);
        }

        return returnValue;
    }

    public static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        else
        {
            return "";
        }
    }

    public static bool RemoveFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CreateFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath))
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CopyFile(string sourceFilePath, string destinationFilePath)
    {
        if (File.Exists(sourceFilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath) ?? "");
            File.Copy(sourceFilePath, destinationFilePath, true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CreateDirectory(string directoryPath)
    {
        bool success = false;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            success = true;
        }
        else
        {
            success = false;
        }
        return success;
    }

    public static string SearchFileContent(string path, string searchTerm)
    {
        try
        {
            string[] files = Directory.GetFiles(path);

            string results = string.Empty;

            foreach (string file in files)
            {
                string[] lines = File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    if (line.Contains(searchTerm))
                    {
                        results += file + ": " + line + Environment.NewLine;
                    }
                }
            }

            if (!string.IsNullOrEmpty(results))
            {
                return results;
            }
            else
            {
                return "No matching results found";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    public static string Exit()
    {
        // You can add any necessary cleanup or finalization code here
        Environment.Exit(0);
        return string.Empty;
    }
}
