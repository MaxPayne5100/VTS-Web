using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.Core.Infrastructure;
using VTS.DAL;

namespace VTS.Repos.Generic
{
    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="TEntity">Generic entity parameter.</typeparam>
    /// <typeparam name="TKey">Generic key parameter.</typeparam>
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class, IIdentifiable<TKey>
    {
        /// <summary>
        /// DbContext.
        /// </summary>
        private readonly VTSDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">DbContext.</param>
        public GenericRepository(VTSDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public virtual async Task<TKey> AddAsync(TEntity item)
        {
            var entity = await context.Set<TEntity>()
                .AddAsync(item);

            return entity.Entity.Id;
        }

        /// <inheritdoc />
        public virtual async void AddRangeAsync(IEnumerable<TEntity> items)
        {
            await context.Set<TEntity>()
                .AddRangeAsync(items);
        }

        /// <inheritdoc />
        public virtual void Remove(TEntity item)
        {
            context.Set<TEntity>()
                .Remove(item);
        }

        /// <inheritdoc />
        public virtual void RemoveRange(IEnumerable<TEntity> items)
        {
            context.Set<TEntity>()
                .RemoveRange(items);
        }

        /// <inheritdoc />
        public void Update(TEntity item)
        {
            context.Set<TEntity>()
                .Update(item);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync(TKey key)
        {
            return await context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id.Equals(key));
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>()
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<int> CountAsync()
        {
            return await context.Set<TEntity>()
                .CountAsync();
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetQuery(bool includeAllNavProp = false)
        {
            var query = context.Set<TEntity>()
                .AsQueryable();

            if (includeAllNavProp)
            {
                foreach (var property in context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                {
                    query = query.Include(property.Name);
                }
            }

            return query;
        }
    }
}