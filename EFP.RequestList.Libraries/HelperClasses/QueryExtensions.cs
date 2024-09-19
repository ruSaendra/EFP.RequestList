using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFP.RequestList.Libraries.HelperClasses
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate) where T : BaseEntity
            => condition 
                ? query.Where(predicate) 
                : query;

        public static IQueryable<T> TryFilter<T>(this IQueryable<T> query, IQueryFilter<T>? filter) where T : BaseEntity
            => filter != null && filter.IsDefined && filter.Type.IsAssignableFrom(typeof(T))
                ? filter.RunFilter(query)
                : query;

        public static IQueryable<T> IncludeWithCheck<T, TProperty>(this IQueryable<T> query, bool condition, Expression<Func<T, TProperty>> predicate) where T : BaseEntity
            => condition
                ? query.Include(predicate)
                : query;

    }
}
