using EFP.RequestList.Libraries.DataStructures.DataBase;

namespace EFP.RequestList.Libraries.HelperClasses
{
    public static class CurrencyConverter
    {
        public static double CurrentyToInternal(this double value, Currency? currency) 
            => value / currency.Rate;

        public static double InternalToCurrency(this double value, Currency? currency) 
            => value * currency.Rate;

        public static double CurrencyToCurrency(this double value, Currency? fromCurrency, Currency? toCurrency)
            => value
            .CurrentyToInternal(fromCurrency)
            .InternalToCurrency(toCurrency);
    }
}
