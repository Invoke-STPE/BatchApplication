using BatchApplication.Core;

namespace BatchApplication;

public class FileSystemReader : IFileSystem
{
    public IEnumerable<string>? EnumerateFiles(string path)
    {
        return Directory.EnumerateFiles(path);
    }

    public StreamReader OpenFile(string path)
    {
        return new StreamReader(path);
    }
}
