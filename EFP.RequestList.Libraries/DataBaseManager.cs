using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.DataStructures.Local;
using EFP.RequestList.Libraries.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace EFP.RequestList.Libraries
{
    public static class DataBaseManager
    {
        private static RequestListContext _db = new RequestListContext();

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

        static DataBaseManager()
        {
            _db.ChangeTracker.DetectedAllChanges += ChangesDetected;
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
            => _db.RequestSet
                .AsEnumerable()
                .GroupBy(req => req.RequestedContent)
                .Select(grp => new RequestedItem
                {
                    Name = grp.Key.Name,
                    Type = grp.Key.Type,
                    Value = grp.Select(req => req.InternalValue).Sum()
                })
                .OrderByDescending(req => req.Value)
                .ToList();

        private static List<Currency> QueryCurrencies()
            => _db.CurrencySet
                .AsEnumerable()
                .ToList();
    }
}
