﻿namespace EFP.RequestList.Libraries.HelperClasses
{
    public static class EnumExtensions
    {
        public static TAttribute? GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value) ?? "";
            return enumType.GetField(name)?.GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}
