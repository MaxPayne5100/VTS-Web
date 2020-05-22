using System.Threading.Tasks;
using VTS.Repos.Clerks;
using VTS.Repos.Employees;
using VTS.Repos.Heads;
using VTS.Repos.Managers;
using VTS.Repos.Users;
using VTS.Repos.UsersVacationInfo;

namespace VTS.Repos.UnitOfWork
{
    /// <summary>
    /// Interface for Unit of Work.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Repositories

        /// <summary>
        /// Gets User repository.
        /// </summary>
        public IUserRepository Users { get; }

        /// <summary>
        /// Gets UserVacationInfo repository.
        /// </summary>
        public IUserVacationInfoRepository UsersVacationInfo { get; }

        /// <summary>
        /// Gets Employee repository.
        /// </summary>
        public IEmployeeRepository Employees { get; }

        /// <summary>
        /// Gets Manager repository.
        /// </summary>
        public IManagerRepository Managers { get; }

        /// <summary>
        /// Gets Clerk repository.
        /// </summary>
        public IClerkRepository Clerks { get; }

        /// <summary>
        /// Gets Head repository.
        /// </summary>
        public IHeadRepository Heads { get; }
        #endregion

        /// <summary>
        /// Commit changes asynchronously.
        /// </summary>
        /// <returns>Task.</returns>
        Task CommitAsync();

        /// <summary>
        /// Rollback last transaction.
        /// </summary>
        void Rollback();
    }
}