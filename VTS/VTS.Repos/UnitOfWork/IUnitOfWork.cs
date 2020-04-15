using System.Threading.Tasks;
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