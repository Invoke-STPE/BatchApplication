using System.ComponentModel;
using System.Text;
using BatchApplication.Core;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.ReturnsExtensions;

namespace BatchApplication.Tests;

[TestClass]
public class CsvReaderTests
{
    public IFileSystem FileSystem { get; set; } = null!;

    [TestInitialize]
    public void Setup()
    {
        FileSystem = Substitute.For<IFileSystem>();
        
    }

    [TestCleanup]
    public void Cleanup()
    {
        FileSystem.ClearSubstitute();
    }

    [TestClass]
    public class ReadAll : CsvReaderTests
    {
        [TestMethod]
        public void ReadAll_WhenCsvFilesPresents_ReturnsAllRecords()
        {
            int expected = 3;
            FileSystem.EnumerateFiles("path/to/file").Returns(["file1.csv", "file2.csv"]);
            FileSystem.OpenFile("file1.csv").Returns(new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Column1,Column2\nValue1,Value2"))));
            FileSystem.OpenFile("file2.csv").Returns(new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Column1,Column2\nValue1,Value2\nValue1,Value2"))));

            CsvReaderService csvReaderService = new(FileSystem);

            var records = csvReaderService.ReadAll<dynamic>("path/to/file");
            var actual = records.Length;
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadAll_WhenNoCsvFilesPresent_ReturnsEmptyArray() 
        {
            var expected = 0;

            FileSystem.EnumerateFiles("path/to/file").ReturnsNull();

            CsvReaderService csvReaderService = new(FileSystem);

            var records = csvReaderService.ReadAll<dynamic>("path/to/file");
            var actual = records.Length;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadAll_WhenCsvFilesAreEmpty_ReturnsEmptyArray() 
        {
            var expected = 0;

            FileSystem.EnumerateFiles("path/to/file").Returns(["file1.csv", "file2.csv"]);
  
            CsvReaderService csvReaderService = new(FileSystem);

            var records = csvReaderService.ReadAll<dynamic>("path/to/file");
            var actual = records.Length;

            Assert.AreEqual(expected, actual);
        }

        // [TestMethod]
        // [Category("Integration")]
        // public void ReadAll_WhenCsvFileIsVeryLarge_ProcessesAllRecords()
        // {
        //     int expected = 100000; // Assuming we have 100,000 records for an integration test

        //     var csvContent = new StringBuilder("Column1,Column2\n");
        //     for (int i = 0; i < 100000; i++)
        //     {
        //         csvContent.AppendLine($"Value1_{i},Value2_{i}");
        //     }

        //     var filePath = "path/to/largefile.csv";
        //     File.WriteAllText(filePath, csvContent.ToString());

        //     CsvReaderService csvReaderService = new CsvReaderService(FileSystem); // Use real file system or a more sophisticated mock

        //     var records = csvReaderService.ReadAll<dynamic>("path/to");
        //     var actual = records.Length;

        //     Assert.AreEqual(expected, actual);
        // }
    }
}