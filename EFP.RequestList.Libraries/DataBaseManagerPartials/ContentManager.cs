using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Filters;
using EFP.RequestList.Libraries.HelperClasses;
using Microsoft.EntityFrameworkCore;

namespace EFP.RequestList.Libraries
{
    // Partial containing methods working with requested content.
    public static partial class DataBaseManager
    {
        public static List<Request> QueryRequests(int amount = Int32.MaxValue, uint startId = 0, bool includeContent = false, bool includeAlias = false, bool includeCurrency = false, RequestFilter? filter = null)
            => db
                .RequestSet
                .IncludeWithCheck(includeCurrency, r => r.Currency)
                .IncludeWithCheck(includeContent, r => r.RequestedContent)
                .IncludeWithCheck(includeAlias, r => r.UserAlias)
                .TryFilter(filter)
                .Where(r => r.Id >= startId)
                .Take(amount)
                .ToList();

        public static List<RequestedContent> QueryContent(int amount = Int32.MaxValue, uint startId = 0, RequestedContentFilter? filter = null)
            => db
                .RequestedContentSet
                .IncludeWithCheck(false, rc => rc.Requests)
                .TryFilter(filter)
                .Where (rc => rc.Id >= startId)
                .Take(amount)
                .ToList();

        public static List<RequestedContent> QueryContentTop(int amount = Int32.MaxValue)
            => db
                .RequestedContentSet
                .Include(rc => rc.Requests)
                .OrderByDescending(rc => rc.RequestSum)
                .Take(amount)
                .ToList();

        public static void AddRequest(uint contentId, double sum, uint? currencyId, uint aliasId)
        {
            var request = currencyId == null ? new Request(sum) : new Request(sum);
        }

        public async static void ArchiveContentRequests(int reqContentId)
        {
            await db
                .RequestedContentSet
                .Include(rc => rc.Requests)
                .Where(rc => rc.Id == reqContentId)
                .SelectMany(rc => rc.Requests)
                .Where(rc => !rc.IsArchived)
                .ForEachAsync(r => r.ArchiveTimeStamp = DateTime.UtcNow);

            db.SaveChanges();
        }
    }
}
