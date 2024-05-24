﻿using System.Globalization;
using Domain;

namespace Infrastructure;

public class CsvReaderService : ICsvReaderService
{
    private readonly IFileSystem _fileSystem;

    public CsvReaderService(IFileSystem fileSystem)
    {
      _fileSystem = fileSystem;
    }

    /// <summary>
    /// Reads all records of type <typeparamref name="T"/> from all CSV files in the specified folder and returns them as an array.
    /// </summary>
    /// <typeparam name="T">The type of the records to be read from the CSV files.</typeparam>
    /// <param name="folderPath">The path to the folder containing the CSV files.</param>
    /// <returns>An array of all records of type <typeparamref name="T"/> from the CSV files.</returns>
    /// <remarks>
    /// This method reads all records from the CSV files in the specified folder and returns them as a single array.
    /// It is intended for use with smaller files where loading all records into memory at once is feasible.
    /// If an error occurs while processing a file, the error is logged, and the method continues with the next file.
    /// </remarks>
    public T[] ReadAll<T>(string folderPath)
    {
        IEnumerable<string> files = _fileSystem.LoadFilesFromPath(folderPath);

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

    /// <summary>
    /// Reads records of type <typeparamref name="T"/> from all CSV files in the specified folder in chunks and processes each chunk.
    /// </summary>
    /// <typeparam name="T">The type of the records to be read from the CSV files.</typeparam>
    /// <param name="folderPath">The path to the folder containing the CSV files.</param>
    /// <param name="chunkSize">The number of records in each chunk to be processed.</param>
    /// <param name="processChunk">An action to process each chunk of records.</param>
    /// <remarks>
    /// This method reads records from the CSV files in the specified folder in chunks of the given size.
    /// Each chunk is processed using the provided <paramref name="processChunk"/> action.
    /// If an error occurs while processing a file, the error is logged, and the method continues with the next file.
    /// </remarks>
    public void ReadInChunks<T>(string folderPath, int chunkSize, Action<List<T>> processChunk)
    {
        IEnumerable<string> files = _fileSystem.LoadFilesFromPath(folderPath);

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

    /// <summary>
    /// Reads a file line by line and processes each line using the specified action.
    /// </summary>
    /// <param name="filePath">The path to the file to be read.</param>
    /// <param name="processLine">An action to process each line of the file.</param>
    /// <remarks>
    /// This method is useful for processing very large files where the overhead of using CSV parsing might be too high,
    /// or when pre-processing is needed before loading the data with a CSV helper.
    /// </remarks>
    public void ReadLineByLine(string folderPath, Action<string> processLine)
    {
      IEnumerable<string> files = _fileSystem.LoadFilesFromPath(folderPath);

      foreach (var file in files)
      {
        using (var reader = new StreamReader(file))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                processLine(line);
            }
        }
      }
    }

    /// <summary>
    /// Streams records of type <typeparamref name="T"/> from all CSV files in the specified folder.
    /// </summary>
    /// <typeparam name="T">The type of the records to be read from the CSV files.</typeparam>
    /// <param name="folderPath">The path to the folder containing the CSV files.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> that streams records of type <typeparamref name="T"/> from the CSV files.
    /// </returns>
    /// <remarks>
    /// This method loads all file paths from the specified folder, opens each file, reads records using CsvHelper, 
    /// and streams the records as objects of type <typeparamref name="T"/>.
    /// </remarks>
    public IEnumerable<T> Stream<T>(string folderPath)
    {
      IEnumerable<string> files = _fileSystem.LoadFilesFromPath(folderPath);

      foreach (var file in files)
      {
        using (var reader = _fileSystem.OpenFile(file))
        using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
        {
            foreach (var record in csv.GetRecords<T>())
            {
                yield return record;
            }
        }
      }
    }
}