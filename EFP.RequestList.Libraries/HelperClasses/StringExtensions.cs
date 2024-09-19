namespace EFP.RequestList.Libraries.HelperClasses
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value?.Trim());

        public static bool ContainsCaseChecked(this string value, string mask, bool caseSensitive)
            => caseSensitive switch
            {
                true => value.ToLower().Contains(mask.ToLower()),
                _ => value.Contains(mask),
            };

        public static bool StartsWithCaseChecked(this string value, string mask, bool caseSensitive)
            => caseSensitive switch
            {
                true=> value.ToLower().StartsWith(mask.ToLower()),
                _ => value.StartsWith(mask),
            };

        public static bool EndsWithCaseChecked(this string value, string mask, bool caseSensitive)
            => caseSensitive switch
            {
                true => value.ToLower().EndsWith(mask.ToLower()),
                _ => value.EndsWith(mask),
            };
    }
}
