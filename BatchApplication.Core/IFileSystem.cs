namespace BatchApplication.Core;

public interface IFileSystem
{
  IEnumerable<string>? EnumerateFiles(string path);
  StreamReader OpenFile(string path);
}
