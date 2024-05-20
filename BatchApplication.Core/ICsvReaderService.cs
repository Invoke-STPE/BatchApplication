namespace BatchApplication.Core;

public interface ICsvReaderService
{
  T[] ReadAll<T>(string folderPath);
  void ReadLineByLine(string filePath, Action<string> processLine);
  void ReadInChunks<T>(string filePath, int chunkSize, Action<List<T>> processChunk);
  IEnumerable<T> Stream<T>(string filePath);
}
