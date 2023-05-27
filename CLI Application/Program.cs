using CommandLine.Text;
using CommandLine;
using CLI_Application;
using System;

void RunOptions(Options options)
{
    string currentDirectory = Directory.GetCurrentDirectory();

    if (options.cd is not null)
    {
        //Directory.Exists(options.cd);
    }

    else if (options.ls)
    {
        var filesAndFolders = CommandPrompt.ListFiles(currentDirectory);

        Console.WriteLine("Files:");
        foreach (string file in filesAndFolders.files)
        {
            Console.WriteLine(file);
        }

        Console.WriteLine("\nDirectories:");
        foreach (string folder in filesAndFolders.folders)
        {
            Console.WriteLine(folder);
        }
    }

    else if (options.cat is not null)
    {
        string filePath = Path.Combine(currentDirectory, options.cat);
        string fileContent = CommandPrompt.ReadFile(filePath);
        if (!String.IsNullOrEmpty(fileContent))
        {
            Console.WriteLine(fileContent);
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    else if (options.rm is not null)
    {
        string filePath = Path.Combine(currentDirectory, options.rm);
        if (CommandPrompt.RemoveFile(filePath))
        {
            Console.WriteLine($"File deleted: {options.rm}");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    else if (options.touch is not null)
    {
        string filePath = Path.Combine(currentDirectory, options.touch);
        if (CommandPrompt.CreateFile(filePath))
        {
            Console.WriteLine($"File created: {options.touch}");
        }
        else
        {
            Console.WriteLine("File already exists.");
        }
    }

    else if (options.cp is not null && options.cp.Count() == 2)
    {
        string sourceFilePath = Path.Combine(currentDirectory, options.cp.ElementAt(0));
        string destinationFilePath = Path.Combine(currentDirectory, options.cp.ElementAt(1));

        if (CommandPrompt.CopyFile(sourceFilePath, destinationFilePath))
        {
            Console.WriteLine($"File copied: {options.cp.ElementAt(0)}");
        }
        else
        {
            Console.WriteLine("Source file not found.");
        }
    }

    else if (options.mkdir is not null)
    {
        string directoryName = options.mkdir;
        string directoryPath = Path.Combine(currentDirectory, directoryName);
        if (CommandPrompt.CreateDirectory(directoryPath))
        {
            Console.WriteLine($"Directory created: {directoryName}");
        }
        else
        {
            Console.WriteLine("Directory already exists.");
        }
    }

    else if (options.echo is not null)
    {
        foreach (var value in options.echo)
        {
            Console.WriteLine(value);
        }
    }
}

void GrepOptions(GrepOptions options)
{
    string currentDirectory = Directory.GetCurrentDirectory();
    FSGrep.Recursive = options.reccursive;
    var results = FSGrep.GetMatchingFiles(options.FileSearchLinePattern, options.FileSearchMask, currentDirectory);
    foreach (var result in results)
    {
        Console.WriteLine(result);
    }
}
void HandleParseError(IEnumerable<Error> errs)
{
    //handle errors
}
Parser.Default.ParseArguments<Options, GrepOptions>(args)
     .WithParsed<Options>(RunOptions)
     .WithParsed<GrepOptions>(GrepOptions)
     .WithNotParsed(HandleParseError);


[Verb("options", isDefault: true, HelpText = "Generic Options")]
class Options
{
    [Option(HelpText = "Change directory to", Required = true, SetName = "cd")]
    public string cd { get; set; }


    [Option(HelpText = "list files in directory", Required = true, SetName = "ls")]
    public bool ls { get; set; }

    [Option(HelpText = "Display file contents in <path>", Required = true, SetName = "cat")]
    public string cat { get; set; }

    [Option(HelpText = "Remove a file in <path>", Required = true, SetName = "rm")]
    public string rm { get; set; }

    [Option(HelpText = "Create a file in <path>", Required = true, SetName = "touch")]
    public string touch { get; set; }

    [Option(HelpText = "Copy a file from <path> to <path>", Min = 2, Max = 2, Required = true, SetName = "cp")]
    public IEnumerable<string> cp { get; set; }

    [Option(HelpText = "Create a directory named <path>", Required = true, SetName = "mkdir")]
    public string mkdir { get; set; }

    [Option(HelpText = "Display status text of the <path>", Required = true, SetName = "echo")]
    public IEnumerable<string> echo { get; set; }
}

[Verb("grep", HelpText = "for searching plain-text data sets for lines that match a regular expression")]
class GrepOptions
{
    [Option('r', HelpText = "run recurssively", SetName = "r", Default = false)]
    public bool reccursive { get; set; }

    [Value(0, Required = true, HelpText = "File Search Line Pattern")]
    public string FileSearchLinePattern { get; set; }

    [Value(1, Required = true, HelpText = "File Search Mask")]
    public string FileSearchMask { get; set; }

}