using EFP.RequestList.Libraries.Enums;
using EFP.RequestList.Libraries.HelperClasses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFP.RequestList.Libraries.Filters
{
    public class StringFilter
    {
        public string Mask { get; set; } = string.Empty;
        public bool CaseSensitive { get; set; } = false;
        public StringFilterCondition Condition { get; set; } = StringFilterCondition.Contains;

        public bool IsApplicable => Mask.Length > 0;

        public bool IsValid(string? value)
        => Condition switch
            {
                StringFilterCondition.Contains => value?.ContainsCaseChecked(Mask, CaseSensitive) ?? false,
                StringFilterCondition.StartsWith => value?.StartsWithCaseChecked(Mask, CaseSensitive) ?? false,
                StringFilterCondition.EndsWith => value?.EndsWithCaseChecked(Mask, CaseSensitive) ?? false,
                _ => false
            };
    }
}
