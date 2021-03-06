using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.DAL;

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

            Users = new Users.UserRepository(_dbContext);
            UsersVacationInfo = new UsersVacationInfo.UserVacationInfoRepository(_dbContext);
            Employees = new Employees.EmployeeRepository(_dbContext);
            Managers = new Managers.ManagerRepository(_dbContext);
            Clerks = new Clerks.ClerkRepository(_dbContext);
            Heads = new Heads.HeadRepository(_dbContext);
            Holidays = new Holidays.HolidayRepository(_dbContext);
            HolidaysAcception = new HolidaysAcception.HolidayAcceptionRepository(_dbContext);
        }

        #region Repositories

        /// <inheritdoc />
        public Users.IUserRepository Users { get; }

        /// <inheritdoc />
        public UsersVacationInfo.IUserVacationInfoRepository UsersVacationInfo { get; }

        /// <inheritdoc />
        public Employees.IEmployeeRepository Employees { get; }

        /// <inheritdoc />
        public Managers.IManagerRepository Managers { get; }

        /// <inheritdoc />
        public Clerks.IClerkRepository Clerks { get; }

        /// <inheritdoc />
        public Heads.IHeadRepository Heads { get; }

        /// <inheritdoc />
        public Holidays.IHolidayRepository Holidays { get; }

        /// <inheritdoc />
        public HolidaysAcception.IHolidayAcceptionRepository HolidaysAcception { get; }
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