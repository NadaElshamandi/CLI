public class FSGrep
{
	public FSGrep()
	{
		this.Recursive = true;
	}

	public String RootPath { get; set; }
	public Boolean Recursive { get; set; }
	public String FileSearchMask { get; set; }
	public String FileSearchLinePattern { get; set; }

	public IEnumerable<String> GetFileNames()
	{
		if (!Directory.Exists(this.RootPath))
			throw new ArgumentException("GetFileNames() -- Can't find RootPath!");

		if (String.IsNullOrWhiteSpace(this.FileSearchMask))
			throw new ArgumentException("GetFileNames() -- FileSearchPattern is empty; use *.*!");

		var searchOptions = System.IO.SearchOption.AllDirectories;
		if (!Recursive)
			searchOptions = SearchOption.TopDirectoryOnly;

		if (FileSearchMask.Contains(','))
		{
			String[] masks = FileSearchMask.Split(',');
			var results = System.IO.Directory.EnumerateFiles(this.RootPath, masks[0], searchOptions);
			if (masks.Length > 1)
			{
				for (Int32 index = 1; index < masks.Length; index++)
				{
					results = results.Concat(System.IO.Directory.EnumerateFiles(this.RootPath, masks[index], searchOptions));
				}
			}
			return results;
		}
		else
		{
			return System.IO.Directory.EnumerateFiles(this.RootPath, this.FileSearchMask, searchOptions);
		}
	}

	public IEnumerable<Result> GetMatchingFiles()
	{
		foreach (var filePath in GetFileNames())
		{
			Int32 lineNumber = 0;
			foreach (var line in File.ReadAllLines(filePath))
			{
				if (System.Text.RegularExpressions.Regex.Match(line, this.FileSearchLinePattern).Success)
					yield return new Result() { FilePath = filePath, FileName = System.IO.Path.GetFileName(filePath), LineNumber = lineNumber, Line = line };

				lineNumber++;
			}
		}
	}

	public class Result
	{
		public String FilePath { get; set; }
		public String FileName { get; set; }
		public String Line { get; set; }
		public Int32 LineNumber { get; set; }

		public override string ToString()
		{
			return String.Format("--file {0}:{1}", FilePath, LineNumber);
		}
	}
}
