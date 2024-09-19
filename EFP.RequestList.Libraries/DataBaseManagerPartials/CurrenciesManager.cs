using EFP.RequestList.Libraries.DataStructures.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EFP.RequestList.Libraries
{
    // Partial containing methods working with currencies.
    public static partial class DataBaseManager
    {
        public static List<Currency> QueryCurrencies()
            => db
                .CurrencySet
                .Include(cur => cur.CurrencyRates)
                .ToList() ?? [];

        public static void AddCurrency(string currName, double currRate)
        {
            var currency = new Currency()
            {
                Name = currName,
                CurrencyRates = [
                    new CurrencyRate()
                    {
                        Rate = currRate,
                        DateTimeStart = DateTime.MinValue,
                        DateTimeEnd = DateTime.MaxValue
                    }]

            };
            db.CurrencySet.Add(currency);
            db.SaveChanges(true);
        }

        public static void EditCurrency(Currency currency, string currNewName)
        {
            if (_db.CurrencySet.Contains(currency))
            {
                _db.CurrencySet.First(curr => curr == currency).Name = currNewName;
                _db.SaveChanges(true);
            }
        }

        public static void DeleteCurrency(Currency currency)
        {
            if (_db.CurrencySet.Contains(currency))
            {
                _db.CurrencySet.Remove(currency);
                _db.SaveChanges(true);
            }
        }

        public static void AddRate(Currency currency, double currRate)
        {
            if (_db.CurrencySet.Contains(currency))
            {
                var currToChange = _db.CurrencySet.First(curr => curr == currency);
                var oldRate = currToChange.GetCurrentRate();

                var dtNow = DateTime.UtcNow;

                if (oldRate.DateTimeEnd == null)
                    oldRate.DateTimeEnd = dtNow;

                currToChange.CurrencyRates.Add(new CurrencyRate()
                {
                    Rate = currRate,
                    DateTimeStart = dtNow
                });
                _db.SaveChanges(true);
            }
        }

        
    }
}
