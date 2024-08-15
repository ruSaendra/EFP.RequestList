using EFP.RequestList.Libraries.HelperClasses;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace EFP.RequestList.WPF.Helpers
{
    public class EnumDescriptionConverter : IValueConverter
    {
        object? IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is not Enum myEnum)
                return null;

            if (myEnum == null)
            {
                return null;
            }
            string? description = myEnum.GetAttribute<DescriptionAttribute>()?.Description;
            if (!description.IsNullOrEmpty())
            {
                return description;
            }
            return myEnum.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
