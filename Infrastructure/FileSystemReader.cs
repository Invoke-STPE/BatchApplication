using Domain;

namespace Infrastructure;

public class FileSystemReader : IFileSystem
{
    public StreamReader OpenFile(string path)
    {
        return new StreamReader(path);
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        return Directory.EnumerateFiles(path) ?? Enumerable.Empty<string>();
    }

    public IEnumerable<string> LoadFilesFromPath(string folderPath)
    {
        var files = EnumerateFiles(folderPath);

        if (files == null || files.Any() == false)
        {
            Console.WriteLine($"No CSV files found in folder: {folderPath}");
            return Enumerable.Empty<string>();
        }

        return files;
    }
}