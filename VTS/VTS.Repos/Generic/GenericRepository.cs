using Microsoft.EntityFrameworkCore;
using VTS.Core.Helpers;
using VTS.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VTS.Repos.Generic
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IIdentifiable<TKey>
    {
        protected readonly VTSDbContext Context;

        public GenericRepository(VTSDbContext context)
        {
            Context = context;
        }

        public virtual TEntity Add(TEntity item)
        {
            return Context.Set<TEntity>()
                .Add(item)
                .Entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            Context.Set<TEntity>()
                .AddRange(items);
        }

        public virtual void Remove(TEntity item)
        {
            Context.Set<TEntity>()
                .Remove(item);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> items)
        {
            Context.Set<TEntity>()
                .RemoveRange(items);
        }

        public virtual async Task<TEntity> FindAsync(TKey key)
        {
            return await Context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id.Equals(key));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await Context.Set<TEntity>()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<TEntity>()
                .CountAsync();
        }
    }
}