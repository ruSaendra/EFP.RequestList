using EFP.RequestList.Libraries.DataStructures.DataBase;

namespace EFP.RequestList.Libraries.HelperClasses
{
    public static class CurrencyConverter
    {
        public static double CurrentyToInternal(this double value, Currency currency, DateTime conversionTime) 
            => value / currency
                .CurrencyRates
                    .Where(cr => conversionTime > cr.DateTimeStart && (cr.DateTimeEnd == null || conversionTime < cr.DateTimeEnd))
                    .OrderBy(cr => cr.DateTimeStart)
                    .Last()
                    .Rate;

        public static double InternalToCurrency(this double value, Currency currency, DateTime conversionTime) 
            => value * currency
                .CurrencyRates
                    .Where(cr => conversionTime > cr.DateTimeStart && (cr.DateTimeEnd == null || conversionTime < cr.DateTimeEnd))
                    .OrderBy(cr => cr.DateTimeStart)
                    .Last()
                    .Rate;

        public static double CurrencyToCurrency(this double value, Currency fromCurrency, Currency toCurrency, DateTime conversionTime)
            => value
            .CurrentyToInternal(fromCurrency, conversionTime)
            .InternalToCurrency(toCurrency, conversionTime);
    }
}
