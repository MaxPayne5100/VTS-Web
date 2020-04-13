using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.Repos.Users;

namespace VTS.Repos.UnitOfWork
{
    /// <summary>
    ///  Unit of Work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VTSDbContext _dbContext;
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">DbContext.</param>
        public UnitOfWork(VTSDbContext context)
        {
            _dbContext = context;
            Users = new UserRepository(_dbContext);
        }

        #region Repositories

        /// <inheritdoc />
        public IUserRepository Users { get; }
        #endregion

        /// <inheritdoc />
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                }
            }
        }

        #region IDisposable Support

        /// <summary>
        /// Method which calls Dispose(disposing = true).
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose virtual methood.
        /// </summary>
        /// <param name="disposing">disposing bool.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                disposedValue = true;
            }
        }
        #endregion
    }
}