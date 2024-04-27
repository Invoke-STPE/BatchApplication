using BatchApplication.Domain;
using CsvHelper;
using CsvHelper.Configuration;
using MoreLinq;
using System.Globalization;

namespace _027_BookingDateBatchJob;
public class BaseBatchJob
{
    public string FilePath { get; init; }

    public IEnumerable<T[]> LoadCsvFiles<T, TMap>(Action<IEnumerable<T[]>> prettyPrint) where TMap : ClassMap<T>
    {
        var file = FilePath + "\\" + "Meetings_16-04-2024.csv";
        var badData = new List<string>();

        using var reader = new StreamReader(file);
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            BadDataFound = context => badData.Add(context.RawRecord)
        };

        using var csvReader = new CsvReader(reader, csvConfiguration);
        csvReader.Context.RegisterClassMap<TMap>();

        var records = csvReader.GetRecords<T>().Batch(BatchJobSize.Size);
        prettyPrint(records);

        return records;
    }
    //public IEnumerable<T> LoadCsvFiles<T, TMap>() where TMap : ClassMap<T>
    //{
    //    //foreach (var file in Directory.GetFiles(FilePath, "*.csv"))
    //    //{

    //    //}
    //    var file = FilePath + "\\" + "Meetings_16-04-2024.csv";
    //    var badData = new List<string>();

    //    using var reader = new StreamReader(file);
    //    var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
    //    {
    //        Delimiter = ";",
    //        BadDataFound = context => badData.Add(context.RawRecord)
    //    };

    //    using var csvReader = new CsvReader(reader, csvConfiguration);
    //    csvReader.Context.RegisterClassMap<TMap>();
    //    var records = csvReader.GetRecords<T>();

    //    Console.WriteLine(records.Count());

    //    return records;
    //}
}
