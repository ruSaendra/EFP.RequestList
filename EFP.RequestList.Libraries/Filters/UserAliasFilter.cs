using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Interfaces;

namespace EFP.RequestList.Libraries.Filters
{
    public class UserAliasFilter : IQueryFilter<UserAlias>
    {
        public bool IsDefined { get; set; }

        public Type Type { get; } = typeof(UserAlias);

        public IQueryable<UserAlias> RunFilter(IQueryable<UserAlias> query)
        {
            throw new NotImplementedException();
        }
    }
}
