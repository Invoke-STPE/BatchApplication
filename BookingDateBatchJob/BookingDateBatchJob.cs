
using BatchApplication.Domain;
using BatchApplication.Domain.BatchJobInterfaces;

namespace _027_BookingDateBatchJob.Domain.BatchJobInterfaces;
[BatchJob]
public class BookingDateBatchJob : BaseBatchJob, IBookingDateBatchJob
{
    public BookingDateBatchJob()
    {
        FilePath = $"C:\\Users\\Spe-Solita\\Documents\\BatchJobs\\{BatchId}_{BatchJobName}";
    }

    public string BatchId => BatchJobIds.BookingDate;
    public string BatchJobName { get; init; } = "BookingDate";

    public void Start()
    {
        LoadCsvFiles<BookingDateDTO, BookingDateMap>(ProcessRecords);

    }

    public void ProcessRecords(IEnumerable<BookingDateDTO[]> batches)
    {
        foreach (var batch in batches)
        {
            foreach (var bookingDate in batch)
            {
                Console.WriteLine($"{bookingDate.RowNumber}: {bookingDate.BookingDate} by {bookingDate.BookedBy} ");
            }
        }
    }


}
