namespace EFP.RequestList.Libraries.Filters
{
    public class FloatFilter
    {
        public double RangeMinValue { get; set; } = double.MinValue;
        public double RangeMaxValue { get; set; } = double.MaxValue;

        public bool IsApplicable => RangeMinValue > double.MinValue || RangeMaxValue < double.MaxValue;
    }
}
