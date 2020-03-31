using VTS.Repos.Users;
using System.Threading.Tasks;

namespace VTS.Repos.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories
        public IUserRepository Users { get; }
        #endregion

        Task CommitAsync();

        void Rollback();
    }
}