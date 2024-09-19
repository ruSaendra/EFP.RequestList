namespace EFP.RequestList.Libraries.Filters
{
    public class DateTimeFilter
    {
        public DateTime RangeStart { get; set; } = DateTime.MinValue;
        public DateTime RangeStartUtc => RangeStart.ToUniversalTime();

        public DateTime RangeEnd { get; set; } = DateTime.MaxValue;
        public DateTime RangeEndUtc => RangeEnd.ToUniversalTime();

        public bool IsApplicable => RangeStart > DateTime.MinValue || RangeEnd < DateTime.MaxValue;
    }
}
