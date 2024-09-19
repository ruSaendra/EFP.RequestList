namespace EFP.RequestList.Libraries.Filters
{
    public class IntFilter
    {
        public long RangeMinValue { get; set; } = long.MinValue;
        public long RangeMaxValue { get; set; } = long.MaxValue;

        public bool IsApplicable => RangeMinValue > long.MinValue || RangeMaxValue < long.MaxValue;
    }
}
