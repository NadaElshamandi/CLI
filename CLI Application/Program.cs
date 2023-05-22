
Console.WriteLine("Welcome to the Command-Line Shell Application!");

string currentDirectory = Directory.GetCurrentDirectory();
bool exit = false;

while (!exit)
{
    Console.WriteLine("\nWhat would you like to do today?");
    Console.WriteLine("1. Navigate directories (cd)");
    Console.WriteLine("2. List files and directories (ls)");
    Console.WriteLine("3. Display file contents (cat)");
    Console.WriteLine("4. Remove a file (rm)");
    Console.WriteLine("5. Create a file (touch)");
    Console.WriteLine("6. Copy a file (cp)");
    Console.WriteLine("7. Create a directory (mkdir)");
    Console.WriteLine("8. Exit the program");

    Console.Write("Enter your choice: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter the directory path: ");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                currentDirectory = Path.GetFullPath(path);
                Console.WriteLine($"Current directory changed to: {currentDirectory}");
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
            break;

        case "2":
            string[] files = Directory.GetFiles(currentDirectory);
            string[] directories = Directory.GetDirectories(currentDirectory);

            Console.WriteLine("Files:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            Console.WriteLine("\nDirectories:");
            foreach (string directory in directories)
            {
                Console.WriteLine(Path.GetFileName(directory));
            }
            break;

        case "3":
            Console.Write("Enter the filename: ");
            string fileName = Console.ReadLine();
            string filePath = Path.Combine(currentDirectory, fileName);
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine(fileContent);
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            break;

        case "4":
            Console.Write("Enter the filename: ");
            fileName = Console.ReadLine();
            filePath = Path.Combine(currentDirectory, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"File deleted: {fileName}");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            break;

        case "5":
            Console.Write("Enter the filename: ");
            fileName = Console.ReadLine();
            filePath = Path.Combine(currentDirectory, fileName);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                Console.WriteLine($"File created: {fileName}");
            }
            else
            {
                Console.WriteLine("File already exists.");
            }
            break;

        case "6":
            Console.Write("Enter the source filename: ");
            string sourceFileName = Console.ReadLine();
            Console.Write("Enter the destination filename: ");
            string destinationFileName = Console.ReadLine();
            string sourceFilePath = Path.Combine(currentDirectory, sourceFileName);
            string destinationFilePath = Path.Combine(currentDirectory, destinationFileName);
            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destinationFilePath);
                Console.WriteLine($"File copied: {destinationFileName}");
            }
            else
            {
                Console.WriteLine("Source file not found.");
            }
            break;

        case "7":
            Console.Write("Enter the directory name: ");
            string directoryName = Console.ReadLine();
            string directoryPath = Path.Combine(currentDirectory, directoryName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory created: {directoryName}");
            }
            else
            {
                Console.WriteLine("Directory already exists.");
            }
            break;

        case "8":
            exit = true;
            break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

Console.WriteLine("Thank you for using the Command-Line Shell Application!");
