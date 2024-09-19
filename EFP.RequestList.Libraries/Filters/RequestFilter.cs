using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Interfaces;

namespace EFP.RequestList.Libraries.Filters
{
    public class RequestFilter : IQueryFilter<Request>
    {
        public bool IsDefined { get; set; }

        public Type Type { get; } = typeof(Request);

        public IQueryable<Request> RunFilter(IQueryable<Request> query)
        {
            return query;
        }
    }
}
