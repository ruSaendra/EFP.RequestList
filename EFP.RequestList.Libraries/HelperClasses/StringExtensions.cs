namespace EFP.RequestList.Libraries.HelperClasses
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value.Trim());
    }
}
