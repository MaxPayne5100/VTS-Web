using System.Collections.Generic;
using System.Threading.Tasks;
using VTS.DAL.Entities;
using VTS.Repos.Generic;

namespace VTS.Repos.Managers
{
    /// <summary>
    /// Interface for Manager Repository.
    /// </summary>
    public interface IManagerRepository : IGenericRepository<Manager, int>
    {
        /// <summary>
        /// Find Manager by User Id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Manager.</returns>
        Task<Manager> FindManagerByUserId(int userId);

        /// <summary>
        /// Get all managers with info about user.
        /// </summary>
        /// <returns>IEnumerable of managers.</returns>
        Task<IEnumerable<Manager>> GetAllWithUserInfo();
    }
}