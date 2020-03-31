using Microsoft.EntityFrameworkCore;
using VTS.DAL;
using VTS.Repos.Users;
using System.Threading.Tasks;

namespace VTS.Repos.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VTSDbContext _dbContext;
        private bool disposedValue = false;

        public UnitOfWork(VTSDbContext context)
        {
            _dbContext = context;
            Users = new UserRepository(_dbContext);
        }

        #region Repositories
        public IUserRepository Users { get; }
        #endregion

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

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

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}