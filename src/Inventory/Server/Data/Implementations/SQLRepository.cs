using Inventory.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.Server.Data
{
    public class SQLRepository<T> : IRepository<T> where T : DataModelBase<T>
    {
        readonly DbSet<T>? dbSet;
        readonly ApplicationDbContext? ctx;
        readonly ILogger<SQLRepository<T>> _logger;

        public SQLRepository(
            ApplicationDbContext ctx,
            ILogger<SQLRepository<T>> _logger)
        {
            if (ctx != null)
            {
                dbSet = ctx.Set<T>();
                this.ctx = ctx;
            }
            this._logger = _logger;
        }

        public async Task Delete(string id)
        {
            if (dbSet != null)
            {
                var existing = await dbSet.FindAsync(id);

                if (existing != null)
                {
                    existing.OnDelete();

                    dbSet.Update(existing);
                    SaveChanges();
                }
            }
        }

        public async Task<T?> FindAsync(string id)
        {
            if (dbSet != null)
                try
                {
                    var existing = await dbSet
                        .AsNoTracking()
                        .FirstAsync(a => a.Id.Equals(id));

                    if (existing != null)
                        return existing;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            return null;
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate)
        {
            try
            {
                if (dbSet != null)
                {
                    if (predicate == null)
                        return Task.FromResult(dbSet
                            .AsNoTracking()
                            .Where(a => a.IsDeleted == false)
                            .AsEnumerable());

                    else
                        return Task.FromResult(dbSet
                            .AsNoTracking()
                            .Where(a => a.IsDeleted == false)
                            .Where(predicate)
                            .AsEnumerable());
                }

                return Task.FromResult(Enumerable.Empty<T>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Task.FromResult(Enumerable.Empty<T>());
            }            
        }

        public Task<T> Save(T entity)
        {
            try
            {
                if (dbSet != null)
                {
                    var existing = dbSet.Contains(entity);

                    if (existing)
                    {
                        entity.OnUpdated();

                        dbSet.Update(entity);
                    }
                    else
                    {
                        entity.OnCreated();
                        dbSet.Add(entity);
                    }
                    SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Task.FromResult(entity);
        }

        void SaveChanges() => ctx?.SaveChanges();        
    }
}
