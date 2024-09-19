using EFP.RequestList.Libraries.DataStructures.DataBase;

namespace EFP.RequestList.Libraries.Interfaces
{
    public interface IQueryFilter<T> where T : BaseEntity
    {
        public bool IsDefined { get; set;}

        public Type Type { get; }

        public abstract IQueryable<T> RunFilter(IQueryable<T> query);
    }
}
