using System.Linq.Expressions;

namespace Inventory.Shared
{
    public interface IRepository<T>
    {
        Task<T?> FindAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate);

        Task<T> Save(T entity);
        Task Delete(string id);
    }
}
