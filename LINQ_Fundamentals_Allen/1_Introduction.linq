<Query Kind="Program" />

void Main()
{
	string path = @"C:\Windows";
	ShowLargeFilesWithoutLinq(path);
	Console.WriteLine("\n*****\n");
	ShowLargeFilesWithLinq(path);
}

public void ShowLargeFilesWithoutLinq(string path)
{
	DirectoryInfo directory = new DirectoryInfo(path);
	FileInfo[] files = directory.GetFiles();
	Array.Sort(files, new FileInfoComparer());
	
	for(int i = 0; i < 5; i++)
	{
		FileInfo file = files[i];
		Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
	}
}

public class FileInfoComparer : IComparer<FileInfo>
{
	public int Compare(FileInfo x, FileInfo y)
	{
		return y.Length.CompareTo(x.Length);
	}
}

public void ShowLargeFilesWithLinq(string path)
{
	// Version A of LINQ Query
	//	var query = from file in new DirectoryInfo(path).GetFiles()
	//					orderby file.Length descending
	//					select file;

	// Version B of LINQ Query
	//	var query = new DirectoryInfo(path).GetFiles()
	//						.OrderByDescending(f => f.Length)
	//						.Take(5);
	//
	//	foreach (var file in query) // if using version A of LINQ, use query.Take(5)
	//	{
	//		Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
	//	}

	// Version C of LINQ Query
	new DirectoryInfo(path).GetFiles()
						.OrderByDescending(f => f.Length)
						.Take(5)
						.Select(file => $"{file.Name,-20} : {file.Length,10:N0}")
						.ToList()
						.ForEach(Console.WriteLine);
}