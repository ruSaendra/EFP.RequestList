using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.HelperClasses;
using EFP.RequestList.Libraries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFP.RequestList.Libraries.Filters
{
    public class UserFilter : IQueryFilter<User>
    {
        public bool IsDefined { get; set; }
        public Type Type { get; } = typeof(User);

        public StringFilter NameFilter { get; set; } = new StringFilter();

        public StringFilter AliasFilter { get; set; } = new StringFilter();

        public uint? PlatformId { get; set; } = null;

        public IQueryable<User> RunFilter(IQueryable<User> query)
        {
            if (NameFilter.IsApplicable) query = query.Filter(true, u => NameFilter.IsValid(u.Name));

            return query;
        }
    }
}
