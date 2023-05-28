namespace LineWizard.Shared;

public static class FSGrep
{
    public static bool Recursive { get; set; } = false;

    private static IEnumerable<string> GetFileNames(string RootPath, string FileSearchMask)
    {
        if (!Directory.Exists(RootPath))
            throw new ArgumentException("GetFileNames() -- Can't find RootPath!");

        if (string.IsNullOrWhiteSpace(FileSearchMask))
            throw new ArgumentException("GetFileNames() -- FileSearchPattern is empty; use *.*!");

        var searchOptions = System.IO.SearchOption.AllDirectories;
        if (!Recursive)
            searchOptions = SearchOption.TopDirectoryOnly;

        if (FileSearchMask.Contains(','))
        {
            string[] masks = FileSearchMask.Split(',');
            var results = System.IO.Directory.EnumerateFiles(RootPath, masks[0], searchOptions);
            if (masks.Length > 1)
            {
                for (int index = 1; index < masks.Length; index++)
                {
                    results = results.Concat(System.IO.Directory.EnumerateFiles(RootPath, masks[index], searchOptions));
                }
            }
            return results;
        }
        else
        {
            return System.IO.Directory.EnumerateFiles(RootPath, FileSearchMask, searchOptions);
        }
    }

    public static IEnumerable<Result> GetMatchingFiles(string FileSearchLinePattern, string FileSearchMask, string RootPath)
    {

        foreach (var filePath in GetFileNames(RootPath, FileSearchMask))
        {
            var lineNumber = 0;
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (System.Text.RegularExpressions.Regex.Match(line, FileSearchLinePattern).Success)
                    yield return new Result() { FilePath = filePath, FileName = System.IO.Path.GetFileName(filePath), LineNumber = lineNumber, Line = line };

                lineNumber++;
            }
        }
    }

    public class Result
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Line { get; set; }
        public int LineNumber { get; set; }

        public override string ToString()
        {
            return string.Format("--file {0}:{1}", FilePath, LineNumber);
        }
    }
}
