using System.Globalization;
using BatchApplication.Core;

namespace BatchApplication;

public class CsvReaderService : ICsvReader
{
    private readonly IFileSystem _fileSystem;

    public CsvReaderService(IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
    }

    public T[] ReadAll<T>(string folderPath)
    {
        IEnumerable<string>? files = LoadFilesFromPath<T>(folderPath);

        if (files.Any() == false) { return Array.Empty<T>(); }

        List<T> allRecords = [];

        foreach (var file in files)
        {
            try
            {
                using var reader = _fileSystem.OpenFile(file);
                using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<T>();
                allRecords.AddRange(records);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {file}: {ex.Message}");
            }
        }

        return [.. allRecords];
    }

    public void ReadInChunks<T>(string folderPath, int chunkSize, Action<List<T>> processChunk)
    {
        IEnumerable<string>? files = LoadFilesFromPath<T>(folderPath);

        if (files.Any() == false) { return; }

        foreach (var file in files)
        {
            try
            {
                using var reader = _fileSystem.OpenFile(file);
                using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

                var records = new List<T>();
                
                while (csv.Read())
                {
                  records.Add(csv.GetRecord<T>());
                  if (records.Count == chunkSize)
                  {
                    processChunk(records);
                    records.Clear();
                  }
                }

                if (records.Any())
                {
                  processChunk(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {file}: {ex.Message}");
            }
        }
    }

    public void ReadLineByLine(string filePath, Action<string> processLine)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<T> Stream<T>(string filePath)
    {
      throw new NotImplementedException();
    }

    private IEnumerable<string> LoadFilesFromPath<T>(string folderPath)
    {
        var files = _fileSystem.EnumerateFiles(folderPath);

        if (files == null || files.Any() == false)
        {
            Console.WriteLine($"No CSV files found in folder: {folderPath}");
            return Enumerable.Empty<string>();
        }

        return files;
    }
}
