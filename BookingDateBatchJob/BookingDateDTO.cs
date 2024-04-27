using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace _027_BookingDateBatchJob
{
    public class BookingDateDTO
    {
        public int? RowNumber { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public string? CompanyId { get; set; }
        public int? BookedBy { get; set; }
        public DateTime MeetingDate { get; set; }
        public string? CPR { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? Advisor { get; set; }
        public int? BrugerId { get; set; }
        public string? Kanal { get; set; }
        public string? CRMID { get; set; }
        public string? ImportId { get; set; }
        public string? StatusType { get; set; }
        public string? Bemærkning { get; set; }
        public string? Afdeling { get; set; }
        public string? Mødelokale { get; set; }
        public string? Sprog { get; set; }
        public string? Landekode { get; set; }
    }

    public sealed class BookingDateMap : ClassMap<BookingDateDTO>
    {
        public BookingDateMap()
        {
            Map(m => m.RowNumber).Convert(record => record.Row.Parser.Row);
            Map(m => m.BookingDate).Name("BookingDate").TypeConverter<DateOnlyConverter>().TypeConverterOption.Format("dd-MM-yyyy HH:mm:ss");
            Map(m => m.BookingTime).Name("BookingTime").TypeConverter<TimeOnlyConverter>().TypeConverterOption.Format("HH:mm:ss");
            Map(m => m.CompanyId).Name("CompanyId");
            Map(m => m.BookedBy).Name("BookedBy");
            Map(m => m.MeetingDate).Name("MeetingDate").TypeConverter<DateTimeConverter>().TypeConverterOption.Format("dd-MM-yyyy HH:mm:ss"); ;
            Map(m => m.CPR).Name("CPR");
            Map(m => m.Email).Name("Email");
            Map(m => m.Telefon).Name("Telefon");
            Map(m => m.Advisor).Name("Advisor");
            Map(m => m.BrugerId).Name("BrugerId");
            Map(m => m.Kanal).Name("Kanal");
            Map(m => m.CRMID).Name("CRMID");
            Map(m => m.ImportId).Name("ImportId");
            Map(m => m.StatusType).Name("StatusType");
            Map(m => m.Bemærkning).Name("Bemærkning");
            Map(m => m.Afdeling).Name("Afdeling");
            Map(m => m.Mødelokale).Name("Mødelokale");
            Map(m => m.Sprog).Name("Sprog");
            Map(m => m.Landekode).Name("Landekode");
        }
    }
}
