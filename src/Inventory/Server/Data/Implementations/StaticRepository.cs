using Inventory.Shared;
using System.Linq.Expressions;

namespace Inventory.Server.Data.Implementations
{
    public class StaticRepository<T> : IRepository<T> where T : DataModelBase<T>
    {
        readonly static IDictionary<string, Asset> _assets = new Dictionary<string, Asset>();
        readonly static IDictionary<string, Station> _stations = new Dictionary<string, Station>();
        readonly static IDictionary<string, Manufacture> _manufactures = new Dictionary<string, Manufacture>();        

        public StaticRepository()
        {
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> Save(T entity)
        {
            throw new NotImplementedException();
        }                
    }
}
