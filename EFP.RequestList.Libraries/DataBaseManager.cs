using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.DataStructures.Local;
using EFP.RequestList.Libraries.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using EFP.RequestList.Libraries.HelperClasses;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.IO;
using System.Net.Http.Headers;

namespace EFP.RequestList.Libraries
{
    public static class DataBaseManager
    {
        private static RequestListContext? _db;

        public delegate void DbChangesSaved();
        public static event DbChangesSaved? RequestSetChanged;
        public static event DbChangesSaved? CurrencySetChanged;
        public static event DbChangesSaved? RequestedContentSetChanged;

        public static List<RequestedItem> RequestedItemList { get; private set; } = [];
        public static List<RequestedContent> RequestedContentList { get; private set; } = [];

        public static List<RequestedItem> FullItemList
            => QueryRequestedItems();

        public static List<RequestedItem> GameList
            => QueryRequestedItems()
                .Where(ri => ri.Type == RequestedContentType.Game)
                .ToList();

        public static List<RequestedItem> VideoList
            => QueryRequestedItems()
                .Where(ri => ri.Type == RequestedContentType.Video)
                .ToList();

        public static List<RequestedItem> MusicList
            => QueryRequestedItems()
                .Where(ri => ri.Type == RequestedContentType.Music)
                .ToList();

        public static List<Currency> CurrencyList
            => QueryCurrencies();

        public static bool CheckIfDbExists(string? path) => (path.IsNullOrEmpty() || !File.Exists(path)) ? false : true;

        public static void OpenDB(string path)
        {
            try
            {
                _db = RequestListContext.Open(path);
                _db.ChangeTracker.DetectedAllChanges += ChangesDetected;
            }
            catch { throw; }
        }

        public static void CreateDB(string path)
        {
            try
            {
                var dir = Path.GetDirectoryName(path);
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                _db = RequestListContext.Create(path);
                _db.ChangeTracker.DetectedAllChanges += ChangesDetected;
            }
            catch { throw; }
        }

        public static void CloseDB()
        {
            try
            {
                _db.ChangeTracker.DetectedAllChanges -= ChangesDetected;
            }
            catch { }
            finally
            {
                try
                {
                    _db?.Close();
                }
                catch { }
            }
        }

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
            _db.CurrencySet.Add(currency);
            _db.SaveChanges(true);
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
                var oldRate = currToChange.CurrentRate;

                var dtNow = DateTime.UtcNow;

                if(oldRate.DateTimeEnd == null)
                    oldRate.DateTimeEnd = dtNow;

                currToChange.CurrencyRates.Add(new CurrencyRate()
                {
                    Rate = currRate,
                    DateTimeStart = dtNow
                });
                _db.SaveChanges(true);
            }
        }

        public static void QueryRates(Currency currency)
        {
            if (_db.CurrencySet.Contains(currency))
            {
                currency.CurrencyRates.Clear();
                currency.CurrencyRates = _db.CurrencyRateSet
                    .Where(cr => cr.Currency == currency)
                    .ToList();
            }
        }

        private static void ChangesDetected(object? sender, DetectedChangesEventArgs e)
        {
            var chTracker = sender as ChangeTracker;
            var entries = chTracker.Entries();
            var types = entries
                .Where(e => e.State != EntityState.Unchanged)
                .Select(e => e.Entity.GetType())
                .GroupBy(e => e)
                .Select(gr => gr.Key)
                .ToArray();

            if (types.Length > 0)
            {
                if (types.Contains(typeof(Currency)))
                    CurrencySetChanged?.Invoke();

                if (types.Contains(typeof(Request)))
                    RequestSetChanged?.Invoke();

                if (types.Contains(typeof(RequestedContent)))
                    RequestedContentSetChanged?.Invoke();
            }
        }

        private static List<RequestedItem> QueryRequestedItems()
            => _db?.RequestSet
                .GroupBy(req => req.RequestedContent)
                .Select(grp => new RequestedItem
                {
                    Name = grp.Key.Name,
                    Type = grp.Key.Type,
                    Value = grp.Select(req => req.ValueBase).Sum()
                })
                .OrderByDescending(req => req.Value)
                .ToList() ?? [];

        private static List<Currency> QueryCurrencies()
            => _db?.CurrencySet
                .ToList() ?? [];
    }
}
