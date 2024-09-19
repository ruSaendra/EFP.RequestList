using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Interfaces;

namespace EFP.RequestList.Libraries.Filters
{
    public class RequestedContentFilter : IQueryFilter<RequestedContent>
    {
        public bool IsDefined { get; set; }

        public Type Type { get; } = typeof(RequestedContent);

        public IQueryable<RequestedContent> RunFilter(IQueryable<RequestedContent> query)
        {
            return query;
        }
    }
}
